package main

import (
	"encoding/json"
	"fmt"
	"golang.org/x/crypto/ssh"
	"log"
	"net"
	"os"
	"os/exec"
	"strings"
	"sync"
	"time"
)

// Logging setup
var logFile *os.File

func init() {
	var err error
	logFile, err = os.OpenFile("go_discovery.log", os.O_APPEND|os.O_CREATE|os.O_WRONLY, 0666)
	if err != nil {
		fmt.Println("Failed to open log file:", err)
		os.Exit(1)
	}
	log.SetOutput(logFile)
	log.Println("=== Go Discovery Plugin Started ===")
}

// RequestType defines the type of operation
type RequestType string

const (
	Discovery RequestType = "discovery"
	Metrics   RequestType = "metrics"
)

// GeneralRequest represents a request structure that can be used for both discovery and metrics
type GeneralRequest struct {
	Type        RequestType `json:"type"`
	IpRanges    []string    `json:"ipranges"`
	Credentials []AuthPair  `json:"credentials"`
	Port        int         `json:"port"`
}

// AuthPair represents a username-password pair with an ID
type AuthPair struct {
	ID       string `json:"id"`
	Username string `json:"username"`
	Password string `json:"password"`
}

// DeviceDiscoveryResult represents the discovery response
type DeviceDiscoveryResult struct {
	IPAddress string `json:"ipaddress"`
	Status    string `json:"status"`
	AuthPair  string `json:"auth_pair_id"`
}

// MetricsResult represents the metrics collection response
type MetricsResult struct {
	IPAddress   string `json:"ipaddress"`
	CPUUsage    string `json:"cpu_usage"`
	MemoryUsage string `json:"memory_usage"`
	DiskUsage   string `json:"disk_usage"`
	AuthPair    string `json:"auth_pair_id"`
}

func main() {
	defer logFile.Close()
	log.Println("Waiting for input...")

	var request GeneralRequest
	decoder := json.NewDecoder(os.Stdin)
	if err := decoder.Decode(&request); err != nil {
		log.Fatalf("Failed to decode input: %v", err)
	}

	log.Printf("Received Request: %+v\n", request)

	// Expand IP ranges before processing request
	expandedIPs := expandIPRanges(request.IpRanges)
	request.IpRanges = expandedIPs

	// Process request based on type
	if request.Type == Discovery {
		results := runDiscovery(request)
		outputResponse(results)
	} else if request.Type == Metrics {
		results := runMetricsCollection(request)
		outputResponse(results)
	} else {
		log.Fatalf("Invalid request type: %s", request.Type)
	}
}

// runDiscovery processes discovery requests in parallel
func runDiscovery(request GeneralRequest) []DeviceDiscoveryResult {
	var results []DeviceDiscoveryResult
	numWorkers := 50
	jobs := make(chan string, len(request.IpRanges))
	resultsChan := make(chan DeviceDiscoveryResult, len(request.IpRanges))
	var wg sync.WaitGroup

	// Start worker goroutines
	for i := 0; i < numWorkers; i++ {
		wg.Add(1)
		go func() {
			defer wg.Done()
			for ip := range jobs {
				resultsChan <- processDiscovery(ip, request.Credentials, request.Port)
			}
		}()
	}

	// Send jobs to workers
	for _, ip := range request.IpRanges {
		jobs <- ip
	}
	close(jobs)

	// Wait for all workers to finish
	wg.Wait()
	close(resultsChan)

	// Collect results
	for res := range resultsChan {
		results = append(results, res)
	}

	return results
}

// processDiscovery checks ping, port, and SSH authentication for an IP
func processDiscovery(ip string, credentials []AuthPair, port int) DeviceDiscoveryResult {
	log.Printf("Processing Discovery for IP: %s (Port: %d)", ip, port)

	// Check if device is reachable via ping
	if !ping(ip) {
		log.Printf("Ping failed for %s", ip)
		return DeviceDiscoveryResult{IPAddress: ip, Status: "Unreachable", AuthPair: "N/A"}
	}

	// Check if port is open
	if !checkPortOpen(ip, port) {
		log.Printf("Port %d is closed on %s", port, ip)
		return DeviceDiscoveryResult{IPAddress: ip, Status: "Port Closed", AuthPair: "N/A"}
	}

	// Attempt SSH authentication with multiple credentials
	for _, cred := range credentials {
		if conn, success := sshAuth(ip, port, cred.Username, cred.Password); success {
			log.Printf("Successful SSH authentication for %s with AuthPair ID: %s", ip, cred.ID)
			conn.Close()
			return DeviceDiscoveryResult{IPAddress: ip, Status: "Reachable", AuthPair: cred.ID}
		}
	}

	return DeviceDiscoveryResult{IPAddress: ip, Status: "SSH Authentication Failed", AuthPair: "N/A"}
}

// runMetricsCollection processes metric requests in parallel
func runMetricsCollection(request GeneralRequest) []MetricsResult {
	var results []MetricsResult
	numWorkers := 50
	jobs := make(chan string, len(request.IpRanges))
	resultsChan := make(chan MetricsResult, len(request.IpRanges))
	var wg sync.WaitGroup

	// Start worker goroutines
	for i := 0; i < numWorkers; i++ {
		wg.Add(1)
		go func() {
			defer wg.Done()
			for ip := range jobs {
				resultsChan <- collectMetrics(ip, request.Credentials, request.Port)
			}
		}()
	}

	// Send jobs to workers
	for _, ip := range request.IpRanges {
		jobs <- ip
	}
	close(jobs)

	// Wait for all workers to finish
	wg.Wait()
	close(resultsChan)

	// Collect results
	for res := range resultsChan {
		results = append(results, res)
	}

	return results
}

// collectMetrics gathers CPU, memory, and disk usage via SSH
func collectMetrics(ip string, credentials []AuthPair, port int) MetricsResult {
	log.Printf("Processing Metrics for IP: %s", ip)

	// Attempt SSH authentication with multiple credentials
	for _, cred := range credentials {
		if conn, success := sshAuth(ip, port, cred.Username, cred.Password); success {
			defer conn.Close()
			metrics := fetchMetrics(conn)
			log.Printf("Metrics collected for %s", ip)
			return MetricsResult{
				IPAddress:   ip,
				CPUUsage:    metrics["cpu"],
				MemoryUsage: metrics["memory"],
				DiskUsage:   metrics["disk"],
				AuthPair:    cred.ID,
			}
		}
	}

	return MetricsResult{IPAddress: ip, CPUUsage: "N/A", MemoryUsage: "N/A", DiskUsage: "N/A", AuthPair: "N/A"}
}

// fetchMetrics executes SSH commands to gather system metrics
func fetchMetrics(conn *ssh.Client) map[string]string {
	metrics := make(map[string]string)

	metrics["cpu"] = executeSSHCommand(conn, "top -bn1 | grep 'Cpu' | awk '{print $2}'")
	metrics["memory"] = executeSSHCommand(conn, "free -m | awk 'NR==2{printf \"%.2f%%\", $3*100/$2 }'")
	metrics["disk"] = executeSSHCommand(conn, "df -h / | awk 'NR==2 {print $5}'")

	return metrics
}

// Output response as JSON
func outputResponse(data interface{}) {
	output, err := json.Marshal(data)
	if err != nil {
		log.Fatalf("Failed to encode output: %v", err)
	}
	log.Printf("Response: %s\n", string(output))
	fmt.Println(string(output))
}

using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LiteNMS.Infrastructure.DBContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<CredentialProfile> CredentialProfiles { get; set; }
    public DbSet<DiscoveryProfile> DiscoveryProfiles { get; set; }
    public DbSet<DeviceDiscoveryResult> DeviceDiscoveryResults{ get; set; }
    public DbSet<DeviceMetricProvision>  DeviceMetricProvisions{ get; set; }
    public DbSet<DeviceMetricResult> DeviceMetricResults{ get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CredentialProfile>()
            .HasIndex(p => p.ProfileName)
            .IsUnique();
    }
}

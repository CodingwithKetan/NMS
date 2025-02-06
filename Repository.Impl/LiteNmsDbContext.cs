using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Repository.Contract.Models;

namespace Repository.Impl;

public class LiteNmsDbContext : DbContext
{
    public LiteNmsDbContext(DbContextOptions<LiteNmsDbContext> options) : base(options) {}
    public DbSet<User> Users { get; set; }
    public DbSet<CredentialProfile> CredentialProfiles { get; set; }
    public DbSet<DiscoveryProfile> DiscoveryProfiles { get; set; }
    public DbSet<Discovery> Discoveries { get; set; }
}
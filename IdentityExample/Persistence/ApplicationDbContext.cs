using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityExample.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<AspNetClaims> Claims => Set<AspNetClaims>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<AspNetClaims>().HasData(GetAspNetClaimsSeedData());
    }

    private static AspNetClaims[] GetAspNetClaimsSeedData()
    {
        var functions = new[] { "customer", "order", "product", "user", "report", "admin", "invoice", "payment", "inventory", "analytics" };
        var methods = new[] { "view", "create", "edit", "delete", "export", "import", "approve", "reject", "download", "upload", "print", "email", "archive", "restore" };

        return [.. functions
            .SelectMany(function => methods
            .Select(method => new AspNetClaims { ClaimValue = $"identity.{function}.{method}" }))];
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityExample.Persistence.Configuration;

public class AspNetClaimsConfiguration : IEntityTypeConfiguration<AspNetClaims>
{
    public void Configure(EntityTypeBuilder<AspNetClaims> builder)
    {
        builder.ToTable("AspNetClaims");
        builder.HasKey(e => e.ClaimValue);

        builder.Property(e => e.ClaimValue).HasMaxLength(256).IsRequired();
    }
}

using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("userprofile");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("uuid_generate_v4()")
            .HasColumnName("id");

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("username");

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("password");

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("email");

        builder
            .Property(u => u.Phone)
            .HasMaxLength(20)
            .HasColumnName("phone");

        builder.Property(u => u.Status)
            .HasConversion<int>()
            .HasMaxLength(20)
            .HasColumnName("status");

        builder.Property(u => u.Role)
            .HasConversion<int>()
            .HasMaxLength(20)
            .HasColumnName("role");

        builder.Property(u => u.CreatedAt)
            .HasColumnName("createdat");

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updatedat");
    }
}

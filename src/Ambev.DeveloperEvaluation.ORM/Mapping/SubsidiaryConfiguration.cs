using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SubsidiaryConfiguration : IEntityTypeConfiguration<Subsidiary>
    {
        public void Configure(EntityTypeBuilder<Subsidiary> builder)
        {
            builder.ToTable("subsidiary");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");

            builder.Property(x => x.Name)
                .HasColumnName("name");

            builder.Property(x => x.Street)
                .HasColumnName("street");

            builder.Property(x => x.City)
                .HasColumnName("city");

            builder.Property(x => x.State)
                .HasColumnName("state");

            builder.Property(x => x.CompanyId)
                .HasColumnType("uuid")
                .HasColumnName("company");

            builder.Property(x => x.Complement)
                .HasColumnName("complement")
                .IsRequired(false);

            builder.Property(x => x.Number)
                .HasColumnName("number");

            builder.Property(x => x.PostalCode)
                .HasColumnName("postal_code")
                .HasMaxLength(8);
        }
    }
}

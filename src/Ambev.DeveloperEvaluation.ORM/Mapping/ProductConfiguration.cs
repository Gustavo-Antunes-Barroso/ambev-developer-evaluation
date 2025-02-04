using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Category)
                .HasColumnName("category")
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnName("price")
                .IsRequired();

            builder.Property(x => x.Stock)
                .HasColumnName("stock");
        }
    }
}

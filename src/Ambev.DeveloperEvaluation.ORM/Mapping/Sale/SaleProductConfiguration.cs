using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping.Sale
{
    public class SaleProductConfiguration : IEntityTypeConfiguration<SaleProduct>
    {
        public void Configure(EntityTypeBuilder<SaleProduct> builder)
        {
            builder.ToTable("sale_product");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");

            builder.Property(x => x.SaleId)
                .HasColumnType("uuid")
                .HasColumnName("sale_id");

            builder.Property(x => x.ProductId)
                .HasColumnType("uuid")
                .HasColumnName("product_id");

            builder.Property(x => x.Name)
                .HasColumnName("name");

            builder.Property(x => x.Quantity)
                .HasColumnName("quantity");

            builder.Property(x => x.Price)
                .HasColumnName("price");

            builder.Property(x => x.Discount)
                .HasColumnName("discount");

            builder.Property(x => x.TotalAmount)
                .HasColumnName("total_amount");

            builder.Property(x => x.TotalAmountWithDiscount)
                .HasColumnName("total_amount_with_discount");
        }
    }
}

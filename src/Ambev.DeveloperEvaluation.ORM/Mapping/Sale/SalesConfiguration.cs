using domainEntity = Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace Ambev.DeveloperEvaluation.ORM.Mapping.Sale
{
    public class SalesConfiguration : IEntityTypeConfiguration<domainEntity.Sale>
    {
        public void Configure(EntityTypeBuilder<domainEntity.Sale> builder)
        {
            builder.ToTable("sale");

            builder.HasKey(x => x.Id);
            builder.Property(u => u.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");

            builder.Property(x => x.Canceled)
                .HasColumnName("canceled")
                .HasColumnType("int")
                .HasConversion<int>();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("createdat");

            builder.Property(x => x.CustomerId)
                    .HasColumnType("uuid")
                    .HasColumnName("customer_id");

            builder.Property(x => x.Discount)
                .HasColumnName("discount");

            builder.Property(x => x.SubsidiaryId)
                    .HasColumnType("uuid")
                    .HasColumnName("subsidiary_id");

            builder.Property(x => x.TotalAmount)
                    .HasColumnName("total_amount");

            builder.Property(x => x.TotalAmountWithDiscount)
                    .HasColumnName("total_amount_with_discount");

            builder.Ignore("Products");
            builder.Ignore("Customer");
            builder.Ignore("Subsidiary");
        }
    }
}

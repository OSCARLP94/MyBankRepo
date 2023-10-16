using CommonDataModels.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappers
{
    internal class ProductMapper : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));
            builder.HasKey(p => p.Id);
            builder.Property(x => x.Id).IsRequired().HasMaxLength(50);
            builder.Property(x => x.IdTypeProduct).IsRequired();
            builder.Property(x=> x.ProductNumber).IsRequired().HasMaxLength(50);
            builder.HasIndex(p => p.ProductNumber).IsUnique();
            builder.Property(x => x.IdCurrentStatus).IsRequired();
            builder.Property(x => x.IdClient).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CreationDate).IsRequired();
        }
    }
}

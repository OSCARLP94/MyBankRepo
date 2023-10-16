using CommonDataModels.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappers
{
    internal class MoneyAccountMapper : IEntityTypeConfiguration<MoneyAccount>
    {
        public void Configure(EntityTypeBuilder<MoneyAccount> builder)
        {
            builder.ToTable(nameof(MoneyAccount));
            builder.HasKey(p => p.Id);
            builder.Property(x => x.Id).IsRequired().HasMaxLength(50);
            builder.Property(x => x.IdProduct).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CurrentBalance);
            builder.Property(x => x.LastUpdateBalance);
        }
    }
}

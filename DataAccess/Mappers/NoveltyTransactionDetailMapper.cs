using CommonDataModels.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappers
{
    internal class NoveltyTransactionDetailMapper : IEntityTypeConfiguration<NoveltyTransactionDetail>
    {
        public void Configure(EntityTypeBuilder<NoveltyTransactionDetail> builder)
        {
            builder.ToTable(nameof(NoveltyTransactionDetail));
            builder.HasKey(p => p.Id);
            builder.Property(x => x.Id).IsRequired().HasMaxLength(50);
            builder.Property(x => x.IdNoveltyTransaction).IsRequired().HasMaxLength(50);
            builder.Property(x => x.AfterValue).HasMaxLength(200);
            builder.Property(x => x.BeforeValue).HasMaxLength(200);
            builder.Property(x => x.Additionals).HasMaxLength(200);
        }
    }
}

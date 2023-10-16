using CommonDataModels.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappers
{
    internal class NoveltyTransactionMapper : IEntityTypeConfiguration<NoveltyTransaction>
    {
        public void Configure(EntityTypeBuilder<NoveltyTransaction> builder)
        {
            builder.ToTable(nameof(NoveltyTransaction));
            builder.HasKey(p => p.Id);
            builder.Property(x => x.Id).IsRequired().HasMaxLength(50);
            builder.Property(x => x.IdTypeTransaction).IsRequired();
            builder.Property(x => x.IdDestinyProduct).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.IdOriginProduct).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.EffectDate);
            builder.Property(x => x.UserOrclientId).HasMaxLength(50);
            builder.Property(x => x.ExplicitValue);
        }
    }
}

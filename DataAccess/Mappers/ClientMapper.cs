using CommonDataModels.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappers
{
    internal class ClientMapper : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable(nameof(Client));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().HasMaxLength(50);
            builder.Property(x=> x.UserName).IsRequired().HasMaxLength(20);
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.Property(x => x.AffiliationDate).IsRequired();
            builder.Property(x => x.IdPerson).IsRequired().HasMaxLength(50);
            builder.Property(x=> x.IdCurrentStatus).IsRequired();
        }
    }
}

using CommonDataModels.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappers
{
    internal class PersonMapper : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable(nameof(Person));
            builder.HasKey(p => p.Id);
            builder.Property(x => x.Id).IsRequired().HasMaxLength(50);
            builder.Property(x=> x.DocumentNumber).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.DocumentNumber).IsUnique();
            builder.Property(x => x.Names).IsRequired().HasMaxLength(100);
            builder.Property(x=> x.Surnames).IsRequired().HasMaxLength(100);
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x=> x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x=> x.Sex).IsRequired().HasMaxLength(1);
        }
    }
}

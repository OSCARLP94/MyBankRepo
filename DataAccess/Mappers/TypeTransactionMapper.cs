﻿using CommonDataModels.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappers
{
    internal class TypeTransactionMapper : IEntityTypeConfiguration<TypeTransaction>
    {
        public void Configure(EntityTypeBuilder<TypeTransaction> builder)
        {
            builder.ToTable(nameof(TypeTransaction));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Code).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.IsEnabled);
        }
    }
}

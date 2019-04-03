using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.EntityConfiguration
{
    public class RentDetailsConfiguration : IEntityTypeConfiguration<RentDetails>
    {
        public void Configure(EntityTypeBuilder<RentDetails> builder)
        {
            builder.HasKey(key => new { key.RentDetailId});

            builder.HasOne(p => p.Rent)
                    .WithMany(t => t.RentDetails)
                    .HasForeignKey(key => key.RentId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(one => one.Inventory)
                   .WithMany(many => many.RentDetails)
                   .HasForeignKey(fkey => fkey.InventoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

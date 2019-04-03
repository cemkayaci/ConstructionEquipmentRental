using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.EntityConfiguration
{
    public class InvertoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            
            builder.HasKey(key => new { key.InventoryId });

            builder.HasOne(one => one.Equipment)
                   .WithMany(many => many.Inventories)
                   .HasForeignKey(fkey => fkey.EquipmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(one => one.Types)
                   .WithMany(many => many.Inventories)
                   .HasForeignKey(fkey => fkey.TypeId)
                   .OnDelete(DeleteBehavior.Restrict);              
           
        }
    }
}

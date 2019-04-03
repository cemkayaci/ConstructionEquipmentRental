using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.EntityConfiguration
{
    public class CustomerRentsConfiguration : IEntityTypeConfiguration<Customer>
    {
       
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
          
            builder.HasKey(key => new { key.CustomerId });

            builder.HasMany(p=>p.Rents)
                    .WithOne(t=>t.Customer)
                    .HasForeignKey(key =>key.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

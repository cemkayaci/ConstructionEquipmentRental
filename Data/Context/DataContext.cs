using Repository.EntityConfiguration;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Customer>  Customers { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<RentDetails> RentDetails { get; set; }
        public DbSet<Types> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new InvertoryConfiguration());
            builder.ApplyConfiguration(new CustomerRentsConfiguration());
            builder.ApplyConfiguration(new TypeConfiguration());
            builder.ApplyConfiguration(new EquipmentConfiguration());
            builder.ApplyConfiguration(new RentDetailsConfiguration());
            builder.Seed();
        }
    }
}

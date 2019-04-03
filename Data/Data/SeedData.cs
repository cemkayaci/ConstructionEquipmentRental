using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Types>().HasData(
                new { TypeId = 1 , Name = "Heavy"},
                new { TypeId = 2 , Name = "Regular"},
                new { TypeId = 3 , Name = "Specialized"}
            );

            modelBuilder.Entity<Equipment>().HasData(
                new { EquipmentId = 1 , Name = "Caterpillar bulldozer" },
                new { EquipmentId = 2 , Name = "KamAZ truck" },
                new { EquipmentId = 3 , Name = "Komatsu crane" },
                new { EquipmentId = 4 , Name = "Volvo steamroller" },
                new { EquipmentId = 5 , Name = "Bosch jackhammer" }
           );

            modelBuilder.Entity<Inventory>().HasData(
                 new { InventoryId = 1,  EquipmentId = 1 , TypeId = 1 },
                 new { InventoryId = 2 , EquipmentId = 2 , TypeId = 2 },
                 new { InventoryId = 3 , EquipmentId = 3 , TypeId = 1 },
                 new { InventoryId = 4 , EquipmentId = 4 , TypeId = 2 },
                 new { InventoryId = 5 , EquipmentId = 5 , TypeId = 3 }
                );
        }
    }
}

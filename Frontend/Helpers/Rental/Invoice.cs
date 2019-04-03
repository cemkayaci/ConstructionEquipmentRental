using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Helpers.Rental
{
    public static class Invoice
    {
        public static string GenerateInvoce(this Rent rent)
        {
             var builder = new StringBuilder();

             builder.AppendLine($"Customer : {rent.Customer.CustomerName}");
             builder.AppendLine();

             builder.AppendLine("EquipmentName     -     Type     -     Days     -    Price");
             foreach (var rentDetails  in rent.RentDetails)
             {
                builder.AppendLine($"{rentDetails.Inventory?.Equipment?.Name} | {rentDetails.Inventory?.Types?.Name} | {rentDetails.Days} | {rentDetails.Price:F2} €");
             }
         
             builder.AppendLine();            
             builder.AppendLine($"Total Price: {rent.RentDetails.Sum(i=>i.Price):F2}");
             builder.AppendLine($"Total Points Earned : {rent.RentDetails.Sum(i => i.LoyalityPoint):F2}");

            return builder.ToString();
        }
    }
}

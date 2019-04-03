using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Helpers.Rental
{
    public static class Calculate
    {
        private const int OneTimeFee = 100;
        private const int PremiumDailyFee = 60;
        private const int RegularDailyFee = 40;


        private const string Heavy = "Heavy";
        private const string Regular = "Regular";
        private const string Specialized = "Specialized";

        public static decimal CalculatePrice(this string typeName, int days)
        {
            switch (typeName)
            {
                case Heavy:
                    return OneTimeFee + (PremiumDailyFee * days);
                case Regular:
                    return days > 2 ? OneTimeFee + (PremiumDailyFee * 2) + (RegularDailyFee * (days - 2)) : OneTimeFee + (PremiumDailyFee * days);
                case Specialized:
                    return days > 3 ? (PremiumDailyFee * 3) + (RegularDailyFee * (days - 3)) : (PremiumDailyFee * days);
                default:
                    return 0;
            }
        }

        public static int CalculateLoyalityPoints(this string typeName)
        {
            switch (typeName)
            {
                case Heavy:
                    return 2;
                case Regular:
                case Specialized:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}

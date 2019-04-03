using AutoMapper;
using Common.Messages.Customer;
using Common.Models;
using Frontend.Helpers.Rental;
using Frontend.Models;
using System.Linq;
 
namespace Frontend.Helpers.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Inventory, RentViewModel>()
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Types.Name))
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src => src.Equipment.Name));

            CreateMap<RentViewModel, Cart >()
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.TypeName.CalculatePrice(int.Parse(src.Days))))
               .ForMember(dest => dest.LoyalityPoint, opt => opt.MapFrom(src => src.TypeName.CalculateLoyalityPoints()))
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => UserDetails.UserName));

            CreateMap<Cart,CustomerRent>()
                .ForMember(dest=>dest.CustomerName, opt => opt.MapFrom(src => UserDetails.UserName));

            CreateMap<Rent,InvoiceViewModel>()               
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.RentDetails.Sum(i =>i.Price)))
                .ForMember(dest => dest.TotalLoyalityPoints, opt => opt.MapFrom(src => src.RentDetails.Sum(i => i.LoyalityPoint)));



        }
    }
}

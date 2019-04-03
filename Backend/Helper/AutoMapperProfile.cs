using AutoMapper;
using Common.Messages.Rent;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RentCreated,RentDetails>();
        }
    }
}

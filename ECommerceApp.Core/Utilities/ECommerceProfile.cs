using AutoMapper;
using ECommerceApp.Core.DTO;
using ECommerceApp.Domain.Enum;
using ECommerceApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ECommerceApp.Core.Utilities
{
    public class ECommerceProfile: Profile
    {
        public ECommerceProfile()
        {
            CreateMap<User, LoginDTO>();
            CreateMap<OrderDetailsDTO, OrderDetail>();
            CreateMap<AddProductDTO, Product>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Transaction, TransactionDTO>();
            CreateMap<User, RegistrationDTO>().ReverseMap()
                .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email.ToLower()));
        }
    }
}

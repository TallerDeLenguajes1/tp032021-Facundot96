using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP3.Models.Entities;
using TP3.Models.ViewModels;

namespace TP3
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Order, ApproveOrderViewModel>().ReverseMap();
            CreateMap<Order, ModifyOrderViewModel>().ReverseMap();

            CreateMap<DeliveryM, ApproveDeliveryMViewModel>().ReverseMap();
            CreateMap<DeliveryM, DeleteDeliveryMViewModel>().ReverseMap();
            CreateMap<DeliveryM, DeliveryMViewModel>().ReverseMap();

            CreateMap<User, ApprovedUserViewModel>().ReverseMap();
            CreateMap<User, EditUserViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, LoginViewModel>().ReverseMap();

        }
    }
}

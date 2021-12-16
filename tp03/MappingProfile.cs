using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Entities;
using tp03.Models.ViewModels;

namespace CourierSystem
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Order, ApproveOrderViewModel>().ReverseMap();
            CreateMap<Order, ModifyOrderViewModel>().ReverseMap();

            CreateMap<DeliveryM, ApproveDeliveryViewModel>().ReverseMap();
            CreateMap<DeliveryM, DeleteDeliveryMViewModel>().ReverseMap();
            CreateMap<DeliveryM, DeliveryMViewModel>().ReverseMap();

            CreateMap<User, ApproveDeliveryViewModel>().ReverseMap();
            CreateMap<User, EditUserViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, LoginViewModel>().ReverseMap();
        }
    }
}

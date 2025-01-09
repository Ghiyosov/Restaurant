using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Infrastructure.Profiles;

public class InfrastructureProfile: Profile
{
    public InfrastructureProfile()
    {
        // AytoMap for Restaurant
        CreateMap<Restaurant, ReadRestaurantDTO>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))
            .ForMember(dest => dest.Menus, opt => opt.MapFrom(src => src.Menus));
        CreateMap<Order, UpdateOrderDTO>();
        CreateMap<Menu, UpdateMenuDTO>();
        
        CreateMap<CreateRestaurantDTO, Restaurant>();
        CreateMap<UpdateRestaurantDTO, Restaurant>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
       
        //AutoMap for Orders
        CreateMap<Order, ReadOrderDTO>()
            .ForMember(dest=>dest.OrderDetails, opt=>opt.MapFrom(src=>src.OrderDetails));
        CreateMap<OrderDetail, UpdateOrderDetailDTO>();
        CreateMap<CreateOrderDTO, Order>();
        CreateMap<UpdateOrderDTO, Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        // Automap for Menu
        CreateMap<Menu, ReadMenuDTO>()
            .ForMember(dest=>dest.Restaurant, opt=>opt.MapFrom(src=>src.Restaurant))
            .ForMember(dest=>dest.OrderDetails, opt=>opt.MapFrom(src=>src.OrderDetails));
        CreateMap<Restaurant, UpdateRestaurantDTO>();
        CreateMap<OrderDetail, UpdateOrderDetailDTO>();
        CreateMap<CreateMenuDTO, Menu>();
        CreateMap<UpdateMenuDTO, Menu>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Menu, UpdateMenuDTO>();
        
        // Automap for User
        CreateMap<User, ReadUserDTO>()
            .ForMember(dest=>dest.Orders, opt=>opt.MapFrom(src=>src.Orders))
            .ForMember(dest=>dest.Couriers, opt=>opt.MapFrom(src=>src.Couriers));
        CreateMap<Order, UpdateOrderDTO>();
        CreateMap<Courier, UpdateCourierDTO>();
        
        CreateMap<CreateUserDTO, User>();
        CreateMap<UpdateUserDTO, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        
        //Automap for Courier
        CreateMap<Courier, ReadCourierDTO>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))
            .ForMember(dest=>dest.User, opt=>opt.MapFrom(src=>src.User));
        CreateMap<User, UpdateUserDTO>();

        CreateMap<CreateCourierDTO, Courier>();
        CreateMap<UpdateCourierDTO, Courier>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        
        //Automap for OrderDetail
        CreateMap<OrderDetail, ReadOrderDetailDTO>()
            .ForMember(dest=>dest.Order, opt=>opt.MapFrom(src=>src.Order));
        CreateMap<CreateOrderDetailDTO, OrderDetail>();
        CreateMap<UpdateOrderDetailDTO, OrderDetail>()
            .ForMember(dest => dest.Order, opt => opt.Ignore());
    }
}
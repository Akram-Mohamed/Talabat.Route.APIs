﻿using Talabat.Core.Entities;
using Talabat.Route.APIs.DTOS;
using AutoMapper;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;
using Route.Talabat.APIs.DTOs;
namespace Talabat.Route.APIs.Helpers
{


	public class MappingProfiles:Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductToReturnDto>()
				.ForMember(P => P.Brand, O => O.MapFrom(S => S.Brand.Name))
				.ForMember(P => P.Category, O => O.MapFrom(S => S.Category.Name));
			//	.ForMember(P => P.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
			CreateMap<CustomerBasketDto, CustomerBasket>();
			CreateMap<BasketItemDto, BasketItem>();
            CreateMap<ShippingAddressDTO, AddressDTO>().ReverseMap();


             

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(orderItemDto => orderItemDto.ProductId, O => O.MapFrom(orderItem => orderItem.Product.ProductId))
                .ForMember(orderItemDto => orderItemDto.ProductName, O => O.MapFrom(orderItem => orderItem.Product.ProductName))
                .ForMember(orderItemDto => orderItemDto.PictureURL, O => O.MapFrom(orderItem => orderItem.Product.PictureURL))
                .ForMember(orderItemDto => orderItemDto.PictureURL, O =>
                {
                    //O.MapFrom<OrderItemPictureUrlResolver>();
                });

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(ordrToReturnDto => ordrToReturnDto.DeliveyMethod, O => O.MapFrom(order => order.DelivreyMethod.ShortName))
                .ForMember(ordrToReturnDto => ordrToReturnDto.DeliveyMethodCoast, O => O.MapFrom(order => order.DelivreyMethod.Cost));

        }


    }
}

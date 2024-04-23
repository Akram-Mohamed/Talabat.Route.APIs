using Talabat.Core.Entities;
using Talabat.Route.APIs.DTOS;
using AutoMapper;
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
			//CreateMap<CustomerBasketDTO, CustomerBasket>();
			//CreateMap<BasketItemDTO, BasketItem>();
		}


	}
}

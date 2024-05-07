using Talabat.Core.Entities;
using Talabat.Route.APIs.DTOS;
using AutoMapper;
using Talabat.Core.Entities.Identity;
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
            CreateMap<Address, AddressDTO>().ReverseMap();

        }


	}
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Repositries.Contract;
using Talabat.Repositries;
using Talabat.Repositries.BasketRepositry;
using Talabat.Repositries.Data;
using Talabat.Route.APIs.Errors;
using Talabat.Route.APIs.Helpers;

namespace Talabat.Route.APIs.Extensions
{
	public static class ApplicationServicesExtension
	{

		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{

			services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
			// services.AddScoped(typeof(IBasketRepositry<>), typeof(BasketRepositry<>));
			services.AddAutoMapper(typeof(MappingProfiles));
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{

					var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
								   .SelectMany(P => P.Value.Errors)
									.Select(E => E.ErrorMessage)
													.ToArray();

					var response = new ApiValidationErrorResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(response);
				};

			});

			services.AddScoped(typeof(IBasketRepositry), typeof(BasketRepositry));






			return services;
		}
	}
}

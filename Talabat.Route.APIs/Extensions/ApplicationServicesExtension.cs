using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Services.Contract;
using Talabat.Core;
using Talabat.Repositries;
using Talabat.Route.APIs.Helpers;
using Talabat.Services.PaymentService;
using Talabat.Services.OrderService;
using Talabat.Services.ProductService;
using Talabat.Route.APIs.Errors;
using Talabat.Core.Repositries.Contract;
using Talabat.Repositries.BasketRepositry;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
        services.AddScoped(typeof(IOrderService), typeof(OrderService));
        //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(ICachingService), typeof(ICachingService));
        services.AddScoped(typeof(IProductService), typeof(ProductService));
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

        services.AddAutoMapper(typeof(MappingProfiles));
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = (actionContext) =>
            {
                var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count > 0)
                                                    .SelectMany(P => P.Value.Errors)
                                                    .Select(E => E.ErrorMessage)
                                                    .ToArray();
                var resonse = new ApiValidationErrorResponse()
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(resonse);
            };
        });
        services.AddScoped(typeof(IBasketRepositry), typeof(BasketRepositry));
        return services;
    }
}
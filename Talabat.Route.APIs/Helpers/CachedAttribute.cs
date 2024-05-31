using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Text;
using Talabat.Core.Services.Contract;
using Talabat.Services.CashService;
using static Talabat.Core.Services.Contract.IResponseCasheService;

namespace Talabat.Route.APIs.Helpers
{

    public class CachedAttribute : Attribute, IAsyncActionFilter
    {

        private readonly int _timeToLiveInSeconds;
        public CachedAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var responseCashService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            // Ask CLR for Creating Object from "ResponseCacheService" Explicilty



            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var response = await responseCashService.GetCachedResponseAsync(cacheKey);



            if (!string.IsNullOrEmpty(response))
            {
                var result = new ContentResult()
                {
                    Content = response,
                    ContentType = "application/json",
                    StatusCode = 200,
                };
                context.Result = result;
                return;
            };
            var executedActionContext = await next.Invoke(); // Will Execute The Next Action Filter

            if (executedActionContext.Result is OkObjectResult okObjectResult && okObjectResult.Value is not null)
            {

                await responseCashService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {

            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path); // /api/products
                                             // pageIndex=1 // pageSize=5
                                             // sort=name

            foreach (var (key, value) in request.Query)
            {
                     keyBuilder.Append($"|{key}-{value}");
            }
            // /api/products|pageIndex-1
            // /api/products | pageIndex-1 | pageSize-5
            // /api/products | pageIndex-1|pageSize-5|sort-name
            return keyBuilder.ToString();
        }
    }
}

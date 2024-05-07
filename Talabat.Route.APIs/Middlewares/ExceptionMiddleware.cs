using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Talabat.Route.APIs.Errors;

namespace Talabat.Route.APIs.Middlewares
{

	// By Convension

	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly IWebHostEnvironment _env;

		public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IWebHostEnvironment env )
		{
			_next = next;
			_logger = logger;
			_env = env;
		}



		//public async Task InvokeAsync(HttpContext httpContext)
		//{

		//	try
		//	{
		//		// Take an Action With the Request
		//		await _next.Invoke(httpContext); // Go To the Next Middleware
		//										 // Take an Action with The Response
		//	}
		//	catch (Exception ex)
		//	{
		//		_logger.LogError(ex.Message); // Development Env
		//		// Log Exception in (Database | Files) // Production Env
		//		httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
		//		httpContext.Response.ContentType= "application/json";

		//		var respones = _env.IsDevelopment() ?
		//			new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
		//			: new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

		//		var json=JsonSerializer.Serialize(respones);


		//		httpContext.Response.WriteAsync(json);
		//	}
		//}

		public async Task InvokeAsync(HttpContext httpContext,RequestDelegate _next)
		{

			try
			{
				// Take an Action With the Request
				await _next.Invoke(httpContext); // Go To the Next Middleware
												 // Take an Action with The Response
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message); // Development Env
											  // Log Exception in (Database | Files) // Production Env
				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				httpContext.Response.ContentType = "application/json";

				var respones = _env.IsDevelopment() ?
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
					: new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

				var json = JsonSerializer.Serialize(respones);


				httpContext.Response.WriteAsync(json);
			}
		}


	}
}
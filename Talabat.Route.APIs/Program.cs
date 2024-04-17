
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Talabat.Core.Entities;
using Talabat.Core.Repositries.Contract;
using Talabat.Repositries;
using Talabat.Repositries.Data;
using Talabat.Route.APIs.Errors;
using Talabat.Route.APIs.Helpers;
using Talabat.Route.APIs.Middlewares;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Talabat.Route.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			//StoreContext dbContext = /*new StoreContext()*/;


			var WebApplicationBuilder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			#region Configrations Services

			WebApplicationBuilder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			WebApplicationBuilder.Services.AddEndpointsApiExplorer();
			WebApplicationBuilder.Services.AddSwaggerGen();

			WebApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(WebApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
			});
			//WebApplicationBuilder.Services.AddScoped<IGenericRepositry<Product>, GenericRepositry<Product>>();
			//WebApplicationBuilder.Services.AddScoped<IGenericRepositry<ProductBrand>, GenericRepositry<Product>>();
			//WebApplicationBuilder.Services.AddScoped<IGenericRepositry<Product>, GenericRepositry<Product>>();
			WebApplicationBuilder.Services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
			WebApplicationBuilder.Services.AddAutoMapper(typeof(MappingProfiles));


				WebApplicationBuilder.Services.Configure<ApiBehaviorOptions>(options =>
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







			#endregion


			var app = WebApplicationBuilder.Build();

			using var scope = app.Services.CreateScope();
			app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var _dbContext = services.GetRequiredService<StoreContext>();
			// ASK CLR for Creating Object from DbContext Explicitly
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				await _dbContext.Database.MigrateAsync();
				await StoredContextSeed.SeedAsync(_dbContext);
			}
			catch (Exception ex)
			{

				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "AN Error Happens On Migrations");

			}

			#region Configure Kestrel Services
			app.UseMiddleware<ExceptionMiddleware>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthorization();


			app.UseHttpsRedirection();

			///app.UseRouting();
			///app.UseEndpoints (endpoints =>
			///{
			///  endpoints. MapControllerRoute(
			///   name: "default",
			///   pattern: "{controller}/{action}/{id?}"
			///   );
			/// endpoints.MapControllers();
			/// });

			app.UseStaticFiles();
			app.MapControllers();


			#endregion

			app.Run();
		}
	}
}

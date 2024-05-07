using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Talabat.Repositries.Data;
using Talabat.Repositries.Identity;
using Talabat.Route.APIs.Extensions;
using Talabat.Route.APIs.Middlewares;
using Talabat.Services.AuthService;

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
			#region Added ON Extesion Method
			//WebApplicationBuilder.Services.AddScoped<IGenericRepositry<Product>, GenericRepositry<Product>>();
			//WebApplicationBuilder.Services.AddScoped<IGenericRepositry<ProductBrand>, GenericRepositry<Product>>();
			//WebApplicationBuilder.Services.AddScoped<IGenericRepositry<Product>, GenericRepositry<Product>>();
			//WebApplicationBuilder.Services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
			//WebApplicationBuilder.Services.AddAutoMapper(typeof(MappingProfiles));

			/*
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

			*/

			#endregion

			WebApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>((servicesProvider) => {
				var connection = WebApplicationBuilder.Configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection!);
			});
            WebApplicationBuilder.Services.AddDbContext<ApplicationIdentityDbContext>((options) => {
                options.UseSqlServer(WebApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
            });

            WebApplicationBuilder.Services.AddApplicationServices();
            WebApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            WebApplicationBuilder.Services.AddScoped<IAuthService, AuthService >();


            #endregion


            var app = WebApplicationBuilder.Build();

			using var scope = app.Services.CreateScope();
			app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var _dbContext = services.GetRequiredService<StoreContext>();
			// ASK CLR for Creating Object from DbContext Explicitly
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var _applicationIdentityDbContext = services.GetRequiredService<ApplicationIdentityDbContext>();
            var _userManger = services.GetRequiredService<UserManager<ApplicationUser>>();            
			try
			{
				await _dbContext.Database.MigrateAsync();
				await StoredContextSeed.SeedAsync(_dbContext);
                await _applicationIdentityDbContext.Database.MigrateAsync();
                await ApplicationIdentityDbContextSeed.DataSeedAsync(_userManger);
            }
			catch (Exception ex)
			{

				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "AN Error Happens On Migrations");

			}

			#region Configure Kestrel Services
			app.UseStatusCodePagesWithRedirects("/errors/{0}");

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

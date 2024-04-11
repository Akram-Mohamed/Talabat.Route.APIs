
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Talabat.Repositries.Data;

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

			WebApplicationBuilder.Services.AddDbContext<StoreContext>(options=>
			{
				options.UseSqlServer(WebApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
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

				var logger= loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "AN Error Happens On Migrations");

			}

			#region Configure Kestrel Services
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


				app.MapControllers(); 


			#endregion

			app.Run();
		}
	}
}

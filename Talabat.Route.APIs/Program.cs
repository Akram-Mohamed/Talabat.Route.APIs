
namespace Talabat.Route.APIs
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			#region Configrations Services

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(); 
			#endregion

			var app = builder.Build();

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

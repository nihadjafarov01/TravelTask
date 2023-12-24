using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Travel.Contexts;

namespace Travel
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
			builder.Services.AddDbContext<TravelDbContext>(opt =>
			{
				opt.UseSqlServer(builder.Configuration["ConnectionStrings:MsSql"]);
			});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

            app.MapControllerRoute(
				name: "Admin",
				pattern: "{area=Admin}/{controller=Destination}/{action=Index}/{id?}");

            app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
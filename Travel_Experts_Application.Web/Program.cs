using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Travel_Experts_Application.BLL.Interfaces;
using Travel_Experts_Application.BLL.Repositories;

namespace Travel_Experts_Application.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<Lib.Models.TravelExpertsDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IAgentRepository, AgentRepository>();

            builder.Services.AddScoped<IPackageRepository, PackageRepository>();

            builder.Services.AddScoped<IBookingDetailRepository, BookingDetailRepository>();

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            builder.Services.AddScoped<IBookingRepository, BookingRepository>();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;        // Disable digit (number) requirement
                options.Password.RequireLowercase = false;    // Disable lowercase letter requirement
                options.Password.RequireUppercase = false;    // Disable uppercase letter requirement
                options.Password.RequireNonAlphanumeric = false; // Disable non-alphanumeric character requirement
                options.Password.RequiredLength = 1; // Set the minimum password length you want to allow
            }).AddEntityFrameworkStores<Lib.Models.TravelExpertsDbContext>();

            //builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<Lib.Models.TravelExpertsDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
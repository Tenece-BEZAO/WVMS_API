using Microsoft.AspNetCore.Identity;
using System;
using System.Reflection;
using WVMS.BLL.Extensions;
using WVMS.BLL.Services;
using WVMS.BLL.ServicesContract;
using WVMS.DAL;
using WVMS.DAL.Entities;
using WVMS.DAL.Implementation;
using WVMS.DAL.Interfaces;

namespace WVMS.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.AddAuthentication();
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureServices();
            builder.Services.AddAutoMapper(Assembly.Load("WVMS.BLL"));
            builder.Services.ConfigureJWT(builder.Configuration);
            //grants super admin access to all routes
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperAdminPolicy", policy =>
                    policy.RequireRole("SuperAdmin"));
            });


            builder.Services.AddControllers();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork<WvmsDbContext>>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<IVendorService, VendorService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            // Create the SuperAdmin role and user
            using var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUsers>>();

            // Create the SuperAdmin role
            if (!await roleManager.RoleExistsAsync("SuperAdmin"))
            {
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }



            // Create the SuperAdmin user with the role
            var superAdmin = new AppUsers
            {
                UserName = "superadmin@example.com",
                Email = "superadmin@example.com"
            };
            var result = await userManager.CreateAsync(superAdmin, "SuperAdminPassword1!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
            }

            await Seed.EnsurePopulatedAsync(app);
            await app.RunAsync();
        }
    }
}
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
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
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();
            
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.AddAuthentication();
            builder.Services.ConfigureIdentity();
                       
            builder.Services.ConfigureJWT(builder.Configuration);
            //grants super admin access to all routes
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperAdminPolicy", policy =>
                    policy.RequireRole("SuperAdmin"));
            });


            builder.Services.AddControllers();
            

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen(c =>
            {
                //c.EnableAnnotations();
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cee store", Version = "v1" });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    },
                });

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WVMS API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            builder.Services.ConfigureServices();
            builder.Services.AddAutoMapper(Assembly.Load("WVMS.BLL"));
            builder.Services.AddHttpContextAccessor();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WVMS API V1");

                });
            }

            if (app.Environment.IsProduction())
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseCors("CorsPolicy");

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

                // Create the SuperAdmin user with the role
                var superAdmin = new AppUsers
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "superadmin@example.com",
                    Email = "superadmin@example.com"
                };
                var result = await userManager.CreateAsync(superAdmin, "SuperAdminPassword1!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                }
            }


            //await Seed.EnsurePopulatedAsync(app);
            await app.RunAsync();
        }
    }
}
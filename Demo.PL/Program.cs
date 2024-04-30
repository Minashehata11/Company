
using Demo.BAL.Interfaces;
using Demo.BAL.Repository;
using Demo.DAl;
using Demo.DAl.Context;
using Demo.DAl.Entities;
using Demo.PL.Controllers;
using Demo.PL.Mapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
                {
                    option.LoginPath = new PathString("/Account/SignIn");
                    option.AccessDeniedPath = new PathString("/Account/AccessDenied");

                });
            builder.Services.AddIdentity<ApplicationUser,ApplicationRole>(option =>
            {
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 10;
                option.SignIn.RequireConfirmedAccount = true;
               // option.Lockout.MaxFailedAccessAttempts = 5;
               //option.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromMinutes(3);
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
            builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = true,
                PositionClass=ToastPositions.TopRight,
                PreventDuplicates=true,
                CloseButton=true

            });
             // builder.Services.AddAutoMapper(typeof(MappingProfile));
             builder.Services.AddAutoMapper(map=>map.AddProfile(new MappingProfile()));



            builder.Services.AddScoped<ILogger,Logger<DepartmentController>>();   
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
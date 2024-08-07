using DRF.infrastructures;
using DRF.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace DRF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(option =>
            {
                option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                option.Filters.Add(new ResponseCacheAttribute() { NoStore = true, Location = ResponseCacheLocation.None });
            }).AddNewtonsoftJson();
            builder.Services.AddSingleton<ISqlConnectionsFactory,SqlConnectionsFactory>();
            builder.Services.AddTransient<ILookupsRepository, LookupsRepository>();
            builder.Services.AddTransient<ILookupsCategoryRepository, LookupsCategoryRepository>();
            builder.Services.AddTransient<IOrganizationRepository, OrganizationRepository>();
            builder.Services.AddTransient<IRequestDonorsRepository, RequestDonorsRepository>();
            builder.Services.AddTransient<IRequestPartnersRepository, RequestPartnersRepository>();
            builder.Services.AddTransient<IRequestsRepository, RequestsRepository>();
            builder.Services.AddTransient<IRequestStatusRepository, RequestStatusRepository>();
            builder.Services.AddTransient<IRequestTargetSectorsRepository, RequestTargetSectorsRepository>();
            builder.Services.AddTransient<IRequestUpdatesRepository, RequestUpdatesRepository>();
            builder.Services.AddTransient<IUploadedDataRepository, UploadedDataRepository>();
            builder.Services.AddTransient<IUsersRepository, UsersRepository>();
            builder.Services.AddTransient<IRequestsRepository, RequestsRepository>();
            builder.Services.AddTransient<IRequestUpdatesRepository, RequestUpdatesRepository>();



            // Configure Entity Framework and add ApplicationDbContext


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

            app.UseAuthorization();

            // Configure routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //// Optionally, add a custom route for your RequestFormController
            //app.MapControllerRoute(
            //    name: "requestForm",
            //    pattern: "RequestForm/{action=Index}/{id?}",
            //    defaults: new { controller = "RequestForm" });

            //app.MapControllerRoute(
            //     name: "viewRequests",
            //     pattern: "viewRequests/{id?}",
            //     defaults: new { controller = "Requests", action = "RequestsList" }
            // );


            //app.MapControllerRoute(
            //    name: "requestDetails",
            //    pattern: "requestDetails/{id?}",
            //    defaults: new { controller = "Requests", action = "Details" }
            //);



            app.Run();
        }
    }
}

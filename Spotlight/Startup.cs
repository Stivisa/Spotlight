using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Spotlight.Models.Identity;
using Spotlight.Models;
using Microsoft.AspNetCore.SignalR;
using Spotlight.Hubs;
using Spotlight.Models.News;
using Spotlight.Models.Listings;

namespace Spotlight
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SpotlightIdentityDbContextConnection")));
            services.AddIdentity<AppUser, IdentityRole>
                (options =>{
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;           
                })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            //change defaul path for [authorize]
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Identity/IdAccount/Login");

            services.AddDbContext<NewsDbContext>(options => options.UseSqlServer(Configuration["Data:SpotlightNews:ConnectionString"]));
            services.AddTransient<INewsPostRepository, EFNewsRepository>();

            services.AddDbContext<ListingDbContext>(options => options.UseSqlServer(Configuration["Data:SpotlightListings:ConnectionString"]));
            services.AddTransient<IListingRepository, EFListingRepository>();

            services.AddControllersWithViews();
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            NewsSeedData.EnsurePopulated(app);
            ListingsSeedData.EnsurePopulated(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            AppIdentityDbContext.CreateAdminAccount(app.ApplicationServices,Configuration).Wait();
            /*
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            }); */
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/IdHome/Index");
            });
            /*app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/ChatHub");
            });*/
        }
    }
}

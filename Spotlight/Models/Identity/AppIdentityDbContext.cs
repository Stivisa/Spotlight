using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace Spotlight.Models.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options) { }

        public DbSet<Message> Messages { get; set; }

        public static async Task CreateAdminAccount(IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            UserManager<AppUser> userManager =
            serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager =
            serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string username = configuration["AdminUser:Name"];
            string email = configuration["AdminUser:Email"];
            string password = configuration["AdminUser:Password"];
            string role = configuration["AdminUser:Role"];
            string image = configuration["AdminUser:ImageName"];
            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
                AppUser user = new AppUser
                {
                    UserName = username,
                    Email = email,
                    ImageName = image
                };
                IdentityResult result = await userManager
                .CreateAsync(user, password);
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);                
                    await userManager.ConfirmEmailAsync(user, token);
                    Console.WriteLine("Admin created with confirmed email!");
                }
            }
            if (await roleManager.FindByNameAsync("Organization") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Organization"));
            }
            if (await roleManager.FindByNameAsync("User common") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User common"));
            }
            if (await roleManager.FindByNameAsync("User help") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User help"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>()
                .HasOne<AppUser>(a => a.Sender)
                .WithMany(d => d.Messages)
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(d => d.UserID);
        }
    }
}

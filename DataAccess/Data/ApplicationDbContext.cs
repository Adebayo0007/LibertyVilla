using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Common;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<HotelRoomImage> HotelRoomImages { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = 1,
                    Role = "SuperAdmin",
                    IsActive = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin0001"),
                    Name = "Adebayo Addullah",
                    PhoneNumber = "08087054632",
                    Gender = "Male",
                    Email = "tijaniadebayoabdllahi@gmail.com",
                    DateCreated = DateTime.Now

                }
            );
        }


        /*public async Task Initialize()
        {
            // Ensure the database is created and apply any pending migrations
            Database.EnsureCreated();
            await Database.MigrateAsync();

            var roleManager = this.GetService<RoleManager<IdentityRole>>();
            var userManager = this.GetService<UserManager<IdentityUser>>();

            // Check if any roles exist, and create them if they don't
            if (!Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                await roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
                await roleManager.CreateAsync(new IdentityRole(SD.Role_Employee));
            }

            // Check if any users exist, and create the default admin user if not
            if (!Users.Any(u => u.UserName == "tijaniadebayoabdllahi@gmail.com"))
            {
                var adminUser = new IdentityUser
                {
                    UserName = "Adebayo Abdullahi",
                    Email = "tijaniadebayoabdllahi@gmail.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser, "Adebayo123"); // Replace with your desired admin password

                // Add the admin user to the "Admin" role
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }*/
    }
}

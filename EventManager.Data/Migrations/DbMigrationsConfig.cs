using System;
using System.Collections.Generic;
using System.Linq;
using EventManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EventManager.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class DbMigrationsConfig : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public DbMigrationsConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole() { Name = "Administrator" };

                manager.Create(role);
            }

            if (!context.Users.Any())
            {
                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store);
                var user = new User()
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    FullName = "System Administrator",
                };

                manager.Create(user, "ASDqwe123_");
                manager.AddToRole(user.Id, "Administrator");
                /*var adminEmail = "admin@admin.com";
                var adminUserName = adminEmail;
                var adminFullName = "System Administrator";
                var adminPassword = adminEmail;
                string adminRole = "Administrator";
                CreateAdminUser(context, adminEmail, adminUserName, adminFullName, adminPassword, adminRole);
                CreateSeveralEvents(context);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var adminUser = new User()
                {
                    UserName = adminUserName,
                    FullName = adminFullName,
                    Email = adminEmail
                };
                

                userManager.PasswordValidator = new PasswordValidator()
                {
                    RequiredLength = 1,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                };

                var userCreateResult = userManager.Create(adminUser, adminPassword);

                if (!userCreateResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", userCreateResult.Errors));
                }

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var roleCreateResult = roleManager.Create(new IdentityRole(adminRole));

                if (!roleCreateResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", roleCreateResult.Errors));
                }

                var addAdminRoleResult = userManager.AddToRole(adminUser.Id, adminRole);

                if (!addAdminRoleResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
                }*/
            }        
        }

        private void CreateSeveralEvents(ApplicationDbContext context)
        {
            context.Events.Add(new Event()
            {
                Title = "Party @ SoftUni",
                StartDateTime = DateTime.Now.Date.AddDays(5).AddHours(21).AddMinutes(30),
                Author = context.Users.First()
            });

            context.Events.Add(new Event()
            {
                Title = "Passed Event <Anonymous>",
                StartDateTime = DateTime.Now.Date.AddDays(-2).AddHours(10).AddMinutes(30),
                Duration = TimeSpan.FromHours(1.5),
                Comments = new HashSet<Comment>()
                {
                    new Comment() { Text = "<Anonymous> comment"},
                    new Comment() { Text = "User comment", Author = context.Users.First()}
                }
            });
        }
    }
}

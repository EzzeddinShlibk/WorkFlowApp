using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowApp.Models.Entities;


namespace WorkFlowApp.Models
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiteState>().HasData(
         new SiteState()
         {
             Id = Guid.NewGuid(),
             State = true,
             ClosingMessage = "الموقع مغلق مؤقتا للتطوير",
             CreatedDate = DateTime.Now

         });


            modelBuilder.Entity<Statues>().HasData(
                new Statues { Id = Guid.NewGuid(), Name = "بانتظار البدء", Num = 1, Color = "purple", Icon = "fas fa-clock", CreatedDate = DateTime.Now },
                new Statues { Id = Guid.NewGuid(), Name = "توقف حرج", Num = 2, Color = "danger", Icon = "fas fa-stop-circle", CreatedDate = DateTime.Now },
                new Statues { Id = Guid.NewGuid(), Name = "بانتظار المراجعة", Num = 3, Color = "warning", Icon = "fas fa-clipboard-check", CreatedDate = DateTime.Now },
                new Statues { Id = Guid.NewGuid(), Name = "قيد التنفيد", Num = 4, Color = "blue", Icon = "fas fa-tasks", CreatedDate = DateTime.Now },
                new Statues { Id = Guid.NewGuid(), Name = "مكتملة", Num =5, Color = "success", Icon = "fas fa-check-circle", CreatedDate = DateTime.Now }
            );

            modelBuilder.Entity<Priority>().HasData(
                new Priority { Id = Guid.NewGuid(), Name = "بدون اولوية", Num = 1, Color = "secondary", CreatedDate = DateTime.Now },
                new Priority { Id = Guid.NewGuid(), Name = "اولوية مبدئية", Num = 2, Color = "pink", CreatedDate = DateTime.Now },
                new Priority { Id = Guid.NewGuid(), Name = "اولوية متوسطة", Num = 3, Color = "warning", CreatedDate = DateTime.Now },
                new Priority { Id = Guid.NewGuid(), Name = "اولوية قصوى", Num = 4, Color = "danger", CreatedDate = DateTime.Now }
            );


            string ProgUserId = Guid.NewGuid().ToString();
            string ProgUserName = "Programmer@Gmail.com";
            string ProgPassword = "111";

            string AdminUserId = Guid.NewGuid().ToString();
            string AdminUserName = "Manager@Gmail.com"; // وضع الإيميل الفعلي للزبون
            string AdminPassword = "111";

            string ProgRoleId = Guid.NewGuid().ToString();
            string ProgRoleName = "Prog";

            string AdminRoleId = Guid.NewGuid().ToString();
            string AdminRoleName = "Admin";

            string UserRoleId = Guid.NewGuid().ToString();
            string UserRoleName = "User";

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ProgRoleId,
                    Name = ProgRoleName,
                    NormalizedName = ProgRoleName.ToUpper()
                },
               new IdentityRole
               {
                   Id = AdminRoleId,
                   Name = AdminRoleName,
                   NormalizedName = AdminRoleName.ToUpper()
               },
            new IdentityRole
            {
                Id = UserRoleId,
                Name = UserRoleName,
                NormalizedName = UserRoleName.ToUpper()
            });

            var progUser = new ApplicationUser
            {
                Id = ProgUserId,
                UserName = ProgUserName,
                NormalizedUserName = ProgUserName.ToUpper(),
                Email = ProgUserName,
                NormalizedEmail = ProgUserName.ToUpper(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = true,
                SecurityStamp = new Guid().ToString(),
                CreatedDate = DateTime.Now,
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            progUser.PasswordHash = hasher.HashPassword(progUser, ProgPassword);
            modelBuilder.Entity<ApplicationUser>().HasData(progUser);


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ProgRoleId,
                    UserId = ProgUserId
                });

            modelBuilder.Entity<Profile>().HasData(
                new Profile()
                {
                    Id = Guid.NewGuid(),
                    DisplayName = "Programmer",
                    PhoneNum = "09233333333",
                    UserId = ProgUserId,
                    Pic = "1",
                    CreatedDate = DateTime.Now
                });

            var Adminuser = new ApplicationUser
            {
                Id = AdminUserId,
                UserName = AdminUserName,
                NormalizedUserName = AdminUserName.ToUpper(),
                Email = AdminUserName,
                NormalizedEmail = AdminUserName.ToUpper(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = new Guid().ToString(),
                CreatedDate = DateTime.Now,
            };

            hasher = new PasswordHasher<ApplicationUser>();
            Adminuser.PasswordHash = hasher.HashPassword(Adminuser, AdminPassword);
            modelBuilder.Entity<ApplicationUser>().HasData(Adminuser);


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ProgRoleId,
                    UserId = AdminUserId
                });

            modelBuilder.Entity<Profile>().HasData(
                new Profile()
                {
                    Id = Guid.NewGuid(),
                    DisplayName = "Manager",
                    PhoneNum = "093435345",
                    UserId = AdminUserId,
                    Pic = "1",
                    CreatedDate = DateTime.Now

                });

        }
    }
}

 
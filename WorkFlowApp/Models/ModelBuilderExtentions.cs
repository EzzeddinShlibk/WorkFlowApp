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
                    CreatedDate = DateTime.Now
                });

        }
    }
}


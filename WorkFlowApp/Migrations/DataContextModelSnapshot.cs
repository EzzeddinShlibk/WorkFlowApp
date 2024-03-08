﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkFlowApp.Models;

#nullable disable

namespace WorkFlowApp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "d5c1d0bb-8b9f-4518-a403-333cd862c83a",
                            Name = "Prog",
                            NormalizedName = "PROG"
                        },
                        new
                        {
                            Id = "c93163d8-5907-4747-baf6-229dd000c402",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "e8603778-b09b-4bf8-b996-3c5966a1f23b",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "fda43775-63be-4ea4-b856-558c273bf09c",
                            RoleId = "d5c1d0bb-8b9f-4518-a403-333cd862c83a"
                        },
                        new
                        {
                            UserId = "825ceed3-960a-4312-b819-6349e5c85b8f",
                            RoleId = "d5c1d0bb-8b9f-4518-a403-333cd862c83a"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "fda43775-63be-4ea4-b856-558c273bf09c",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "304f8894-3c24-4614-9c50-50e07e91c8c8",
                            CreatedDate = new DateTime(2024, 3, 8, 22, 49, 51, 217, DateTimeKind.Local).AddTicks(8962),
                            Email = "Programmer@Gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            NormalizedEmail = "PROGRAMMER@GMAIL.COM",
                            NormalizedUserName = "PROGRAMMER@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEEuO2gXqy1k8iAh2sptZB+ivrL+Fdm408H0+L2Izr5jAdtrb9pugX+7ELOPhoYZUag==",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "00000000-0000-0000-0000-000000000000",
                            TwoFactorEnabled = false,
                            UserName = "Programmer@Gmail.com"
                        },
                        new
                        {
                            Id = "825ceed3-960a-4312-b819-6349e5c85b8f",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "3d1ef061-2c3c-4c97-bf11-7780e352b8bb",
                            CreatedDate = new DateTime(2024, 3, 8, 22, 49, 51, 269, DateTimeKind.Local).AddTicks(4038),
                            Email = "Manager@Gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "MANAGER@GMAIL.COM",
                            NormalizedUserName = "MANAGER@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEBQRMX+kRlGCSL5xguEERwqiQJXFK6TVatUgUrXcQeZL3wJ89hDKFz/hioAJGcEjLQ==",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "00000000-0000-0000-0000-000000000000",
                            TwoFactorEnabled = false,
                            UserName = "Manager@Gmail.com"
                        });
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("projectTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("projectTaskId");

                    b.HasIndex("userId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.Priority", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Num")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Priorities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("41d83aa8-6115-482c-972d-afd084f1b376"),
                            Color = "secondary",
                            CreatedDate = new DateTime(2024, 3, 8, 22, 49, 51, 217, DateTimeKind.Local).AddTicks(8803),
                            Name = "بدون اولوية",
                            Num = 1
                        },
                        new
                        {
                            Id = new Guid("d2171277-c8eb-4ed3-b163-dfaf1087381a"),
                            Color = "pink",
                            CreatedDate = new DateTime(2024, 3, 8, 22, 49, 51, 217, DateTimeKind.Local).AddTicks(8806),
                            Name = "اولوية مبدئية",
                            Num = 2
                        },
                        new
                        {
                            Id = new Guid("6ce54352-72d8-461d-9acf-20397a5bdab9"),
                            Color = "warning",
                            CreatedDate = new DateTime(2024, 3, 8, 22, 49, 51, 217, DateTimeKind.Local).AddTicks(8817),
                            Name = "اولوية متوسطة",
                            Num = 3
                        },
                        new
                        {
                            Id = new Guid("04c3228c-87b4-4442-ba78-8240cc7b9e8b"),
                            Color = "danger",
                            CreatedDate = new DateTime(2024, 3, 8, 22, 49, 51, 217, DateTimeKind.Local).AddTicks(8818),
                            Name = "اولوية قصوى",
                            Num = 4
                        });
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Profile");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b17d9d78-ff1a-4f88-9123-929a736122eb"),
                            CreatedDate = new DateTime(2024, 3, 8, 22, 49, 51, 269, DateTimeKind.Local).AddTicks(4007),
                            DisplayName = "Programmer",
                            Gender = false,
                            PhoneNum = "09233333333",
                            Pic = "",
                            UserId = "fda43775-63be-4ea4-b856-558c273bf09c",
                            bio = ""
                        },
                        new
                        {
                            Id = new Guid("129ff7a6-0fa1-4bf0-b87f-7534849b4d71"),
                            CreatedDate = new DateTime(2024, 3, 8, 22, 49, 51, 321, DateTimeKind.Local).AddTicks(238),
                            DisplayName = "Manager",
                            Gender = false,
                            PhoneNum = "093435345",
                            Pic = "",
                            UserId = "825ceed3-960a-4312-b819-6349e5c85b8f",
                            bio = ""
                        });
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.ProjectTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("priorityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("projectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("statuesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("priorityId");

                    b.HasIndex("projectId");

                    b.HasIndex("statuesId");

                    b.HasIndex("userId");

                    b.ToTable("ProjectTasks");
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.ProjectsUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("projectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("projectId");

                    b.HasIndex("userId");

                    b.ToTable("ProjectsUsers");
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.Statues", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Num")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Statuess");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3ee2d299-c611-4b5b-a80b-2cf789bcfa44"),
                            Color = "purple",
                            CreatedDate = new DateTime(2024, 3, 8, 22, 49, 51, 217, DateTimeKind.Local).AddTicks(8673),
                            Icon = "fas fa-clock",
                            Name = "بانتظار البدء",
                            Num = 1
                        },
                        new
                        {
                            Id = new Guid("d43b77ce-d434-43d5-baa2-326ee7b8179e"),
                            Color = "danger",
                            CreatedDate = new DateTime(2024, 3, 8, 22, 49, 51, 217, DateTimeKind.Local).AddTicks(8686),
                            Icon = "fas fa-stop-circle",
                            Name = "توقف حرج",
                            Num = 2
                        },
                        new
                        {
                            Id = new Guid("c081b072-a395-47ee-a0b5-dfe49dbbd5b8"),
                            Color = "warning",
                            CreatedDate = new DateTime(2024, 3, 8, 22, 49, 51, 217, DateTimeKind.Local).AddTicks(8688),
                            Icon = "fas fa-clipboard-check",
                            Name = "بانتظار المراجعة",
                            Num = 3
                        },
                        new
                        {
                            Id = new Guid("0ec8fadf-5b9c-4c76-ac42-4c4d3c3729a7"),
                            Color = "blue",
                            CreatedDate = new DateTime(2024, 3, 8, 22, 49, 51, 217, DateTimeKind.Local).AddTicks(8689),
                            Icon = "fas fa-tasks",
                            Name = "قيد التنفيد",
                            Num = 4
                        },
                        new
                        {
                            Id = new Guid("5d7f95e2-9766-4d72-9c13-e240d9436657"),
                            Color = "success",
                            CreatedDate = new DateTime(2024, 3, 8, 22, 49, 51, 217, DateTimeKind.Local).AddTicks(8691),
                            Icon = "fas fa-check-circle",
                            Name = "مكتملة",
                            Num = 5
                        });
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.TeamUser", b =>
                {
                    b.Property<Guid>("teamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("isApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("teamId", "userId");

                    b.HasIndex("userId");

                    b.ToTable("TeamUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WorkFlowApp.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WorkFlowApp.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkFlowApp.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WorkFlowApp.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.Comment", b =>
                {
                    b.HasOne("WorkFlowApp.Models.Entities.ProjectTask", "projectTask")
                        .WithMany()
                        .HasForeignKey("projectTaskId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkFlowApp.Models.Entities.Profile", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("projectTask");

                    b.Navigation("user");
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.Profile", b =>
                {
                    b.HasOne("WorkFlowApp.Models.Entities.ApplicationUser", "user")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.ProjectTask", b =>
                {
                    b.HasOne("WorkFlowApp.Models.Entities.Priority", "priority")
                        .WithMany()
                        .HasForeignKey("priorityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkFlowApp.Models.Entities.Project", "project")
                        .WithMany()
                        .HasForeignKey("projectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkFlowApp.Models.Entities.Statues", "statues")
                        .WithMany()
                        .HasForeignKey("statuesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkFlowApp.Models.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("priority");

                    b.Navigation("project");

                    b.Navigation("statues");
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.ProjectsUser", b =>
                {
                    b.HasOne("WorkFlowApp.Models.Entities.Project", "project")
                        .WithMany("ProjectsUsers")
                        .HasForeignKey("projectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkFlowApp.Models.Entities.ApplicationUser", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("project");

                    b.Navigation("user");
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.TeamUser", b =>
                {
                    b.HasOne("WorkFlowApp.Models.Entities.Team", "team")
                        .WithMany()
                        .HasForeignKey("teamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkFlowApp.Models.Entities.ApplicationUser", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("team");

                    b.Navigation("user");
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.Project", b =>
                {
                    b.Navigation("ProjectsUsers");
                });
#pragma warning restore 612, 618
        }
    }
}

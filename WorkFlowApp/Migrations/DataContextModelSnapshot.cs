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
                            Id = "900c3278-7b0f-4727-a87a-d7056fd0985c",
                            Name = "Prog",
                            NormalizedName = "PROG"
                        },
                        new
                        {
                            Id = "dd5a5b4f-74c9-4e3a-89f6-b32d3a009a24",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "bf84bd5f-bd4a-4279-a151-f2b0c641e830",
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
                            UserId = "cd8b5805-6627-4a91-8009-263145ed9d74",
                            RoleId = "900c3278-7b0f-4727-a87a-d7056fd0985c"
                        },
                        new
                        {
                            UserId = "5a9abb2e-7e81-47d6-8677-ef6e565d9104",
                            RoleId = "900c3278-7b0f-4727-a87a-d7056fd0985c"
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
                            Id = "cd8b5805-6627-4a91-8009-263145ed9d74",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b5253822-3f3f-491b-994b-92eab84f31cc",
                            CreatedDate = new DateTime(2024, 3, 6, 22, 56, 11, 458, DateTimeKind.Local).AddTicks(9072),
                            Email = "Programmer@Gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            NormalizedEmail = "PROGRAMMER@GMAIL.COM",
                            NormalizedUserName = "PROGRAMMER@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAELMiWYCVEwP0eTSLXCVKukh7RJCRXjMdNB10WK8yAabNDKWj3pP4Qy1qRuC+NB72Qg==",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "00000000-0000-0000-0000-000000000000",
                            TwoFactorEnabled = false,
                            UserName = "Programmer@Gmail.com"
                        },
                        new
                        {
                            Id = "5a9abb2e-7e81-47d6-8677-ef6e565d9104",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1e1299dc-591b-47a9-ba45-2d2de978b9be",
                            CreatedDate = new DateTime(2024, 3, 6, 22, 56, 11, 545, DateTimeKind.Local).AddTicks(8298),
                            Email = "Manager@Gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "MANAGER@GMAIL.COM",
                            NormalizedUserName = "MANAGER@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAED4P+UqTNqTX/uCuABsuSFIdKHby7PJUm2F+mAN/AYgkcnP7yuCMysH/JMoV7ERgsA==",
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
                            Id = new Guid("fd9bd3a6-4367-4d7f-891d-aee115fb2a33"),
                            CreatedDate = new DateTime(2024, 3, 6, 22, 56, 11, 458, DateTimeKind.Local).AddTicks(8913),
                            Name = "Low",
                            Num = 0
                        },
                        new
                        {
                            Id = new Guid("b5057584-26d3-4898-9b65-12cb8fd3351c"),
                            CreatedDate = new DateTime(2024, 3, 6, 22, 56, 11, 458, DateTimeKind.Local).AddTicks(8917),
                            Name = "Medium",
                            Num = 1
                        },
                        new
                        {
                            Id = new Guid("b327fa94-963c-47c6-9ba3-65e538a529c8"),
                            CreatedDate = new DateTime(2024, 3, 6, 22, 56, 11, 458, DateTimeKind.Local).AddTicks(8919),
                            Name = "High",
                            Num = 2
                        },
                        new
                        {
                            Id = new Guid("74f5ab65-ec4c-48c9-a4b8-3d451d03e796"),
                            CreatedDate = new DateTime(2024, 3, 6, 22, 56, 11, 458, DateTimeKind.Local).AddTicks(8921),
                            Name = "Very High",
                            Num = 3
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
                            Id = new Guid("55411784-4696-4034-8d88-d660f8b0e0ad"),
                            CreatedDate = new DateTime(2024, 3, 6, 22, 56, 11, 545, DateTimeKind.Local).AddTicks(8201),
                            DisplayName = "Programmer",
                            Gender = false,
                            PhoneNum = "09233333333",
                            Pic = "",
                            UserId = "cd8b5805-6627-4a91-8009-263145ed9d74",
                            bio = ""
                        },
                        new
                        {
                            Id = new Guid("f71c7f02-8c77-4b9d-a6c5-df9cabaf4f2b"),
                            CreatedDate = new DateTime(2024, 3, 6, 22, 56, 11, 638, DateTimeKind.Local).AddTicks(1864),
                            DisplayName = "Manager",
                            Gender = false,
                            PhoneNum = "093435345",
                            Pic = "",
                            UserId = "5a9abb2e-7e81-47d6-8677-ef6e565d9104",
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
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("priorityId");

                    b.HasIndex("projectId");

                    b.HasIndex("statuesId");

                    b.HasIndex("userId");

                    b.ToTable("ProjectTasks");
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.ProjectsUser", b =>
                {
                    b.Property<Guid>("projectId")
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

                    b.Property<bool>("isManger")
                        .HasColumnType("bit");

                    b.HasKey("projectId", "userId");

                    b.HasIndex("userId");

                    b.ToTable("ProjectsUsers");
                });

            modelBuilder.Entity("WorkFlowApp.Models.Entities.Statues", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Num")
                        .HasColumnType("int");

                    b.Property<int>("Percent")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Statuess");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2dd0fa62-95ae-468e-8187-714feeb22e90"),
                            CreatedDate = new DateTime(2024, 3, 6, 22, 56, 11, 458, DateTimeKind.Local).AddTicks(8688),
                            Name = "Pending Task",
                            Num = 0,
                            Percent = 0
                        },
                        new
                        {
                            Id = new Guid("c201bda9-80ce-4021-a475-858c20dda3c7"),
                            CreatedDate = new DateTime(2024, 3, 6, 22, 56, 11, 458, DateTimeKind.Local).AddTicks(8703),
                            Name = "Critical Issue",
                            Num = 1,
                            Percent = 0
                        },
                        new
                        {
                            Id = new Guid("04bc1582-351a-48fe-bede-8bcaf979716f"),
                            CreatedDate = new DateTime(2024, 3, 6, 22, 56, 11, 458, DateTimeKind.Local).AddTicks(8706),
                            Name = "In Progress",
                            Num = 2,
                            Percent = 0
                        },
                        new
                        {
                            Id = new Guid("28129857-c9ae-4870-99b7-d8be4e51cfdc"),
                            CreatedDate = new DateTime(2024, 3, 6, 22, 56, 11, 458, DateTimeKind.Local).AddTicks(8708),
                            Name = "Done Pending Review",
                            Num = 3,
                            Percent = 50
                        },
                        new
                        {
                            Id = new Guid("69561463-28cd-4b75-b5cc-071e9250cd69"),
                            CreatedDate = new DateTime(2024, 3, 6, 22, 56, 11, 458, DateTimeKind.Local).AddTicks(8711),
                            Name = "Completed",
                            Num = 4,
                            Percent = 100
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

                    b.HasOne("WorkFlowApp.Models.Entities.Profile", "User")
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

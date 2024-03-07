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
                            Id = "cbc021e2-5244-43e3-8651-544e19a88961",
                            Name = "Prog",
                            NormalizedName = "PROG"
                        },
                        new
                        {
                            Id = "68f2d9c5-fb96-4af7-b31e-b51112bc9538",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "c3252ca4-8a0c-43c8-9002-c59e6e65be8f",
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
                            UserId = "d0ef1789-a296-4e56-b43c-34266a010333",
                            RoleId = "cbc021e2-5244-43e3-8651-544e19a88961"
                        },
                        new
                        {
                            UserId = "4bc81711-124b-46f9-bfc6-4b22215e286b",
                            RoleId = "cbc021e2-5244-43e3-8651-544e19a88961"
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
                            Id = "d0ef1789-a296-4e56-b43c-34266a010333",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "bdbb9781-0e7a-45f7-a607-f519881d9f43",
                            CreatedDate = new DateTime(2024, 3, 7, 19, 35, 22, 94, DateTimeKind.Local).AddTicks(1457),
                            Email = "Programmer@Gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            NormalizedEmail = "PROGRAMMER@GMAIL.COM",
                            NormalizedUserName = "PROGRAMMER@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEKPpl6ZgcQOI2eZSDUz3QFrq+4roRBdaOE6dfylgP2qzl2L+9463NSLERgyXGqBl6g==",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "00000000-0000-0000-0000-000000000000",
                            TwoFactorEnabled = false,
                            UserName = "Programmer@Gmail.com"
                        },
                        new
                        {
                            Id = "4bc81711-124b-46f9-bfc6-4b22215e286b",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "aabb917d-5ff6-420b-9cca-93e737254a2d",
                            CreatedDate = new DateTime(2024, 3, 7, 19, 35, 22, 181, DateTimeKind.Local).AddTicks(671),
                            Email = "Manager@Gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "MANAGER@GMAIL.COM",
                            NormalizedUserName = "MANAGER@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEIlj7eLJ/2CROcO3PI/Gw1AXq4qr4f1ncvyFIq1eKE08ku9dha2ENm2qISgqWAXsDA==",
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
                            Id = new Guid("ac2287c4-2c16-4f18-b468-6b8273ac765e"),
                            CreatedDate = new DateTime(2024, 3, 7, 19, 35, 22, 94, DateTimeKind.Local).AddTicks(1321),
                            Name = "Low",
                            Num = 0
                        },
                        new
                        {
                            Id = new Guid("1fff2929-30d5-4fb4-a894-99b72f006a99"),
                            CreatedDate = new DateTime(2024, 3, 7, 19, 35, 22, 94, DateTimeKind.Local).AddTicks(1324),
                            Name = "Medium",
                            Num = 1
                        },
                        new
                        {
                            Id = new Guid("c151c1c4-827e-4fb4-b150-df30b5870987"),
                            CreatedDate = new DateTime(2024, 3, 7, 19, 35, 22, 94, DateTimeKind.Local).AddTicks(1326),
                            Name = "High",
                            Num = 2
                        },
                        new
                        {
                            Id = new Guid("844002c6-8367-4836-b2fc-ea4c3ba09a41"),
                            CreatedDate = new DateTime(2024, 3, 7, 19, 35, 22, 94, DateTimeKind.Local).AddTicks(1328),
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
                            Id = new Guid("bacd550d-2539-4f6c-84fb-9d5ac720af0d"),
                            CreatedDate = new DateTime(2024, 3, 7, 19, 35, 22, 181, DateTimeKind.Local).AddTicks(580),
                            DisplayName = "Programmer",
                            Gender = false,
                            PhoneNum = "09233333333",
                            Pic = "",
                            UserId = "d0ef1789-a296-4e56-b43c-34266a010333",
                            bio = ""
                        },
                        new
                        {
                            Id = new Guid("51d346a1-72d3-48db-9f7f-c5bfbab3e95b"),
                            CreatedDate = new DateTime(2024, 3, 7, 19, 35, 22, 255, DateTimeKind.Local).AddTicks(2581),
                            DisplayName = "Manager",
                            Gender = false,
                            PhoneNum = "093435345",
                            Pic = "",
                            UserId = "4bc81711-124b-46f9-bfc6-4b22215e286b",
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
                            Id = new Guid("f89ead59-ad4a-4c52-8140-b45f62515439"),
                            CreatedDate = new DateTime(2024, 3, 7, 19, 35, 22, 94, DateTimeKind.Local).AddTicks(1181),
                            Name = "Pending Task",
                            Num = 0,
                            Percent = 0
                        },
                        new
                        {
                            Id = new Guid("e2be3174-dbf8-4a34-9c7a-8215749a96ee"),
                            CreatedDate = new DateTime(2024, 3, 7, 19, 35, 22, 94, DateTimeKind.Local).AddTicks(1193),
                            Name = "Critical Issue",
                            Num = 1,
                            Percent = 0
                        },
                        new
                        {
                            Id = new Guid("a09237bd-de17-4116-9c73-7d9275bedd40"),
                            CreatedDate = new DateTime(2024, 3, 7, 19, 35, 22, 94, DateTimeKind.Local).AddTicks(1196),
                            Name = "In Progress",
                            Num = 2,
                            Percent = 0
                        },
                        new
                        {
                            Id = new Guid("4129ad41-ad2d-46f7-a2ab-7258e489ac9d"),
                            CreatedDate = new DateTime(2024, 3, 7, 19, 35, 22, 94, DateTimeKind.Local).AddTicks(1208),
                            Name = "Done Pending Review",
                            Num = 3,
                            Percent = 50
                        },
                        new
                        {
                            Id = new Guid("b7c9fa87-7a8c-4f26-a925-02d79797631d"),
                            CreatedDate = new DateTime(2024, 3, 7, 19, 35, 22, 94, DateTimeKind.Local).AddTicks(1210),
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

﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkFlowApp.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pic1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pic2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pic3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content3 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainContent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FeatureTilte1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeatureTilte2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeatureTilte3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeatureDescription1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeatureDescription2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeatureDescription3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeaturePic1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeaturePic2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeaturePic3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionPic1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionTitle1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionDescription1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionPic2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionTitle2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionDescription2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Num = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    ClosingMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuess",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Num = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    bio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectsUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    projectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectsUsers_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectsUsers_Projects_projectId",
                        column: x => x.projectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    projectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    statuesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    priorityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Priorities_priorityId",
                        column: x => x.priorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Projects_projectId",
                        column: x => x.projectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Statuess_statuesId",
                        column: x => x.statuesId,
                        principalTable: "Statuess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamUsers",
                columns: table => new
                {
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    teamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isAdmin = table.Column<int>(type: "int", nullable: false),
                    isApproved = table.Column<bool>(type: "bit", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamUsers", x => new { x.teamId, x.userId });
                    table.ForeignKey(
                        name: "FK_TeamUsers_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamUsers_Teams_teamId",
                        column: x => x.teamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    projectTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_ProjectTasks_projectTaskId",
                        column: x => x.projectTaskId,
                        principalTable: "ProjectTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36ac8cf8-9228-4c4d-8ea2-3ee5576cdf12", null, "Prog", "PROG" },
                    { "3a71b356-4b3e-4c6f-b523-f358a0d43660", null, "User", "USER" },
                    { "560f2836-77d0-4df3-a585-f006c309387a", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModifiedDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3184473c-d7c8-4596-865e-cd143e1a31e5", 0, "4b41976e-4c51-4a0c-aab9-40b8bd9024b5", new DateTime(2024, 3, 22, 1, 5, 10, 102, DateTimeKind.Local).AddTicks(6066), "Programmer@Gmail.com", true, true, null, null, "PROGRAMMER@GMAIL.COM", "PROGRAMMER@GMAIL.COM", "AQAAAAIAAYagAAAAEAx/41EmT1bIeNNJXaeJ221hIDSdba60VX8l4TPdOyAoQMZ1z2uj2NbGJATYEZAdlw==", null, true, "00000000-0000-0000-0000-000000000000", false, "Programmer@Gmail.com" },
                    { "62d83155-59df-4dc4-a6a7-0adf9c045dde", 0, "0e59ea52-fa47-4440-bc08-ad9d7a88d0dc", new DateTime(2024, 3, 22, 1, 5, 10, 166, DateTimeKind.Local).AddTicks(4997), "Manager@Gmail.com", true, false, null, null, "MANAGER@GMAIL.COM", "MANAGER@GMAIL.COM", "AQAAAAIAAYagAAAAENIIFFQR9yKni9KrTCTttXetDZMPhjnh07r9r/unOXAqFvRAK6WPXKVKKJWjHBZ+KA==", null, true, "00000000-0000-0000-0000-000000000000", false, "Manager@Gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "Id", "Color", "CreatedDate", "ModifiedDate", "Name", "Num" },
                values: new object[,]
                {
                    { new Guid("5d6dd89b-97e0-475c-83d2-a2f9831ecfee"), "pink", new DateTime(2024, 3, 22, 1, 5, 10, 102, DateTimeKind.Local).AddTicks(5960), null, "اولوية مبدئية", 2 },
                    { new Guid("77b1abb7-460d-4b51-a94c-c93fd1f651a1"), "warning", new DateTime(2024, 3, 22, 1, 5, 10, 102, DateTimeKind.Local).AddTicks(5961), null, "اولوية متوسطة", 3 },
                    { new Guid("8f02310b-aef0-408e-b904-0318d272e6c2"), "danger", new DateTime(2024, 3, 22, 1, 5, 10, 102, DateTimeKind.Local).AddTicks(5963), null, "اولوية قصوى", 4 },
                    { new Guid("f1143c08-13f9-4a24-815e-a327cd804703"), "secondary", new DateTime(2024, 3, 22, 1, 5, 10, 102, DateTimeKind.Local).AddTicks(5946), null, "بدون اولوية", 1 }
                });

            migrationBuilder.InsertData(
                table: "SiteStates",
                columns: new[] { "Id", "ClosingMessage", "CreatedDate", "ModifiedDate", "State" },
                values: new object[] { new Guid("3670f5b5-365e-459d-b9f7-e9b612ae68c2"), "الموقع مغلق مؤقتا للتطوير", new DateTime(2024, 3, 22, 1, 5, 10, 102, DateTimeKind.Local).AddTicks(5768), null, true });

            migrationBuilder.InsertData(
                table: "Statuess",
                columns: new[] { "Id", "Color", "CreatedDate", "Icon", "ModifiedDate", "Name", "Num" },
                values: new object[,]
                {
                    { new Guid("6b0f34a5-5077-4ed6-8a49-ec1c22f713a3"), "danger", new DateTime(2024, 3, 22, 1, 5, 10, 102, DateTimeKind.Local).AddTicks(5915), "fas fa-stop-circle", null, "توقف حرج", 2 },
                    { new Guid("87bdd620-2c1d-4a84-be89-b38417b75e04"), "purple", new DateTime(2024, 3, 22, 1, 5, 10, 102, DateTimeKind.Local).AddTicks(5913), "fas fa-clock", null, "بانتظار البدء", 1 },
                    { new Guid("8db19228-8050-4e24-81f9-42028c6f8a24"), "warning", new DateTime(2024, 3, 22, 1, 5, 10, 102, DateTimeKind.Local).AddTicks(5917), "fas fa-clipboard-check", null, "بانتظار المراجعة", 3 },
                    { new Guid("a60d00c1-409f-4c19-b0c8-cb2a2becbda7"), "success", new DateTime(2024, 3, 22, 1, 5, 10, 102, DateTimeKind.Local).AddTicks(5920), "fas fa-check-circle", null, "مكتملة", 5 },
                    { new Guid("c31b4b5c-14d0-4d61-a21e-d38f69bdd1e4"), "blue", new DateTime(2024, 3, 22, 1, 5, 10, 102, DateTimeKind.Local).AddTicks(5919), "fas fa-tasks", null, "قيد التنفيد", 4 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "36ac8cf8-9228-4c4d-8ea2-3ee5576cdf12", "3184473c-d7c8-4596-865e-cd143e1a31e5" },
                    { "36ac8cf8-9228-4c4d-8ea2-3ee5576cdf12", "62d83155-59df-4dc4-a6a7-0adf9c045dde" }
                });

            migrationBuilder.InsertData(
                table: "Profile",
                columns: new[] { "Id", "CreatedDate", "DisplayName", "Gender", "ModifiedDate", "PhoneNum", "Pic", "UserId", "bio" },
                values: new object[,]
                {
                    { new Guid("0785ad36-5c2c-445c-875f-f9810ae709e1"), new DateTime(2024, 3, 22, 1, 5, 10, 231, DateTimeKind.Local).AddTicks(9274), "Manager", false, null, "093435345", "1", "62d83155-59df-4dc4-a6a7-0adf9c045dde", "" },
                    { new Guid("c7951c58-0c13-4e7a-8e94-64f2a5dce845"), new DateTime(2024, 3, 22, 1, 5, 10, 166, DateTimeKind.Local).AddTicks(4916), "Programmer", false, null, "09233333333", "1", "3184473c-d7c8-4596-865e-cd143e1a31e5", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_projectTaskId",
                table: "Comments",
                column: "projectTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_userId",
                table: "Comments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_UserId",
                table: "Profile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsUsers_projectId",
                table: "ProjectsUsers",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsUsers_userId",
                table: "ProjectsUsers",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_priorityId",
                table: "ProjectTasks",
                column: "priorityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_projectId",
                table: "ProjectTasks",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_statuesId",
                table: "ProjectTasks",
                column: "statuesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_userId",
                table: "ProjectTasks",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamUsers_userId",
                table: "TeamUsers",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CalendarEvents");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "MainContent");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "ProjectsUsers");

            migrationBuilder.DropTable(
                name: "SiteStates");

            migrationBuilder.DropTable(
                name: "TeamUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ProjectTasks");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Statuess");
        }
    }
}

using System;
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
<<<<<<<< HEAD:WorkFlowApp/Migrations/20240318231435_ff.cs
                    { "0281a998-7cf7-41e0-a6d4-e4e1474d0823", null, "Admin", "ADMIN" },
                    { "1b091320-8969-4bca-859c-942adcc6b181", null, "Prog", "PROG" },
                    { "7dd96ab9-c35c-4ae0-9326-9e86bd1bd2d3", null, "User", "USER" }
========
                    { "2a395107-2ef3-4120-9a8c-0665c662ecb6", null, "User", "USER" },
                    { "d19380dc-5f1f-47f2-bb69-109aefdde5e0", null, "Admin", "ADMIN" },
                    { "f88212a7-605c-4b99-802c-b18c7c389d7c", null, "Prog", "PROG" }
>>>>>>>> 250b0beed780f879d34a64dd96a3e3ab1f3356ec:WorkFlowApp/Migrations/20240318211511_m1.cs
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModifiedDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
<<<<<<<< HEAD:WorkFlowApp/Migrations/20240318231435_ff.cs
                    { "5fcc3790-cf58-4f7f-9529-1ad11d184a71", 0, "f668e17c-00e2-4bb0-80c4-4024c3838a39", new DateTime(2024, 3, 19, 1, 14, 34, 599, DateTimeKind.Local).AddTicks(7932), "Manager@Gmail.com", true, false, null, null, "MANAGER@GMAIL.COM", "MANAGER@GMAIL.COM", "AQAAAAIAAYagAAAAEH5Aqw1FXwz/TdcncOnRRRHgK5ziq/2r8hICG8+wq2ydv6DO73ajvqzW5UaueTSmLA==", null, true, "00000000-0000-0000-0000-000000000000", false, "Manager@Gmail.com" },
                    { "d9bcb368-92c7-46bc-84e4-d9fd8c9ddca7", 0, "3ea40f04-87ae-4e4b-82f1-45a34ce88a8c", new DateTime(2024, 3, 19, 1, 14, 34, 447, DateTimeKind.Local).AddTicks(8450), "Programmer@Gmail.com", true, true, null, null, "PROGRAMMER@GMAIL.COM", "PROGRAMMER@GMAIL.COM", "AQAAAAIAAYagAAAAEFmbZPvJN8dLQtL5dfARPiCPV8PeS3ETrQaKyfC5OeFt/VfhAIjCagVJjJ1r+2usbA==", null, true, "00000000-0000-0000-0000-000000000000", false, "Programmer@Gmail.com" }
========
                    { "645a651a-e097-4ab4-84a0-020e12244991", 0, "807a3cdc-9e1a-4089-8770-f6b15d6cb034", new DateTime(2024, 3, 18, 23, 15, 10, 41, DateTimeKind.Local).AddTicks(1433), "Manager@Gmail.com", true, false, null, null, "MANAGER@GMAIL.COM", "MANAGER@GMAIL.COM", "AQAAAAIAAYagAAAAEI7kllfNRQQtK0APynU38BNoM9XNu2f5nAZN0KOEtGqNcTZLYdBLwOmIh17Dy9VIaA==", null, true, "00000000-0000-0000-0000-000000000000", false, "Manager@Gmail.com" },
                    { "7ed4550c-f7b9-4824-9634-df36ef079bdf", 0, "4a94f494-449a-4762-a81f-1330789d9c3b", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6538), "Programmer@Gmail.com", true, true, null, null, "PROGRAMMER@GMAIL.COM", "PROGRAMMER@GMAIL.COM", "AQAAAAIAAYagAAAAEIV8bAOqQR3HHucktP7UnKSmSWYPV/LfTudpVTR0tv/3ZdBGTNFOALyn4wttiIcomw==", null, true, "00000000-0000-0000-0000-000000000000", false, "Programmer@Gmail.com" }
>>>>>>>> 250b0beed780f879d34a64dd96a3e3ab1f3356ec:WorkFlowApp/Migrations/20240318211511_m1.cs
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "Id", "Color", "CreatedDate", "ModifiedDate", "Name", "Num" },
                values: new object[,]
                {
<<<<<<<< HEAD:WorkFlowApp/Migrations/20240318231435_ff.cs
                    { new Guid("511d20a7-27a9-4fa2-9586-1c14a69f27ac"), "pink", new DateTime(2024, 3, 19, 1, 14, 34, 447, DateTimeKind.Local).AddTicks(8220), null, "اولوية مبدئية", 2 },
                    { new Guid("92619301-ff24-4ba4-b169-cca4958a887e"), "danger", new DateTime(2024, 3, 19, 1, 14, 34, 447, DateTimeKind.Local).AddTicks(8231), null, "اولوية قصوى", 4 },
                    { new Guid("bfbede89-b1e9-4465-a7a8-e915275c9246"), "warning", new DateTime(2024, 3, 19, 1, 14, 34, 447, DateTimeKind.Local).AddTicks(8228), null, "اولوية متوسطة", 3 },
                    { new Guid("f54f37a8-4800-445a-a1d1-ec039cde3096"), "secondary", new DateTime(2024, 3, 19, 1, 14, 34, 447, DateTimeKind.Local).AddTicks(8216), null, "بدون اولوية", 1 }
========
                    { new Guid("18b9bd13-d7ba-4ff0-94d1-9acfef356ec2"), "danger", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6435), null, "اولوية قصوى", 4 },
                    { new Guid("3a02425e-e679-452c-816f-fd8a76819855"), "pink", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6432), null, "اولوية مبدئية", 2 },
                    { new Guid("6683dca6-cee2-429f-a3b9-a164c3c4fd3e"), "secondary", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6430), null, "بدون اولوية", 1 },
                    { new Guid("f7073f86-7739-4f46-80c1-28a89710e93d"), "warning", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6434), null, "اولوية متوسطة", 3 }
>>>>>>>> 250b0beed780f879d34a64dd96a3e3ab1f3356ec:WorkFlowApp/Migrations/20240318211511_m1.cs
                });

            migrationBuilder.InsertData(
                table: "SiteStates",
                columns: new[] { "Id", "ClosingMessage", "CreatedDate", "ModifiedDate", "State" },
<<<<<<<< HEAD:WorkFlowApp/Migrations/20240318231435_ff.cs
                values: new object[] { new Guid("0ab2d1db-07ba-4cb0-8374-1fb76570e0d6"), "The site is temporarily closed for development", new DateTime(2024, 3, 19, 1, 14, 34, 447, DateTimeKind.Local).AddTicks(7844), null, true });
========
                values: new object[] { new Guid("8a6cf2d2-d35f-4b2c-89b5-f5a1daaaf39f"), "The site is temporarily closed for development", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6214), null, true });
>>>>>>>> 250b0beed780f879d34a64dd96a3e3ab1f3356ec:WorkFlowApp/Migrations/20240318211511_m1.cs

            migrationBuilder.InsertData(
                table: "Statuess",
                columns: new[] { "Id", "Color", "CreatedDate", "Icon", "ModifiedDate", "Name", "Num" },
                values: new object[,]
                {
<<<<<<<< HEAD:WorkFlowApp/Migrations/20240318231435_ff.cs
                    { new Guid("0b5ec8d2-018b-4548-997a-9a848a886c68"), "purple", new DateTime(2024, 3, 19, 1, 14, 34, 447, DateTimeKind.Local).AddTicks(8146), "fas fa-clock", null, "بانتظار البدء", 1 },
                    { new Guid("122f5504-676b-400f-98f9-3b96166e0764"), "blue", new DateTime(2024, 3, 19, 1, 14, 34, 447, DateTimeKind.Local).AddTicks(8157), "fas fa-tasks", null, "قيد التنفيد", 4 },
                    { new Guid("6b2f2b76-6213-4ee6-a610-c97e4e8f69d6"), "warning", new DateTime(2024, 3, 19, 1, 14, 34, 447, DateTimeKind.Local).AddTicks(8154), "fas fa-clipboard-check", null, "بانتظار المراجعة", 3 },
                    { new Guid("8da65bc4-7e92-47b3-ab3e-215262350bf3"), "danger", new DateTime(2024, 3, 19, 1, 14, 34, 447, DateTimeKind.Local).AddTicks(8150), "fas fa-stop-circle", null, "توقف حرج", 2 },
                    { new Guid("b2cad5c9-a001-4c63-b58f-a5af7e8d6a47"), "success", new DateTime(2024, 3, 19, 1, 14, 34, 447, DateTimeKind.Local).AddTicks(8161), "fas fa-check-circle", null, "مكتملة", 5 }
========
                    { new Guid("5350e14d-c886-4253-b241-3e865f5cebb9"), "blue", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6403), "fas fa-tasks", null, "قيد التنفيد", 4 },
                    { new Guid("7d05ccc9-1f5a-4c05-bb00-b300616b8c66"), "purple", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6390), "fas fa-clock", null, "بانتظار البدء", 1 },
                    { new Guid("b3f9ee36-f18e-4468-b1c3-9cefc9ff4a3a"), "success", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6405), "fas fa-check-circle", null, "مكتملة", 5 },
                    { new Guid("e878c244-5343-487e-a16e-09708f501a56"), "danger", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6400), "fas fa-stop-circle", null, "توقف حرج", 2 },
                    { new Guid("f9aa2780-9a37-4c41-b2af-4c0f3e8b18f7"), "warning", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6402), "fas fa-clipboard-check", null, "بانتظار المراجعة", 3 }
>>>>>>>> 250b0beed780f879d34a64dd96a3e3ab1f3356ec:WorkFlowApp/Migrations/20240318211511_m1.cs
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
<<<<<<<< HEAD:WorkFlowApp/Migrations/20240318231435_ff.cs
                    { "1b091320-8969-4bca-859c-942adcc6b181", "5fcc3790-cf58-4f7f-9529-1ad11d184a71" },
                    { "1b091320-8969-4bca-859c-942adcc6b181", "d9bcb368-92c7-46bc-84e4-d9fd8c9ddca7" }
========
                    { "f88212a7-605c-4b99-802c-b18c7c389d7c", "645a651a-e097-4ab4-84a0-020e12244991" },
                    { "f88212a7-605c-4b99-802c-b18c7c389d7c", "7ed4550c-f7b9-4824-9634-df36ef079bdf" }
>>>>>>>> 250b0beed780f879d34a64dd96a3e3ab1f3356ec:WorkFlowApp/Migrations/20240318211511_m1.cs
                });

            migrationBuilder.InsertData(
                table: "Profile",
                columns: new[] { "Id", "CreatedDate", "DisplayName", "Gender", "ModifiedDate", "PhoneNum", "Pic", "UserId", "bio" },
                values: new object[,]
                {
<<<<<<<< HEAD:WorkFlowApp/Migrations/20240318231435_ff.cs
                    { new Guid("734f3c43-d0f2-4b99-b914-a9ddf4b4baca"), new DateTime(2024, 3, 19, 1, 14, 34, 676, DateTimeKind.Local).AddTicks(3876), "Manager", false, null, "093435345", "1", "5fcc3790-cf58-4f7f-9529-1ad11d184a71", "" },
                    { new Guid("a2f8c831-7df0-442e-9ee9-559d97882dcc"), new DateTime(2024, 3, 19, 1, 14, 34, 599, DateTimeKind.Local).AddTicks(7726), "Programmer", false, null, "09233333333", "1", "d9bcb368-92c7-46bc-84e4-d9fd8c9ddca7", "" }
========
                    { new Guid("2457b59e-f5cf-4d2f-86bb-b7aa78f0bc9b"), new DateTime(2024, 3, 18, 23, 15, 10, 106, DateTimeKind.Local).AddTicks(8670), "Manager", false, null, "093435345", "", "645a651a-e097-4ab4-84a0-020e12244991", "" },
                    { new Guid("7fb310a0-21bb-4503-99d2-0c8afbd6f36a"), new DateTime(2024, 3, 18, 23, 15, 10, 41, DateTimeKind.Local).AddTicks(1285), "Programmer", false, null, "09233333333", "", "7ed4550c-f7b9-4824-9634-df36ef079bdf", "" }
>>>>>>>> 250b0beed780f879d34a64dd96a3e3ab1f3356ec:WorkFlowApp/Migrations/20240318211511_m1.cs
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

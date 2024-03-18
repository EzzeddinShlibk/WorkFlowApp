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
                name: "CalendarEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvent", x => x.Id);
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
                name: "SiteState",
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
                    table.PrimaryKey("PK_SiteState", x => x.Id);
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
                    { "2a395107-2ef3-4120-9a8c-0665c662ecb6", null, "User", "USER" },
                    { "d19380dc-5f1f-47f2-bb69-109aefdde5e0", null, "Admin", "ADMIN" },
                    { "f88212a7-605c-4b99-802c-b18c7c389d7c", null, "Prog", "PROG" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModifiedDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "645a651a-e097-4ab4-84a0-020e12244991", 0, "807a3cdc-9e1a-4089-8770-f6b15d6cb034", new DateTime(2024, 3, 18, 23, 15, 10, 41, DateTimeKind.Local).AddTicks(1433), "Manager@Gmail.com", true, false, null, null, "MANAGER@GMAIL.COM", "MANAGER@GMAIL.COM", "AQAAAAIAAYagAAAAEI7kllfNRQQtK0APynU38BNoM9XNu2f5nAZN0KOEtGqNcTZLYdBLwOmIh17Dy9VIaA==", null, true, "00000000-0000-0000-0000-000000000000", false, "Manager@Gmail.com" },
                    { "7ed4550c-f7b9-4824-9634-df36ef079bdf", 0, "4a94f494-449a-4762-a81f-1330789d9c3b", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6538), "Programmer@Gmail.com", true, true, null, null, "PROGRAMMER@GMAIL.COM", "PROGRAMMER@GMAIL.COM", "AQAAAAIAAYagAAAAEIV8bAOqQR3HHucktP7UnKSmSWYPV/LfTudpVTR0tv/3ZdBGTNFOALyn4wttiIcomw==", null, true, "00000000-0000-0000-0000-000000000000", false, "Programmer@Gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "Id", "Color", "CreatedDate", "ModifiedDate", "Name", "Num" },
                values: new object[,]
                {
                    { new Guid("18b9bd13-d7ba-4ff0-94d1-9acfef356ec2"), "danger", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6435), null, "اولوية قصوى", 4 },
                    { new Guid("3a02425e-e679-452c-816f-fd8a76819855"), "pink", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6432), null, "اولوية مبدئية", 2 },
                    { new Guid("6683dca6-cee2-429f-a3b9-a164c3c4fd3e"), "secondary", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6430), null, "بدون اولوية", 1 },
                    { new Guid("f7073f86-7739-4f46-80c1-28a89710e93d"), "warning", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6434), null, "اولوية متوسطة", 3 }
                });

            migrationBuilder.InsertData(
                table: "SiteState",
                columns: new[] { "Id", "ClosingMessage", "CreatedDate", "ModifiedDate", "State" },
                values: new object[] { new Guid("8a6cf2d2-d35f-4b2c-89b5-f5a1daaaf39f"), "The site is temporarily closed for development", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6214), null, true });

            migrationBuilder.InsertData(
                table: "Statuess",
                columns: new[] { "Id", "Color", "CreatedDate", "Icon", "ModifiedDate", "Name", "Num" },
                values: new object[,]
                {
                    { new Guid("5350e14d-c886-4253-b241-3e865f5cebb9"), "blue", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6403), "fas fa-tasks", null, "قيد التنفيد", 4 },
                    { new Guid("7d05ccc9-1f5a-4c05-bb00-b300616b8c66"), "purple", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6390), "fas fa-clock", null, "بانتظار البدء", 1 },
                    { new Guid("b3f9ee36-f18e-4468-b1c3-9cefc9ff4a3a"), "success", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6405), "fas fa-check-circle", null, "مكتملة", 5 },
                    { new Guid("e878c244-5343-487e-a16e-09708f501a56"), "danger", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6400), "fas fa-stop-circle", null, "توقف حرج", 2 },
                    { new Guid("f9aa2780-9a37-4c41-b2af-4c0f3e8b18f7"), "warning", new DateTime(2024, 3, 18, 23, 15, 9, 976, DateTimeKind.Local).AddTicks(6402), "fas fa-clipboard-check", null, "بانتظار المراجعة", 3 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "f88212a7-605c-4b99-802c-b18c7c389d7c", "645a651a-e097-4ab4-84a0-020e12244991" },
                    { "f88212a7-605c-4b99-802c-b18c7c389d7c", "7ed4550c-f7b9-4824-9634-df36ef079bdf" }
                });

            migrationBuilder.InsertData(
                table: "Profile",
                columns: new[] { "Id", "CreatedDate", "DisplayName", "Gender", "ModifiedDate", "PhoneNum", "Pic", "UserId", "bio" },
                values: new object[,]
                {
                    { new Guid("2457b59e-f5cf-4d2f-86bb-b7aa78f0bc9b"), new DateTime(2024, 3, 18, 23, 15, 10, 106, DateTimeKind.Local).AddTicks(8670), "Manager", false, null, "093435345", "", "645a651a-e097-4ab4-84a0-020e12244991", "" },
                    { new Guid("7fb310a0-21bb-4503-99d2-0c8afbd6f36a"), new DateTime(2024, 3, 18, 23, 15, 10, 41, DateTimeKind.Local).AddTicks(1285), "Programmer", false, null, "09233333333", "", "7ed4550c-f7b9-4824-9634-df36ef079bdf", "" }
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
                name: "CalendarEvent");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "MainContent");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "ProjectsUsers");

            migrationBuilder.DropTable(
                name: "SiteState");

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

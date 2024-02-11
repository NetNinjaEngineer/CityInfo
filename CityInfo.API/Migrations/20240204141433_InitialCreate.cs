using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CityInfo.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Population = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointOfInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(1500)", maxLength: 1500, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointOfInterests_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Country", "Latitude", "Longitude", "Name", "Population" },
                values: new object[,]
                {
                    { 1, "USA", 40.712800000000001, -74.006, "New York", 8398748 },
                    { 2, "UK", 51.509900000000002, -0.11799999999999999, "London", 8982000 },
                    { 3, "Japan", 35.689500000000002, 139.6917, "Tokyo", 13929286 },
                    { 4, "China", 39.904200000000003, 116.4074, "Beijing", 21516000 },
                    { 5, "France", 48.8566, 2.3521999999999998, "Paris", 2140526 },
                    { 6, "India", 28.613900000000001, 77.209000000000003, "Delhi", 30290936 },
                    { 7, "Brazil", -23.5505, -46.633299999999998, "Sao Paulo", 2200281 },
                    { 8, "Turkey", 41.008200000000002, 28.978400000000001, "Istanbul", 15462435 },
                    { 9, "Nigeria", 6.5244, 3.3792, "Lagos", 14497000 },
                    { 10, "Russia", 55.755800000000001, 37.617600000000003, "Moscow", 12615882 },
                    { 11, "India", 19.076000000000001, 72.877700000000004, "Mumbai", 12691836 },
                    { 12, "Egypt", 30.0444, 31.235700000000001, "Cairo", 20484965 },
                    { 13, "Mexico", 19.432600000000001, -99.133200000000002, "Mexico City", 9209944 },
                    { 14, "Thailand", 13.7563, 100.5018, "Bangkok", 8280925 },
                    { 15, "South Korea", 37.566499999999998, 126.97799999999999, "Seoul", 9720846 }
                });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "Category", "CityId", "Description", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 1, "Park", 1, "A large urban park in Manhattan.", 0.0, 0.0, "Central Park" },
                    { 2, "Landmark", 1, "Iconic commercial intersection and neighborhood in Midtown Manhattan.", 0.0, 0.0, "Times Square" },
                    { 3, "Landmark", 2, "The London residence and administrative headquarters of the monarch of the United Kingdom.", 0.0, 0.0, "Buckingham Palace" },
                    { 4, "Museum", 2, "World-famous museum of art and antiquities.", 0.0, 0.0, "The British Museum" },
                    { 5, "Park", 3, "Famous public park in central Tokyo.", 0.0, 0.0, "Ueno Park" },
                    { 6, "Landmark", 3, "Iconic communications and observation tower.", 0.0, 0.0, "Tokyo Tower" },
                    { 7, "Landmark", 5, "Iconic iron lattice tower located on the Champ de Mars in Paris.", 0.0, 0.0, "Eiffel Tower" },
                    { 8, "Museum", 5, "The world's largest art museum and a historic monument in Paris.", 0.0, 0.0, "Louvre Museum" },
                    { 9, "Landmark", 10, "City square in Moscow, often considered the central square of Moscow and all of Russia.", 0.0, 0.0, "Red Square" },
                    { 10, "Landmark", 10, "Historic fortified complex at the heart of Moscow, overlooking the Moskva River to the south.", 0.0, 0.0, "The Kremlin" },
                    { 11, "Landmark", 6, "Historical gate built in the 17th century in Delhi, India.", 0.0, 0.0, "Delhi Gate" },
                    { 12, "Landmark", 6, "The tallest brick minaret in the world, located in Delhi, India.", 0.0, 0.0, "Qutub Minar" },
                    { 13, "Landmark", 7, "Art Deco statue of Jesus Christ in Rio de Janeiro, Brazil.", 0.0, 0.0, "Cristo Redentor" },
                    { 14, "Museum", 8, "Archaeological museum complex in Istanbul, Turkey.", 0.0, 0.0, "Istanbul Archaeology Museums" },
                    { 15, "Market", 8, "One of the largest and oldest covered markets in the world, located in Istanbul, Turkey.", 0.0, 0.0, "Grand Bazaar" },
                    { 16, "Bridge", 1, "Iconic bridge that connects San Francisco to Marin County, California, USA.", 0.0, 0.0, "Golden Gate Bridge" },
                    { 17, "Landmark", 1, "Symbolic statue located on Liberty Island in New York Harbor.", 0.0, 0.0, "Statue of Liberty" },
                    { 18, "Landmark", 1, "An ancient amphitheater in the center of Rome, Italy.", 0.0, 0.0, "The Colosseum" },
                    { 19, "Historical Site", 1, "Ancient citadel located on a rocky outcrop above the city of Athens, Greece.", 0.0, 0.0, "Acropolis of Athens" },
                    { 20, "Landmark", 1, "Iconic structure in Sydney, Australia, known for its distinctive sail-like design.", 0.0, 0.0, "Sydney Opera House" },
                    { 21, "Skyscraper", 1, "Twin towers in Kuala Lumpur, Malaysia, once the tallest buildings in the world.", 0.0, 0.0, "Petronas Towers" },
                    { 22, "Historical Site", 4, "A series of fortifications made of stone, brick, tamped earth, and other materials, generally built along an east-to-west line across the historical northern borders of China.", 0.0, 0.0, "Great Wall of China" }
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
                name: "IX_PointOfInterests_CityId",
                table: "PointOfInterests",
                column: "CityId");
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
                name: "PointOfInterests");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CityInfo.MVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { 1, "Park", 1, "A large urban park in Manhattan.", 40.7851, -73.968299999999999, "Central Park" },
                    { 2, "Landmark", 1, "Iconic skyscraper in Midtown Manhattan.", 40.748399999999997, -73.985699999999994, "Empire State Building" },
                    { 3, "Park", 2, "One of the largest parks in London.", 51.507399999999997, -0.16569999999999999, "Hyde Park" },
                    { 4, "Museum", 2, "World-famous museum of art and antiquities.", 51.519399999999997, -0.127, "British Museum" },
                    { 5, "Park", 3, "Famous public park in central Tokyo.", 35.714599999999997, 139.7732, "Ueno Park" },
                    { 6, "Landmark", 3, "Iconic communications and observation tower.", 35.6586, 139.74539999999999, "Tokyo Tower" },
                    { 7, "Park", 4, "A large urban park in Manhattan.", 40.7851, -73.968299999999999, "Central Park" },
                    { 8, "Landmark", 4, "Iconic skyscraper in Midtown Manhattan.", 40.748399999999997, -73.985699999999994, "Empire State Building" },
                    { 9, "Park", 5, "One of the largest parks in London.", 51.507399999999997, -0.16569999999999999, "Hyde Park" },
                    { 10, "Museum", 5, "World-famous museum of art and antiquities.", 51.519399999999997, -0.127, "British Museum" },
                    { 11, "Park", 6, "Famous public park in central Tokyo.", 35.714599999999997, 139.7732, "Ueno Park" },
                    { 12, "Landmark", 5, "Iconic communications and observation tower.", 35.6586, 139.74539999999999, "Tokyo Tower" },
                    { 13, "Park", 7, "A large urban park in Manhattan.", 40.7851, -73.968299999999999, "Central Park" },
                    { 14, "Landmark", 8, "Iconic skyscraper in Midtown Manhattan.", 40.748399999999997, -73.985699999999994, "Empire State Building" },
                    { 15, "Park", 9, "One of the largest parks in London.", 51.507399999999997, -0.16569999999999999, "Hyde Park" },
                    { 16, "Museum", 10, "World-famous museum of art and antiquities.", 51.519399999999997, -0.127, "British Museum" },
                    { 17, "Park", 11, "Famous public park in central Tokyo.", 35.714599999999997, 139.7732, "Ueno Park" },
                    { 18, "Landmark", 12, "Iconic communications and observation tower.", 35.6586, 139.74539999999999, "Tokyo Tower" },
                    { 19, "Park", 13, "A large urban park in Manhattan.", 40.7851, -73.968299999999999, "Central Park" },
                    { 20, "Landmark", 15, "Iconic skyscraper in Midtown Manhattan.", 40.748399999999997, -73.985699999999994, "Empire State Building" },
                    { 21, "Park", 13, "One of the largest parks in London.", 51.507399999999997, -0.16569999999999999, "Hyde Park" },
                    { 22, "Museum", 13, "World-famous museum of art and antiquities.", 51.519399999999997, -0.127, "British Museum" },
                    { 23, "Park", 10, "Famous public park in central Tokyo.", 35.714599999999997, 139.7732, "Ueno Park" },
                    { 24, "Landmark", 14, "Iconic communications and observation tower.", 35.6586, 139.74539999999999, "Tokyo Tower" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PointOfInterests_CityId",
                table: "PointOfInterests",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointOfInterests");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchPointBackend.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeometryId = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationPropeties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CourtName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourtRating = table.Column<float>(type: "real", nullable: false),
                    SafetyRating = table.Column<float>(type: "real", nullable: false),
                    Conditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amenities = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationPropeties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationGeometry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CoodinatesId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationGeometry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationGeometry_Coordinates_CoodinatesId",
                        column: x => x.CoodinatesId,
                        principalTable: "Coordinates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationPropertiesModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_LocationPropeties_LocationPropertiesModelId",
                        column: x => x.LocationPropertiesModelId,
                        principalTable: "LocationPropeties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertiesId = table.Column<int>(type: "int", nullable: true),
                    GeometryId = table.Column<int>(type: "int", nullable: true),
                    LocationCollectionModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_LocationCollection_LocationCollectionModelId",
                        column: x => x.LocationCollectionModelId,
                        principalTable: "LocationCollection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Locations_LocationGeometry_GeometryId",
                        column: x => x.GeometryId,
                        principalTable: "LocationGeometry",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Locations_LocationPropeties_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "LocationPropeties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_LocationPropertiesModelId",
                table: "Comments",
                column: "LocationPropertiesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationGeometry_CoodinatesId",
                table: "LocationGeometry",
                column: "CoodinatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_GeometryId",
                table: "Locations",
                column: "GeometryId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LocationCollectionModelId",
                table: "Locations",
                column: "LocationCollectionModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_PropertiesId",
                table: "Locations",
                column: "PropertiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LocationCollection");

            migrationBuilder.DropTable(
                name: "LocationGeometry");

            migrationBuilder.DropTable(
                name: "LocationPropeties");

            migrationBuilder.DropTable(
                name: "Coordinates");
        }
    }
}

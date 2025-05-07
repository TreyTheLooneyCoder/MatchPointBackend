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
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourtRating = table.Column<float>(type: "real", nullable: false),
                    SafetyRating = table.Column<float>(type: "real", nullable: false),
                    Conditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amenities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<float>(type: "real", nullable: false),
                    Longitude = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
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
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    CourtModelId = table.Column<int>(type: "int", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Comments_Locations_CourtModelId",
                        column: x => x.CourtModelId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LocationFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertiesId = table.Column<int>(type: "int", nullable: true),
                    GeometryId = table.Column<int>(type: "int", nullable: true),
                    LocationCollectionModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationFeatures_LocationCollection_LocationCollectionModelId",
                        column: x => x.LocationCollectionModelId,
                        principalTable: "LocationCollection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LocationFeatures_LocationGeometry_GeometryId",
                        column: x => x.GeometryId,
                        principalTable: "LocationGeometry",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LocationFeatures_LocationPropeties_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "LocationPropeties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CourtModelId",
                table: "Comments",
                column: "CourtModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_LocationPropertiesModelId",
                table: "Comments",
                column: "LocationPropertiesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationFeatures_GeometryId",
                table: "LocationFeatures",
                column: "GeometryId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationFeatures_LocationCollectionModelId",
                table: "LocationFeatures",
                column: "LocationCollectionModelId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationFeatures_PropertiesId",
                table: "LocationFeatures",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationGeometry_CoodinatesId",
                table: "LocationGeometry",
                column: "CoodinatesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "LocationFeatures");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Locations");

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

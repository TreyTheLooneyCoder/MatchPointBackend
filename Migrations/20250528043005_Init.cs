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
                name: "LocationCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationGeometry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationGeometry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CourtName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amenities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surface = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationProperties", x => x.Id);
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
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationPropertiesModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_LocationProperties_LocationPropertiesModelId",
                        column: x => x.LocationPropertiesModelId,
                        principalTable: "LocationProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourtRatingModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CourtRating = table.Column<float>(type: "real", nullable: false),
                    LocationPropertiesModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtRatingModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourtRatingModel_LocationProperties_LocationPropertiesModelId",
                        column: x => x.LocationPropertiesModelId,
                        principalTable: "LocationProperties",
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
                        name: "FK_Locations_LocationProperties_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "LocationProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SafetyRatingModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SafetyRating = table.Column<float>(type: "real", nullable: false),
                    LocationPropertiesModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyRatingModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyRatingModel_LocationProperties_LocationPropertiesModelId",
                        column: x => x.LocationPropertiesModelId,
                        principalTable: "LocationProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_LocationPropertiesModelId",
                table: "Comments",
                column: "LocationPropertiesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtRatingModel_LocationPropertiesModelId",
                table: "CourtRatingModel",
                column: "LocationPropertiesModelId");

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

            migrationBuilder.CreateIndex(
                name: "IX_SafetyRatingModel_LocationPropertiesModelId",
                table: "SafetyRatingModel",
                column: "LocationPropertiesModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "CourtRatingModel");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "SafetyRatingModel");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LocationCollection");

            migrationBuilder.DropTable(
                name: "LocationGeometry");

            migrationBuilder.DropTable(
                name: "LocationProperties");
        }
    }
}

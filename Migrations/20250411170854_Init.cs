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
                name: "AddLocationModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddLocationModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourtAmenityModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtAmenityModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourtConditionModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtConditionModel", x => x.Id);
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
                name: "AmenityModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amenity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourtAmenityModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmenityModel_CourtAmenityModel_CourtAmenityModelId",
                        column: x => x.CourtAmenityModelId,
                        principalTable: "CourtAmenityModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConditionModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourtConditionModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConditionModel_CourtConditionModel_CourtConditionModelId",
                        column: x => x.CourtConditionModelId,
                        principalTable: "CourtConditionModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourtRating = table.Column<int>(type: "int", nullable: false),
                    SafetyRating = table.Column<int>(type: "int", nullable: false),
                    ConditionId = table.Column<int>(type: "int", nullable: false),
                    AmenitiesId = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<int>(type: "int", nullable: false),
                    CommentsId = table.Column<int>(type: "int", nullable: false),
                    NewLocationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_AddLocationModel_NewLocationsId",
                        column: x => x.NewLocationsId,
                        principalTable: "AddLocationModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locations_CommentModel_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "CommentModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locations_CourtAmenityModel_AmenitiesId",
                        column: x => x.AmenitiesId,
                        principalTable: "CourtAmenityModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locations_CourtConditionModel_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "CourtConditionModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmenityModel_CourtAmenityModelId",
                table: "AmenityModel",
                column: "CourtAmenityModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionModel_CourtConditionModelId",
                table: "ConditionModel",
                column: "CourtConditionModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AmenitiesId",
                table: "Locations",
                column: "AmenitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CommentsId",
                table: "Locations",
                column: "CommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ConditionId",
                table: "Locations",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_NewLocationsId",
                table: "Locations",
                column: "NewLocationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmenityModel");

            migrationBuilder.DropTable(
                name: "ConditionModel");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AddLocationModel");

            migrationBuilder.DropTable(
                name: "CommentModel");

            migrationBuilder.DropTable(
                name: "CourtAmenityModel");

            migrationBuilder.DropTable(
                name: "CourtConditionModel");
        }
    }
}

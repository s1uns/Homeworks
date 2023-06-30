using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORMPractice.Migrations
{
    /// <inheritdoc />
    public partial class AddedCountryLanguageClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country_Languages",
                columns: table => new
                {
                    CountryLanguageId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Official = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country_Languages", x => x.CountryLanguageId);
                    table.ForeignKey(
                        name: "FK_Country_Languages_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Country_Languages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Country_Stats",
                columns: table => new
                {
                    CountryYearId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    Year = table.Column<long>(type: "bigint", nullable: false),
                    Population = table.Column<int>(type: "int", nullable: false),
                    GDP = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country_Stats", x => x.CountryYearId);
                    table.ForeignKey(
                        name: "FK_Country_Stats_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_Languages_CountryId",
                table: "Country_Languages",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Languages_LanguageId",
                table: "Country_Languages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Stats_CountryId",
                table: "Country_Stats",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Country_Languages");

            migrationBuilder.DropTable(
                name: "Country_Stats");
        }
    }
}

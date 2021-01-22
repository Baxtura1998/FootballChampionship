using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballChampionship.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Championship",
                columns: table => new
                {
                    ChampionshipRankId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FootballTeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<int>(type: "int", nullable: false),
                    ChampionshipPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Championship", x => x.ChampionshipRankId);
                });

            migrationBuilder.CreateTable(
                name: "ChampionshipFixtureResults",
                columns: table => new
                {
                    ChampionshipFixtureResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FootballTeam1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalsScoredByFirstTeam = table.Column<int>(type: "int", nullable: false),
                    FootballTeam2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalsScoredBySecondTeam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChampionshipFixtureResults", x => x.ChampionshipFixtureResultId);
                });

            migrationBuilder.CreateTable(
                name: "FootballTeam",
                columns: table => new
                {
                    FootballTeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FootballTeamName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballTeam", x => x.FootballTeamId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Championship");

            migrationBuilder.DropTable(
                name: "ChampionshipFixtureResults");

            migrationBuilder.DropTable(
                name: "FootballTeam");
        }
    }
}

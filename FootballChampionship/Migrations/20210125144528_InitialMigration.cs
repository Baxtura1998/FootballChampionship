using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballChampionship.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FootballTeams",
                columns: table => new
                {
                    FootballTeamsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FootballTeamName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballTeams", x => x.FootballTeamsId);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstTeamFootballTeamsId = table.Column<int>(type: "int", nullable: true),
                    GoalsScoredByFirstTeam = table.Column<int>(type: "int", nullable: false),
                    SecondTeamFootballTeamsId = table.Column<int>(type: "int", nullable: true),
                    GoalsScoredBySecondTeam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchesId);
                    table.ForeignKey(
                        name: "FK_Matches_FootballTeams_FirstTeamFootballTeamsId",
                        column: x => x.FirstTeamFootballTeamsId,
                        principalTable: "FootballTeams",
                        principalColumn: "FootballTeamsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_FootballTeams_SecondTeamFootballTeamsId",
                        column: x => x.SecondTeamFootballTeamsId,
                        principalTable: "FootballTeams",
                        principalColumn: "FootballTeamsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    RanksId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamFootballTeamsId = table.Column<int>(type: "int", nullable: true),
                    Place = table.Column<int>(type: "int", nullable: false),
                    ChampionshipPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.RanksId);
                    table.ForeignKey(
                        name: "FK_Ranks_FootballTeams_TeamFootballTeamsId",
                        column: x => x.TeamFootballTeamsId,
                        principalTable: "FootballTeams",
                        principalColumn: "FootballTeamsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_FirstTeamFootballTeamsId",
                table: "Matches",
                column: "FirstTeamFootballTeamsId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_SecondTeamFootballTeamsId",
                table: "Matches",
                column: "SecondTeamFootballTeamsId");

            migrationBuilder.CreateIndex(
                name: "IX_Ranks_TeamFootballTeamsId",
                table: "Ranks",
                column: "TeamFootballTeamsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Ranks");

            migrationBuilder.DropTable(
                name: "FootballTeams");
        }
    }
}

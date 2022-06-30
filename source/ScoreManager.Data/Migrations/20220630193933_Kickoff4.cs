using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreManager.Migrations
{
    public partial class Kickoff4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Cellphone = table.Column<string>(type: "TEXT", nullable: true),
                    Document = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    PrimaryCandidateId = table.Column<int>(type: "INTEGER", nullable: true),
                    SecondaryCandidateId = table.Column<int>(type: "INTEGER", nullable: true),
                    Score = table.Column<decimal>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perform", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perform_Candidate_PrimaryCandidateId",
                        column: x => x.PrimaryCandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Perform_Candidate_SecondaryCandidateId",
                        column: x => x.SecondaryCandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Perform_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_Document",
                table: "Candidate",
                column: "Document",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Perform_CategoryId",
                table: "Perform",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Perform_PrimaryCandidateId",
                table: "Perform",
                column: "PrimaryCandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Perform_SecondaryCandidateId",
                table: "Perform",
                column: "SecondaryCandidateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Perform");

            migrationBuilder.DropTable(
                name: "Candidate");
        }
    }
}

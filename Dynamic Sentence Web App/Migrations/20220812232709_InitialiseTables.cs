using Microsoft.EntityFrameworkCore.Migrations;

namespace Dynamic_Sentence_Web_App.Migrations
{
    public partial class InitialiseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sentence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sentence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(nullable: true),
                    WordTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordUnit_WordType_WordTypeId",
                        column: x => x.WordTypeId,
                        principalTable: "WordType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordSentence",
                columns: table => new
                {
                    WordUnitId = table.Column<int>(nullable: false),
                    SentenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordSentence", x => new { x.WordUnitId, x.SentenceId });
                    table.ForeignKey(
                        name: "FK_WordSentence_Sentence_SentenceId",
                        column: x => x.SentenceId,
                        principalTable: "Sentence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordSentence_WordUnit_WordUnitId",
                        column: x => x.WordUnitId,
                        principalTable: "WordUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordSentence_SentenceId",
                table: "WordSentence",
                column: "SentenceId");

            migrationBuilder.CreateIndex(
                name: "IX_WordUnit_WordTypeId",
                table: "WordUnit",
                column: "WordTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordSentence");

            migrationBuilder.DropTable(
                name: "Sentence");

            migrationBuilder.DropTable(
                name: "WordUnit");

            migrationBuilder.DropTable(
                name: "WordType");
        }
    }
}

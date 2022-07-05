using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreManager.Migrations
{
    public partial class Kickoff6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Perform_Category_CategoryId",
                table: "Perform");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Perform",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "MusicInterpreter",
                table: "Perform",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Perform",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SongLyrics",
                table: "Perform",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SongTitle",
                table: "Perform",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Category",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Perform_Category_CategoryId",
                table: "Perform",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Perform_Category_CategoryId",
                table: "Perform");

            migrationBuilder.DropColumn(
                name: "MusicInterpreter",
                table: "Perform");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Perform");

            migrationBuilder.DropColumn(
                name: "SongLyrics",
                table: "Perform");

            migrationBuilder.DropColumn(
                name: "SongTitle",
                table: "Perform");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Perform",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Category",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Perform_Category_CategoryId",
                table: "Perform",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMatchAndNewsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_133_Matches_133_Members_Loser1Id",
                table: "133_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_133_Matches_133_Members_Loser2Id",
                table: "133_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_133_Matches_133_Members_Winner1Id",
                table: "133_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_133_Matches_133_Members_Winner2Id",
                table: "133_Matches");

            migrationBuilder.DropIndex(
                name: "IX_133_Matches_Loser1Id",
                table: "133_Matches");

            migrationBuilder.DropIndex(
                name: "IX_133_Matches_Winner1Id",
                table: "133_Matches");

            migrationBuilder.RenameColumn(
                name: "PostedDate",
                table: "133_News",
                newName: "PublishedAt");

            migrationBuilder.RenameColumn(
                name: "Winner2Id",
                table: "133_Matches",
                newName: "Player4Id");

            migrationBuilder.RenameColumn(
                name: "Winner1Id",
                table: "133_Matches",
                newName: "ScoreTeam2");

            migrationBuilder.RenameColumn(
                name: "MatchDate",
                table: "133_Matches",
                newName: "PlayedAt");

            migrationBuilder.RenameColumn(
                name: "Loser2Id",
                table: "133_Matches",
                newName: "Player3Id");

            migrationBuilder.RenameColumn(
                name: "Loser1Id",
                table: "133_Matches",
                newName: "ScoreTeam1");

            migrationBuilder.RenameIndex(
                name: "IX_133_Matches_Winner2Id",
                table: "133_Matches",
                newName: "IX_133_Matches_Player4Id");

            migrationBuilder.RenameIndex(
                name: "IX_133_Matches_Loser2Id",
                table: "133_Matches",
                newName: "IX_133_Matches_Player3Id");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "133_News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "133_News",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPinned",
                table: "133_News",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "133_Matches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player1Id",
                table: "133_Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Player2Id",
                table: "133_Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_133_News_AuthorId",
                table: "133_News",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_133_Matches_Player1Id",
                table: "133_Matches",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_133_Matches_Player2Id",
                table: "133_Matches",
                column: "Player2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_133_Matches_133_Members_Player1Id",
                table: "133_Matches",
                column: "Player1Id",
                principalTable: "133_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_133_Matches_133_Members_Player2Id",
                table: "133_Matches",
                column: "Player2Id",
                principalTable: "133_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_133_Matches_133_Members_Player3Id",
                table: "133_Matches",
                column: "Player3Id",
                principalTable: "133_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_133_Matches_133_Members_Player4Id",
                table: "133_Matches",
                column: "Player4Id",
                principalTable: "133_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_133_News_133_Members_AuthorId",
                table: "133_News",
                column: "AuthorId",
                principalTable: "133_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_133_Matches_133_Members_Player1Id",
                table: "133_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_133_Matches_133_Members_Player2Id",
                table: "133_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_133_Matches_133_Members_Player3Id",
                table: "133_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_133_Matches_133_Members_Player4Id",
                table: "133_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_133_News_133_Members_AuthorId",
                table: "133_News");

            migrationBuilder.DropIndex(
                name: "IX_133_News_AuthorId",
                table: "133_News");

            migrationBuilder.DropIndex(
                name: "IX_133_Matches_Player1Id",
                table: "133_Matches");

            migrationBuilder.DropIndex(
                name: "IX_133_Matches_Player2Id",
                table: "133_Matches");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "133_News");

            migrationBuilder.DropColumn(
                name: "IsPinned",
                table: "133_News");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "133_Matches");

            migrationBuilder.DropColumn(
                name: "Player1Id",
                table: "133_Matches");

            migrationBuilder.DropColumn(
                name: "Player2Id",
                table: "133_Matches");

            migrationBuilder.RenameColumn(
                name: "PublishedAt",
                table: "133_News",
                newName: "PostedDate");

            migrationBuilder.RenameColumn(
                name: "ScoreTeam2",
                table: "133_Matches",
                newName: "Winner1Id");

            migrationBuilder.RenameColumn(
                name: "ScoreTeam1",
                table: "133_Matches",
                newName: "Loser1Id");

            migrationBuilder.RenameColumn(
                name: "Player4Id",
                table: "133_Matches",
                newName: "Winner2Id");

            migrationBuilder.RenameColumn(
                name: "Player3Id",
                table: "133_Matches",
                newName: "Loser2Id");

            migrationBuilder.RenameColumn(
                name: "PlayedAt",
                table: "133_Matches",
                newName: "MatchDate");

            migrationBuilder.RenameIndex(
                name: "IX_133_Matches_Player4Id",
                table: "133_Matches",
                newName: "IX_133_Matches_Winner2Id");

            migrationBuilder.RenameIndex(
                name: "IX_133_Matches_Player3Id",
                table: "133_Matches",
                newName: "IX_133_Matches_Loser2Id");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "133_News",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_133_Matches_Loser1Id",
                table: "133_Matches",
                column: "Loser1Id");

            migrationBuilder.CreateIndex(
                name: "IX_133_Matches_Winner1Id",
                table: "133_Matches",
                column: "Winner1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_133_Matches_133_Members_Loser1Id",
                table: "133_Matches",
                column: "Loser1Id",
                principalTable: "133_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_133_Matches_133_Members_Loser2Id",
                table: "133_Matches",
                column: "Loser2Id",
                principalTable: "133_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_133_Matches_133_Members_Winner1Id",
                table: "133_Matches",
                column: "Winner1Id",
                principalTable: "133_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_133_Matches_133_Members_Winner2Id",
                table: "133_Matches",
                column: "Winner2Id",
                principalTable: "133_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

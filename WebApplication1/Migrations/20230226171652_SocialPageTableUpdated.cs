using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class SocialPageTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialPages_Authors_AuthorId1",
                table: "SocialPages");

            migrationBuilder.DropIndex(
                name: "IX_SocialPages_AuthorId1",
                table: "SocialPages");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "SocialPages");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "SocialPages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "SocialPages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId1",
                table: "SocialPages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialPages_AuthorId1",
                table: "SocialPages",
                column: "AuthorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialPages_Authors_AuthorId1",
                table: "SocialPages",
                column: "AuthorId1",
                principalTable: "Authors",
                principalColumn: "Id");
        }
    }
}

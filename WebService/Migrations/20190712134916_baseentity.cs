using Microsoft.EntityFrameworkCore.Migrations;

namespace WebService.Migrations
{
    public partial class baseentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserInfoForeignKey",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserInfoForeignKey",
                table: "Users",
                column: "UserInfoForeignKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserInfos_UserInfoForeignKey",
                table: "Users",
                column: "UserInfoForeignKey",
                principalTable: "UserInfos",
                principalColumn: "UserInfoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserInfos_UserInfoForeignKey",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserInfoForeignKey",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserInfoForeignKey",
                table: "Users");
        }
    }
}

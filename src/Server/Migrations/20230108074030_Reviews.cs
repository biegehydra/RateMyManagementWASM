using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateMyManagementWASM.Server.Migrations
{
    /// <inheritdoc />
    public partial class Reviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "LocationReviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LocationReviews_ApplicationUserId",
                table: "LocationReviews",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationReviews_AspNetUsers_ApplicationUserId",
                table: "LocationReviews",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationReviews_AspNetUsers_ApplicationUserId",
                table: "LocationReviews");

            migrationBuilder.DropIndex(
                name: "IX_LocationReviews_ApplicationUserId",
                table: "LocationReviews");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "LocationReviews");

        }
    }
}

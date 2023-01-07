using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RateMyManagementWASM.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0bbb2a21-f429-49d0-add1-f5b8fc0103f4", 0, "3e45dc7b-c117-4b14-baf1-36255e979237", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEE9wbiDMioih+rgxXqiZVE/w5v5F4TjX3GcO9VVj4S1kxzay0TMtB58MrZC2KoIH1g==", null, false, "9605e0b3-1e8a-4ef9-9dfd-b4ec079e2980", false, "Administrator" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0bbb2a21-f429-49d0-add1-f5b8fc0103f4");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorId.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01baf8bc-271b-45bb-aaf9-eb7d32a10763");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f23c27ee-7da3-444f-a0b1-cf662ba625b2");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c59162b-c8df-4a5c-9036-37aa1be3539e", null, "IdentityRole", "Visitor", "VISITOR" },
                    { "86947e3c-15f1-49dc-8297-34bf1a9837e5", null, "IdentityRole", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c59162b-c8df-4a5c-9036-37aa1be3539e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86947e3c-15f1-49dc-8297-34bf1a9837e5");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01baf8bc-271b-45bb-aaf9-eb7d32a10763", null, "Visitor", "VISITOR" },
                    { "f23c27ee-7da3-444f-a0b1-cf662ba625b2", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}

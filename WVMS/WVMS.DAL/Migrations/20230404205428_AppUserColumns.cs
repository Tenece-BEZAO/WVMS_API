using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WVMS.DAL.Migrations
{
    public partial class AppUserColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1dc2243b-c8d5-4927-944b-8417d3052336");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5061a1b6-7428-4b78-a2ec-4c98427302b7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef189564-ae36-4a15-ab01-bf1d2cf9383e");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "688cb81c-e1fb-4675-9c96-3a0e80b956cc", "33430a4a-4632-48d5-afd7-81e0aff64046", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "76a766c6-035b-4263-ad38-73e59b127793", "4a8d68b2-745f-47c1-b58b-c70049a6a39b", "Vendor", "VENDOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab2aef89-996e-4a82-a089-0ee717181821", "902031ae-20ae-405e-85a8-48a47de48ec6", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "688cb81c-e1fb-4675-9c96-3a0e80b956cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76a766c6-035b-4263-ad38-73e59b127793");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab2aef89-996e-4a82-a089-0ee717181821");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1dc2243b-c8d5-4927-944b-8417d3052336", "79d71165-debf-4bee-8607-eb04d4d4a9eb", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5061a1b6-7428-4b78-a2ec-4c98427302b7", "38637d14-b189-495c-a8c2-33e28bd859a5", "Vendor", "VENDOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ef189564-ae36-4a15-ab01-bf1d2cf9383e", "de752c26-f135-4f6e-beb7-54d46916ef13", "Customer", "CUSTOMER" });
        }
    }
}

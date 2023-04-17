using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WVMS.DAL.Migrations
{
    public partial class AddedPaymentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9f4e73c-9365-43f3-b5fa-1bab9e34662f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cae48b34-c937-49bb-a1d7-df7e1111983f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3ea8d48-20fc-41fa-90be-ffd0e010a7b3");

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3c1bd2ed-6efb-456a-9e8c-9b3e6a01e95b", "f6f1a831-f0f6-4bdb-8af3-f575b9ab51a1", "Vendor", "VENDOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b87c29b-75c2-4e1a-a070-0c38e28fe873", "c6bb1601-204e-41ab-a479-3b0766d1d97c", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dd6e6f42-1d2f-4443-8360-9662b9d4e9f5", "4eadbd43-9b4c-4eab-b9e4-f7298815c07d", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserId1",
                table: "Payment",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c1bd2ed-6efb-456a-9e8c-9b3e6a01e95b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b87c29b-75c2-4e1a-a070-0c38e28fe873");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd6e6f42-1d2f-4443-8360-9662b9d4e9f5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c9f4e73c-9365-43f3-b5fa-1bab9e34662f", "55bd943e-df4a-4cf5-a20c-7fd29a944d4a", "Vendor", "VENDOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cae48b34-c937-49bb-a1d7-df7e1111983f", "58a0fa77-07b7-4765-96e0-c4fba16409c8", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e3ea8d48-20fc-41fa-90be-ffd0e010a7b3", "d0ffa862-eb3a-468b-a935-b6c01ada2d7b", "Admin", "ADMIN" });
        }
    }
}

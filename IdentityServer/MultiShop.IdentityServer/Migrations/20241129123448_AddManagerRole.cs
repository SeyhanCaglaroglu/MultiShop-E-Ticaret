using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MultiShop.IdentityServer.Migrations
{
    public partial class AddManagerRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Manager rolünü ekle
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Manager", "MANAGER", Guid.NewGuid().ToString() }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Manager rolünü geri al
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Name",
                keyValue: "Manager"
            );
        }
    }

}

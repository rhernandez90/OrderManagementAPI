using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagementAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("53093121-52bd-414c-ac0f-2b799fcd5566"),
                column: "Type",
                value: "0");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8c708ed6-0def-42af-9b8d-ed84c507ddf2"),
                column: "Type",
                value: "0");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e9196394-61d4-4fd0-8fca-daffd301b35f"),
                column: "Type",
                value: "0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Products");
        }
    }
}

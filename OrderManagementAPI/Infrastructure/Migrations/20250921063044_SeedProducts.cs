using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagementAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CapacityMl", "Color", "Description", "HeightCm", "Name", "Paper", "Price", "Size", "Sku", "WidthCm" },
                values: new object[,]
                {
                    { new Guid("53093121-52bd-414c-ac0f-2b799fcd5566"), null, null, "", 60, "Poster Motivacional", "Mate", 12.00m, null, "POSTER001", 40 },
                    { new Guid("8c708ed6-0def-42af-9b8d-ed84c507ddf2"), 350, "Negro", "", null, "Taza de Café", null, 7.50m, null, "MUG001", null },
                    { new Guid("e9196394-61d4-4fd0-8fca-daffd301b35f"), null, "Blanco", "", null, "Playera Básica", null, 15.99m, "M", "TSHIRT001", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("53093121-52bd-414c-ac0f-2b799fcd5566"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8c708ed6-0def-42af-9b8d-ed84c507ddf2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e9196394-61d4-4fd0-8fca-daffd301b35f"));
        }
    }
}

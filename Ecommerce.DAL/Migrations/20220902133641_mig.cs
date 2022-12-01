using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.DAL.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBrands_ProuctBrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProuctTypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProuctTypeId",
                table: "Products",
                newName: "ProductTypeId");

            migrationBuilder.RenameColumn(
                name: "ProuctBrandId",
                table: "Products",
                newName: "ProductBrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProuctTypeId",
                table: "Products",
                newName: "IX_Products_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProuctBrandId",
                table: "Products",
                newName: "IX_Products_ProductBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBrands_ProductBrandId",
                table: "Products",
                column: "ProductBrandId",
                principalTable: "ProductBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBrands_ProductBrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductTypeId",
                table: "Products",
                newName: "ProuctTypeId");

            migrationBuilder.RenameColumn(
                name: "ProductBrandId",
                table: "Products",
                newName: "ProuctBrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                newName: "IX_Products_ProuctTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductBrandId",
                table: "Products",
                newName: "IX_Products_ProuctBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBrands_ProuctBrandId",
                table: "Products",
                column: "ProuctBrandId",
                principalTable: "ProductBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProuctTypeId",
                table: "Products",
                column: "ProuctTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

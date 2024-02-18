using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryAplication.Migrations
{
    public partial class ProductLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationID",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_LocationID",
                table: "Product",
                column: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Location_LocationID",
                table: "Product",
                column: "LocationID",
                principalTable: "Location",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Location_LocationID",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_LocationID",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "Product");
        }
    }
}

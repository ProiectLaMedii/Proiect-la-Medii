using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryAplication.Migrations
{
    public partial class AddRequestProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Client_ClientID",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Product_ProductID",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_ProductID",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Request");

            migrationBuilder.AlterColumn<int>(
                name: "ClientID",
                table: "Request",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "RequestProduct",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestProduct", x => new { x.RequestID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_RequestProduct_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestProduct_Request_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Request",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestProduct_ProductID",
                table: "RequestProduct",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Client_ClientID",
                table: "Request",
                column: "ClientID",
                principalTable: "Client",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Client_ClientID",
                table: "Request");

            migrationBuilder.DropTable(
                name: "RequestProduct");

            migrationBuilder.AlterColumn<int>(
                name: "ClientID",
                table: "Request",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Request_ProductID",
                table: "Request",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Client_ClientID",
                table: "Request",
                column: "ClientID",
                principalTable: "Client",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Product_ProductID",
                table: "Request",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

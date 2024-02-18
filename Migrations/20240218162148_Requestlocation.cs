using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryAplication.Migrations
{
    public partial class Requestlocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryLocationID",
                table: "Request",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_DeliveryLocationID",
                table: "Request",
                column: "DeliveryLocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Location_DeliveryLocationID",
                table: "Request",
                column: "DeliveryLocationID",
                principalTable: "Location",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Location_DeliveryLocationID",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_DeliveryLocationID",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "DeliveryLocationID",
                table: "Request");
        }
    }
}

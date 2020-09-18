using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaEShop.DataLayer.Migrations
{
    public partial class _01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                schema: "User",
                table: "UserAddresses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_OrderId",
                schema: "User",
                table: "UserAddresses",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Orders_OrderId",
                schema: "User",
                table: "UserAddresses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Orders_OrderId",
                schema: "User",
                table: "UserAddresses");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_OrderId",
                schema: "User",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "User",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Orders");
        }
    }
}

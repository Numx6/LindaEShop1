using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaEShop.DataLayer.Migrations
{
    public partial class _02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorName",
                table: "OrderDetails",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SizeName",
                table: "OrderDetails",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "SizeName",
                table: "OrderDetails");
        }
    }
}

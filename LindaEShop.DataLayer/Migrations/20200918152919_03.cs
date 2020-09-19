using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaEShop.DataLayer.Migrations
{
    public partial class _03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankCode",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankCode",
                table: "Orders");
        }
    }
}

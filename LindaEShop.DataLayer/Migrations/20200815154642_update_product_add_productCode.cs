using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaEShop.DataLayer.Migrations
{
    public partial class update_product_add_productCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ActivCode",
                table: "Users",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "Products",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "ActivCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}

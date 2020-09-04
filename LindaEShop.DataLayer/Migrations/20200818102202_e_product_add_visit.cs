using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaEShop.DataLayer.Migrations
{
    public partial class e_product_add_visit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Products",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Visit",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visit",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450,
                oldNullable: true);
        }
    }
}

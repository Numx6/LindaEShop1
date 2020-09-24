using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaEShop.DataLayer.Migrations
{
    public partial class parentID_in_group : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "groupType",
                table: "ProductGroups",
                newName: "GroupType");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "ProductGroups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_ParentId",
                table: "ProductGroups",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroups_ProductGroups_ParentId",
                table: "ProductGroups",
                column: "ParentId",
                principalTable: "ProductGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroups_ProductGroups_ParentId",
                table: "ProductGroups");

            migrationBuilder.DropIndex(
                name: "IX_ProductGroups_ParentId",
                table: "ProductGroups");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ProductGroups");

            migrationBuilder.RenameColumn(
                name: "GroupType",
                table: "ProductGroups",
                newName: "groupType");
        }
    }
}

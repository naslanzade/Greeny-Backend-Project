using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greeny.Migrations
{
    public partial class ChangeColomunNameOfProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductImages",
                newName: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "ProductImages",
                newName: "Name");
        }
    }
}

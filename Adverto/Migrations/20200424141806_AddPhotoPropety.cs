using Microsoft.EntityFrameworkCore.Migrations;

namespace Adverto.Migrations
{
    public partial class AddPhotoPropety : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Adverts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Adverts");
        }
    }
}

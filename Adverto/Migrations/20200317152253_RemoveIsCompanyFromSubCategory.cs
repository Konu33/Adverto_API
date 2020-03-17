using Microsoft.EntityFrameworkCore.Migrations;

namespace Adverto.Migrations
{
    public partial class RemoveIsCompanyFromSubCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCompany",
                table: "SubCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCompany",
                table: "SubCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

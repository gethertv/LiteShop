using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteShop.Migrations
{
    /// <inheritdoc />
    public partial class customerupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sname",
                table: "Customer",
                newName: "City");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Customer",
                newName: "Sname");
        }
    }
}

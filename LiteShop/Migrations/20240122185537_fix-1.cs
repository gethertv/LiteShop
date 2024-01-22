using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteShop.Migrations
{
    /// <inheritdoc />
    public partial class fix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "grade",
                table: "Sczegoly_Zamowienia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "grade",
                table: "Sczegoly_Zamowienia",
                type: "float(1)",
                precision: 1,
                scale: 2,
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

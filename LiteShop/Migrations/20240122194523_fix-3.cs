using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteShop.Migrations
{
    /// <inheritdoc />
    public partial class fix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sczegoly_Zamowienia",
                table: "Sczegoly_Zamowienia");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Sczegoly_Zamowienia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sczegoly_Zamowienia",
                table: "Sczegoly_Zamowienia",
                columns: new[] { "OrderId", "ProductId", "OrderDetailId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sczegoly_Zamowienia",
                table: "Sczegoly_Zamowienia");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Sczegoly_Zamowienia");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sczegoly_Zamowienia",
                table: "Sczegoly_Zamowienia",
                columns: new[] { "OrderId", "ProductId" });
        }
    }
}

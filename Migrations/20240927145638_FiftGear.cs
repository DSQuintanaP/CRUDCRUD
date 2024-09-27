using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDCRUD.Migrations
{
    /// <inheritdoc />
    public partial class FiftGear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "productPrice",
                table: "Productos",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "detalles",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "totalValue",
                table: "compras",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productPrice",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "detalles");

            migrationBuilder.AlterColumn<decimal>(
                name: "totalValue",
                table: "compras",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");
        }
    }
}

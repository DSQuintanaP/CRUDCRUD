using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDCRUD.Migrations
{
    /// <inheritdoc />
    public partial class fourthGear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detalles_clientes_ClienteIDCustomer",
                table: "detalles");

            migrationBuilder.DropIndex(
                name: "IX_detalles_ClienteIDCustomer",
                table: "detalles");

            migrationBuilder.DropColumn(
                name: "ClienteIDCustomer",
                table: "detalles");

            migrationBuilder.CreateIndex(
                name: "IX_compras_IDCustomer",
                table: "compras",
                column: "IDCustomer");

            migrationBuilder.AddForeignKey(
                name: "FK_compras_clientes_IDCustomer",
                table: "compras",
                column: "IDCustomer",
                principalTable: "clientes",
                principalColumn: "IDCustomer",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_compras_clientes_IDCustomer",
                table: "compras");

            migrationBuilder.DropIndex(
                name: "IX_compras_IDCustomer",
                table: "compras");

            migrationBuilder.AddColumn<int>(
                name: "ClienteIDCustomer",
                table: "detalles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_detalles_ClienteIDCustomer",
                table: "detalles",
                column: "ClienteIDCustomer");

            migrationBuilder.AddForeignKey(
                name: "FK_detalles_clientes_ClienteIDCustomer",
                table: "detalles",
                column: "ClienteIDCustomer",
                principalTable: "clientes",
                principalColumn: "IDCustomer");
        }
    }
}

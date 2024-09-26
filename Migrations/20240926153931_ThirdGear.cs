using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDCRUD.Migrations
{
    /// <inheritdoc />
    public partial class ThirdGear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    IDCustomer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    numberPhone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.IDCustomer);
                });

            migrationBuilder.CreateTable(
                name: "compras",
                columns: table => new
                {
                    IDOrder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDCustomer = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    totalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compras", x => x.IDOrder);
                });

            migrationBuilder.CreateTable(
                name: "detalles",
                columns: table => new
                {
                    prductID = table.Column<int>(type: "int", nullable: false),
                    IDOrder = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ClienteIDCustomer = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalles", x => new { x.prductID, x.IDOrder });
                    table.ForeignKey(
                        name: "FK_detalles_Productos_prductID",
                        column: x => x.prductID,
                        principalTable: "Productos",
                        principalColumn: "prductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalles_clientes_ClienteIDCustomer",
                        column: x => x.ClienteIDCustomer,
                        principalTable: "clientes",
                        principalColumn: "IDCustomer");
                    table.ForeignKey(
                        name: "FK_detalles_compras_IDOrder",
                        column: x => x.IDOrder,
                        principalTable: "compras",
                        principalColumn: "IDOrder",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_detalles_ClienteIDCustomer",
                table: "detalles",
                column: "ClienteIDCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_detalles_IDOrder",
                table: "detalles",
                column: "IDOrder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detalles");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "compras");
        }
    }
}

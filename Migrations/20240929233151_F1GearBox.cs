using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDCRUD.Migrations
{
    /// <inheritdoc />
    public partial class F1GearBox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detalles_Productos_prductID",
                table: "detalles");

            migrationBuilder.DropForeignKey(
                name: "FK_detalles_compras_IDOrder",
                table: "detalles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_detalles",
                table: "detalles");

            migrationBuilder.RenameTable(
                name: "detalles",
                newName: "detallecompra");

            migrationBuilder.RenameIndex(
                name: "IX_detalles_IDOrder",
                table: "detallecompra",
                newName: "IX_detallecompra_IDOrder");

            migrationBuilder.AlterColumn<string>(
                name: "productName",
                table: "detallecompra",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "detallecompra",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<int>(
                name: "IDOrder",
                table: "detallecompra",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "prductID",
                table: "detallecompra",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "detalleID",
                table: "detallecompra",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_detallecompra",
                table: "detallecompra",
                column: "detalleID");

            migrationBuilder.CreateIndex(
                name: "IX_detallecompra_prductID",
                table: "detallecompra",
                column: "prductID");

            migrationBuilder.AddForeignKey(
                name: "FK_detallecompra_Productos_prductID",
                table: "detallecompra",
                column: "prductID",
                principalTable: "Productos",
                principalColumn: "prductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detallecompra_compras_IDOrder",
                table: "detallecompra",
                column: "IDOrder",
                principalTable: "compras",
                principalColumn: "IDOrder",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detallecompra_Productos_prductID",
                table: "detallecompra");

            migrationBuilder.DropForeignKey(
                name: "FK_detallecompra_compras_IDOrder",
                table: "detallecompra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_detallecompra",
                table: "detallecompra");

            migrationBuilder.DropIndex(
                name: "IX_detallecompra_prductID",
                table: "detallecompra");

            migrationBuilder.DropColumn(
                name: "detalleID",
                table: "detallecompra");

            migrationBuilder.RenameTable(
                name: "detallecompra",
                newName: "detalles");

            migrationBuilder.RenameIndex(
                name: "IX_detallecompra_IDOrder",
                table: "detalles",
                newName: "IX_detalles_IDOrder");

            migrationBuilder.AlterColumn<string>(
                name: "productName",
                table: "detalles",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "prductID",
                table: "detalles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "detalles",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "IDOrder",
                table: "detalles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_detalles",
                table: "detalles",
                columns: new[] { "prductID", "IDOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_detalles_Productos_prductID",
                table: "detalles",
                column: "prductID",
                principalTable: "Productos",
                principalColumn: "prductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detalles_compras_IDOrder",
                table: "detalles",
                column: "IDOrder",
                principalTable: "compras",
                principalColumn: "IDOrder",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

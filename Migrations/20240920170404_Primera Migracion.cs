using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDCRUD.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    prductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    productsInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.prductID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}

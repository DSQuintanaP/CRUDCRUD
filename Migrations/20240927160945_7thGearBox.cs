using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDCRUD.Migrations
{
    /// <inheritdoc />
    public partial class _7thGearBox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "productName",
                table: "detalles",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productName",
                table: "detalles");
        }
    }
}

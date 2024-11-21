using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProdutoGW.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "Nome", "Role", "SenhaHash" },
                values: new object[] { 1, "admin@dominio.com", "Administrador", "admin", "$2b$10$MrN.C6KDUmEGxmgaKc7bn.l9yZSUhLlJ2v1IgtTlLcu59XaSTQHi2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

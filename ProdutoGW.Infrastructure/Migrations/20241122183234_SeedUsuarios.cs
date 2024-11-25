using System;
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
                columns: new[] { "Id", "Guid", "Nome", "Email", "SenhaHash", "Role" },
                values: new object[] {1, new Guid("d893c4bf-2baf-4098-ad88-d3b807385408"), "admin", "admin@dominio.com", "$2b$10$mKvz/rIrH7r9snxB/rsNlupdmWmw1heYWjwVBeyILu8CJHgdgr2AO", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}

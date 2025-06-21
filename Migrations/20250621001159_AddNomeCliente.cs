using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalizacaoImobiliaria.Migrations
{
    /// <inheritdoc />
    public partial class AddNomeCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Unidade",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Solicitacao",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Personalizacao",
                newName: "UsuarioId");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioNome",
                table: "Unidade",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioNome",
                table: "Solicitacao",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioNome",
                table: "Personalizacao",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioNome",
                table: "Unidade");

            migrationBuilder.DropColumn(
                name: "UsuarioNome",
                table: "Solicitacao");

            migrationBuilder.DropColumn(
                name: "UsuarioNome",
                table: "Personalizacao");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Unidade",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Solicitacao",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Personalizacao",
                newName: "UserId");
        }
    }
}

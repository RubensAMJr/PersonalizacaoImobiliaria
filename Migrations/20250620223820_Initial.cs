using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PersonalizacaoImobiliaria.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.CreateTable(
                name: "Personalizacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personalizacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    NumeroUnidade = table.Column<int>(type: "integer", nullable: false),
                    NomeCliente = table.Column<string>(type: "text", nullable: false),
                    CpfCliente = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solicitacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitacao_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalizacaoSolicitacao",
                columns: table => new
                {
                    PersonalizacoesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SolicitacoesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalizacaoSolicitacao", x => new { x.PersonalizacoesId, x.SolicitacoesId });
                    table.ForeignKey(
                        name: "FK_PersonalizacaoSolicitacao_Personalizacao_PersonalizacoesId",
                        column: x => x.PersonalizacoesId,
                        principalTable: "Personalizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalizacaoSolicitacao_Solicitacao_SolicitacoesId",
                        column: x => x.SolicitacoesId,
                        principalTable: "Solicitacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalizacaoSolicitacao_SolicitacoesId",
                table: "PersonalizacaoSolicitacao",
                column: "SolicitacoesId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_UnidadeId",
                table: "Solicitacao",
                column: "UnidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalizacaoSolicitacao");

            migrationBuilder.DropTable(
                name: "Personalizacao");

            migrationBuilder.DropTable(
                name: "Solicitacao");

            migrationBuilder.DropTable(
                name: "Unidade");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }
    }
}

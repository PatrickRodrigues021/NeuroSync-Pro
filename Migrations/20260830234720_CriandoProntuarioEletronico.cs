using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuroSync.Migrations
{
    /// <inheritdoc />
    public partial class CriandoProntuarioEletronico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "diagnostico_principal",
                table: "paciente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "medicamentos_continuos",
                table: "paciente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "metas_curto_prazo",
                table: "paciente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "metas_longo_prazo",
                table: "paciente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "anexo",
                columns: table => new
                {
                    id_anexo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false),
                    nome_arquivo = table.Column<string>(type: "TEXT", nullable: false),
                    caminho_arquivo = table.Column<string>(type: "TEXT", nullable: false),
                    data_upload = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anexo", x => x.id_anexo);
                    table.ForeignKey(
                        name: "FK_anexo_paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "paciente",
                        principalColumn: "id_paciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "evolucao",
                columns: table => new
                {
                    id_evolucao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false),
                    data_registro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    anotacao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evolucao", x => x.id_evolucao);
                    table.ForeignKey(
                        name: "FK_evolucao_paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "paciente",
                        principalColumn: "id_paciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_anexo_id_paciente",
                table: "anexo",
                column: "id_paciente");

            migrationBuilder.CreateIndex(
                name: "IX_evolucao_id_paciente",
                table: "evolucao",
                column: "id_paciente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anexo");

            migrationBuilder.DropTable(
                name: "evolucao");

            migrationBuilder.DropColumn(
                name: "diagnostico_principal",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "medicamentos_continuos",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "metas_curto_prazo",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "metas_longo_prazo",
                table: "paciente");
        }
    }
}

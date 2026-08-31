using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuroSync.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelaAgenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agendamento",
                columns: table => new
                {
                    id_agendamento = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false),
                    data_hora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    tipo_sessao = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    observacoes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agendamento", x => x.id_agendamento);
                    table.ForeignKey(
                        name: "FK_agendamento_paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "paciente",
                        principalColumn: "id_paciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_agendamento_id_paciente",
                table: "agendamento",
                column: "id_paciente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendamento");
        }
    }
}

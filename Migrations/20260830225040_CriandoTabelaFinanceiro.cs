using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuroSync.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelaFinanceiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cobranca",
                columns: table => new
                {
                    id_cobranca = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false),
                    id_agendamento = table.Column<int>(type: "INTEGER", nullable: true),
                    valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    data_vencimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    data_pagamento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    descricao = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cobranca", x => x.id_cobranca);
                    table.ForeignKey(
                        name: "FK_cobranca_agendamento_id_agendamento",
                        column: x => x.id_agendamento,
                        principalTable: "agendamento",
                        principalColumn: "id_agendamento");
                    table.ForeignKey(
                        name: "FK_cobranca_paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "paciente",
                        principalColumn: "id_paciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cobranca_id_agendamento",
                table: "cobranca",
                column: "id_agendamento");

            migrationBuilder.CreateIndex(
                name: "IX_cobranca_id_paciente",
                table: "cobranca",
                column: "id_paciente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cobranca");
        }
    }
}

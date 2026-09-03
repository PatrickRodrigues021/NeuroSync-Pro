using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuroSync.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agenda",
                columns: table => new
                {
                    id_agenda = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    data_inicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    data_fim = table.Column<DateTime>(type: "TEXT", nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agenda", x => x.id_agenda);
                });

            migrationBuilder.CreateTable(
                name: "avaliacao",
                columns: table => new
                {
                    id_avaliacao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    data_inicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    data_fim = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_avaliacao", x => x.id_avaliacao);
                });

            migrationBuilder.CreateTable(
                name: "intervencao",
                columns: table => new
                {
                    id_intervencao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    data_inicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    data_fim = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_intervencao", x => x.id_intervencao);
                });

            migrationBuilder.CreateTable(
                name: "paciente",
                columns: table => new
                {
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    cpf = table.Column<string>(type: "TEXT", maxLength: 14, nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    idade = table.Column<int>(type: "INTEGER", nullable: false),
                    endereco = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    nome_pai = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    nome_mae = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    escolaridade_pais = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    profissao_pais = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    escola_estuda = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    serie = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    nome_professora = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    nome_pedagoga_psicologa = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    criado_em = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paciente", x => x.id_paciente);
                });

            migrationBuilder.CreateTable(
                name: "Profissionais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Especialidade = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissionais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    senha = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    criado_em = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "sessao",
                columns: table => new
                {
                    id_sessao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    observacoes = table.Column<string>(type: "TEXT", nullable: false),
                    anexos = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    id_avaliacao = table.Column<int>(type: "INTEGER", nullable: false),
                    id_intervencao = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessao", x => x.id_sessao);
                    table.ForeignKey(
                        name: "FK_sessao_avaliacao_id_avaliacao",
                        column: x => x.id_avaliacao,
                        principalTable: "avaliacao",
                        principalColumn: "id_avaliacao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sessao_intervencao_id_intervencao",
                        column: x => x.id_intervencao,
                        principalTable: "intervencao",
                        principalColumn: "id_intervencao");
                });

            migrationBuilder.CreateTable(
                name: "prontuario",
                columns: table => new
                {
                    id_prontuario = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    data_criacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false),
                    queixa = table.Column<string>(type: "TEXT", nullable: false),
                    id_avaliacao = table.Column<int>(type: "INTEGER", nullable: true),
                    id_intervencao = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prontuario", x => x.id_prontuario);
                    table.ForeignKey(
                        name: "FK_prontuario_avaliacao_id_avaliacao",
                        column: x => x.id_avaliacao,
                        principalTable: "avaliacao",
                        principalColumn: "id_avaliacao");
                    table.ForeignKey(
                        name: "FK_prontuario_intervencao_id_intervencao",
                        column: x => x.id_intervencao,
                        principalTable: "intervencao",
                        principalColumn: "id_intervencao");
                    table.ForeignKey(
                        name: "FK_prontuario_paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "paciente",
                        principalColumn: "id_paciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pagamento",
                columns: table => new
                {
                    id_pagamento = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tipo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    data_pagamento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    id_sessao = table.Column<int>(type: "INTEGER", nullable: true),
                    id_avaliacao = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pagamento", x => x.id_pagamento);
                    table.ForeignKey(
                        name: "FK_pagamento_avaliacao_id_avaliacao",
                        column: x => x.id_avaliacao,
                        principalTable: "avaliacao",
                        principalColumn: "id_avaliacao");
                    table.ForeignKey(
                        name: "FK_pagamento_sessao_id_sessao",
                        column: x => x.id_sessao,
                        principalTable: "sessao",
                        principalColumn: "id_sessao");
                });

            migrationBuilder.CreateIndex(
                name: "IX_pagamento_id_avaliacao",
                table: "pagamento",
                column: "id_avaliacao");

            migrationBuilder.CreateIndex(
                name: "IX_pagamento_id_sessao",
                table: "pagamento",
                column: "id_sessao");

            migrationBuilder.CreateIndex(
                name: "IX_prontuario_id_avaliacao",
                table: "prontuario",
                column: "id_avaliacao");

            migrationBuilder.CreateIndex(
                name: "IX_prontuario_id_intervencao",
                table: "prontuario",
                column: "id_intervencao");

            migrationBuilder.CreateIndex(
                name: "IX_prontuario_id_paciente",
                table: "prontuario",
                column: "id_paciente");

            migrationBuilder.CreateIndex(
                name: "IX_sessao_id_avaliacao",
                table: "sessao",
                column: "id_avaliacao");

            migrationBuilder.CreateIndex(
                name: "IX_sessao_id_intervencao",
                table: "sessao",
                column: "id_intervencao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agenda");

            migrationBuilder.DropTable(
                name: "pagamento");

            migrationBuilder.DropTable(
                name: "Profissionais");

            migrationBuilder.DropTable(
                name: "prontuario");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "sessao");

            migrationBuilder.DropTable(
                name: "paciente");

            migrationBuilder.DropTable(
                name: "avaliacao");

            migrationBuilder.DropTable(
                name: "intervencao");
        }
    }
}

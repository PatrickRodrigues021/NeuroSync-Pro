using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuroSync.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoDadosPais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "profissao_pais",
                table: "paciente",
                newName: "profissao_pai");

            migrationBuilder.RenameColumn(
                name: "escolaridade_pais",
                table: "paciente",
                newName: "profissao_mae");

            migrationBuilder.AddColumn<string>(
                name: "cpf_mae",
                table: "paciente",
                type: "TEXT",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cpf_pai",
                table: "paciente",
                type: "TEXT",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "data_nascimento_mae",
                table: "paciente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "data_nascimento_pai",
                table: "paciente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email_mae",
                table: "paciente",
                type: "TEXT",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email_pai",
                table: "paciente",
                type: "TEXT",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "telefone_mae",
                table: "paciente",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "telefone_pai",
                table: "paciente",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cpf_mae",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "cpf_pai",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "data_nascimento_mae",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "data_nascimento_pai",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "email_mae",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "email_pai",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "telefone_mae",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "telefone_pai",
                table: "paciente");

            migrationBuilder.RenameColumn(
                name: "profissao_pai",
                table: "paciente",
                newName: "profissao_pais");

            migrationBuilder.RenameColumn(
                name: "profissao_mae",
                table: "paciente",
                newName: "escolaridade_pais");
        }
    }
}

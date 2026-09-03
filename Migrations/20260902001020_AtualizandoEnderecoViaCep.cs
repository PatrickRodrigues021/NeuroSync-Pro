using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuroSync.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoEnderecoViaCep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "endereco",
                table: "paciente",
                newName: "logradouro");

            migrationBuilder.AddColumn<string>(
                name: "bairro",
                table: "paciente",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cep",
                table: "paciente",
                type: "TEXT",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cidade",
                table: "paciente",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "complemento",
                table: "paciente",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "estado",
                table: "paciente",
                type: "TEXT",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "numero",
                table: "paciente",
                type: "TEXT",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bairro",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "cep",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "cidade",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "complemento",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "numero",
                table: "paciente");

            migrationBuilder.RenameColumn(
                name: "logradouro",
                table: "paciente",
                newName: "endereco");
        }
    }
}

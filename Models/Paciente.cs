using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroSync.Models
{
    [Table("paciente")]
    public class Paciente
    {
        [Key]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(14)]
        [Column("cpf")]
        public string Cpf { get; set; } = string.Empty;

        [Column("data_nascimento")]
        public DateTime DataNascimento { get; set; }

        [Column("idade")]
        public int Idade { get; set; }

        [MaxLength(150)]
        [Column("endereco")]
        public string Endereco { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("nome_pai")]
        public string NomePai { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("nome_mae")]
        public string NomeMae { get; set; } = string.Empty;

        [MaxLength(150)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        [Column("telefone")]
        public string Telefone { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("escolaridade_pais")]
        public string EscolaridadePais { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("profissao_pais")]
        public string ProfissaoPais { get; set; } = string.Empty;

        [MaxLength(150)]
        [Column("escola_estuda")]
        public string EscolaEstuda { get; set; } = string.Empty;

        [MaxLength(50)]
        [Column("serie")]
        public string Serie { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("nome_professora")]
        public string NomeProfessora { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("nome_pedagoga_psicologa")]
        public string NomePedagogaPsicologa { get; set; } = string.Empty;

        [Required]
        [Column("criado_em")]
        public DateTime CriadoEm { get; set; } = DateTime.Now;
    }
}
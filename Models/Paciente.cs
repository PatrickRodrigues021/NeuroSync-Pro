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

        // --- DADOS DO PACIENTE ---
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [MaxLength(100)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(14)]
        [Column("cpf")]
        public string? Cpf { get; set; }

        [Column("data_nascimento")]
        public DateTime DataNascimento { get; set; }

        [Column("idade")]
        public int Idade { get; set; }

        [MaxLength(150)]
        [Column("endereco")]
        public string? Endereco { get; set; }

        [MaxLength(150)]
        [Column("email")]
        public string? Email { get; set; }

        [MaxLength(20)]
        [Column("telefone")]
        public string? Telefone { get; set; }


        // --- DADOS DO PAI ---
        [MaxLength(100)]
        [Column("nome_pai")]
        public string? NomePai { get; set; }

        [MaxLength(14)]
        [Column("cpf_pai")]
        public string? CpfPai { get; set; }

        [MaxLength(20)]
        [Column("telefone_pai")]
        public string? TelefonePai { get; set; }

        [Column("data_nascimento_pai")]
        public DateTime? DataNascimentoPai { get; set; }

        [MaxLength(150)]
        [Column("email_pai")]
        public string? EmailPai { get; set; }

        [MaxLength(100)]
        [Column("profissao_pai")]
        public string? ProfissaoPai { get; set; }


        // --- DADOS DA MÃE ---
        [MaxLength(100)]
        [Column("nome_mae")]
        public string? NomeMae { get; set; }

        [MaxLength(14)]
        [Column("cpf_mae")]
        public string? CpfMae { get; set; }

        [MaxLength(20)]
        [Column("telefone_mae")]
        public string? TelefoneMae { get; set; }

        [Column("data_nascimento_mae")]
        public DateTime? DataNascimentoMae { get; set; }

        [MaxLength(150)]
        [Column("email_mae")]
        public string? EmailMae { get; set; }

        [MaxLength(100)]
        [Column("profissao_mae")]
        public string? ProfissaoMae { get; set; }


        // --- DADOS ESCOLARES ---
        [MaxLength(150)]
        [Column("escola_estuda")]
        public string? EscolaEstuda { get; set; }

        [MaxLength(50)]
        [Column("serie")]
        public string? Serie { get; set; }

        [MaxLength(100)]
        [Column("nome_professora")]
        public string? NomeProfessora { get; set; }

        [MaxLength(100)]
        [Column("nome_pedagoga_psicologa")]
        public string? NomePedagogaPsicologa { get; set; }

        [Required]
        [Column("criado_em")]
        public DateTime CriadoEm { get; set; } = DateTime.Now;

        // --- DADOS CLÍNICOS (ANAMNESE) ---
        
        [Column("diagnostico_principal")]
        public string? DiagnosticoPrincipal { get; set; }

        [Column("medicamentos_continuos")]
        public string? MedicamentosContinuos { get; set; }

        // --- PLANO TERAPÊUTICO ---
        
        [Column("metas_curto_prazo")]
        public string? MetasCurtoPrazo { get; set; }

        [Column("metas_longo_prazo")]
        public string? MetasLongoPrazo { get; set; }
    }
}
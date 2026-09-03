using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroSync.Models
{
    [Table("agendamento")]
    public class Agendamento
    {
        [Key]
        [Column("id_agendamento")]
        public int IdAgendamento { get; set; }

        // --- RELACIONAMENTO COM O PACIENTE ---
        [Required(ErrorMessage = "É obrigatório selecionar um paciente.")]
        [Column("id_paciente")]
        public int PacienteId { get; set; }

        // Essa propriedade ensina o C# a "puxar" os dados do paciente automaticamente
        [ForeignKey("PacienteId")]
        public Paciente? Paciente { get; set; }

        // --- DADOS DA SESSÃO ---
        [Required(ErrorMessage = "A data e horário são obrigatórios.")]
        [Column("data_hora")]
        public DateTime DataHora { get; set; }

        [MaxLength(50)]
        [Column("status")]
        public string Status { get; set; } = "Agendado"; // Ex: Agendado, Realizado, Cancelado, Falta

        [MaxLength(50)]
        [Column("tipo_sessao")]
        public string TipoSessao { get; set; } = "Intervenção"; // Ex: Avaliação, Intervenção, Devolutiva

        [MaxLength(500)]
        [Column("observacoes")]
        public string? Observacoes { get; set; }
    }
}
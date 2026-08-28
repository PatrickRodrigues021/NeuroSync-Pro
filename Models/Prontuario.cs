using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroSync.Models
{
    [Table("prontuario")]
    public class Prontuario
    {
        [Key]
        [Column("id_prontuario")]
        public int Id { get; set; }

        [Required]
        [Column("data_criacao")]
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [Required]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        [ForeignKey("IdPaciente")]
        public Paciente? Paciente { get; set; }

        [Column("queixa")]
        public string Queixa { get; set; } = string.Empty;

        // Relação Prontuario 1 - 1 Avaliacao
        [Column("id_avaliacao")]
        public int? IdAvaliacao { get; set; }

        [ForeignKey("IdAvaliacao")]
        public Avaliacao? Avaliacao { get; set; }

        // Intervenção vinculada ao prontuário
        [Column("id_intervencao")]
        public int? IdIntervencao { get; set; }

        [ForeignKey("IdIntervencao")]
        public Intervencao? Intervencao { get; set; }
    }
}
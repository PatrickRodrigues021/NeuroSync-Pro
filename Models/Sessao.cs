using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroSync.Models
{
    [Table("sessao")]
    public class Sessao
    {
        [Key]
        [Column("id_sessao")]
        public int Id { get; set; }

        [Required]
        [Column("data")]
        public DateTime Data { get; set; }

        [Column("observacoes")]
        public string Observacoes { get; set; } = string.Empty;

        [MaxLength(500)]
        [Column("anexos")]
        public string Anexos { get; set; } = string.Empty;

        // Ligação N - 1 com Avaliação
        [Required]
        [Column("id_avaliacao")]
        public int IdAvaliacao { get; set; }

        [ForeignKey("IdAvaliacao")]
        public Avaliacao? Avaliacao { get; set; }

        // Ligação com Intervenção
        [Column("id_intervencao")]
        public int? IdIntervencao { get; set; }

        [ForeignKey("IdIntervencao")]
        public Intervencao? Intervencoes { get; set; }
    }
}
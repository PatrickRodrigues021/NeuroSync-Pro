using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroSync.Models
{
    [Table("intervencao")]
    public class Intervencao
    {
        [Key]
        [Column("id_intervencao")]
        public int Id { get; set; }

        [Required]
        [Column("data_inicio")]
        public DateTime DataInicio { get; set; }

        [Column("data_fim")]
        public DateTime? DataFim { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("status")]
        public string Status { get; set; } = "Em Andamento";

        // Sessões vinculadas à intervenção
        public ICollection<Sessao> Sessoes { get; set; } = new List<Sessao>();
    }
}
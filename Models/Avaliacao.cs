using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroSync.Models
{
    [Table("avaliacao")]
    public class Avaliacao
    {
        [Key]
        [Column("id_avaliacao")]
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

        // Relação Avaliacao 1 - N Sessao
        public ICollection<Sessao> Sessoes { get; set; } = new List<Sessao>();
    }
}
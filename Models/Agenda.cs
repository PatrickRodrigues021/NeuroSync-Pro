using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroSync.Models
{
    [Table("agenda")]
    public class Agenda
    {
        [Key]
        [Column("id_agenda")]
        public int Id { get; set; }

        [Required]
        [Column("data_inicio")]
        public DateTime DataInicio { get; set; }

        [Required]
        [Column("data_fim")]
        public DateTime DataFim { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("status")]
        public string Status { get; set; } = "Agendado";
    }
}
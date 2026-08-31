using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroSync.Models
{
    [Table("evolucao")]
    public class Evolucao
    {
        [Key]
        [Column("id_evolucao")]
        public int IdEvolucao { get; set; }

        [Required]
        [Column("id_paciente")]
        public int PacienteId { get; set; }
        
        [ForeignKey("PacienteId")]
        public Paciente? Paciente { get; set; }

        [Required]
        [Column("data_registro")]
        public DateTime DataRegistro { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "A anotação da evolução é obrigatória.")]
        [Column("anotacao", TypeName = "text")]
        public string Anotacao { get; set; } = string.Empty;
    }
}
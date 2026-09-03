using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroSync.Models
{
    [Table("anexo")]
    public class Anexo
    {
        [Key]
        [Column("id_anexo")]
        public int IdAnexo { get; set; }

        [Required]
        [Column("id_paciente")]
        public int PacienteId { get; set; }
        
        [ForeignKey("PacienteId")]
        public Paciente? Paciente { get; set; }

        [Required]
        [Column("nome_arquivo")]
        public string NomeArquivo { get; set; } = string.Empty;

        [Required]
        [Column("caminho_arquivo")] // Guarda onde o PDF/Imagem foi salvo
        public string CaminhoArquivo { get; set; } = string.Empty;

        [Column("data_upload")]
        public DateTime DataUpload { get; set; } = DateTime.Now;
    }
}
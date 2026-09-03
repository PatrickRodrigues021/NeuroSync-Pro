using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroSync.Models
{
    [Table("cobranca")]
    public class Cobranca
    {
        [Key]
        [Column("id_cobranca")]
        public int IdCobranca { get; set; }

        // --- RELACIONAMENTO OBRIGATÓRIO: PACIENTE ---
        [Required(ErrorMessage = "O paciente é obrigatório.")]
        [Column("id_paciente")]
        public int PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente? Paciente { get; set; }

        // --- RELACIONAMENTO OPCIONAL: AGENDA (Para sessões avulsas) ---
        // O "?" significa que pode ser nulo (ou seja, se for mensalidade, fica vazio)
        [Column("id_agendamento")]
        public int? AgendamentoId { get; set; } 

        [ForeignKey("AgendamentoId")]
        public Agendamento? Agendamento { get; set; }

        // --- DADOS DO DINHEIRO ---
        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Column("valor", TypeName = "decimal(10,2)")] // Formato de moeda (ex: 150.00)
        public decimal Valor { get; set; }

        [Required]
        [Column("data_vencimento")]
        public DateTime DataVencimento { get; set; }

        [Column("data_pagamento")]
        public DateTime? DataPagamento { get; set; } // Fica nulo até o paciente pagar

        [Required]
        [MaxLength(50)]
        [Column("status")]
        public string Status { get; set; } = "Pendente"; // Pendente, Pago, Atrasado, Cancelado

        [Required]
        [MaxLength(100)]
        [Column("descricao")]
        public string Descricao { get; set; } = string.Empty; // Ex: "Sessão Avulsa 30/08" ou "Mensalidade Agosto"
    }
}
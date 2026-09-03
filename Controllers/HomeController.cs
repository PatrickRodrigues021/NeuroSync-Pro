using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeuroSync.Data;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace NeuroSync.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 1. Total de Pacientes
            ViewBag.TotalPacientes = _context.Pacientes.Count();

            // 2. Consultas Hoje
            var hoje = DateTime.Today;
            ViewBag.ConsultasHoje = _context.Agendamentos
                                            .Count(a => a.DataHora.Date == hoje);

            // 3. A RECEBER NO MÊS (A Mágica do Financeiro!)
            var mesAtual = hoje.Month;
            var anoAtual = hoje.Year;
                        
            // Vai no banco, filtra as cobranças deste mês que estão Pendentes...
            var totalReceber = _context.Cobrancas
                .Where(c => c.DataVencimento.Month == mesAtual && 
                            c.DataVencimento.Year == anoAtual && 
                            c.Status == "Pendente")
                .ToList() // <--- A CORREÇÃO ENTRA AQUI! (Traz para a memória do C#)
                .Sum(c => c.Valor); // Faz a soma com precisão financeira!
                                    
            ViewBag.TotalReceberMes = totalReceber;

            
// 5. PRÓXIMOS AGENDAMENTOS (Mini Calendário para a tela inicial)
            ViewBag.ProximosAgendamentos = _context.Agendamentos
                                                   .Include(a => a.Paciente)
                                                   .Where(a => a.DataHora >= DateTime.Today) // De hoje para frente
                                                   .OrderBy(a => a.DataHora) // Do mais próximo para o mais distante
                                                   .Take(5) // Limita a 5 consultas para não poluir a tela
                                                   .ToList();


            // 4. TABELA DE PACIENTES
            var pacientes = _context.Pacientes.OrderBy(p => p.Nome).ToList();

            return View(pacientes);
        }
    }
}
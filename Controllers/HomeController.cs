using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using NeuroSync.Data;
using NeuroSync.Models;

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

        public async Task<IActionResult> Index()
        {
            // 1. Definir datas (Hoje e Primeiro dia do Mês)
            var hoje = DateTime.Today;
            var inicioDoMes = new DateTime(hoje.Year, hoje.Month, 1);
            var fimDoMes = inicioDoMes.AddMonths(1).AddDays(-1);

            // 2. INDICADORES DOS CARDS (Top)
            ViewBag.TotalPacientes = await _context.Pacientes.CountAsync();
            
            ViewBag.SessoesHoje = await _context.Agendamentos
                .CountAsync(a => a.DataHora.Date == hoje);
                
            ViewBag.SessoesConcluidas = await _context.Agendamentos
                .CountAsync(a => a.DataHora.Date == hoje && a.Status.Contains("Realizado"));

            // Pendências (Exemplo: Agendamentos antigos que não foram realizados/concluídos)
            ViewBag.Pendencias = await _context.Agendamentos
                .CountAsync(a => a.DataHora.Date < hoje && !a.Status.Contains("Realizado") && !a.Status.Contains("Cancelado"));

            // 1. O Banco de Dados traz a lista de cobranças pagas do mês para a memória
            var cobrancasDoMes = await _context.Cobrancas
                .Where(c => c.Status == "Pago" && c.DataPagamento >= inicioDoMes && c.DataPagamento <= fimDoMes)
                .ToListAsync();

            // 2. O C# faz a soma matemática com os decimais de forma segura
            ViewBag.ReceitaMes = cobrancasDoMes.Sum(c => c.Valor);

            // 3. PRÓXIMOS ATENDIMENTOS (Para a lista)
            // O Include(a => a.Paciente) traz o Nome do paciente automaticamente!
            ViewBag.ProximosAtendimentos = await _context.Agendamentos
                .Include(a => a.Paciente)
                .Where(a => a.DataHora.Date == hoje && a.DataHora >= DateTime.Now)
                .OrderBy(a => a.DataHora)
                .Take(5)
                .ToListAsync();

            // 4. DADOS PARA O GRÁFICO (Status do mês atual)
            var sessoesMes = await _context.Agendamentos
                .Where(a => a.DataHora >= inicioDoMes && a.DataHora <= fimDoMes)
                .ToListAsync();

            ViewBag.TotalMes = sessoesMes.Count;
            ViewBag.Realizadas = sessoesMes.Count(a => a.Status.Contains("Realizado"));
            ViewBag.Agendadas = sessoesMes.Count(a => a.Status.Contains("Agendado"));
            ViewBag.Canceladas = sessoesMes.Count(a => a.Status.Contains("Cancelado"));
            ViewBag.Faltas = sessoesMes.Count(a => a.Status.Contains("Falta"));
            
            // Passa o nome do mês atual de forma dinâmica (Ex: "setembro", "outubro")
            ViewBag.NomeMes = hoje.ToString("MMMM");

            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NeuroSync.Data;
using NeuroSync.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace NeuroSync.Controllers
{
    [Authorize]
    public class FinanceiroController : Controller
    {
        private readonly AppDbContext _context;

        public FinanceiroController(AppDbContext context)
        {
            _context = context;
        }

// 1. TELA PRINCIPAL (Lista de todas as cobranças)
        public IActionResult Index()
        {
            // Busca as cobranças, INCLUI o Paciente e ordena pela data de vencimento
            var cobrancas = _context.Cobrancas
                                    .Include(c => c.Paciente)
                                    .OrderBy(c => c.DataVencimento)
                                    .ToList();

            return View(cobrancas);
        }

        // 2. GET: Abre a tela para gerar uma nova cobrança
        public IActionResult Create()
        {
            // Puxa a lista de pacientes para o Select2
            ViewBag.Pacientes = new SelectList(_context.Pacientes.OrderBy(p => p.Nome), "IdPaciente", "Nome");
            return View();
        }

        // 4. GET: Abre a tela de confirmação de pagamento
        public IActionResult Baixa(int? id)
        {
            if (id == null) return NotFound();

            // Busca a cobrança e o nome do paciente
            var cobranca = _context.Cobrancas
                                   .Include(c => c.Paciente)
                                   .FirstOrDefault(c => c.IdCobranca == id);

            if (cobranca == null) return NotFound();

            // Sugere a data de hoje como a data do pagamento
            cobranca.DataPagamento = DateTime.Today;

            return View(cobranca);
        }

        // 5. POST: Efetiva o pagamento no banco de dados
        [HttpPost]
        public IActionResult Baixa(int id, Cobranca cobranca)
        {
            if (id != cobranca.IdCobranca) return NotFound();

            // Vai no banco e pega a cobrança original para não perder os outros dados
            var cobrancaOriginal = _context.Cobrancas.Find(id);
            
            if (cobrancaOriginal != null)
            {
                // Atualiza apenas o que importa para o pagamento
                cobrancaOriginal.Status = "Pago";
                cobrancaOriginal.DataPagamento = cobranca.DataPagamento;
                
                _context.SaveChanges();
            }

            // Volta para a tabela financeira
            return RedirectToAction("Index");
        }

        // 6. GET: Abre a tela de confirmação para apagar o boleto
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            // Busca a cobrança e o paciente associado
            var cobranca = _context.Cobrancas
                                   .Include(c => c.Paciente)
                                   .FirstOrDefault(c => c.IdCobranca == id);

            if (cobranca == null) return NotFound();

            return View(cobranca);
        }

        // 7. POST: Vai no banco de dados e apaga de vez
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var cobranca = _context.Cobrancas.Find(id);
            
            if (cobranca != null)
            {
                _context.Cobrancas.Remove(cobranca);
                _context.SaveChanges();
            }
            
            // Volta para a tabela financeira
            return RedirectToAction("Index");
        }

        // 3. POST: Salva o boleto/cobrança no banco
        [HttpPost]
        public IActionResult Create(Cobranca cobranca)
        {
            if (ModelState.IsValid)
            {
                _context.Cobrancas.Add(cobranca);
                _context.SaveChanges();
                
                // Redireciona para o painel inicial para vermos o número do cartão mudar!
                return RedirectToAction("Index", "Home"); 
            }
            
            ViewBag.Pacientes = new SelectList(_context.Pacientes.OrderBy(p => p.Nome), "IdPaciente", "Nome", cobranca.PacienteId);
            return View(cobranca);
        }
    }
}
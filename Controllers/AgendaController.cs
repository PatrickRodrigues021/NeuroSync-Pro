using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NeuroSync.Data;
using NeuroSync.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace NeuroSync.Controllers
{
    [Authorize]
    public class AgendaController : Controller
    {
        private readonly AppDbContext _context;

        public AgendaController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Ação para uma futura tela de listagem
// 1. TELA PRINCIPAL (Lista de horários)
        public IActionResult Index()
        {
            // Vai no banco, busca a agenda, INCLUI os dados do Paciente e ordena pela data mais próxima
            var agendamentos = _context.Agendamentos
                                       .Include(a => a.Paciente)
                                       .OrderBy(a => a.DataHora)
                                       .ToList();

            return View(agendamentos);
        }

        // 2. GET: Abre a tela de agendamento e envia a lista de pacientes
        public IActionResult Create()
        {
            // Pega os pacientes do banco e cria a lista para o Select2 usar
            ViewBag.Pacientes = new SelectList(_context.Pacientes.OrderBy(p => p.Nome), "IdPaciente", "Nome");
            return View();
        }

        // 4. GET: Abre a tela de edição preenchida
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = _context.Agendamentos.Find(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            // Recarrega a lista de pacientes, já deixando selecionado o paciente atual
            ViewBag.Pacientes = new SelectList(_context.Pacientes.OrderBy(p => p.Nome), "IdPaciente", "Nome", agendamento.PacienteId);
            return View(agendamento);
        }

        // 5. POST: Salva as alterações da sessão
        [HttpPost]
        public IActionResult Edit(int id, Agendamento agendamento)
        {
            if (id != agendamento.IdAgendamento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(agendamento);
                _context.SaveChanges();
                
                // Manda de volta para a lista da agenda!
                return RedirectToAction("Index");
            }
            
            ViewBag.Pacientes = new SelectList(_context.Pacientes.OrderBy(p => p.Nome), "IdPaciente", "Nome", agendamento.PacienteId);
            return View(agendamento);
        }

        // 6. GET: Abre a tela de confirmação de exclusão
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Busca a sessão no banco, trazendo o Paciente junto para podermos mostrar o nome na tela
            var agendamento = _context.Agendamentos
                                      .Include(a => a.Paciente)
                                      .FirstOrDefault(a => a.IdAgendamento == id);
            
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // 7. POST: A ação que realmente apaga a sessão do banco
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var agendamento = _context.Agendamentos.Find(id);
            
            if (agendamento != null)
            {
                _context.Agendamentos.Remove(agendamento);
                _context.SaveChanges();
            }
            
            // Volta para a tabela da agenda
            return RedirectToAction("Index");
        }

        // 3. POST: Salva a sessão no banco de dados
        [HttpPost]
        public IActionResult Create(Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                _context.Agendamentos.Add(agendamento);
                _context.SaveChanges();
                
                // Redireciona para o painel inicial por enquanto
                return RedirectToAction("Index", "Home"); 
            }
            
            // Se der erro, recarrega a lista
            ViewBag.Pacientes = new SelectList(_context.Pacientes.OrderBy(p => p.Nome), "IdPaciente", "Nome", agendamento.PacienteId);
            return View(agendamento);
        }
    }
}
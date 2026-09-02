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

        // ==========================================
        // 2. GET: Abre a tela de Novo Agendamento
        // ==========================================
        [HttpGet]
        public IActionResult Create()
        {
            // Pega os pacientes do banco e cria a lista para o Select2 usar
            ViewBag.Pacientes = new SelectList(_context.Pacientes.OrderBy(p => p.Nome), "IdPaciente", "Nome");
            return View();
        }

        // ==========================================
        // 3. POST: Salva a sessão (com Repetição!)
        // ==========================================
        [HttpPost]
        public IActionResult Create(Agendamento agendamento, int semanasRepeticao = 1)
        {
            if (ModelState.IsValid)
            {
                // O laço de repetição: vai rodar 1, 4, 12 ou 24 vezes dependendo da escolha
                for (int i = 0; i < semanasRepeticao; i++)
                {
                    var novaSessao = new Agendamento
                    {
                        PacienteId = agendamento.PacienteId,
                        
                        // O pulo do gato: Pega a data original e soma 7 dias multiplicados pela semana atual
                        DataHora = agendamento.DataHora.AddDays(7 * i),
                        
                        // Garante que todas nasçam como "Agendadas"
                        Status = "Agendado" 
                    };
                    
                    _context.Agendamentos.Add(novaSessao);
                }
                
                // Salva todos os clones no banco de dados de uma vez só!
                _context.SaveChanges();
                
                // Redireciona para a tela da Agenda
                return RedirectToAction("Index"); 
            }
            
            ViewBag.Pacientes = new SelectList(_context.Pacientes.OrderBy(p => p.Nome), "IdPaciente", "Nome", agendamento.PacienteId);
            return View(agendamento);
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
    }
}
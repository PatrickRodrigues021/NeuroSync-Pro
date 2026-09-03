using Microsoft.AspNetCore.Mvc;
using NeuroSync.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace NeuroSync.Controllers
{
    [Authorize]
    public class ProntuariosController : Controller
    {
        private readonly AppDbContext _context;

        public ProntuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Prontuarios
        public IActionResult Index()
        {
            // Busca os pacientes no banco em ordem alfabética para a tela de seleção
            var pacientes = _context.Pacientes.OrderBy(p => p.Nome).ToList();
            return View(pacientes);
        }
    }
}
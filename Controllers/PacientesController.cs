using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.IO; 
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Hosting; 
using NeuroSync.Models;
using NeuroSync.Data;

namespace NeuroSync.Controllers
{
    [Authorize]
    public class PacientesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        // O Construtor recebe o banco de dados e o controle de pastas do servidor
        public PacientesController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // 1. TELA DE BUSCA E LISTAGEM
        public IActionResult Index(string termoBusca)
        {
            var pacientes = _context.Pacientes.AsQueryable();
            if (!string.IsNullOrEmpty(termoBusca))
            {
                pacientes = pacientes.Where(p => p.Nome.ToLower().Contains(termoBusca.ToLower()));
                ViewBag.BuscaAtual = termoBusca;
            }
            return View(pacientes.OrderBy(p => p.Nome).ToList());
        }

        // 2. TELA DE PRONTUÁRIO / PERFIL
        // GET: Pacientes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // 1. Busca os dados do paciente
            var paciente = _context.Pacientes
                .FirstOrDefault(m => m.IdPaciente == id);

            if (paciente == null)
            {
                return NotFound();
            }

            // 2. Busca os PRÓXIMOS Atendimentos (Agendamentos com data futura)
            ViewBag.ProximosAtendimentos = _context.Agendamentos
                .Where(a => a.PacienteId == id && a.DataHora >= DateTime.Now)
                .OrderBy(a => a.DataHora)
                .Take(3)
                .ToList();

            // 3. Busca as ÚLTIMAS Evoluções (Sessões passadas)
            ViewBag.UltimasEvolucoes = _context.Agendamentos
                .Where(a => a.PacienteId == id && a.Status.Contains("Realizado"))
                .OrderByDescending(a => a.DataHora)
                .Take(3)
                .ToList();

            // 4. Busca os ANEXOS (Documentos/Exames salvos)
            ViewBag.Anexos = _context.Anexos
                .Where(a => a.PacienteId == id)
                .OrderByDescending(a => a.DataUpload)
                .ToList();

            return View(paciente);
        }

        // 3. SALVAR EVOLUÇÃO
        [HttpPost]
        public IActionResult AdicionarEvolucao(int PacienteId, string Anotacao)
        {
            if (!string.IsNullOrEmpty(Anotacao))
            {
                var novaEvolucao = new Evolucao { PacienteId = PacienteId, Anotacao = Anotacao, DataRegistro = DateTime.Now };
                _context.Evolucoes.Add(novaEvolucao);
                _context.SaveChanges();
            }
            return RedirectToAction("Details", new { id = PacienteId });
        }

        // ========================================================
        // 4. NOVA FUNÇÃO: UPLOAD DE ANEXOS (PDF/IMAGEM)
        // ========================================================
        [HttpPost]
        public async Task<IActionResult> UploadAnexo(int pacienteId, IFormFile arquivoUpload)
        {
            if (arquivoUpload != null && arquivoUpload.Length > 0)
            {
                // 1. Descobre onde é a pasta wwwroot/uploads
                string pastaUploads = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                
                // 2. Se a pasta não existir, cria ela automaticamente
                if (!Directory.Exists(pastaUploads))
                {
                    Directory.CreateDirectory(pastaUploads);
                }

                // 3. Cria um nome único para o arquivo não substituir outro
                string nomeUnico = Guid.NewGuid().ToString() + "_" + arquivoUpload.FileName;
                string caminhoCompleto = Path.Combine(pastaUploads, nomeUnico);

                // 4. Copia o arquivo do computador para dentro da pasta do sistema
                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await arquivoUpload.CopyToAsync(stream);
                }

                // 5. Salva o registro no Banco de Dados
                var novoAnexo = new Anexo
                {
                    PacienteId = pacienteId,
                    NomeArquivo = arquivoUpload.FileName,
                    CaminhoArquivo = "/uploads/" + nomeUnico, // Rota para acessar na web
                    DataUpload = DateTime.Now
                };

                _context.Anexos.Add(novoAnexo);
                await _context.SaveChangesAsync();
            }

            // Volta para a tela do paciente atualizada
            return RedirectToAction("Details", new { id = pacienteId });
        }

        // 5. CADASTRAR NOVO PACIENTE
        public IActionResult Create() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            if (ModelState.IsValid) { _context.Add(paciente); await _context.SaveChangesAsync(); return RedirectToAction("Index"); }
            return View(paciente);
        }

        // 6. EDITAR PACIENTE E ANAMNESE
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null) return NotFound();
            return View(paciente);
        }

        [HttpPost]
        public IActionResult Edit(int id, Paciente paciente)
        {
            if (id != paciente.IdPaciente) return NotFound();
            if (ModelState.IsValid) { _context.Update(paciente); _context.SaveChanges(); return RedirectToAction("Details", new { id = paciente.IdPaciente }); }
            return View(paciente);
        }

        // 7. EXCLUIR PACIENTE
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null) return NotFound();
            return View(paciente);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente != null) { _context.Pacientes.Remove(paciente); _context.SaveChanges(); }
            return RedirectToAction("Index");
        }
    }
}
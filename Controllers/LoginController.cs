using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NeuroSync.Controllers
{
    public class LoginController : Controller
    {
        // Abre a tela visual de login
        public IActionResult Index() => View();

        // Recebe os dados quando o usuário clica em "Entrar"
        [HttpPost]
        public async Task<IActionResult> Entrar(string usuario, string senha)
        {
            // Validação de segurança simples para testes
            if (usuario == "admin" && senha == "admin123")
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, usuario) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                
                // Gera o "crachá" (Cookie) e libera a entrada
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                
                // Manda o usuário para o Dashboard
                return RedirectToAction("Index", "Home");
            }
            
            // Se errar a senha, mostra mensagem de erro na tela
            ViewBag.Erro = "Usuário ou senha inválidos!";
            return View("Index");
        }

        // Função para clicar no botão "Sair" e rasgar o crachá
        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync(); 
            return RedirectToAction("Index", "Login");
        }
    }
}
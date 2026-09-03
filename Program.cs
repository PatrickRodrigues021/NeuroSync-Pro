using Microsoft.AspNetCore.Authentication.Cookies; // 1. NOVO USING DA SEGURANÇA
using Microsoft.EntityFrameworkCore;
using NeuroSync.Data;
using QuestPDF.Infrastructure;

// Licença gratuita (Community) do QuestPDF, usada para gerar os PDFs do Financeiro
QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// Registra o AppDbContext no sistema
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// 2. CONFIGURAÇÃO DA RECEPÇÃO (O "Crachá" de Acesso)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index"; // Para onde ir se não estiver logado
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 3. ATIVA A SEGURANÇA (Obrigatório vir ANTES do UseAuthorization)
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
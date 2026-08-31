# NeuroSync 🧠 

Um sistema completo de Gestão Clínica e Prontuário Eletrônico do Paciente (PEP), projetado com foco em clínicas de terapias e neurodesenvolvimento.

## 🛠️ Tecnologias Utilizadas
* **Back-end:** C# com ASP.NET Core MVC
* **Banco de Dados:** SQLite com Entity Framework Core (ORM)
* **Front-end:** HTML5, CSS3, Razor Pages e Bootstrap 5
* **Segurança:** Autenticação baseada em Cookies (CookieAuthentication)

## 🚀 Funcionalidades e Últimas Atualizações

* **🔐 Segurança e Controle de Acesso:** Rotas protegidas com `[Authorize]`, bloqueio de usuários não logados, saudação dinâmica e fluxo de Logout.
* **📊 Dashboard Dinâmico:** Painel inicial com métricas em tempo real e timeline minimalista de próximos agendamentos com indicadores visuais de status.
* **🔍 Motor de Busca Inteligente:** Listagem de pacientes com filtro de pesquisa construído via consultas LINQ.
* **🗂️ Prontuário Eletrônico (PEP) em Abas:**
  * **Anamnese e Plano Terapêutico:** Registros de diagnóstico (CID), medicamentos de uso contínuo e metas de curto/longo prazo.
  * **Histórico de Evoluções:** Registro cronológico de sessões com carimbo automático de data e hora.
  * **Gestão de Documentos:** Upload de exames, laudos e testes (PDFs/Imagens) salvos fisicamente no servidor e mapeados no banco de dados.


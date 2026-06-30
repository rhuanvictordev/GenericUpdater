using AtualizadorGenerico.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace AtualizadorGenerico.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var pasta = Path.Combine(AppContext.BaseDirectory, "Programas");

            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
                return View(new List<Programa>());
            }

            var subpastas = Directory.GetDirectories(pasta);
            var programas = new List<Programa>();

            foreach (var subpasta in subpastas)
            {
                string caminhoManifest = Path.Combine(subpasta, "manifest.json");
                if (System.IO.File.Exists(caminhoManifest))
                {
                    string jsonManifest = System.IO.File.ReadAllText(caminhoManifest);
                    Debug.WriteLine(jsonManifest);
                    try
                    {
                        var programa = JsonSerializer.Deserialize<Programa>(jsonManifest);
                        if (programa != null && programa.Nome != null && programa.Versao != null)
                        {
                            programas.Add(programa);
                        }
                        else
                        {
                            return View("Erro", jsonManifest);
                        }
                    }
                    catch (Exception ex)
                    {
                        return View("Erro", jsonManifest);
                    }
                }
            }

            return View(programas);
        }

        public IActionResult Modelo()
        {
            return View();
        }

        
        [HttpPost]
        public JsonResult ObterVersaoAtual(string nomePrograma)
        {
            return null;
        }
    }
}
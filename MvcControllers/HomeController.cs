using AtualizadorGenerico.Models;
using Microsoft.AspNetCore.Mvc;
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

                if (File.Exists(caminhoManifest))
                {
                    string json = File.ReadAllText(caminhoManifest);

                    var programa = JsonSerializer.Deserialize<Programa>(json);

                    if (programa != null)
                    {
                        programas.Add(programa);
                    }
                }
            }

            return View(programas);
        }
    }
}
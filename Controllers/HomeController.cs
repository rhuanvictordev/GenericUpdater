using AtualizadorGenerico.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace AtualizadorGenerico.Controllers
{
    public class HomeController : Controller
    {
        private readonly string pasta = Path.Combine(AppContext.BaseDirectory, "Programas");

        public IActionResult Index()
        {
            string[] subpastas = Directory.GetDirectories(pasta);
            var programas = new List<Programa>();

            foreach (var subpasta in subpastas)
            {
                string caminhoManifest = Path.Combine(subpasta, "manifest.json");
                if (System.IO.File.Exists(caminhoManifest))
                {
                    string jsonManifest = System.IO.File.ReadAllText(caminhoManifest);
                    try
                    {
                        var programa = JsonSerializer.Deserialize<Programa>(jsonManifest);
                        if (programa != null && programa.AppKeyName != null && programa.Version != null)
                        {
                            programa.AppName = Path.GetFileName(subpasta);

                            foreach (var item in programas)
                            {
                                if (programa.AppKeyName.Equals(item.AppKeyName))
                                {
                                    ViewBag.Header = "O sistema encontrou um erro ao tentar converter um manifest.json em um Objeto";
                                    ViewBag.Body = $"Foram encontrados 2 programas com os mesmos manifests: {jsonManifest}";
                                    return View("Erro");
                                }
                            }

                            programas.Add(programa);
                        }
                        else
                        {
                            ViewBag.Header = "O sistema encontrou um erro ao tentar converter um manifest.json em um Objeto";
                            return View("Erro");
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Header = "O sistema encontrou um erro";
                        ViewBag.Body = ex.Message;
                        return View("Erro");
                    }
                }
            }

            return View(programas);
        }

        public IActionResult Modelo()
        {
            Debug.WriteLine("AAAAAA");
            return View();
        }

    }
}
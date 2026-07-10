using AtualizadorGenerico.Models;
using AtualizadorGenerico.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace AtualizadorGenerico.Controllers
{
    public class HomeController : Controller
    {
        private readonly string pasta = Path.Combine(AppContext.BaseDirectory, "Programas");
        private ProgramaRepository programaRepository = new ProgramaRepository();

        public IActionResult Index()
        {
            string[] subpastas = Directory.GetDirectories(pasta);
            var programas = new List<Programa>();

            foreach (var subpasta in subpastas)
            {
                string manifest = Path.Combine(subpasta, "manifest.json");
                if (System.IO.File.Exists(manifest))
                {
                    string jsonManifest = System.IO.File.ReadAllText(manifest);
                    try
                    {
                        var programa = JsonSerializer.Deserialize<Programa>(jsonManifest);
                        if (programa != null)
                        {
                            string nomePasta = Path.GetFileName(subpasta);
                            if (programa.Version == null || programa.Version == "" || programa.Version == String.Empty)
                            {
                                ViewBag.Header = "O sistema encontrou um erro";
                                ViewBag.Body = $"Versão do programa: {nomePasta}, não definida no manifest: {jsonManifest}";
                                return View("Erro");
                            }

                            foreach (var item in programas)
                            {
                                if (programa.AppKeyName.Equals(item.AppKeyName))
                                {
                                    ViewBag.Header = "O sistema encontrou um erro";
                                    ViewBag.Body = $"Foram encontrados 2 programas com os mesmos AppKeyName nos manifests: {jsonManifest}";
                                    return View("Erro");
                                }
                            }

                            programa.AppName = nomePasta;
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
                        ViewBag.Header = $"Tentativa de leitura do Json falhou: {ex.Message}";
                        ViewBag.Body = $"{jsonManifest}";
                        return View("Erro");
                    }
                }
            }

            return View(programas);
        }

        public IActionResult Modelo()
        {
            return View();
        }


        public IActionResult Atualizar(string appKeyName)
        {
            var programa = programaRepository.CarregarPrograma(appKeyName);
            if (programa == null)
                return NotFound();

            UploadViewModel upModel = new UploadViewModel();
            upModel.Programa = programa;

            return View(upModel);
        }

        public IActionResult AdicionarPrograma()
        {
            Programa p = new Programa();
            UploadViewModel upModel = new UploadViewModel();
            upModel.Programa = p;
            upModel.Programa.Version = DateTime.Now.ToString("dd.MM.yyyy");
            return View(upModel);
        }



    }
}
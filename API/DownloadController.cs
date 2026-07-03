using AtualizadorGenerico.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace AtualizadorGenerico.API
{
    public class DownloadController : Controller
    {
        private readonly string pasta = Path.Combine(AppContext.BaseDirectory, "Programas");

        [HttpGet("/Download/{AppKeyName}")]
        public IActionResult Index(string AppKeyName)
        {
            try
            {
                string[] subpastas = Directory.GetDirectories(pasta);
                foreach (var subpasta in subpastas)
                { 
                    string manifest = Path.Combine(subpasta, "manifest.json");
                    string packageZip = Path.Combine(subpasta, "package.zip");

                    string jsonManifest = System.IO.File.ReadAllText(manifest);
                    var programa = JsonSerializer.Deserialize<Programa>(jsonManifest);
                    if (programa.AppKeyName != null && programa.AppKeyName == AppKeyName)
                    {
                        var stream = new FileStream(packageZip, FileMode.Open, FileAccess.Read);
                        return File(stream, "application/zip", "package.zip");
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { mensagem = "Erro interno do servidor", erro = ex.Message });
            }
            return StatusCode(404, new { mensagem = "Programa não encontrado no servidor", AppKeyName = AppKeyName });
        }
    }
}
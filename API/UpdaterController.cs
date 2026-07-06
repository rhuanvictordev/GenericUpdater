using AtualizadorGenerico.Models;
using AtualizadorGenerico.Models.Request;
using AtualizadorGenerico.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;

namespace AtualizadorGenerico.ApiControllers
{
    [ApiController]
    //[Route("CheckVersion")]

    // com as anotacoes acima, o swagger irá documentar essa rota
    // a rota desse controller sera definida assim: dominio:porta/nomeController (sem o nome controller)
    // exemplo: "ClienteController.cs"
    // rota base:  "dominio:porta/Cliente"
    // a rota base servirá para mapear a model Cliente e a assinatura dela será determinada pelos métodos (IActionResults) com a assinatura [HttpGet], [HttpPost] ... acima deles]:

    public class UpdaterController : ControllerBase
    {
        private readonly string pasta = Path.Combine(AppContext.BaseDirectory, "Programas");
        private readonly ProgramaRepository programaRepository = new ProgramaRepository();

        [HttpPost("CheckVersion")]
        public IActionResult EntregarVersaoAtual([FromBody] GetVersionRequest req)
        {
            try
            {
                string[] subpastas = Directory.GetDirectories(pasta);
                foreach (var subpasta in subpastas)
                {
                    string manifest = Path.Combine(subpasta, "manifest.json");
                    string jsonManifest = System.IO.File.ReadAllText(manifest);
                    var programa = JsonSerializer.Deserialize<Programa>(jsonManifest);
                    if (programa != null && programa.AppKeyName != null && programa.AppKeyName != "" && programa.Version != null && programa.Version != "")
                    {
                        if (programa.AppKeyName == req.AppKeyName)
                        {
                            return Ok(programa.Version);
                        }
                    }
                    else
                    {
                        return StatusCode(404, new { mensagem = "Programa não encontrado" });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { mensagem = "Erro interno do servidor", erro = ex.Message });
            }

            return StatusCode(404, new { mensagem = "Programa não encontrado" });
        }




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



        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile arquivo, [FromBody] UploadViewModel model)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");


            var pasta = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            Directory.CreateDirectory(pasta);

            var caminho = Path.Combine(pasta, arquivo.FileName);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return Ok("Arquivo salvo.");
        }
    }
}

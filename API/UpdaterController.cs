using AtualizadorGenerico.Models;
using AtualizadorGenerico.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;

namespace AtualizadorGenerico.ApiControllers
{
    [ApiController]
    [Route("CheckVersion")]
    
    // com as anotacoes acima, o swagger irá documentar essa rota
    // a rota desse controller sera definida assim: dominio:porta/nomeController (sem o nome controller)
    // exemplo: "ClienteController.cs"
    // rota base:  "dominio:porta/Cliente"
    // a rota base servirá para mapear a model Cliente e a assinatura dela será determinada pelos métodos (IActionResults) com a assinatura [HttpGet], [HttpPost] ... acima deles]:

    public class UpdaterController : ControllerBase
    {
        private readonly string pasta = Path.Combine(AppContext.BaseDirectory, "Programas");

        [HttpPost]
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
                    if (programa.AppKeyName != null && programa.AppKeyName == "")
                    {
                        return Ok(programa.Version);
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

            return StatusCode(404, new { mensagem = "Programa não encontrado" });
        }

    }
}

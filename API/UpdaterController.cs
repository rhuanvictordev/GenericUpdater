using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AtualizadorGenerico.Models.Request;

namespace AtualizadorGenerico.ApiControllers
{
    [ApiController]
    [Route("/[controller]")]
    
    // com as anotacoes acima, o swagger irá documentar essa rota
    // a rota desse controller sera definida assim: dominio:porta/nomeController (sem o nome controller)
    // exemplo: "ClienteController.cs"
    // rota base:  "dominio:porta/Cliente"
    // a rota base servirá para mapear a model Cliente e a assinatura dela será determinada pelos métodos (IActionResults) com a assinatura [HttpGet], [HttpPost] ... acima deles]:

    public class UpdaterController : ControllerBase
    {
        [HttpGet]
        // endpoint:  "/Updater"
        public IActionResult Get()
        {
            List<string> lista = new List<string>{"A","B","C"};
            return Ok(lista);
        }

        // endpoint:  "/Updater/lista2"
        [HttpGet("lista2")]
        public IActionResult Get2()
        {
            List<string> lista = new List<string> {"D","E","F"};
            return Ok(lista);
        }

        // endpoint:  "/Updater"
        [HttpPost]
        public IActionResult EntregarVersaoAtual([FromBody] GetVersionRequest req)
        {
            List<string> lista = new List<string> { "D", "E", "F" };
            return Ok(req.AppKeyName);
        }

        [HttpGet("Download")]
        public IActionResult Download() {
            return Ok();
        }


    }
}

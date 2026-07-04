using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AtualizadorGenerico.Models.Request;
using AtualizadorGenerico.Models.Request.TestPost;
using AtualizadorGenerico.Models.Request.TestPut;

namespace AtualizadorGenerico.ApiControllers
{
    /*
     
    [ApiController]
    [Route("/[controller]")]
    
    // com as anotacoes acima, o swagger irá documentar essa rota
    // a rota desse controller sera definida assim: dominio:porta/nomeController (sem o nome controller)
    // exemplo: "ClienteController.cs"
    // rota base:  "dominio:porta/Cliente"
    // a rota base servirá para mapear a model Cliente e a assinatura dela será determinada pelos métodos (IActionResults) com a assinatura [HttpGet], [HttpPost] ... acima deles]:

    public class TestController : ControllerBase
    {
        [Route("/[controller]")]

        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(200,"Get sucesso!");
        }




        // melhor retorno:
        //
        // return StatusCode(codigoStatus, json);
        // exemplo:  return StatusCode(201, new { mensagem = "Cliente salvo com sucesso", codigoCliente = "0001" });
        //

        [HttpGet("Status/{key}")]
        public IActionResult GetModel(int key)
        {
            switch (key)
            {
                case 200:
                    return StatusCode(200, "Okay :)");
                case 201:
                    return StatusCode(201, "Criado :)");
                case 404:
                    return StatusCode(404, "Not Found :)");
                case 500:
                    return StatusCode(500, new { mensagem = "Erro interno.", codigo = "Testee", codigoString = "TesteString" });
                default:
                    return Ok();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] TestPostModel request)
        {
            var response = "Post recebido com sucesso!";
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TestPutModel request)
        {
            List<string> lista = new List<string> {"D","E","F"};
            return Ok(lista);
        }

        // endpoint:  "/Test/lista2"
        [HttpPatch]
        public IActionResult Patch()
        {
            List<string> lista = new List<string> { "D", "E", "F" };
            return Ok(lista);
        }

        *//*// endpoint:  "/Test/lista2"
        [HttpDelete("lista2")]
        public IActionResult Delete()
        {
            List<string> lista = new List<string> { "D", "E", "F" };
            return Ok(lista);
        }*//*

        

        [HttpGet("Download")]
        public IActionResult Download() {
            return Ok();
        }


    }
    
    */
}

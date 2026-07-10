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



        [RequestFormLimits(MultipartBodyLengthLimit = 1024L * 1024 * 1024)] // limitando a 1gb
        [RequestSizeLimit(1024L * 1024 * 1024)] // limitando a 1gb
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] UploadViewModel? model)
        {
            if (model.Arquivo == null || model.Arquivo.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            Programa programaOld = programaRepository.CarregarPrograma(model.Programa.AppKeyName);
            bool fileBackupSaved = false;

            try
            {
                var raizPrograma = Path.Combine(AppContext.BaseDirectory, "Programas", model.Programa.AppName);
                var packageOld = Path.Combine(AppContext.BaseDirectory, "Programas", model.Programa.AppName, "package.zip");
                var pastaProgramaOld = Path.Combine(AppContext.BaseDirectory, "Programas", model.Programa.AppName, "old_versions");

                //if (System.IO.Directory.Exists(pastaProgramaOld)){}
                Directory.CreateDirectory(pastaProgramaOld);

                var packageBackup = Path.Combine(pastaProgramaOld, $"{programaOld.Version}_backup.zip");
                System.IO.File.Copy(packageOld, packageBackup, true);
                fileBackupSaved = true;

            }
            catch (FileNotFoundException ex)
            {
                //return StatusCode(500, "Arquivo package.zip não localizado no servidor. Insira manualmente um package.zip antes de tentar atualizar!");
                fileBackupSaved = false;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            var pasta = Path.Combine(AppContext.BaseDirectory, "Programas", model.Programa.AppName);
            var caminho = Path.Combine(pasta, "package.zip");

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await model.Arquivo.CopyToAsync(stream);
            }

            programaRepository.AtualizarManifest(model.Programa.AppKeyName, programaOld.ObterNovaVersao());

            if (fileBackupSaved)
            {
                return Ok("Programa atualizado e o backup da versão anterior foi salvo.");
            }
            else
            {
                return Ok("Programa atualizado, backup não localizado");
            }
        }



        [RequestFormLimits(MultipartBodyLengthLimit = 1024L * 1024 * 1024)] // limitando a 1gb
        [RequestSizeLimit(1024L * 1024 * 1024)] // limitando a 1gb
        [HttpPost("UploadNewProgram")]
        public async Task<IActionResult> UploadNewProgram([FromForm] UploadViewModel model)
        {
            if (model.Arquivo == null || model.Arquivo.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            Programa programa = programaRepository.CarregarPrograma(model.Programa.AppKeyName);
            if (programa != null)
            {
                return BadRequest("Nome Chave já existe em outro programa.");
            }

            try
            {
                var raizPrograma = Path.Combine(AppContext.BaseDirectory, "Programas", model.Programa.AppName);
                Directory.CreateDirectory(raizPrograma);

                string novoManifest = Path.Combine(raizPrograma, "manifest.json");

                var obj = new { AppKeyName = model.Programa.AppKeyName.Replace(" ","").Trim(), Version = model.Programa.Version };

                string novoJson = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(novoManifest, novoJson);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


            var pasta = Path.Combine(AppContext.BaseDirectory, "Programas", model.Programa.AppName);
            var caminho = Path.Combine(pasta, "package.zip");

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await model.Arquivo.CopyToAsync(stream);
            }

            return Ok("Programa salvo");
        }
    }










}

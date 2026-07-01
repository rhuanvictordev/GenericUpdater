using AtualizadorGenerico.Models;
using System.Diagnostics;
using System.Text.Json;

namespace AtualizadorGenerico.Service
{
    public abstract class UpdaterService
    {
        string pasta = Path.Combine(AppContext.BaseDirectory, "Programas");

        public void getVersion(string AppKeyName)
        {
            var subpastas = Directory.GetDirectories(pasta);

            foreach (var subpasta in subpastas)
            {
                if(!subpasta.Equals(AppKeyName)) continue;

                string caminhoManifest = Path.Combine(subpasta, "manifest.json");
                if (File.Exists(caminhoManifest))
                {
                    /*string jsonManifest = File.ReadAllText(caminhoManifest);
                    try
                    {
                        var programa = JsonSerializer.Deserialize<Programa>(jsonManifest);
                        if (programa != null && programa.AppKeyName != null && programa.Version != null)
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
                    }*/
                }
            }
        }
    }
}

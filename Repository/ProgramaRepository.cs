using AtualizadorGenerico.Models;
using System.Text.Json;

namespace AtualizadorGenerico.Repository
{
    public class ProgramaRepository : IProgramaRepository
    {
        public Programa CarregarPrograma(string appKeyName)
        {
            string[] subpastas = Directory.GetDirectories(Path.Combine(AppContext.BaseDirectory, "Programas"));
            foreach (var subpasta in subpastas)
            {
                string manifest = Path.Combine(subpasta, "manifest.json");
                if (System.IO.File.Exists(manifest))
                {
                    string jsonManifest = System.IO.File.ReadAllText(manifest);
                    var programa = JsonSerializer.Deserialize<Programa>(jsonManifest);
                    if (programa != null && programa.AppKeyName == appKeyName)
                    {
                        programa.AppName = Path.GetFileName(subpasta);
                        return programa;
                    }
                }
            }
            return null;
        }
    }
}

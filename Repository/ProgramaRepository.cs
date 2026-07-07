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

        public bool AtualizarManifest(string appKeyName, string novaVersao)
        {
            string[] subpastas = Directory.GetDirectories(Path.Combine(AppContext.BaseDirectory, "Programas"));
            foreach (var subpasta in subpastas)
            {
                string manifest = Path.Combine(subpasta, "manifest.json");
                if (System.IO.File.Exists(manifest))
                {
                    string jsonManifest = System.IO.File.ReadAllText(manifest);
                    var programa = JsonSerializer.Deserialize<Programa>(jsonManifest);
                    if (programa.AppKeyName == appKeyName)
                    {
                        programa.Version = novaVersao;
                        string novoJson = JsonSerializer.Serialize(programa, new JsonSerializerOptions { WriteIndented = true });
                        System.IO.File.WriteAllText(manifest, novoJson);
                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return false;
        }
    }
}

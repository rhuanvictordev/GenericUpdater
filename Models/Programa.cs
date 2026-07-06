using System.IO;
using System.Text.Json;

namespace AtualizadorGenerico.Models
{
    public class Programa
    {
        public string AppName { get; set; }
        public string AppKeyName { get; set; }
        public string Version { get; set; }

        public Programa() { }

        public Programa(string appName, string appKeyName, string version)
        {
            AppName = appName;
            AppKeyName = appKeyName;
            Version = version;
        }

        public string ObterNovaVersao()
        {
            string hoje = DateTime.Now.ToString("dd.MM.yyyy");

            if (!Version.StartsWith(hoje))
            {
                return hoje;
            }

            if (!Version.Contains('-'))
            {
                return $"{hoje}-2";
            }

            string[] partes = Version.Split('-');
            int numero = int.Parse(partes[1]);

            return $"{hoje}-{numero+1}";
        }

    }
}

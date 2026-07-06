using System.IO;
using System.Text.Json;

namespace AtualizadorGenerico.Models
{
    public class Programa
    {
        public string? AppName { get; set; }
        public string AppKeyName { get; set; }
        public string Version { get; set; }

        public Programa() { }

        public Programa(string appName, string appKeyName, string version)
        {
            AppName = appName;
            AppKeyName = appKeyName;
            Version = version;
        }

        public string obterNovaVersao()
        {
            try
            {
                var versaoDouble = double.Parse(this.Version);
                return (versaoDouble + 1).ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}

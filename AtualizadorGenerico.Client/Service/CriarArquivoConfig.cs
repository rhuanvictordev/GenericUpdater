using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Updater.Model;

namespace Updater.Service
{
    public abstract class CriarArquivoConfig
    {
        public static bool CriarArquivo()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
                if (!File.Exists(path))
                {
                    var programa = new Programa("nenhum", "nenhum", "nenhum", "nenhum", "nenhum");
                    string programaJson = JsonSerializer.Serialize(programa, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(path, programaJson);
                }
                return true;
            }
            catch (Exception ex)

            {
                throw new Exception("Erro ao criar o arquivo config.json");
            }
        }

        public static bool SalvarConfig(Programa programa)
        {
            try
            {
                string arquivoConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
                string programaJson = JsonSerializer.Serialize(programa, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(arquivoConfig, programaJson);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar o arquivo config.json");
            }
        }
    }
}

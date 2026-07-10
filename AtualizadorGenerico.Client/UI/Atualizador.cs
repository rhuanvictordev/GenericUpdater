using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Updater.Model;
using Updater.Service;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Updater
{
    public partial class Atualizador : Form
    {
        private readonly HttpClient _httpClient;
        private Programa _programa;
        private string _raizAplicacao = AppDomain.CurrentDomain.BaseDirectory;
        private string novaVersao = "";

        public Atualizador()
        {
            _httpClient = new HttpClient();
            _programa = new Programa();
            InitializeComponent();
            CarregaInformacoes();
            notificarUI("Consultando Servidor", "Verificando Atualização");
        }


        public void CarregaInformacoes()
        {
            try
            {
                string arquivoConfigJson = Path.Combine(_raizAplicacao, "config.json");

                if (!File.Exists(arquivoConfigJson))
                {
                    throw new Exception("Arquivo de configuração não encontrado");
                }
                var conteudo = File.ReadAllText(arquivoConfigJson);
                var programa = JsonConvert.DeserializeObject<Programa>(conteudo);
                if (programa != null)
                {
                    _programa = programa;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro", ex.Message);
                Application.Exit();
            }
        }


        public void notificarUI(string logMessage, string infoMessage)
        {
            lblLog.Text = !logMessage.Equals(String.Empty) ? $"{logMessage}..." : lblLog.Text;
            lblInfo.Text = !infoMessage.Equals(String.Empty) ? infoMessage : lblInfo.Text;
        }


        private async void Atualizador_Load(object sender, EventArgs e)
        {
            lblNomeSistema.Text = $"{_programa.AppName}   [ Atualizador ]";
            string versaoRecebida = await VerificarVersaoAtual(_programa.BaseApiURL, _programa.AppKeyName);

            if (versaoRecebida == null) 
            {
                IniciarAplicacao();
                return;
            }

            if (versaoRecebida != _programa.AppVersion)
            {
                novaVersao = versaoRecebida;
                var choice = MessageBox.Show("Atualizar agora?", $"{_programa.AppName} - [Atualização Disponível]", MessageBoxButtons.OKCancel);
                if (choice == DialogResult.OK)
                {
                    notificarUI("", "Baixando Atualização");
                    progressBar1.Visible = true;
                    await BaixarArquivo();
                    ExtrairArquivo();
                }
                else
                {
                    IniciarAplicacao();
                }
            }
            else {
                IniciarAplicacao();
            }
        }



        private async Task BaixarArquivo()
        {
            string pastaTemp = Path.Combine(_raizAplicacao, "temp");
            Directory.CreateDirectory(pastaTemp);
            string caminhoPacote = Path.Combine(pastaTemp, "package.zip");

            using (HttpResponseMessage response = await _httpClient.GetAsync($"{_programa.BaseApiURL}/Download/{_programa.AppKeyName}", HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                var totalBytes = response.Content.Headers.ContentLength;
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var fileStream = new FileStream(caminhoPacote, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        byte[] buffer = new byte[65536];
                        long totalRead = 0;
                        int bytesRead;
                        int lastProgress = 0;

                        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead);
                            totalRead += bytesRead;

                            if (totalBytes.HasValue)
                            {
                                int progress = (int)((totalRead * 100) / totalBytes.Value);
                                if (progress != lastProgress)
                                {
                                    notificarUI($"Baixando Arquivos {progress}%", "");
                                    progressBar1.Value = progress;
                                    lastProgress = progress;
                                }
                            }
                        }

                        progressBar1.Value = 100;
                    }
                }
            }

        }


        private void ExtrairArquivo()
        {
            notificarUI("Extraindo", "Instalando...");

            try
            {
                string arquivoZip = Path.Combine(_raizAplicacao, "temp", "package.zip");
                if (!File.Exists(arquivoZip))
                {
                    throw new Exception("Arquivo Compactado não encontrado");
                }
                else
                {
                    using (ZipArchive archive = ZipFile.OpenRead(arquivoZip))
                    {
                        foreach (var entry in archive.Entries)
                        {
                            string destino = Path.Combine(_raizAplicacao, entry.FullName);

                            string pasta = Path.GetDirectoryName(destino);
                            if (!Directory.Exists(pasta))
                                Directory.CreateDirectory(pasta);

                            if (!string.IsNullOrEmpty(entry.Name))
                            {
                                if (File.Exists(destino))
                                    File.Delete(destino);

                                entry.ExtractToFile(destino);
                            }
                        }
                    }

                    notificarUI("Extração Completa", "Finalizando");
                    _programa.AppVersion = novaVersao;
                    bool salvo = CriarArquivoConfig.SalvarConfig(_programa);
                    if (!salvo)
                    {
                        MessageBox.Show("Ocorreu um erro ao escrever a nova versão", "Informação");
                    }
                    IniciarAplicacao();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocorreu um erro");
                Application.Exit();
            }
        }


        private void IniciarAplicacao()
        {
            string arquivoExecutavel = Path.Combine(_raizAplicacao, _programa.ExeName);

            if (File.Exists(arquivoExecutavel))
            {
                Process.Start(new ProcessStartInfo{ FileName = arquivoExecutavel, UseShellExecute = true});
            }

            Application.Exit();
        }






        private async Task<string> VerificarVersaoAtual(string baseUrl, string appKeyName)
        {
            try
            {
                var obj = new { appKeyName = appKeyName };
                string json = JsonConvert.SerializeObject(obj);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{baseUrl}/CheckVersion", content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

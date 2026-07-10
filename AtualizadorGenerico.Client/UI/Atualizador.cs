using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using Updater.Model;

namespace Updater
{
    public partial class Atualizador : Form
    {
        private readonly HttpClient _httpClient;
        private Programa _programa;

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
                string raizAplicacao = AppContext.BaseDirectory;
                string arquivoConfigJson = Path.Combine(raizAplicacao, "config.json");

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

            MessageBox.Show(versaoRecebida);
        }

        private async Task<string> VerificarVersaoAtual(string baseUrl, string appKeyName)
        {
            try
            {
                Object requestBody = new { appKeyName = appKeyName };
                string json = JsonConvert.SerializeObject(requestBody);

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{baseUrl}/CheckVersion", content);

                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                return "Erro do catch";
            }
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

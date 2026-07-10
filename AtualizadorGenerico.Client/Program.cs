using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Updater.Model;
using Updater.Service;

namespace Updater
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CriarArquivoConfig.CriarArquivo();
            Application.Run(new Atualizador());
        }
    }
}

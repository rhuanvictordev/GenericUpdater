using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.Model
{
    public class Programa
    {
        public string AppName { get; set; }
        public string AppKeyName { get; set; }
        public string AppVersion { get; set; }
        public string BaseApiURL { get; set; }
        public string ExeName { get; set; }

        public Programa() { }

        public Programa(string appName, string appKeyName, string appVersion, string baseApiURL, string exeName)
        {
            AppName = appName;
            AppKeyName = appKeyName;
            AppVersion = appVersion;
            BaseApiURL = baseApiURL;
            ExeName = exeName;
        }
    }
}

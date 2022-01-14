using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class Settings
    {
        public static dbSettings DatabaseLogin { get; set; }

        static Settings()
        {
            DatabaseLogin = new dbSettings();
        }
    }

    public class dbSettings
    {
        public string ServerLocation { get; set; }
        public string dbUser { get; set; }
        public string dbPassword { get; set; }
        public string DatabaseName { get; set; }
    }
}

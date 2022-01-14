using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Core.Data;
using Microsoft.Extensions.Configuration;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.LoadAppSettings();
        }

        /// <summary>
        /// Loads the correct appsettings.json file and adds it to the <see cref="Core.Data.Settings"/> class
        /// </summary>
        private void LoadAppSettings()
        {
            // To load the the appsettings.json file we need the following to NuGet packages
            //<PackageReference Include="Microsoft.Extensions.Configuration.Binder" Version="6.0.0" />
            //<PackageReference Include = "Microsoft.Extensions.Configuration.Json" Version = "6.0.0" />

            // Load the appsettings.json file (this is the first file we will try and load)
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");


            // Now try and overrite the appsettings.json with eaither appsettings.Development.json or 
            // appsettings.Production.json (depdending on which development we are under, debug or release)
#if DEBUG
            builder.AddJsonFile("appsettings.Development.json");
#else
            builder.AddJsonFile("appsettings.Production.json");
#endif

            // run the build so we can get the correct appsettings.json file.
            IConfiguration config = builder.Build();

            // load the settings from appsettings.json into the Core.Data.Settings.DatabaseLogin
            Settings.DatabaseLogin = config.GetSection("Database").Get<dbSettings>();
            
        }

    }
}

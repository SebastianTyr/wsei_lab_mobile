using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AirMonitor.Views;
using AirMonitor.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace AirMonitor
{
    /// <summary>
    /// Aplikacja "AirMonitor" - stworzona w ramach laboratorium "Programowanie aplikacji mobilnych"
    /// </summary>
    public partial class App : Application
    {
        public static string AirlyApiKey { get; private set; }
        public static string AirlyApiUrl { get; private set; }
        public static string AirlyApiMeasurementUrl { get; private set; }
        public static string AirlyApiInstallationUrl { get; private set; }

        public static string PathDB;

        private DatabaseHelper _dBhelper;

        public App(string dbPath)
        {
            PathDB = dbPath;

            InitializeComponent();

            InitializeApp();
        }

        private void InitializeApp()
        {
            LoadConfig();

            _dBhelper = new DatabaseHelper();

            MainPage = new RootTabbedPage();
        }

        private static void LoadConfig()
        {
            var assembly = Assembly.GetAssembly(typeof(App));
            var resourceNames = assembly.GetManifestResourceNames();
            var configName = resourceNames.FirstOrDefault(s => s.Contains("config.json"));

            using (var stream = assembly.GetManifestResourceStream(configName))
            {
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    var dynamicJson = JObject.Parse(json);

                    AirlyApiKey = dynamicJson["AirlyApiKey"].Value<string>();
                    AirlyApiUrl = dynamicJson["AirlyApiUrl"].Value<string>();
                    AirlyApiMeasurementUrl = dynamicJson["AirlyApiMeasurementUrl"].Value<string>();
                    AirlyApiInstallationUrl = dynamicJson["AirlyApiInstallationUrl"].Value<string>();
                }
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            _dBhelper.Dispose();
        }

        protected override void OnResume()
        {
            _dBhelper.Connect();
        }
    }
}
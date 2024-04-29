using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Automatic9045.AtsEx.UpdateChecker.Data
{
    [XmlRoot]
    public class Config
    {
        public static readonly string UrlRoot = "https://automatic9045.github.io/AtsEx.Plugins.UpdateChecker";

        private static readonly string ConfigLocation;

        public static Config Instance { get; }

        static Config()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            ConfigLocation = Path.Combine(Path.GetDirectoryName(assemblyLocation), Path.GetFileNameWithoutExtension(assemblyLocation) + ".Config.xml");
            Instance = ConfigSerializer.Deserialize(ConfigLocation, true);
        }

        public static void Save()
        {
            Instance.Serialize(ConfigLocation);
        }


        public string Version = "0.0";
        public string DownloadUrl = UrlRoot + "/";
        public long StopNotifyUntil = 0;

        public Source Source = new Source();
    }
}

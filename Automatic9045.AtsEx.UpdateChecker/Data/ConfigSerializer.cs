using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Automatic9045.AtsEx.UpdateChecker.Data
{
    internal static class ConfigSerializer
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Config));

        public static Config Deserialize(string path, bool generateIfNotExists)
        {
            if (!File.Exists(path) && generateIfNotExists)
            {
                Config empty = new Config();
                empty.Serialize(path);
            }

            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                return (Config)Serializer.Deserialize(sr);
            }
        }

        public static void Serialize(this Config config, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                Serializer.Serialize(sw, config);
            }
        }
    }
}

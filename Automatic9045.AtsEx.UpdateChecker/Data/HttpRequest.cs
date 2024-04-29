using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Automatic9045.AtsEx.UpdateChecker.Data
{
    [XmlRoot]
    public class HttpRequest : SourceBase
    {
        [XmlAttribute]
        public string VersionUrl = Config.UrlRoot + "/version.txt";

        [XmlAttribute]
        public string UpdateInfoUrl = Config.UrlRoot + "/update-info.md";
    }
}

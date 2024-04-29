using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Automatic9045.AtsEx.UpdateChecker.Data
{
    [XmlRoot]
    public class GitHubRepository : SourceBase
    {
        [XmlAttribute]
        public string Owner;

        [XmlAttribute]
        public string Name;
    }
}

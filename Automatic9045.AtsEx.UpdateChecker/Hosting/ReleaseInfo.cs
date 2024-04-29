using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatic9045.AtsEx.UpdateChecker.Hosting
{
    internal class ReleaseInfo
    {
        public Version Version { get; }
        public string UpdateInfoUrl { get; }

        public ReleaseInfo(Version version, string updateInfoUrl)
        {
            Version = version;
            UpdateInfoUrl = updateInfoUrl;
        }
    }
}

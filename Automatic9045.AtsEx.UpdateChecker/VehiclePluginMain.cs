using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AtsEx.PluginHost.Plugins;

namespace Automatic9045.AtsEx.UpdateChecker
{
    [Plugin(PluginType.VehiclePlugin)]
    internal class VehiclePluginMain : AssemblyPluginBase
    {
        public VehiclePluginMain(PluginBuilder builder) : base(builder)
        {
            Hosting.UpdateChecker.CheckUpdates();
        }

        public override void Dispose()
        {
        }

        public override TickResult Tick(TimeSpan elapsed)
        {
            return new VehiclePluginTickResult();
        }
    }
}

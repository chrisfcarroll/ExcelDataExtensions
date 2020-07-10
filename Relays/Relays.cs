using System;
using Microsoft.Extensions.Logging;

namespace Relays
{
    /// <summary>A component which … </summary>
    public class Relays
    {
        public void Do()
        {
            log.LogDebug("Called on {@OS}", Environment.OSVersion);
        }

        public Relays(ILogger log, Settings settings)
        {
            this.log = log;
            this.settings = settings;
            log.LogDebug("Created with Settings={@Settings}", settings);
        }
        readonly ILogger log;
        readonly Settings settings;
    }
}
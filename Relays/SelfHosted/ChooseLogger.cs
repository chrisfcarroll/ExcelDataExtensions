using Microsoft.Extensions.Logging;

namespace Relays.SelfHosted
{
    static class ChooseLogger
    {
        public static ILoggerFactory Choose()
        {
            return UseSerilog.GetFactory();
        }
    }
}
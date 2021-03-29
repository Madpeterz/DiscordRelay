using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordRelay.Discord
{
    public abstract class DiscordFunctions : DiscordValues
    {
        public bool exit()
        {
            if (targetserver == 0)
            {
                Console.WriteLine("No target server configed - forcing exit");
                return true;
            }
            if (targetchannel == 0)
            {
                Console.WriteLine("No target channel configed - forcing exit");
                return true;
            }
            if (logintoken.Length == 0)
            {
                Console.WriteLine("No login token - forcing exit");
                return true;
            }
            if (master == 0)
            {
                Console.WriteLine("No master id - forcing exit");
                return true;
            }
            if(relays.Count == 0)
            {
                Console.WriteLine("No relays configed - forcing exit");
                return true;
            }
            return forceExit;
        }
        protected long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }
    }
}

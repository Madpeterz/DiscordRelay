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
                return true;
            }
            if (targetchannel == 0)
            {
                return true;
            }
            if (logintoken.Length == 0)
            {
                return true;
            }
            if (master == 0)
            {
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

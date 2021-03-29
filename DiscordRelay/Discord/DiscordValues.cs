using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Timers;

namespace DiscordRelay.Discord
{
    public abstract class DiscordValues
    {
        protected DiscordSocketClient DiscordClient;
        protected bool DiscordClientConnected;
        protected IGuild DiscordServer;
        protected long DiscordUnixTimeOnine;
        protected bool haveMainServer;
        protected ulong targetserver = 0;
        protected ulong targetchannel = 0;
        protected ulong master = 0;
        protected string logintoken = "";
        protected Timer ReconnectTimer;
        protected bool forceExit;


        protected Dictionary<ulong, ulong> relays = new Dictionary<ulong, ulong>();

        public void setToken(string token)
        {
            Console.WriteLine("Info: Setting token");
            logintoken = token;
        }
        public void setTargetServer(ulong serverid, ulong channelid)
        {
            Console.WriteLine("Info: Setting target server");
            targetserver = serverid;
            targetchannel = channelid;
        }
        public void setMaster(ulong masterid)
        {
            Console.WriteLine("Info: Setting master");
            master = masterid;
        }
    }
}

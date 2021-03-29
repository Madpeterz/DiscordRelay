using DiscordRelay.Discord;
using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Info: startup");
            DiscordBot mybot = new DiscordBot();
            mybot.setToken(Environment.GetEnvironmentVariable("Discord_Token"));
            if(ulong.TryParse(Environment.GetEnvironmentVariable("Discord_Target_server"),out ulong targetserverid) == true)
            {
                if (ulong.TryParse(Environment.GetEnvironmentVariable("Discord_Target_channel"), out ulong targetchannelid) == true)
                {
                    mybot.setTargetServer(targetserverid, targetchannelid);
                }
            }
            if (ulong.TryParse(Environment.GetEnvironmentVariable("Discord_Master"), out ulong targetmasterid) == true)
            {
                mybot.setMaster(targetmasterid);
            }
            int loop = 1;
            bool exit = false;
            while(exit == false)
            {
                exit = true;
                if (ulong.TryParse(Environment.GetEnvironmentVariable("Relay_"+loop.ToString()+"_server"), out ulong relayserverid) == true)
                {
                    if (ulong.TryParse(Environment.GetEnvironmentVariable("Relay_" + loop.ToString() + "_channel"), out ulong relaychannelid) == true)
                    {
                        mybot.addRelayServer(relayserverid, relaychannelid);
                        exit = false;
                    }
                }
                loop++;
            }
            mybot.StartRelay();
            while(mybot.exit() == false)
            {
                Thread.Sleep(5000);
            }
            Console.WriteLine("Info: shutting down");
        }
    }
}

using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Timers;

namespace DiscordRelay.Discord
{
    public class DiscordBot: DiscordInbound
    {
        public async void StartRelay()
        {
            Console.WriteLine("Status: StartRelay");
            if (exit() == true)
            {
                Console.WriteLine("Exiting - bad config");
                return;
            }
            await StartDiscordClientService().ConfigureAwait(false); 
        }


        protected async Task StartDiscordClientService()
        {
            if (DiscordClient != null)
            {
                DiscordClient.Dispose();
            }
            Console.WriteLine("Status: Starting discord service");
            DiscordClient = new DiscordSocketClient();
            DiscordClient.Ready += DiscordClientReady;
            DiscordClient.LoggedOut += DiscordClientLoggedOut;
            DiscordClient.LoggedIn += DiscordClientLoggedIn;
            ReconnectTimer = new Timer();
            ReconnectTimer.Interval = 10000;
            ReconnectTimer.Elapsed += ReconnectTimerEvent;
            ReconnectTimer.AutoReset = false;
            ReconnectTimer.Enabled = false;
            await DiscordClient.LoginAsync(TokenType.Bot, logintoken);
        }

        protected Task DiscordClientLoggedIn()
        {
            Console.WriteLine("Status: logged into discord");
            ReconnectTimer.Enabled = false;
            _ = DiscordClient.StartAsync();
            return Task.CompletedTask;
        }
        
        protected Task DiscordClientLoggedOut()
        {
            Console.WriteLine("Status: logged out of discord - reconnecting in 5 secs");
            DiscordClientConnected = false;
            ReconnectTimer = new Timer();
            ReconnectTimer.Interval = 5000;
            ReconnectTimer.Elapsed += ReconnectTimerEvent;
            ReconnectTimer.AutoReset = false;
            ReconnectTimer.Enabled = false;
            return Task.CompletedTask;
        }

        private void ReconnectTimerEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            ReconnectTimer.Enabled = false;
            _ = StartDiscordClientService().ConfigureAwait(false);
        }

        protected async Task<Task> DiscordClientReady()
        {
            Console.WriteLine("Status: Discord client ready");
            await DiscordClient.SetStatusAsync(UserStatus.Idle);
            await DiscordClient.SetGameAsync("Finding main server", null, ActivityType.CustomStatus);
            DiscordServer = DiscordClient.GetGuild(targetserver);
            if (DiscordServer != null)
            {
                Console.WriteLine("Status: Have target server - opening listener");
                DiscordClient.MessageReceived += DiscordClientMessageReceived;
                haveMainServer = true;
                DiscordUnixTimeOnine = UnixTimeNow();
                await DiscordClient.SetStatusAsync(UserStatus.Online);
                await DiscordClient.SetGameAsync("Ready", null, ActivityType.CustomStatus);
            }
            else
            {
                Console.WriteLine("Status: Unable to find target server");
                await DiscordClient.SetGameAsync("No main server", null, ActivityType.CustomStatus);
                await DiscordClient.SetStatusAsync(UserStatus.DoNotDisturb);
                DiscordClientConnected = false;
                forceExit = true;
            }
            return Task.CompletedTask;
        }
    }
}

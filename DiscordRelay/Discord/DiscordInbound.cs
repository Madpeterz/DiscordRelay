using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace DiscordRelay.Discord
{
    public abstract class DiscordInbound: DiscordHelper
    {
        protected async Task<Task> DiscordClientMessageReceived(SocketMessage message)
        {
            if (message.Author.IsBot == false)
            {
                if (message.Channel.Name.Contains("@") == true)
                {
                    if (message.Author.Id == master)
                    {
                        if (message.Content == "exit")
                        {
                            Console.WriteLine("Status: exit command from master");
                            await message.Author.SendMessageAsync("Exiting now").ConfigureAwait(true);
                            forceExit = true;
                            return Task.CompletedTask;
                        }
                        await message.Author.SendMessageAsync("Accepted commands: exit").ConfigureAwait(true);
                        return Task.CompletedTask;
                    }
                    await message.Author.SendMessageAsync("I dont love you go away").ConfigureAwait(true);
                    return Task.CompletedTask;
                }
                ITextChannel Chan = (ITextChannel)message.Channel;
                IGuildUser user = await Chan.Guild.GetUserAsync(message.Author.Id).ConfigureAwait(true);

                if ((Chan.Id != targetchannel) && (Chan.GuildId != targetserver))
                {
                    if (relays.ContainsKey(Chan.GuildId) == true)
                    {
                        if (relays[Chan.GuildId] == Chan.Id)
                        {
                            string username = user.Nickname;
                            if (username == null)
                            {
                                username = message.Author.Username;
                            }
                            await SendMessageToChannelAsync(targetchannel, targetserver, username + ": " + message.Content).ConfigureAwait(false);
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}

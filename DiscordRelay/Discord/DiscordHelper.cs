using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordRelay.Discord
{
    public abstract class DiscordHelper : DiscordFunctions
    {
        protected async Task<IUserMessage> SendMessageToChannelAsync(ulong channelid, ulong serverid, string message)
        {
            try
            {
                SocketTextChannel Channel = FindTextChannel(channelid, serverid);
                if (Channel != null)
                {
                    return await Channel.SendMessageAsync(message).ConfigureAwait(false);
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        protected SocketTextChannel FindTextChannel(ulong channelid, ulong serverid)
        {
            SocketGuild onserver = DiscordClient.GetGuild(serverid);
            if(onserver == null)
            {
                return null;
            }
            SocketTextChannel channel = onserver.GetTextChannel(channelid);
            return channel;
        }
    }
}

using Discord;
using Discord.WebSocket;

namespace DiscordBot.discord
{
    public static class SocketConfig
    {
        public static DiscordSocketConfig GetDefault() {
            return new DiscordSocketConfig{
                LogLevel = LogSeverity.Verbose
            };
        }
    }
}
using System.Threading.Tasks;
using Discord;

namespace DiscordBot.discord
{
    public class DiscordLogger
    {
        private ILogger _logger;
        public DiscordLogger(ILogger logger) {
            _logger = logger;
        }
        public Task Log(LogMessage message)
        {
            _logger.Log(message.Message);
            return Task.CompletedTask;
        }
    }
}
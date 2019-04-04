using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.discord.handlers
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _service;
        public CommandHandler(DiscordSocketClient client, CommandService service) {
            _client = client;
            _service = service;
        }

        public async Task InitAsync() {
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), null);
        }

        public async Task ClientMessageReceived(SocketMessage msg) {
            var message = msg as SocketUserMessage;
            if(message == null) return;

            var context = new SocketCommandContext(_client, message);
            int argPos = 0;
            if(message.HasStringPrefix("!", ref argPos)
                || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos, null, MultiMatchHandling.Best);
                if(!result.IsSuccess && result.Error != CommandError.UnknownCommand) 
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }

    }
}
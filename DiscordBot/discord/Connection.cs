using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordBot.discord.entities;
using DiscordBot.discord.handlers;

namespace DiscordBot.discord
{
    public class Connection
    {
        private readonly DiscordSocketClient _client;
        private readonly DiscordLogger _logger;
        private readonly CommandHandler _commandhandler;

        public Connection(DiscordLogger logger, DiscordSocketClient client, CommandHandler commandHandler)
        {
            _logger = logger;
            _client = client;
            _commandhandler = commandHandler;
        }
        internal async Task ConnectAsync(TimsBotConfig config)
        {
            await _commandhandler.InitAsync();

            _client.Log += _logger.Log;
            _client.MessageReceived += _commandhandler.ClientMessageReceived;

            await _client.LoginAsync(TokenType.Bot, config.Token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
    }
}
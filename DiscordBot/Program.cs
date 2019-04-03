using System;
using System.Threading.Tasks;
using DiscordBot.discord;
using DiscordBot.discord.entities;
using DiscordBot.strorage;

namespace DiscordBot
{
    internal class Program
    {
        private static async Task Main()
        {
            Unity.RegisterTypes();
            Console.WriteLine("Hello Discord!");

            var storage = Unity.Resolve<IDataStorage>();

            var connection = Unity.Resolve<Connection>();
            await connection.ConnectAsync(new TimsBotConfig
            {
                Token = storage.RestoreObject<string>("config/Token")
            });
        }
    }
}

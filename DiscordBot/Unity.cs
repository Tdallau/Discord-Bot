using Discord.WebSocket;
using DiscordBot.discord;
using DiscordBot.strorage;
using DiscordBot.strorage.implementations;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;

namespace DiscordBot
{
    public static class Unity
    {
        private static UnityContainer _container;

        public static UnityContainer Container
        {
            get
            {
                if (_container == null)
                    RegisterTypes();
                return _container;
            }
        }

        public static void RegisterTypes()
        {
            _container = new UnityContainer();
            _container.RegisterSingleton<IDataStorage, JsonStorage>();
            _container.RegisterSingleton<ILogger, Logger>();
            _container.RegisterFactory<DiscordSocketConfig>(_ => SocketConfig.GetDefault());
            _container.RegisterSingleton<DiscordSocketClient>(new InjectionConstructor(typeof(DiscordSocketConfig)));
            _container.RegisterSingleton<discord.Connection>();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
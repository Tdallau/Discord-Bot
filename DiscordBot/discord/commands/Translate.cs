using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBot.strorage;
using Newtonsoft.Json;

namespace DiscordBot.discord.commands
{
    public class Translate : ModuleBase<SocketCommandContext>
    {
        private IAzureService _AzureService = Unity.Resolve<IAzureService>();

        [Command("translate"), Alias("t"), Summary("Translate some text")]
        public async Task TranslateSentence(string lang, [Remainder]string sentence) {
            var translatedObject = _AzureService.Translate(sentence, lang);
            if(translatedObject == null) {
                var msgSend = await Context.Channel.SendMessageAsync("Something has gone wrong, probably the country code does not exist. Within a minute, this message will be deleted");
                Thread.Sleep(60000);
                await Context.Channel.DeleteMessageAsync(msgSend);
                return;
            }
            var msg = await Context.Channel.GetMessageAsync(Context.Message.Id);
            await  Context.Channel.DeleteMessageAsync(msg);

            var embed = new EmbedBuilder 
                {
                     Description = translatedObject[0].translations[0].text
                };
            embed.WithAuthor(Context.User.Username);
            embed.WithColor(Color.DarkBlue);
            await Context.Channel.SendMessageAsync("",false, embed.Build());
            

        }
        
    }
}
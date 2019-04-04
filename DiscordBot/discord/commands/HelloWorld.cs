using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordBot.discord.commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Summary("Prints Hello World")]
        public async Task Hello() {
            await Context.Channel.SendMessageAsync("Hello World!");
        }

        [Command("plus"), Alias("sum"), Summary("Gives outcome of sum")]
        public async Task Plus(int num1 = 0, [Remainder]int num2 = 0) {

            if(num1 == 0 || num2 == 0) {
                await Context.Channel.SendMessageAsync($"num1 of num2 is niet gevonden Type: !plus ***<num1>*** ***<num2>***");
                return;
            }

            await Context.Channel.SendMessageAsync($" {num1} + {num2} = {num1 + num2}");
        }
    }
}
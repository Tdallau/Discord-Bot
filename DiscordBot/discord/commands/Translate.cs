using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace DiscordBot.discord.commands
{
    public class Translate : ModuleBase<SocketCommandContext>
    {
        [Command("translate"), Summary("Translate some text")]
        public async Task TranslateSentence(string sentence, [Remainder]string lang) {

            string host = "https://api.cognitive.microsofttranslator.com";
            string route = $"/translate?api-version=3.0&to={lang}";
            string subscriptionKey = "63d7d4bd26014c72a63d180abc0216a4";

            var msg = await Context.Channel.GetMessageAsync(Context.Message.Id);
            await  Context.Channel.DeleteMessageAsync(msg);

            System.Object[] body = new System.Object[] { new { Text = sentence } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Set the method to POST
                request.Method = HttpMethod.Post;
                // Construct the full URI
                request.RequestUri = new Uri(host + route);
                // Add the serialized JSON object to your request
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                // Add the authorization header
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                // Send request, get response
                var response = client.SendAsync(request).Result;
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                TranslateResponse[] obj = JsonConvert.DeserializeObject<TranslateResponse[]>(jsonResponse);
                // Print the response
                var embed = new EmbedBuilder 
                {
                     Description = obj[0].translations[0].text
                };
                embed.WithAuthor(Context.User.Username);
                embed.WithColor(Color.DarkBlue);
                await Context.Channel.SendMessageAsync("",false, embed.Build());
            }

        }
        
    }
}
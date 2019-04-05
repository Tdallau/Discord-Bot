using System;
using System.Net.Http;
using System.Text;
using DiscordBot.strorage;
using Newtonsoft.Json;

namespace DiscordBot
{
    public class AzureService : IAzureService
    {
        public TranslateResponse[] Translate(string txt, string lang)
        {
            var storage = Unity.Resolve<IDataStorage>();

            string host = "https://api.cognitive.microsofttranslator.com";
            string route = $"/translate?api-version=3.0&to={lang}";
            string subscriptionKey = storage.RestoreObject<string>("config/AzureKey");

            System.Object[] body = new System.Object[] { new { Text = txt } };
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

                try
                {
                    TranslateResponse[] obj = JsonConvert.DeserializeObject<TranslateResponse[]>(jsonResponse);
                    return obj;
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }
    }
}
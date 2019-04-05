namespace DiscordBot
{
    public interface IAzureService
    {
        TranslateResponse[] Translate(string txt, string lang);
    }
}
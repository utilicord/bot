using Discord;
using Discord.WebSocket;
using UtiliCord.Config;

namespace UtiliCord
{
    public class Core
    {
        private const string Version = "canary";
        private static DiscordSocketClient Client { get; set; }

        public static async void Start()
        {
            Client = new DiscordSocketClient();
            Client.Log += Log;

            await Client.LoginAsync(TokenType.Bot, BotConfig.ClientConfig.Token);
            await Client.StartAsync();
        }

        private static Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
    }
}

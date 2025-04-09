using Discord;
using Discord.WebSocket;
using Serilog;
using UtiliCord.Config;

namespace UtiliCord
{
    public class Core
    {
        private const string Version = "canary";
        private static DiscordSocketClient Client { get; set; }
        private static DateTime StartupTime { get; set; }

        public static async void Start()
        {
            StartupTime = DateTime.Now;
            Log.Information("Starting in {Mode} mode...", Logging.IsDebugMode ? "DEBUG" : "RELEASE");

            // Initialize Discord.Net
            Client = new DiscordSocketClient();
            Client.Log += Logging.ProcessDNetLog;

            // Connect to Discord
            Log.Debug("Connecting to Discord...");

            if (BotConfig.ClientConfig.Token == "" || BotConfig.ClientConfig.Token == null)
            {
                Log.Error("No bot token provided. Aborting startup...");
                return;
            }

            await Client.LoginAsync(TokenType.Bot, BotConfig.ClientConfig.Token);
            await Client.StartAsync();
        }
    }
}

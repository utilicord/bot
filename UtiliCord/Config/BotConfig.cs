using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtiliCord.Config
{
    public class BotConfig
    {
        private const string configFolder = "data";
        private const string configFile = "config.json";
        public static readonly Config ClientConfig;

        static BotConfig()
        {
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }

            if (!File.Exists(configFolder + "/" + configFile))
            {
                ClientConfig = new Config();
                string json = JsonConvert.SerializeObject(ClientConfig, Formatting.Indented);
                File.WriteAllText(configFolder + "/" + configFile, json);
            }
            else
            {
                string json = File.ReadAllText(configFolder + "/" + configFile);
                ClientConfig = JsonConvert.DeserializeObject<Config>(json);
            }
        }

        public struct Config
        {
            [JsonProperty("token")]
            public string Token { get; set; }
        }
    }
}

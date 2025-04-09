namespace UtiliCord
{
    public class Program
    {
        static void Main(string[] args)
            => MainAsync(args).GetAwaiter().GetResult();

        public static async Task MainAsync(string[] _)
        {
            Logging.Configure();
            Core.Start();

            await Task.Delay(-1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Sir_Bearington
{
    public class Program
    {
        private DiscordSocketClient _client;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
        
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            string token = "MzU4OTc4ODc2MjcwOTY4ODMy.DKAdeA.lt4ajGzEIEx7TKD68ZUUETT8-Ps"; //Security risk! Keep this private!
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            //block this task until the program is closed
            await Task.Delay(-1);

        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}

using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using System.IO;

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

            _client.MessageReceived += MessageReceived;

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

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content == "Ping!") //test command
            {
                await message.Channel.SendMessageAsync("Pong!");
            }

            if (message.Content.StartsWith("/sb repeat "))
            {
                string command = message.Content.Substring(11);

                if (!command.StartsWith("/"))
                {
                    //this fix prevents bearington from responding to nested commands
                    //feels a bit hacky and I'd rather test user is not a bot in initial if
                    //so I guess looking into that is a ToDo
                    await message.Channel.SendMessageAsync("RARGH!");
                    await message.Channel.SendMessageAsync("'Yes, Sir Bearington. They *did* say, \"" + command + "\".'");
                }                
            }

            if (message.Content.StartsWith("/sb search "))
            {
                string command = message.Content.Substring(11);

                WebRequest testRequest = WebRequest.Create("https://roll20.net/compendium/dnd5e/searchbook/?terms=" + command);

                if (testRequest.GetResponse().ResponseUri.ToString() != "https://roll20.net/compendium/dnd5e/searchbook/?terms=" + command)
                {
                    Console.WriteLine(SearchRoll20(testRequest, command));
                    await message.Channel.SendMessageAsync(testRequest.GetResponse().ResponseUri.ToString());                    
                }
                else
                {
                    await message.Channel.SendMessageAsync("RARGH!");
                    await message.Channel.SendMessageAsync("'I'm sorry, friends, but Sir Bearington can't find any information on \"" + command + "\".'");
                }

                
            }
        }

        private string SearchRoll20(WebRequest request, string command)
        {
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string fullHTML = reader.ReadToEnd();
            string responseURI = response.ResponseUri.ToString();         
            string header = responseURI.Substring(responseURI.IndexOf("#h-") + 3);
            int index = fullHTML.IndexOf(">" + header);
            string finalString = "error";
            Console.WriteLine("Made it to SearchRoll20!");

            if (index >= 0)
            {
                Console.WriteLine("First IF");
                string begin = fullHTML.Substring(index);
                if (begin != null)
                {
                    Console.WriteLine("Second IF");
                    int newIndex = begin.IndexOf(">");
                    int finalIndex = begin.IndexOf("<");
                    if (newIndex >= 0)
                    {
                        Console.WriteLine("Final IF");
                        finalString = begin.Substring(newIndex + 1, finalIndex);
                        Console.WriteLine(finalString);
                    }
                }
            }

            return finalString;
        }

    }
}

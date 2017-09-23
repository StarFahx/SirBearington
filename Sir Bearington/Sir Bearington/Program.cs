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

            string token = File.ReadAllText(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/BearingtonToken.txt");
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

        private async Task<string> MessageReceived(SocketMessage message)
        {
            string returnedString = "";
            Console.WriteLine("New Message Received");

            if (message.Content == "Ping!") //test command
            {
                Discord.Rest.RestUserMessage sentMessage = await message.Channel.SendMessageAsync("Pong!").ConfigureAwait(false);                
                returnedString = MessageProcess(message, sentMessage);
            }

            if (message.Content.StartsWith("/sb repeat "))
            {
                string command = message.Content.Substring(11);

                Discord.Rest.RestUserMessage sentMessage = await message.Channel.SendMessageAsync("RARGH!" + Environment.NewLine + "'Yes, Sir Bearington. They *did* say, \"" + command + "\".'").ConfigureAwait(false);
                Console.WriteLine("...");
                returnedString = MessageProcess(message, sentMessage);
            }

            if (message.Content.StartsWith("I'm ")) //dad jokes
            {
                string command = message.Content.Substring(4);

                Discord.Rest.RestUserMessage sentMessage = await message.Channel.SendMessageAsync("Hello " + command + ", I'm Sir Bearington.").ConfigureAwait(false);
                returnedString = MessageProcess(message, sentMessage);
            }

            if (message.Content.StartsWith("/sb search "))//maybe refactor this
            {
                string command = message.Content.Substring(11);

                WebRequest testRequest = WebRequest.Create("https://roll20.net/compendium/dnd5e/searchbook/?terms=" + command);

                if (testRequest.GetResponse().ResponseUri.ToString() != "https://roll20.net/compendium/dnd5e/searchbook/?terms=" + command)//has the search worked? this doesn't account for users including %20s in their text!!!
                {
                    string response = SearchRoll20(testRequest, command);
                    if (response != "error")
                    {
                        Discord.Rest.RestUserMessage sentMessage = await message.Channel.SendMessageAsync("```" + response + "```").ConfigureAwait(false);
                        returnedString = MessageProcess(message, sentMessage);
                    }
                    else
                    {
                        Discord.Rest.RestUserMessage sentMessage = await message.Channel.SendMessageAsync(testRequest.GetResponse().ResponseUri.ToString().Replace(" ", "%20")).ConfigureAwait(false);
                        returnedString = MessageProcess(message, sentMessage);
                    }
                }
                else
                {                    
                    Discord.Rest.RestUserMessage sentMessage = await message.Channel.SendMessageAsync("RARGH!" + Environment.NewLine + "'I'm sorry, friends, but Sir Bearington can't find any information on \"" + command + "\".'").ConfigureAwait(false);
                    returnedString = MessageProcess(message, sentMessage);
                }


            }

            if (returnedString != "")
            {
                Console.WriteLine("We said: " + returnedString);
            }
            else
            {
                Console.WriteLine("Other message received.");
            }            
            return returnedString;
        }

        private string MessageProcess(SocketMessage message, Discord.Rest.RestUserMessage sentMessage)
        {
            Console.WriteLine("They said: " + message.Content);
            Discord.Rest.RestUserMessage newMessage = sentMessage;
            return newMessage.Content;
        }

        private string SearchRoll20(WebRequest request, string command)//returns the data from Roll20, if in correct format. There are lots of formats, though, so this can be improved. Also refactoring.
        {
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string fullHTML = reader.ReadToEnd();
            string responseURI = response.ResponseUri.ToString();         
            string header = responseURI.Substring(responseURI.IndexOf("#h-") + 3);
            int index = fullHTML.IndexOf(">" + header + "</");
            string titleString = "error";
            string headingTag;

            if (index >= 0)
            {
                string reverse = new string(fullHTML.Substring(0, index).ToCharArray().Reverse().ToArray());
                int reverseIndex = reverse.Length - reverse.IndexOf("/<");
                string headingTagLong = fullHTML.Substring(index - reverseIndex);

                if (headingTagLong.IndexOf(" ") < headingTagLong.IndexOf(">"))
                {
                    headingTag = "</" + headingTagLong.Substring(0, headingTagLong.IndexOf(" ")) + ">";
                }
                else
                {
                    int headingTagIndex = headingTagLong.IndexOf(">");
                    headingTag = "</" + headingTagLong.Substring(0, headingTagIndex + 1);
                }

                
                int newIndex = fullHTML.IndexOf(header + headingTag);
                Console.WriteLine(headingTag);

                if (newIndex >= 0)
                {
                    string contentStart = fullHTML.Substring(newIndex + headingTag.Length + header.Length + 1);

                    int endIndex = contentStart.IndexOf("<");

                    string content = contentStart.Substring(0, endIndex);

                    titleString = content;
                }              
            }

            return titleString;
        }

    }
}

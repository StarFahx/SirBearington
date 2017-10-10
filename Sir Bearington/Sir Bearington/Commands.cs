using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Net.Http;
using System.Net;
using System.IO;

namespace Sir_Bearington
{
    public class ExampleUngrouppedCommands
    {
        [Command("ping")] // let's define this method as a command
        [Description("Example ping command")] // this will be displayed to tell users what this command does when they invoke help
        [Aliases("pong")] // alternative names for the command
        public async Task Ping(CommandContext ctx) // this command takes no arguments
        {
            // let's trigger a typing indicator to let
            // users know we're working
            await ctx.TriggerTypingAsync();

            // let's make the message a bit more colourful
            var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:");

            // respond with ping
            await ctx.RespondAsync($"{emoji} Pong! Ping: {ctx.Client.Ping}ms");
        }

        [Command("greet"), Description("Says hi to specified user."), Aliases("sayhi", "say_hi")]
        public async Task Greet(CommandContext ctx, [Description("The user to say hi to.")] DiscordMember member) // this command takes a member as an argument; you can pass one by username, nickname, id, or mention
        {
            // note the [Description] attribute on the argument.
            // this will appear when people invoke help for the
            // command.

            // let's trigger a typing indicator to let
            // users know we're working
            await ctx.TriggerTypingAsync();

            // let's make the message a bit more colourful
            var emoji = DiscordEmoji.FromName(ctx.Client, ":wave:");

            // and finally, let's respond and greet the user.
            await ctx.RespondAsync($"{emoji} Hello, {member.Mention}!");
        }

        [Command("insult"), Description("Says mean things to specified user."), Aliases("hurl_abuse_at", "roast"), Hidden]
        public async Task Insult(CommandContext ctx, [Description("The user to burn.")] DiscordMember member)
        {           
            //await ctx.TriggerTypingAsync();

            // let's make the message a bit more colourful
            var emoji = DiscordEmoji.FromName(ctx.Client, ":fire:");

            // let's give them a random burn
            var rnd = new Random();
            var nxtAdj = rnd.Next(0, 14);
            var nxtVerb = rnd.Next(0, 14);
            var nxtCon = rnd.Next(0, 14);
            string adjective = "";
            string verb = "";
            string consequence = "";

            Console.WriteLine(nxtAdj.ToString() + " " + nxtVerb.ToString() + " " + nxtCon.ToString());

            switch (nxtAdj)
            {
                case 0:
                    adjective = "fat";
                    break;
                case 1:
                    adjective = "orange";
                    break;
                case 2:
                    adjective = "carrot-like";
                    break;
                case 3:
                    adjective = "grotesque";
                    break;
                case 4:
                    adjective = "carrot-like";
                    break;
                case 5:
                    adjective = "attracted to Hitler";
                    break;
                case 6:
                    adjective = "dumb";
                    break;
                case 7:
                    adjective = "insulted by this joke";
                    break;
                case 8:
                    adjective = "lulrandom";
                    break;
                case 9:
                    adjective = "aroused";
                    break;
                case 10:
                    adjective = "devilish";
                    break;
                case 11:
                    adjective = "danube";
                    break;
                case 12:
                    adjective = "frightening";
                    break;
                case 13:
                    adjective = "much like a fish";
                    break;
                case 14:
                    adjective = "broken";
                    break;
            }

            Console.WriteLine(adjective);

            switch(nxtVerb)
            {
                case 0:
                    verb = "wakes up";
                break;
                case 1:
                    verb = "plays video games";
                break;
                case 2:
                    verb = "masturbates";
                break;
                case 3:
                    verb = "break-dances";
                break;
                case 4:
                    verb = "dies";
                break;
                case 5:
                    verb = "sleeps with Hitler";
                break;
                case 6:
                    verb = "slaps me (oh yeah baby hit me harder)";
                break;
                case 7:
                    verb = "eats salmon";
                break;
                case 8:
                    verb = "walks";
                break;
                case 9:
                    verb = "sleeps";
                break;
                case 10:
                    verb = "tries to climb Mt. Everest";
                break;
                case 11:
                    verb = "danubes";
                break;
                case 12:
                    verb = "spooks children";
                break;
                case 13:
                    verb = "explodes";
                break;
                case 14:
                    verb = "goes on a murderous rampage";
                break;
            }

            Console.WriteLine(verb);

            switch (nxtCon)
            {
                case 0:
                    consequence = "the world cries tears of joy";
                    break;
                case 1:
                    consequence = "I can't help but scream";
                    break;
                case 2:
                    consequence = "the polar ice cap melts";
                    break;
                case 3:
                    consequence = "there's an earthquake in Scotland";
                    break;
                case 4:
                    consequence = $"{member.Mention} wishes Fox never programmed this insult generator";
                    break;
                case 5:
                    consequence = "she sleeps with Hitler";
                    break;
                case 6:
                    consequence = "a single tear falls from my eye";
                    break;
                case 7:
                    consequence = "somewhere in Africa, a child cries for the last time as the sun set slowly upon the horizon. When she wakes, the world will have forever changed. It will not change for the better";
                    break;
                case 8:
                    consequence = "C'thulhu rises";
                    break;
                case 9:
                    consequence = "Adam Wingard makes a Death Note sequel";
                    break;
                case 10:
                    consequence = "she falls over";
                    break;
                case 11:
                    consequence = "the Danube danubes danubely";
                    break;
                case 12:
                    consequence = "I cri evrytim";
                    break;
                case 13:
                    consequence = "an angel dies";
                    break;
                case 14:
                    consequence = "ABBA hold a concert";
                    break;
            }

            Console.WriteLine(consequence);

            // and finally, let's respond and greet the user.
            await ctx.RespondAsync($"{emoji} {member.Mention}'s mum is so {adjective}, when she {verb} {consequence}!");
        }

        [Command("sum"), Description("Sums all given numbers and returns said sum.")]
        public async Task SumOfNumbers(CommandContext ctx, [Description("Integers to sum.")] params int[] args)
        {
            // note the params on the argument. It will indicate
            // that the command will capture all the remaining arguments
            // into a single array

            // let's trigger a typing indicator to let
            // users know we're working
            await ctx.TriggerTypingAsync();

            // calculate the sum
            var sum = args.Sum();

            // and send it to the user
            await ctx.RespondAsync($"The sum of these numbers is {sum.ToString("#,##0")}");
        }

        // this command will use our custom type, for which have 
        // registered a converter during initialization
        [Command("math"), Description("Does basic math.")]
        public async Task Math(CommandContext ctx, [Description("Operation to perform on the operands.")] MathOperation operation, [Description("First operand.")] double num1, [Description("Second operand.")] double num2)
        {
            var result = 0.0;
            switch (operation)
            {
                case MathOperation.Add:
                    result = num1 + num2;
                    break;

                case MathOperation.Subtract:
                    result = num1 - num2;
                    break;

                case MathOperation.Multiply:
                    result = num1 * num2;
                    break;

                case MathOperation.Divide:
                    result = num1 / num2;
                    break;

                case MathOperation.Modulo:
                    result = num1 % num2;
                    break;
            }

            var emoji = DiscordEmoji.FromName(ctx.Client, ":1234:");
            await ctx.RespondAsync($"{emoji} The result is {result.ToString("#,##0.00")}");
        }
    }

    [Group("admin")] // let's mark this class as a command group
    [Description("Administrative commands.")] // give it a description for help purposes
    [Hidden] // let's hide this from the eyes of curious users
    [RequirePermissions(Permissions.ManageGuild)] // and restrict this to users who have appropriate permissions
    public class ExampleGrouppedCommands
    {
        // all the commands will need to be executed as <prefix>admin <command> <arguments>

        // this command will be only executable by the bot's owner
        [Command("sudo"), Description("Executes a command as another user."), Hidden, RequireOwner]
        public async Task Sudo(CommandContext ctx, [Description("Member to execute as.")] DiscordMember member, [RemainingText, Description("Command text to execute.")] string command)
        {
            // note the [RemainingText] attribute on the argument.
            // it will capture all the text passed to the command

            // let's trigger a typing indicator to let
            // users know we're working
            await ctx.TriggerTypingAsync();

            // get the command service, we need this for
            // sudo purposes
            var cmds = ctx.Client.GetCommandsNext();

            // and perform the sudo
            await cmds.SudoAsync(member, ctx.Channel, command);
        }

        [Command("nick"), Description("Gives someone a new nickname."), RequirePermissions(Permissions.ManageNicknames)]
        public async Task ChangeNickname(CommandContext ctx, [Description("Member to change the nickname for.")] DiscordMember member, [RemainingText, Description("The nickname to give to that user.")] string new_nickname)
        {
            // let's trigger a typing indicator to let
            // users know we're working
            await ctx.TriggerTypingAsync();

            try
            {
                // let's change the nickname, and tell the 
                // audit logs who did it.
                await member.ModifyAsync(new_nickname, reason: $"Changed by {ctx.User.Username} ({ctx.User.Id}).");

                // let's make a simple response.
                var emoji = DiscordEmoji.FromName(ctx.Client, ":+1:");

                // and respond with it.
                await ctx.RespondAsync(emoji.ToString());
            }
            catch (Exception)
            {
                // oh no, something failed, let the invoker now
                var emoji = DiscordEmoji.FromName(ctx.Client, ":-1:");
                await ctx.RespondAsync(emoji.ToString());
            }
        }
    }

    [Group("D&D")] //commands for checking roll20
    [Description("Here is Sir Bearington's main functionality. Other functionality will be stripped when this is complete.")]
    [Aliases("d&d", "DnD", "dnd")]
    public class Roll20ExecutableGroup
    {
        [Command("search"), Aliases("lookup"), Description("Searches the roll20 compendium for the phrase you follow it with.")]
        public async Task Compendium(CommandContext ctx, params string[] searchStringArray)
        {
            await ctx.TriggerTypingAsync();
            string searchString = "";
            string searchStringReplaced = "";
            foreach (string entry in searchStringArray)
            {
                searchString += entry + " ";
            }
            if (searchString != "")
            {
                searchString.Trim();
                searchStringReplaced = searchString.Replace(" ", "%20");
            }

            WebRequest search = WebRequest.Create("https://roll20.net/compendium/dnd5e/searchbook/?terms=" + searchStringReplaced);

            if (search.GetResponse().ResponseUri.ToString() != "https://roll20.net/compendium/dnd5e/searchbook/?terms=" + searchStringReplaced)//has the search worked? this doesn't account for users including %20s in their text!!!
            {
                string response = SearchRoll20(search, searchString);
                if (response != "error")
                {
                    await ctx.RespondAsync("```" + response + "```");
                }
                else
                {
                    await ctx.RespondAsync(search.GetResponse().ResponseUri.ToString().Replace(" ", "%20"));
                }
            }
            else
            {
                await ctx.RespondAsync("RARGH!" + Environment.NewLine + "'I'm sorry, friends, but Sir Bearington can't find any information on \"" + searchString + "\".'");
            }
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

    [Group("memes", CanInvokeWithoutSubcommand = true)] // this makes the class a group, but with a twist; the class now needs an ExecuteGroupAsync method
    [Description("Contains some memes. When invoked without subcommand, returns a random one.")]
    [Aliases("copypasta")]
    public class ExampleExecutableGroup
    {
        // commands in this group need to be executed as 
        // <prefix>memes [command] or <prefix>copypasta [command]

        // this is the group's command; unlike with other commands, 
        // any attributes on this one are ignored, but like other
        // commands, it can take arguments
        public async Task ExecuteGroupAsync(CommandContext ctx)
        {
            // let's give them a random meme
            var rnd = new Random();
            var nxt = rnd.Next(0, 4);

            switch (nxt)
            {
                case 0:
                    await NavySeal(ctx);
                    return;
                case 1:
                    await RickNMorty(ctx);
                    return;
                case 2:
                    await Smartest(ctx);
                    return;
                case 3:
                    await Socialism(ctx);
                    return;
                case 4:
                    await Immortal(ctx);
                    return;
            }
        }
        
        [Command("navyseal"), Aliases("gorillawarfare"), Description("What the fuck did you just say to me?")]
        public async Task NavySeal(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("What the fuck did you just fucking say about me, you little bitch? I’ll have you know I graduated top of my class in the Navy Seals, and I’ve been involved in numerous secret raids on Al-Quaeda, and I have over 300 confirmed kills. I am trained in gorilla warfare and I’m the top sniper in the entire US armed forces. You are nothing to me but just another target. I will wipe you the fuck out with precision the likes of which has never been seen before on this Earth, mark my fucking words. You think you can get away with saying that shit to me over the Internet? Think again, fucker. As we speak I am contacting my secret network of spies across the USA and your IP is being traced right now so you better prepare for the storm, maggot. The storm that wipes out the pathetic little thing you call your life. You’re fucking dead, kid. I can be anywhere, anytime, and I can kill you in over seven hundred ways, and that’s just with my bare hands. Not only am I extensively trained in unarmed combat, but I have access to the entire arsenal of the United States Marine Corps and I will use it to its full extent to wipe your miserable ass off the face of the continent, you little shit. If only you could have known what unholy retribution your little “clever” comment was about to bring down upon you, maybe you would have held your fucking tongue. But you couldn’t, you didn’t, and now you’re paying the price, you goddamn idiot. I will shit fury all over you and you will drown in it. You’re fucking dead, kiddo.");
        }

        [Command("ricknmorty"), Aliases("intellectual"), Description("To be fair...")]
        public async Task RickNMorty(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("To be fair, you have to have a very high IQ to understand Rick and Morty. The humor is extremely subtle, and without a solid grasp of theoretical physics most of the jokes will go over a typical viewer's head. There's also Rick's nihilistic outlook, which is deftly woven into his characterisation - his personal philosophy draws heavily fromNarodnaya Volya literature, for instance. The fans understand this stuff; they have the intellectual capacity to truly appreciate the depths of these jokes, to realize that they're not just funny- they say something deep about LIFE. As a consequence people who dislike Rick and Morty truly ARE idiots- of course they wouldn't appreciate, for instance, the humour in Rick's existencial catchphrase 'Wubba Lubba Dub Dub, ' which itself is a cryptic reference to Turgenev's Russian epic Fathers and Sons I'm smirking right now just imagining one of those addlepated simpletons scratching their heads in confusion as Dan Harmon's genius unfolds itself on their television screens. What fools... how I pity them. And yes by the way, I DO have a Rick and Morty tattoo. And no, you cannot see it. It's for the ladies' eyes only- And even they have to demonstrate that they're within 5 IQ points of my own (preferably lower) beforehand.");
        }

        [Command("smartest"), Description("Very, very smart.")]
        public async Task Smartest(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("You sir are the smartest person here. To be able to weed through all of the opinions and perspectives and still formulate your own is something that may seem simple to you but it really isn't for a lot of people. Not only that but your opinion is so reserved and open to change which is really important in this world - there is no such thing as the truth, only the most widely accepted perspective - nobody knows what is going on in anyone's head for you to articulate all the reasons why makes me happy. Despite all the unknowns I do have a sense that this man deserves more empathy than scrutiny. He also dealt with the media very well regardless of his guilt or lack of it however you choose to see and interpret the evidence we have. I like you man, or woman but I'm feeling man.");
        }

        [Command("socialism"), Aliases("pleasedeleteyouraccount"), Description("Jesus. Socialism is bad.")]
        public async Task Socialism(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("Jesus. Socialism is bad. I hate it. I know you think it works, but it's awful. I can't stand it. It's silly, it's for fannies, and I don't support it. Your ideology, just promotes it. I cannot stand for that. I don't like i at all. It's well designed, but it promotes this idea of economic inequity that is unfair. Taxation is not just theft, it's bullying. I cannot stand by and allow you to propagate the mental illness that is socialism. I implore you, please delete your account, and destroy the computer it was stored on. Please.");
        }

        [Command("immortal"), Aliases("himynameis"), Description("Truly this is a work of literature.")]
        public async Task Immortal(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("Hi my name is Ebony Dark'ness Dementia Raven Way and I have long ebony black hair (that's how I got my name) with purple streaks and red tips that reaches my mid-back and icy blue eyes like limpid tears and a lot of people tell me I look like Amy Lee (AN: if u don't know who she is get da hell out of here!). I'm not related to Gerard Way but I wish I was because he's a major fucking hottie. I'm a vampire but my teeth are straight and white. I have pale white skin. I'm also a witch, and I go to a magic school called Hogwarts in England where I'm in the seventh year (I'm seventeen). I'm a goth (in case you couldn't tell) and I wear mostly black. I love Hot Topic and I buy all my clothes from there. For example today I was wearing a black corset with matching lace around it and a black leather miniskirt, pink fishnets and black combat boots. I was wearing black lipstick, white foundation, black eyeliner and red eye shadow. I was walking outside Hogwarts. It was snowing and raining so there was no sun, which I was very happy about. A lot of preps stared at me. I put up my middle finger at them.");
        }

        // this is a subgroup; you can nest groups as much 
        // as you like
        [Group("mememan", CanInvokeWithoutSubcommand = true), Hidden]
        public class MemeMan
        {
            public async Task ExecuteGroupAsync(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();

                // wrap it into an embed
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Meme man",
                    ImageUrl = "http://i.imgur.com/tEmKtNt.png"
                };
                await ctx.RespondAsync("", embed: embed);
            }

            [Command("ukip"), Description("The UKIP pledge.")]
            public async Task Ukip(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();

                // wrap it into an embed
                var embed = new DiscordEmbedBuilder
                {
                    Title = "UKIP pledge",
                    ImageUrl = "http://i.imgur.com/ql76fCQ.png"
                };
                await ctx.RespondAsync("", embed: embed);
            }

            [Command("lineofsight"), Description("Line of sight.")]
            public async Task LOS(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();

                // wrap it into an embed
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Line of sight",
                    ImageUrl = "http://i.imgur.com/ZuCUnEb.png"
                };
                await ctx.RespondAsync("", embed: embed);
            }

            [Command("art"), Description("Art.")]
            public async Task Art(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();

                // wrap it into an embed
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Art",
                    ImageUrl = "http://i.imgur.com/VkmmmQd.png"
                };
                await ctx.RespondAsync("", embed: embed);
            }

            [Command("seeameme"), Description("When you see a meme.")]
            public async Task SeeMeme(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();

                // wrap it into an embed
                var embed = new DiscordEmbedBuilder
                {
                    Title = "When you see a meme",
                    ImageUrl = "http://i.imgur.com/8GD0hbZ.jpg"
                };
                await ctx.RespondAsync("", embed: embed);
            }

            [Command("thisis"), Description("This is meme man.")]
            public async Task ThisIs(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();

                // wrap it into an embed
                var embed = new DiscordEmbedBuilder
                {
                    Title = "This is meme man",
                    ImageUrl = "http://i.imgur.com/57vDOe6.png"
                };
                await ctx.RespondAsync("", embed: embed);
            }

            [Command("deepdream"), Description("Deepdream'd meme man.")]
            public async Task DeepDream(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();

                // wrap it into an embed
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Deep dream",
                    ImageUrl = "http://i.imgur.com/U666J6x.png"
                };
                await ctx.RespondAsync("", embed: embed);
            }

            [Command("sword"), Description("Meme with a sword?")]
            public async Task Sword(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();

                // wrap it into an embed
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Meme with a sword?",
                    ImageUrl = "http://i.imgur.com/T3FMXdu.png"
                };
                await ctx.RespondAsync("", embed: embed);
            }

            [Command("christmas"), Description("Beneath the christmas spike...")]
            public async Task ChristmasSpike(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();

                // wrap it into an embed
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Christmas spike",
                    ImageUrl = "http://i.imgur.com/uXIqUS7.png"
                };
                await ctx.RespondAsync("", embed: embed);
            }
        }
    }
}
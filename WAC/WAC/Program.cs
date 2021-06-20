using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;
using DiscordRPC.Logging;

namespace WAC
{
    class Program
    {
        static void Main(string[] args)
        {
            DiscordRpcClient client = new DiscordRpcClient("815958403259695104");
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            client.Initialize();


            client.SetPresence(new RichPresence()
            {
                Details = "Join the Legion",
                State = "Be a Warrior",
                Assets = new Assets
                {
                    LargeImageKey = "large",
                    LargeImageText = "discord.gg/N49Gxsu",
                    SmallImageKey = "small",
                    SmallImageText = "WAC"
                },
                Buttons = new Button[] {
                    new Button
                    {
                        Label = "Join us ⚔️",
                        Url = "https://discord.gg/N49Gxsu"
                    }
                }
            });

            Console.Write("Allez précher la bonne parole ! WAC WAC WAC !");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using Plugins;

namespace JunkBot
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient irc;
            NetworkStream stream;
            StreamReader reader;
            StreamWriter writer;

            try
            {
                irc = new TcpClient("chat.freenode.net", 6667);
                stream = irc.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                writer.WriteLine("NICK JunkBot");
                writer.Flush();
                writer.WriteLine("USER JunkBot 8 * :CSBot");
                writer.Flush();
                writer.WriteLine("JOIN ##DiCrew");
                writer.Flush();

                while (true)
                {
                    string input = reader.ReadLine();
                    IPlugin plugin;

                    while ((input) != null)
                    {
                        Console.WriteLine(input);

                        string[] ex = input.Split(' ');

                        if (ex[0] == "PING")
                        {
                            writer.WriteLine("PONG"+" "+ex[1]);
                        }

                        if (ex[0].ToLower().StartsWith("locate")&&ex.Length<3)
                        {
                            string nick = input.Substring(input.IndexOf(" "));

                            try
                            {
                                plugin = new Locator();
                                plugin.OnMessage();
                            }
                            catch
                            {
                                Console.WriteLine(nick + " could not be located.");
                            }
                        }
                        else if (input.ToLower().Contains("joke"))
                        {
                            plugin = new Jokes();
                            string joke = plugin.OnMessage();
                            writer.WriteLine(joke);
                        }
                    }
                }

                //writer.Close() says "unsearchable code" is detected.
                //need to look into what code it is referring to.
                writer.Close();
                reader.Close();
                irc.Close();
            }
            catch
            {

            }
        }
    }
}

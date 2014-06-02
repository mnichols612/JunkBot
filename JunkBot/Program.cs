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
            try
            {
                TcpClient irc;
                NetworkStream stream;
                StreamReader reader;
                StreamWriter writer;

                irc = new TcpClient("chat.freenode.net", 6667);
                stream = irc.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                writer.WriteLine("USER JunkBot 8 * :C#Bot");
                writer.Flush();
                writer.WriteLine("NICK JunkBot");
                writer.Flush();
                writer.WriteLine("JOIN ##DiCrew");
                writer.Flush();

                while (true)
                {
                    string input;
                    IPlugin plugin;

                    while ((input = reader.ReadLine()) != null)
                    {
                        string[] ex = input.Split(' ');

                        if (ex[0] == "PING")
                        {
                            writer.WriteLine("PONG"+" "+ex[1]);
                        }

                        if (ex[0].ToLower().StartsWith("locate"))
                        {
                            string nick = input.Substring(input.IndexOf(" "));

                            try
                            {
                                //Look into how to locate a user's city and state by IP address or a simpler way if there is one.
                            }
                            catch
                            {
                                Console.WriteLine(nick + " could not be located.");
                            }
                        }
                        else if (input.ToLower().Contains("joke"))
                        {
                            plugin = new Jokes();
                            writer.WriteLine(plugin.OnMessage());
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

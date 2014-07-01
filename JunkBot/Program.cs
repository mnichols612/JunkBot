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

                string input = "", output = "";

                string[] ex = input.Split(' ');

                irc = new TcpClient("chat.freenode.net", 6667);
                stream = irc.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);

                writer.WriteLine("NICK JunkBot");
                writer.Flush();

                writer.WriteLine("USER JunkBot 8 * :CSBot");
                writer.Flush();

                input = reader.ReadLine();

                if (ex[0] == "PING")
                {
                    Console.WriteLine("Received: " + input);
                    output = "PONG " + ex[1];
                    writer.WriteLine(output);
                    Console.WriteLine("Sent: " + output);
                    writer.Flush();
                }

                writer.WriteLine("JOIN ##DiCrew");
                writer.Flush();

                while (input != null)
                {
                    IPlugin plugin;

                    if ((input = reader.ReadLine()) != null)
                    {
                        ex = input.Split(' ');
                        Console.WriteLine("Received: " + input);

                        if (ex[0] == "PING")
                        {
                            output = "PONG " + ex[1];
                            writer.WriteLine(output);
                            writer.Flush();
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
                                output = nick + " could not be located.";
                                writer.WriteLine(output);
                            }
                        }
                        else if (input.ToLower().Contains("joke"))
                        {
                            plugin = new Jokes();
                            output = "PRIVMSG ##DiCrew :" + plugin.OnMessage();
                            writer.WriteLine(output);
                            writer.Flush();
                        }

                        Console.WriteLine("Sent: " + output);
                        output = "";
                    }
                }

                //writer.Close() says "unsearchable code" is detected.
                //need to look into what code it is referring to.
                writer.Close();
                reader.Close();
                irc.Close();
                Console.ReadLine();
            }
            catch
            {

            }
        }
    }
}

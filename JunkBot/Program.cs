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

                string input = reader.ReadLine(), output = "";

                while (input!=null)
                {
                    IPlugin plugin;

                    if ((input) != null)
                    {
                        Console.WriteLine("Received: " + input);

                        string[] ex = input.Split(' ');

                        if (ex[0] == "PING")
                        {
                            output = "PONG" + " " + ex[1];
                            writer.WriteLine(output);
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
                            output = plugin.OnMessage();
                            writer.WriteLine(output);
                        }

                        Console.WriteLine("Sent: " + output);
                        input = null;
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

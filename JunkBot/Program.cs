using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BusinessLayer;
using EntitiesLayer;

namespace JunkBot
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BLL.Connect();

                while (true)
                {
                    string input;

                    while ((input = BLL.GetIrcReader().ReadLine()) != null)
                    {
                        if (input.ToLower().StartsWith("locate "))
                        {
                            string nick = input.Substring(input.IndexOf(" "));

                            try
                            {
                                Console.WriteLine(nick + " is located at " + BLL.Locate(nick));
                            }
                            catch
                            {
                                Console.WriteLine(nick + " could not be located.");
                            }
                        }
                        else if (input.ToLower().Contains("joke"))
                        {
                            Console.WriteLine(BLL.TellAJoke());
                        }
                    }
                }

                //I don't think it will pose a problem as it accesses it through a method in BLL, but there is a warning here about "unreachable code"
                BLL.IrcDisconnect();
            }
            catch
            {

            }
        }
    }
}

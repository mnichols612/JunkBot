using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using EntitiesLayer;

namespace JunkBot
{
    class Program
    {
        static void Main(string[] args)
        {
            BLL.Connect();

            while (true)
            {
                string input = Console.ReadLine();
                
                if (input.ToLower().StartsWith("locate "))
                {
                    string nick = input.Substring(input.IndexOf(" "));

                    try
                    {
                        Console.WriteLine(nick +" is located at "+BLL.Locate(nick));
                    }
                    catch
                    {
                        Console.WriteLine(nick+" could not be located.");
                    }
                }
                else if (input.ToLower().Contains("joke"))
                {
                    Console.WriteLine(BLL.TellAJoke());
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Plugins
{
    public interface IPlugin
    {
        string Name { get; }

        string OnMessage();
    }

    public class Jokes : IPlugin
    {
        public static FileStream stream;
        public static StreamReader reader;

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public string OnMessage()
        {

            string joke = "";

            int line = 0, lines = 0;
            Random r = new Random();

            stream = new FileStream("C:\\Users\\Michael\\Documents\\Visual Studio 2012\\Projects\\JunkBot\\Documents\\Jokes.txt", FileMode.Open);

            reader = new StreamReader(stream);

            while (reader.ReadLine() != null)
            {
                lines++;
            }

            line = r.Next(0, lines);
            lines = 0;
            stream.Position = 0;
            reader.DiscardBufferedData();

            while (reader.ReadLine() != null)
            {
                lines++;
                if (lines == line)
                {
                    joke = reader.ReadLine();
                    break;
                }
            }

            stream.Close();

            return joke;
        }
    }
}
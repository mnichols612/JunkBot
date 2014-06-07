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
            string file = System.IO.Directory.GetFiles("Documents/Jokes.txt").ToString();

            stream = new FileStream(file, FileMode.Open);

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
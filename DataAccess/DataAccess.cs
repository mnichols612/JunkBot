using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace DataAccess
{
    public interface IConnection
    {
        void Connect(string connectionInformation);

        void Disconnect();
    }

    public class IRC : IConnection
    {
        private TcpClient irc;

        public TcpClient Irc
        {
            get { return irc; }
            set { irc = value; }
        }
        
        private StreamReader reader;

        public StreamReader Reader
        {
            get { return reader; }
            set { reader = value; }
        }

        private StreamWriter writer;

        public StreamWriter Writer
        {
            get { return writer; }
            set { writer = value; }
        }

        private NetworkStream stream;

        public NetworkStream Stream
        {
            get { return stream; }
            set { stream = value; }
        }
        
        public void Connect(string connectionInformation)
        {
                string[] info = connectionInformation.Split(',');
                string host = info[0];
                int port = Convert.ToInt32(info[1]);
                irc = new TcpClient(host, port);
                stream = irc.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                writer.WriteLine("USER JunkBot 8 * :I am a C# irc bot");
                writer.Flush();
                writer.WriteLine("NICK JunkBot");
                writer.Flush();
                writer.WriteLine("JOIN ##DiCrew");
                writer.Flush();
        }

        public void Disconnect()
        {
            writer.Close();
            reader.Close();
            irc.Close();
        }

        public string Locate(string nick)
        {
            throw new NotImplementedException();
        }
    }

    //not entirely sure what I am going to use this for. It is here in case I end up with a need to access a page.
    public class WebPage : IConnection
    {
        public void Connect(string connectionInformation)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }

    //Mostly txt files with separate lines for each entry.
    //txt files may contain lists of jokes, web pages, quotes, etc.
    public class File : IConnection
    {
        FileStream stream;
        StreamReader reader;

        public void Connect(string connectionInformation)
        {
            stream = new FileStream(connectionInformation, FileMode.Open);
        }

        public void Disconnect()
        {
            stream.Close();
        }

        //for some reason, this method always completes but occasionally does not show any text.
        public string GetJoke()
        {
            string joke = "";

            int line = 0, lines = 0;
            Random r = new Random();

            Connect("C:\\Users\\Michael\\Documents\\Visual Studio 2012\\Projects\\JunkBot\\Documents\\Jokes.txt");
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

            Disconnect();

            return joke;
        }
    }
}

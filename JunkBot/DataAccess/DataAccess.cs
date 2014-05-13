using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using EntitiesLayer;

namespace DataAccess
{
    public class IRC : IConnection
    {
        TcpClient irc;
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;

        public IRC()
        { }

        public void Connect(string connectionInformation)
        {
            try
            {
                string[] info = connectionInformation.Split(',');
                string host = info[0];
                int port = Convert.ToInt32(info[1]);
                irc = new TcpClient(host, port);
                stream = irc.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
            }
            catch
            {

            }
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public string Locate(string nick)
        {
            throw new NotImplementedException();
        }

        //Will update EntitiesLayer.Users.userList by adding users who join and removing users who leave
        public List<User> GetUsers(List<User> userList)
        {
            return userList;
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

        //for some reason, this method occasionally completes but does not show any text.
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

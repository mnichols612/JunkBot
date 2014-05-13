using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;
using System.IO;
using DataAccess;

namespace BusinessLayer
{
    public static class BLL
    {
        public static IRC irc = new IRC();
        public static WebPage page = new WebPage();
        public static DataAccess.File file = new DataAccess.File();

        #region File Methods
        public static string TellAJoke()
        {
            string joke;

            joke = file.GetJoke();

            return joke;
        }
        #endregion

        #region IRC Methods
        public static StreamReader GetIrcReader()
        {
            return irc.Reader;
        }

        public static StreamWriter GetIrcWriter()
        {
            return irc.Writer;
        }

        public static void IrcDisconnect()
        {
            irc.Disconnect();
        }

        public static void Connect()
        {
            irc.Connect("chat.freenode.net,6667");
        }

        public static string Locate(string nick)
        {
                string location;
                location = irc.Locate(nick);
                return location;
        }
        #endregion
    }
}

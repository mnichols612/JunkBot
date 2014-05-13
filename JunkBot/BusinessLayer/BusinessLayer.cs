using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;
using DataAccess;

namespace BusinessLayer
{
    public static class BLL
    {
        public static IRC irc = new IRC();
        public static WebPage page = new WebPage();
        public static File file = new File();

        #region File Methods
        public static string TellAJoke()
        {
            string joke;

            joke = file.GetJoke();

            return joke;
        }
        #endregion

        #region IRC Methods

        public static void Connect()
        {
            irc.Connect("##DiCrew,6667");
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

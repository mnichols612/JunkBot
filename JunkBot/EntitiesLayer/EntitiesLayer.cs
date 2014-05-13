using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public interface IConnection
    {
        void Connect(string connectionInformation);

        void Disconnect();
    }

    //Users and User classes will keep from having to open a connection every time
    //the list of online users or location of a user is requested by updating when the user logs in.
    //I am considering further uses for the two classes as well.
    public static class Users
    {
        public static readonly List<User> userList = new List<User>();
    }

    public class User
    {
        public User(string nick, string location)
        {
            Nick = nick;
            Location = location;

            Users.userList.Add(this);
        }

        private string nick;

        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }

        private string location;

        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        
    }
}

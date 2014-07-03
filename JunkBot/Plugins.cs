using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml;
using System.Collections.Specialized;

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
            get { return "Jokes"; }
        }

        public string OnMessage()
        {
            string joke = "", file="";

            try
            {

                int line = 0, lines = 0;
                Random r = new Random();
                file = System.IO.Directory.GetFiles("Documents/Jokes.txt").ToString();

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

                while ((joke = reader.ReadLine()) != null)
                {
                    lines++;
                    if (lines == line)
                    {
                        break;
                    }
                }

                stream.Close();

                return joke;
            }
            catch(Exception e)
            {
                Console.Out.WriteLine(file);
                Console.Out.WriteLine(e.StackTrace);
                return "I'm not feeling funny at the moment.";
            }
        }
    }

    public class Locator : IPlugin
    {
        private string ipAddress;

        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }
        

        public string Name
        {
            get { return "Locator"; }
        }

        public string OnMessage()
        {
            string location = GetCountry(ipAddress);
            return location;
        }

        public string GetCountry(string ipAddress)
        {

            string ipResponse = IPRequestHelper("http://ipinfodb.com/ip_query_country.php?ip=", ipAddress);

            XmlDocument ipInfoXML = new XmlDocument();

            ipInfoXML.LoadXml(ipResponse);

            XmlNodeList responseXML = ipInfoXML.GetElementsByTagName("Response");

            NameValueCollection dataXML = new NameValueCollection();

            dataXML.Add(responseXML.Item(0).ChildNodes[2].InnerText, responseXML.Item(0).ChildNodes[2].Value);

            string xmlValue = dataXML.Keys[0];

            return xmlValue;
        }

        public string IPRequestHelper(string url, string ipAddress)
        {
            string checkURL = url + ipAddress;

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            StreamReader responseStream = new StreamReader(objResponse.GetResponseStream());

            string responseRead = responseStream.ReadToEnd();

            responseStream.Close();
            responseStream.Dispose();

            return responseRead;

        }
    }
}
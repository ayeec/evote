using System;
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pruebaxml
{
    class Program
    {
        static void Main(string[] args)
        {
            /*XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNodeList list= doc.GetElementsByTagName("voto");
            XmlNode nodo=list.Item(0);
            XmlAttributeCollection at= nodo.Attributes;
            nodo=at.GetNamedItem("puesto");
            Console.WriteLine(nodo.Value);*/

            /*XPathDocument path = new XPathDocument("config.xml");
            XPathNavigator nav = path.CreateNavigator();
            nav.MoveToRoot();
            Console.WriteLine(nav.MoveToFirstChild());
            Console.WriteLine(nav.MoveToFirstAttribute());
            Console.WriteLine(nav.Value);*/
            Console.WriteLine(BitConverter.ToInt32(new byte[] { 1, 0, 0, 0 }, 0));
            Console.WriteLine(String.Join(",", BitConverter.GetBytes(1).Select(o => o.ToString()).ToArray()));
        }
    }
}

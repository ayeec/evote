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
            for (int i = 0; i <= 4; i++)
            {
                Console.WriteLine(10 + ((i % 2 == 0) ? i*310 : i + 1 * 310));
                Console.WriteLine((i % 2 == 0) ? 10 : 290);
                Console.WriteLine("\n");
            }
        }
    }
}

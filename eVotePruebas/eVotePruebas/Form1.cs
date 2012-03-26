using System;
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace eVotePruebas
{
    public partial class Form1 : Form
    {

        public static XmlDocument config;
        public XPathDocument xconf;
        public static XmlNodeList list;

        public Form1()
        {
            config = new XmlDocument();
            config.Load("config.xml");
            list=config.GetElementsByTagName("voto");
            //InitializeComponent();
            new Progresivo().Visible=true;

            inicializar();
        }


        void botonclick(object sender, EventArgs e)
        {
            new MainForm((sender as System.Windows.Forms.Button).Text).ShowDialog();
            ((System.Windows.Forms.Button)sender).Enabled = false;


            //MessageBox.Show(sender.ToString().Substring(35));
           
        }

    }
}

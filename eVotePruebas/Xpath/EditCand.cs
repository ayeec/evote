using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace XPath
{
    public partial class EditCand : Form
    {
        public EditCand()
        {
            InitializeComponent();
        }

        const string FILE_NAME = "config.xml";
        XPathDocument doc;
        XPathNavigator nav;
        XPathExpression expr;
        XPathNodeIterator iterator;
        string oldTitle = "";

        private void EditCand_Load(object sender, EventArgs e)
        {
            doc = new XPathDocument(FILE_NAME);
            nav = doc.CreateNavigator();

            // Compile a standard XPath expression

            expr = nav.Compile("/config/voto/@puesto");
            iterator = nav.Select(expr);

            // Iterate on the node set
            comboBox1.Items.Clear();
            try
            {
                while (iterator.MoveNext())
                {
                    XPathNavigator nav2 = iterator.Current.Clone();
                    comboBox1.Items.Add(nav2.Value);
                    comboBox1.SelectedIndex = 0;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            //save old title 
            oldTitle = comboBox1.Text;

        }
    }
}

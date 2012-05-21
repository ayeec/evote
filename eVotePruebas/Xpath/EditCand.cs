using System;
using System.IO;
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

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            country.Text=Path.GetFileName(openFileDialog1.FileName);
            
        }

        //inserta nuevo candidato al puesto
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //XmlTextReader reader = new XmlTextReader(FILE_NAME);
                //reader.ProhibitDtd = false;
                //XmlDocument doc = new XmlDocument();
                //doc.Load(reader);
                //reader.Close();
                //XmlNode currNode;

                //XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                //docFrag.InnerXml = "<candidato partido=\"" + comboBox2.Text + "\"" +" nombre=\""+price.Text+"\n"+
                //    "</candidato>";
                //// insert the availability node into the document 
                //currNode = doc.DocumentElement;
                //currNode.InsertAfter(docFrag, currNode.LastChild);
                ////save the output to a file 
                //doc.Save(FILE_NAME);
                //this.DialogResult = DialogResult.OK;

                XmlTextReader reader = new XmlTextReader(FILE_NAME);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();

                //Select the cd node with the matching title
                XmlNode oldCd;
                XmlElement root = doc.DocumentElement;
                oldCd = root.SelectSingleNode("/config/voto[@puesto=\"" + comboBox2.Text + "\"]");

                XmlNode cand = doc.CreateNode(XmlNodeType.Element, "candidato", null);
                XmlElement el = doc.CreateElement("candidato");
                el.SetAttribute("partido", comboBox2.Text);
                el.SetAttribute("nombre", price.Text);

                oldCd.InsertAfter(el,oldCd.LastChild);
                this.DialogResult = DialogResult.OK;
                doc.Save(FILE_NAME);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());
                this.DialogResult = DialogResult.Cancel;
            } 

        }
    }
}

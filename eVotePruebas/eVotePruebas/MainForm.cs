/*
 * Created by SharpDevelop.
 * User: Alejandro
 * Date: 25/01/2012
 * Time: 05:25 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace eVotePruebas
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
        XmlElement elem;
        XmlNodeList list;

		public MainForm(string voto)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			//InitializeComponent();

            elem = Form1.config.GetElementById(voto);
            list=elem.GetElementsByTagName("candidato");
            inicializar(voto);
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

        bool nobotones()
        {
            bool noescogido=true;
            for (int i = 0; i < botones.Length; i++)
            {
                if (botones[i].Checked)
                {
                    noescogido = false;
                    break;
                }
            }
            return noescogido;
        }

        void Button4Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (nobotones())
            {
                MessageBox.Show("No ha escogido candidato");
                e.Cancel = true;
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nobotones() && textBox1.Text == "")
            {
                MessageBox.Show("No ha escogido candidato");

            }
            else if (nobotones() && !(textBox1.Text == ""))
            {

            }
            else
            {
                this.Close();
            }

        }
	}
}

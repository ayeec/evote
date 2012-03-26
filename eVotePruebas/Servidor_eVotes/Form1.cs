using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;

namespace Servidor_eVotes
{
    public partial class Form1 : Form
    {
        Thread hilo;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            hilo = hilo = new Thread(new ThreadStart(this.mensaje));
            hilo.Start();
        }

        private void txtCasilla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = true;
            
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (txtCasilla.Text != "" && txtClave.Text != "")
            {
                using (TcpClient client = new TcpClient("localhost", 51111))

                using (NetworkStream n = client.GetStream())
                {

                    BinaryWriter w = new BinaryWriter(n);

                    w.Write(txtCasilla.Text+";"+txtClave.Text);

                    w.Flush();

                    MessageBox.Show(new BinaryReader(n).ReadString());

                }
            }
        }

        public void mensaje()
        {
            int timer = 0;
            while (true)
            {
                TcpListener listener = new TcpListener(IPAddress.Any, 51112);

                listener.Start();

                using (TcpClient c = listener.AcceptTcpClient())

                using (NetworkStream n = c.GetStream())
                {

                    string msg = new BinaryReader(n).ReadString();

                    BinaryWriter w = new BinaryWriter(n);
                    lblStatus.Text = msg;
                    w.Write(msg);
                    if(msg!= null &&msg!="")
                    {
                        lblStatus.Text = msg;
                        if (timer < 5)
                            timer++;
                        else
                            timer = 0;
                    }
                    w.Flush();

                }
                if (timer == 5)
                {
                    lblStatus.Text = "";
                }
                Thread.Sleep(4000);
            }

            
        }
    }
}

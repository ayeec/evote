using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace eVotePruebas
{
    public class Bloqueo :Panel
    {
        Label mensaje = new Label();
        Label casilla = new Label();
        //Client cliente = new Client();
        //short id;

        public Bloqueo()
        {
            init();
           
        }


        public Bloqueo(short i):this()
        {
            casilla.Text = i+"";
        }

        private void init()
        {
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;


            //id.ToString();
            casilla.AutoSize = true;
            casilla.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width/100, Screen.PrimaryScreen.Bounds.Height/7);
            casilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 300F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            
            
            mensaje.Text = "La terminal esta bloqueada,\ndebe de ser desbloqueada\npor los funcionarios de casilla";
            mensaje.AutoSize = true;
            mensaje.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width/2 ,Screen.PrimaryScreen.Bounds.Height/2);
            mensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            mensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            //socket
            IPAddress direc = Dns.GetHostEntry("localhost").AddressList[0];
            //IPEndPoint Ep = new IPEndPoint(direccion, puerto);

            this.Controls.Add(casilla);
            this.Controls.Add(mensaje);
            this.Visible = true;
            //hilo.Start();
        }


        public void esperarDesbloqueo()
        {
           
        }
    }
}

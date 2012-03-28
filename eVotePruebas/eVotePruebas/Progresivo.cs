using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Net;
using Community.CsharpSqlite;

namespace eVotePruebas
{
    public partial class Progresivo : Form
    {

        #region Variables globales de la clase
        Thread hilo;
        public static XmlDocument config;
        public XPathDocument xconf;
        public static XmlNodeList list;

        private Bloqueo bloqueo;

        private Panel [] paneles;
        private Button[] botones;
        private Button [] btnCancelar;
        private int ctrPaneles = 0;
        #endregion
        #region CANDIDATOS: Variables de los cadidatos
        private Label [] lblNoListado;
        private Label[] lblTitulo;
        private RadioButton [][] rbtBotones;
        private TextBox [] txtNoListado;

        string id;
        string clave;
#endregion

        public Progresivo()
        {
            config = new XmlDocument();
            config.Load("config.xml");
            list = config.GetElementsByTagName("voto");
            string error = "";
            for (int i = 0; i < list.Count; i++)
            {
                string puesto=list.Item(i).Attributes.GetNamedItem("puesto").Value;
                XmlNodeList tmp = list.Item(i).ChildNodes;
                for (int j = 0; j < tmp.Count; i++)
                {
                    string candidato = tmp.Item(i).Attributes.GetNamedItem("nombre").Value;
                    string partido = tmp.Item(i).Attributes.GetNamedItem("partido").Value;
                    string insert="insert into candidato values(null,'"+candidato+"','"+partido+"','"+puesto+"',0);";
                    if (Sqlite3.sqlite3_exec(Program.db, insert, null, null, ref error) != Sqlite3.SQLITE_OK)
                    {
                        MessageBox.Show(error);
                    }
                }
            }
            InitializeComponent();
            
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;


            CheckForIllegalCrossThreadCalls = false;
            //hilo = new Thread(new ThreadStart(bloqueo.esperarDesbloqueo));
            hilo = new Thread(new ThreadStart(this.esperar));
            
            
            
        }

        private void Progresivo_Load(object sender, EventArgs e)
        {
            paneles= new Panel[list.Count];
            botones = new Button[list.Count];
            btnCancelar = new Button[list.Count];
            rbtBotones = new RadioButton[list.Count][];
            lblNoListado = new Label[list.Count];
            txtNoListado = new TextBox[list.Count];

            id=Inputbox.Show("Número de casilla", "¿Qué número de casilla es?", FormStartPosition.CenterScreen);
            clave = Inputbox.Show("Cláve", "¿Cláve para esta casilla?", FormStartPosition.CenterScreen);
            lblTitulo = new Label[list.Count];
            bloqueo = new Bloqueo(short.Parse(id) );
            this.Controls.Add(bloqueo);
           
            for (int i = 0; i < list.Count; i++)
            {
                paneles[i] = new Panel();
                paneles[i].Visible = true;
                paneles[i].Name = list.Item(i).Attributes.GetNamedItem("puesto").Value;
                paneles[i].Width = this.Width-50;
                paneles[i].Height = this.Height-50;

                botones[i] = new Button();
                botones[i].Text = "Votar para continuar";
                botones[i].Width = 150;
                botones[i].Height = 70;
                botones[i].Location = new Point(paneles[i].Width-botones[i].Width, paneles[i].Height-botones[i].Height);
                botones[i].Click += new EventHandler(botonclick);
                botones[i].Visible = true;
                botones[i].TextAlign = ContentAlignment.MiddleCenter;
                botones[i].Image = Properties.Resources.palomita;
                botones[i].TextImageRelation = TextImageRelation.ImageBeforeText;
                

                btnCancelar[i] = new Button();
                btnCancelar[i].Text = "Terminar Votación";
                btnCancelar[i].Click += new EventHandler(btnCancelar_Click);
                btnCancelar[i].Visible = true;
                btnCancelar[i].Width = botones[i].Width;
                btnCancelar[i].Height = botones[i].Height;
                btnCancelar[i].Location = new Point(paneles[i].Width - botones[i].Width - btnCancelar[i].Width - 10, paneles[i].Height - botones[i].Height);

                int x=addCandidates(i);

                paneles[i].Controls.Add(lblTitulo[i]);
                paneles[i].Controls.Add(btnCancelar[i]);
                paneles[i].Controls.Add(botones[i]);
                paneles[i].Controls.Add(lblNoListado[i]);
                paneles[i].Controls.Add(txtNoListado[i]);
                
                

                for(int a=0;a<x; a++)
                    paneles[i].Controls.Add(rbtBotones[i][a]);

                this.Controls.Add(paneles[i]);

            }
            paneles[0].Visible = true;
            this.Visible = true;
            hilo.Start();
            
        }

        private int addCandidates(int i)
        {
            
            XmlNodeList subList = ((XmlElement)list[i]).GetElementsByTagName("candidato");
            rbtBotones[i] = new RadioButton[subList.Count];
            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblNoListado[i] = new System.Windows.Forms.Label();
            txtNoListado[i]= new TextBox();
            lblTitulo[i] = new Label();

            lblTitulo[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblTitulo[i].Text = list.Item(i).Attributes.GetNamedItem("puesto").Value.ToUpper();
            lblTitulo[i].AutoSize = true;
            lblTitulo[i].Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2-lblTitulo[i].Width/2, 10);
            lblTitulo[i].Name = list.Item(i).Attributes.GetNamedItem("puesto").Value;


            lblNoListado[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblNoListado[i].Location = new System.Drawing.Point(13, 513);
            lblNoListado[i].Name = "label1";
            lblNoListado[i].Size = new System.Drawing.Size(199, 23);
            lblNoListado[i].TabIndex = 5;
            lblNoListado[i].Text = "Candidato no Listado";
         
            txtNoListado[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtNoListado[i].Location = new System.Drawing.Point(13, 540);
            txtNoListado[i].Name = "textBox1";
            txtNoListado[i].Size = new System.Drawing.Size(199, 29);
            txtNoListado[i].TabIndex = 8;
            txtNoListado[i].Location = new Point(13, 540);


            int x = 15, y = 50;

            for (int j = 0; j < subList.Count; j++)
            {
                rbtBotones[i][j] = new RadioButton();
                rbtBotones[i][j].Appearance = Appearance.Button;
                rbtBotones[i][j].AutoSize = true;
                rbtBotones[i][j].Image = (Image)Properties.Resources.ResourceManager.GetObject(((XmlElement)subList[j]).GetAttribute("partido"));
                rbtBotones[i][j].Size = new System.Drawing.Size(277, 206);
                rbtBotones[i][j].TabIndex = j;
                rbtBotones[i][j].TabStop = true;
                rbtBotones[i][j].Text = ((XmlElement)subList[j]).GetAttribute("nombre");
                rbtBotones[i][j].Name = ((XmlElement)subList[j]).GetAttribute("partido");
                rbtBotones[i][j].TextAlign = ContentAlignment.MiddleCenter;
                rbtBotones[i][j].TextImageRelation = TextImageRelation.ImageBeforeText;
                rbtBotones[i][j].UseVisualStyleBackColor = true;
                rbtBotones[i][j].Visible = true;
                rbtBotones[i][j].Location = new Point(x, y);
                rbtBotones[i][j].CheckedChanged += new EventHandler(Progresivo_CheckedChanged);
                x+=271;
                if (x > Screen.PrimaryScreen.Bounds.Width-271)
                {
                    x = 6;
                    y += 271;
                }
            }

            // 
            //// radioButton4
            //// 
            //this.radioButton4.Appearance = System.Windows.Forms.Appearance.Button;
            //this.radioButton4.AutoSize = true;
            //this.radioButton4.Image = global::eVotePruebas.Properties.Resources.prd;
            //this.radioButton4.Location = new System.Drawing.Point(289, 241);
            //this.radioButton4.Name = "radioButton4";
            //this.radioButton4.Size = new System.Drawing.Size(277, 206);
            //this.radioButton4.TabIndex = 13;
            //this.radioButton4.TabStop = true;
            //this.radioButton4.Text = "radioButton1";
            //this.radioButton4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //this.radioButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            //this.radioButton4.UseVisualStyleBackColor = true;
            //// 
            //// radioButton2
            //// 
            //this.radioButton2.Appearance = System.Windows.Forms.Appearance.Button;
            //this.radioButton2.AutoSize = true;
            //this.radioButton2.Image = global::eVotePruebas.Properties.Resources.pan;
            //this.radioButton2.Location = new System.Drawing.Point(289, 12);
            //this.radioButton2.Name = "radioButton2";
            //this.radioButton2.Size = new System.Drawing.Size(277, 206);
            //this.radioButton2.TabIndex = 13;
            //this.radioButton2.TabStop = true;
            //this.radioButton2.Text = "radioButton1";
            //this.radioButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //this.radioButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            //this.radioButton2.UseVisualStyleBackColor = true;
            //// 
            //// radioButton3
            //// 
            //this.radioButton3.Appearance = System.Windows.Forms.Appearance.Button;
            //this.radioButton3.AutoSize = true;
            //this.radioButton3.Image = global::eVotePruebas.Properties.Resources.pt;
            //this.radioButton3.Location = new System.Drawing.Point(6, 241);
            //this.radioButton3.Name = "radioButton3";
            //this.radioButton3.Size = new System.Drawing.Size(277, 206);
            //this.radioButton3.TabIndex = 13;
            //this.radioButton3.TabStop = true;
            //this.radioButton3.Text = "radioButton1";
            //this.radioButton3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //this.radioButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            //this.radioButton3.UseVisualStyleBackColor = true;
            //// 
            //// radioButton1
            //// 
            //this.radioButton1.Appearance = System.Windows.Forms.Appearance.Button;
            //this.radioButton1.AutoSize = true;
            //this.radioButton1.Image = ((System.Drawing.Image)(resources.GetObject("radioButton1.Image")));
            //this.radioButton1.Location = new System.Drawing.Point(6, 12);
            //this.radioButton1.Name = "radioButton1";
            //this.radioButton1.Size = new System.Drawing.Size(277, 206);
            //this.radioButton1.TabIndex = 13;
            //this.radioButton1.TabStop = true;
            //this.radioButton1.Text = "radioButton1";
            //this.radioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //this.radioButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            //this.radioButton1.UseVisualStyleBackColor = true;

            return subList.Count;
        }

        void Progresivo_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                Image im = (sender as RadioButton).Image;
                Graphics g = Graphics.FromImage(im);
                g.DrawImageUnscaled((System.Drawing.Image)eVotePruebas.Properties.Resources.ResourceManager.GetObject("tacha"), 0, 0);
                (sender as RadioButton).Image = im;
                (sender as RadioButton).Refresh();
            }
            else
            {
                (sender as RadioButton).Image = (Image)Properties.Resources.ResourceManager.GetObject((sender as RadioButton).Name);
            }
        }

        void botonclick(object sender, EventArgs e)
        {
            string update = "update candidato set votos=votos+1 where nombre='" + candidato() + "';";
            string error = "";
            if (Sqlite3.sqlite3_exec(Program.db, update, null, null, ref error) != Sqlite3.SQLITE_OK)
            {
                MessageBox.Show(error);
            }

            if (ctrPaneles < list.Count)
            {
                paneles[ctrPaneles].Visible = false;
                ctrPaneles++;
                if (ctrPaneles != list.Count)
                    paneles[ctrPaneles].Visible = true;
                else
                {
                    MessageBox.Show("Votación terminada, gracias por participar", "Votos correctamente efectados", MessageBoxButtons.OK);
                    //this.Close();
                    restaurar();
                }
            }
            

        }
        void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea anular todos los votos que no ha efectuado? Los votos realizados se registraran y los no efectuados se anularan", "Terminar las votaciones", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //this.Close();
                restaurar();
            }
            
        }


        void restaurar()
        {
            bloqueo.Visible = true;
            ctrPaneles = 0;
            paneles[0].Visible = true;
            paneles[list.Count - 1].Visible = false;
            for (int i = 0; i < rbtBotones.Length; i++)
            {
                for (int j = 0; j < rbtBotones[i].Length; j++)
                {
                    rbtBotones[i][j].Checked = false;
                }

            }
        }
        void esperar()
        {
            string mensaje="";
             while (true)
            {
                TcpListener listener = new TcpListener(IPAddress.Any, 51111);

                listener.Start();

                using (TcpClient c = listener.AcceptTcpClient())
                using (NetworkStream n = c.GetStream())
                {

                    string msg = new BinaryReader(n).ReadString();
                    BinaryWriter w = new BinaryWriter(n);

                    w.Write(msg);
                    string []arr=msg.Split(';');
                    if (arr[0].Equals(id) && arr[1].Equals(clave))
                    {
                        
                        mensaje= "Casilla " + n + " desbloqueada satisfactoriamente";
                        w.Flush();
                        bloqueo.Visible = false;
                        return;
                    }
                    else
                    {
                        if (!arr[1].Equals(clave))
                        {
                            mensaje="Clave incorrecta para la casilla: " + n;
                        }
                    }

                    w.Flush();

                    enviarMensaje(mensaje);

                }

                //listener.Stop(  );


                Thread.Sleep(2000);
            }
            //Thread.Sleep(1000);
        }

        void enviarMensaje(string msg)
        {
            using (TcpClient client = new TcpClient("localhost", 51112))
            using (NetworkStream ns = client.GetStream())
            {

                BinaryWriter bw = new BinaryWriter(ns);

                bw.Write(msg);

                bw.Flush();

                MessageBox.Show( new BinaryReader(ns).ReadString());

            }
        }

        private string candidato()
        {
            string candidato = "";
            foreach (RadioButton r in rbtBotones[ctrPaneles])
            {
                if (r.Checked)
                {
                    candidato = r.Text;
                }
            }
            return candidato;
        }

        private string partido()
        {
            string candidato = "";
            foreach (RadioButton r in rbtBotones[ctrPaneles])
            {
                if (r.Checked)
                {
                    candidato = r.Name;
                }
            }
            return candidato;
        }
    }
}

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
using System.Security.Cryptography;
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
            try
            {
                config = new XmlDocument();

                config.Load("config.xml");
                list = config.GetElementsByTagName("voto");
                string error = "";
                for (int i = 0; i < list.Count; i++)
                {
                    string puesto = list.Item(i).Attributes.GetNamedItem("puesto").Value;
                    XmlNodeList tmp = list.Item(i).ChildNodes;
                    for (int j = 0; j < tmp.Count; j++)
                    {
                        string candidato = tmp.Item(j).Attributes.GetNamedItem("nombre").Value;
                        string partido = tmp.Item(j).Attributes.GetNamedItem("partido").Value;
                        string insert = "insert into candidato values(null,'" + candidato + "','" + partido + "','" + puesto + "',0);";
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
            catch (Exception)
            {
                MessageBox.Show("Favor de colocar el archivo config.xml\nEn la misma carpeta que el ejecutable.", "Archivo no existente");
            }
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
                rbtBotones[i][j].Checked = false;
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
                    encriptar();
                    restaurar();
                    
                }
            }
            

        }
        void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea anular todos los votos que no ha efectuado? Los votos realizados se registraran y los no efectuados se anularan", "Terminar las votaciones", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //this.Close();
                encriptar();
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

        private void encriptar()
        {
            Sqlite3.sqlite3_close(Program.db);
            FileStream fs = new FileStream(Program.dbase, FileMode.Open, FileAccess.Read, FileShare.None);
            byte[] datos = new byte[fs.Length];
            fs.Read(datos, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            RijndaelManaged crip = new RijndaelManaged();
            crip.Mode = CipherMode.CBC;
            PasswordDeriveBytes contra = new PasswordDeriveBytes(clave, Encoding.ASCII.GetBytes(clave + id),"SHA1",2);
            byte[] llave = contra.GetBytes(256 / 8);
            byte[] vi=Enumerable.Repeat<byte>(0,16).ToArray();
            Array.Copy(Encoding.ASCII.GetBytes(id), vi, id.Length > 16 ? 16 : id.Length);
            ICryptoTransform ict = crip.CreateEncryptor(llave,vi);
            MemoryStream memstr = new MemoryStream();
            CryptoStream crystr = new CryptoStream(memstr, ict, CryptoStreamMode.Write);
            crystr.Write(datos, 0, datos.Length);
            crystr.FlushFinalBlock();
            byte[] encriptado = memstr.ToArray();
            memstr.Close();
            crystr.Close();
            crip.Clear();

            File.WriteAllBytes(Program.dbase, encriptado);

        }
    }
}

using System;
using System.Drawing;
using System.IO;

namespace eVotePruebas
{
    partial class Form1
    {
        private void inicializar()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            botones = new System.Windows.Forms.Button[list.Count];
            voted = new System.Windows.Forms.PictureBox[list.Count];
            
            
           
            this.SuspendLayout();
            for (int i = 0; i < botones.Length; i++)
            {
                voted[i] = new System.Windows.Forms.PictureBox();
                botones[i]=new System.Windows.Forms.Button();
                botones[i].Location = new System.Drawing.Point(10, 10 + (i * 80));
                botones[i].Size = new System.Drawing.Size(250, 70);
                botones[i].Text=list.Item(i).Attributes.GetNamedItem("puesto").Value;
                botones[i].Name = list.Item(i).Attributes.GetNamedItem("puesto").Value;
                botones[i].Click+=new EventHandler(botonclick);
                
                this.Controls.Add(botones[i]);


                   // voted[i].Image = (System.Drawing.Bitmap)global::eVotePruebas.Properties.Resources.ResourceManager.GetObject(palomita);
                voted[i].Image = Properties.Resources.palomita;
                //voted[i].Width = imagen.Width;
                //voted[i].Height = imagen.Height;
                voted[i].Location= new Point(10+250, 10 + (i * 80)+70);
                voted[i].Visible = false;
                voted[i].Name = botones[i].Name;
                this.Controls.Add(voted[i]);
                //Agregar imagenes de que ya voto


            }

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 350);
            this.Name = "Form1";
            this.Text = "Form1";
            /*((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();*/
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button[] botones;
        private System.Windows.Forms.PictureBox[] voted;
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eVotePruebas
{
	partial class MainForm
	{
        private void inicializar(string voto)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            botones=new System.Windows.Forms.RadioButton[list.Count];
            this.SuspendLayout();

            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();

            for (int i = 0; i < botones.Length; i++)
            {
                botones[i]=new System.Windows.Forms.RadioButton();
                botones[i].Appearance = System.Windows.Forms.Appearance.Button;
                botones[i].AutoSize = true;
                //gracias a dios por el operador ternario, calcula el lugar segun el indice del arreglo
                botones[i].Location = new System.Drawing.Point(10 + ((int)(i/2) * 300), (i % 2 == 0) ? 10 : 226);
                botones[i].Size = new System.Drawing.Size(300, 206);
                botones[i].Text = list.Item(i).Attributes.GetNamedItem("nombre").Value;
                string partido = list.Item(i).Attributes.GetNamedItem("partido").Value;
                botones[i].Image = (System.Drawing.Bitmap)global::eVotePruebas.Properties.Resources.ResourceManager.GetObject(partido);
                botones[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                botones[i].TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                botones[i].UseVisualStyleBackColor = true;
                this.Controls.Add(botones[i]);


            }
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 510);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Candidato no Listado";

            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(10, 540);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 30);
            this.textBox1.TabIndex = 8;

            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button4.Location = new System.Drawing.Point(10, 585);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(130, 65);
            this.button4.TabIndex = 12;
            this.button4.Text = "Anular";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4Click);

            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(250, 560);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(190, 85);
            this.button3.TabIndex = 11;
            this.button3.Text = "VOTAR";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Votación para "+voto;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();


        }

        private System.Windows.Forms.RadioButton[] botones;
	}
}

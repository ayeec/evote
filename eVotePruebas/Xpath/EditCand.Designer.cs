namespace XPath
{
    partial class EditCand
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.country = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.TextBox();
            this.artist = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(103, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(128, 21);
            this.comboBox1.TabIndex = 22;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(223, 185);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 24);
            this.button3.TabIndex = 21;
            this.button3.Text = "Remove";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(135, 185);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 24);
            this.button2.TabIndex = 20;
            this.button2.Text = "Insert";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 185);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 24);
            this.button1.TabIndex = 19;
            this.button1.Text = "Update";
            // 
            // country
            // 
            this.country.Location = new System.Drawing.Point(103, 137);
            this.country.Name = "country";
            this.country.Size = new System.Drawing.Size(128, 20);
            this.country.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(23, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Puesto:";
            // 
            // price
            // 
            this.price.Location = new System.Drawing.Point(103, 101);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(128, 20);
            this.price.TabIndex = 16;
            // 
            // artist
            // 
            this.artist.Location = new System.Drawing.Point(103, 65);
            this.artist.Name = "artist";
            this.artist.Size = new System.Drawing.Size(128, 20);
            this.artist.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(23, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Price:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(23, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Artist:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(23, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "Puesto:";
            // 
            // EditCand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 221);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.country);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.price);
            this.Controls.Add(this.artist);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EditCand";
            this.Text = "EditCand";
            this.Load += new System.EventHandler(this.EditCand_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox country;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox price;
        private System.Windows.Forms.TextBox artist;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
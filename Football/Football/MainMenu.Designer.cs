namespace Football
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBoxBallRight = new System.Windows.Forms.PictureBox();
            this.pictureBoxBallLeft = new System.Windows.Forms.PictureBox();
            this.pictureBoxArrowRight = new System.Windows.Forms.PictureBox();
            this.pictureBoxArrowLeft = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.MuteCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBallRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBallLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArrowRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArrowLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label1.Location = new System.Drawing.Point(108, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Let\'s play foosball!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "TeamA:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 349);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "TeamB:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(62, 289);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(208, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(62, 346);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(208, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(152, 400);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label4.Location = new System.Drawing.Point(109, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Name your teams:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(276, 405);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 28);
            this.button2.TabIndex = 9;
            this.button2.Text = "Statistics";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(42, 400);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 38);
            this.button3.TabIndex = 10;
            this.button3.Text = "Tournament";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBoxBallRight
            // 
            this.pictureBoxBallRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxBallRight.BackgroundImage")));
            this.pictureBoxBallRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxBallRight.Location = new System.Drawing.Point(327, 336);
            this.pictureBoxBallRight.Name = "pictureBoxBallRight";
            this.pictureBoxBallRight.Size = new System.Drawing.Size(42, 39);
            this.pictureBoxBallRight.TabIndex = 8;
            this.pictureBoxBallRight.TabStop = false;
            // 
            // pictureBoxBallLeft
            // 
            this.pictureBoxBallLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxBallLeft.BackgroundImage")));
            this.pictureBoxBallLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxBallLeft.Location = new System.Drawing.Point(327, 280);
            this.pictureBoxBallLeft.Name = "pictureBoxBallLeft";
            this.pictureBoxBallLeft.Size = new System.Drawing.Size(42, 39);
            this.pictureBoxBallLeft.TabIndex = 8;
            this.pictureBoxBallLeft.TabStop = false;
            // 
            // pictureBoxArrowRight
            // 
            this.pictureBoxArrowRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxArrowRight.BackgroundImage")));
            this.pictureBoxArrowRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxArrowRight.Location = new System.Drawing.Point(276, 336);
            this.pictureBoxArrowRight.Name = "pictureBoxArrowRight";
            this.pictureBoxArrowRight.Size = new System.Drawing.Size(54, 39);
            this.pictureBoxArrowRight.TabIndex = 8;
            this.pictureBoxArrowRight.TabStop = false;
            // 
            // pictureBoxArrowLeft
            // 
            this.pictureBoxArrowLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxArrowLeft.BackgroundImage")));
            this.pictureBoxArrowLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxArrowLeft.Location = new System.Drawing.Point(276, 280);
            this.pictureBoxArrowLeft.Name = "pictureBoxArrowLeft";
            this.pictureBoxArrowLeft.Size = new System.Drawing.Size(54, 39);
            this.pictureBoxArrowLeft.TabIndex = 8;
            this.pictureBoxArrowLeft.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(31, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(322, 178);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Football.Properties.Resources.imagescup;
            this.pictureBox2.Location = new System.Drawing.Point(12, 400);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(36, 37);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // MuteCheckBox
            // 
            this.MuteCheckBox.AutoSize = true;
            this.MuteCheckBox.Location = new System.Drawing.Point(276, 243);
            this.MuteCheckBox.Name = "MuteCheckBox";
            this.MuteCheckBox.Size = new System.Drawing.Size(98, 17);
            this.MuteCheckBox.TabIndex = 12;
            this.MuteCheckBox.Text = "Off Soundtrack";
            this.MuteCheckBox.UseVisualStyleBackColor = true;
            this.MuteCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(381, 450);
            this.Controls.Add(this.MuteCheckBox);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBoxBallRight);
            this.Controls.Add(this.pictureBoxBallLeft);
            this.Controls.Add(this.pictureBoxArrowRight);
            this.Controls.Add(this.pictureBoxArrowLeft);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainMenu_FormClosed);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBallRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBallLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArrowRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArrowLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBoxArrowLeft;
        private System.Windows.Forms.PictureBox pictureBoxArrowRight;
        private System.Windows.Forms.PictureBox pictureBoxBallLeft;
        private System.Windows.Forms.PictureBox pictureBoxBallRight;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox MuteCheckBox;
    }
}
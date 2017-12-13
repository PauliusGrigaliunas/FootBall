using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Football
{
    public partial class ScoreEditor : Form
    {
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
        private ContextMenuStrip contextMenuStrip3;
        private Label TeamA;
        private Label TeamB;
        private Label TeamAScore;
        private Label TeamBScore;
        private Label colon;
        private Button BPlus;
        private Button BMinus;
        private Button AMinus;
        private Button APlus;
        private Button Close;
        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TeamA = new System.Windows.Forms.Label();
            this.TeamB = new System.Windows.Forms.Label();
            this.TeamAScore = new System.Windows.Forms.Label();
            this.TeamBScore = new System.Windows.Forms.Label();
            this.colon = new System.Windows.Forms.Label();
            this.BPlus = new System.Windows.Forms.Button();
            this.BMinus = new System.Windows.Forms.Button();
            this.AMinus = new System.Windows.Forms.Button();
            this.APlus = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(61, 4);
            // 
            // TeamA
            // 
            this.TeamA.AutoSize = true;
            this.TeamA.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamA.Location = new System.Drawing.Point(51, 32);
            this.TeamA.Name = "TeamA";
            this.TeamA.Size = new System.Drawing.Size(91, 29);
            this.TeamA.TabIndex = 3;
            this.TeamA.Text = "TeamA";
            this.TeamA.Click += new System.EventHandler(this.TeamA_Click);
            // 
            // TeamB
            // 
            this.TeamB.AutoSize = true;
            this.TeamB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamB.Location = new System.Drawing.Point(232, 30);
            this.TeamB.Name = "TeamB";
            this.TeamB.Size = new System.Drawing.Size(92, 29);
            this.TeamB.TabIndex = 4;
            this.TeamB.Text = "TeamB";
            this.TeamB.Click += new System.EventHandler(this.TeamB_Click);
            // 
            // TeamAScore
            // 
            this.TeamAScore.AutoSize = true;
            this.TeamAScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamAScore.Location = new System.Drawing.Point(114, 71);
            this.TeamAScore.Name = "TeamAScore";
            this.TeamAScore.Size = new System.Drawing.Size(40, 38);
            this.TeamAScore.TabIndex = 5;
            this.TeamAScore.Text = "X";
            this.TeamAScore.Click += new System.EventHandler(this.TeamAScore_Click);
            // 
            // TeamBScore
            // 
            this.TeamBScore.AutoSize = true;
            this.TeamBScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamBScore.Location = new System.Drawing.Point(216, 71);
            this.TeamBScore.Name = "TeamBScore";
            this.TeamBScore.Size = new System.Drawing.Size(40, 38);
            this.TeamBScore.TabIndex = 6;
            this.TeamBScore.Text = "X";
            this.TeamBScore.Click += new System.EventHandler(this.TeamBScore_Click);
            // 
            // colon
            // 
            this.colon.AutoSize = true;
            this.colon.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colon.Location = new System.Drawing.Point(173, 71);
            this.colon.Name = "colon";
            this.colon.Size = new System.Drawing.Size(27, 38);
            this.colon.TabIndex = 7;
            this.colon.Text = ":";
            // 
            // BPlus
            // 
            this.BPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BPlus.Location = new System.Drawing.Point(272, 62);
            this.BPlus.Name = "BPlus";
            this.BPlus.Size = new System.Drawing.Size(41, 41);
            this.BPlus.TabIndex = 8;
            this.BPlus.Text = "+";
            this.BPlus.UseVisualStyleBackColor = true;
            this.BPlus.Click += new System.EventHandler(this.BPlus_Click);
            // 
            // BMinus
            // 
            this.BMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BMinus.Location = new System.Drawing.Point(272, 112);
            this.BMinus.Name = "BMinus";
            this.BMinus.Size = new System.Drawing.Size(41, 39);
            this.BMinus.TabIndex = 9;
            this.BMinus.Text = "-";
            this.BMinus.UseVisualStyleBackColor = true;
            this.BMinus.Click += new System.EventHandler(this.BMinus_Click);
            // 
            // AMinus
            // 
            this.AMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AMinus.Location = new System.Drawing.Point(56, 112);
            this.AMinus.Name = "AMinus";
            this.AMinus.Size = new System.Drawing.Size(41, 39);
            this.AMinus.TabIndex = 10;
            this.AMinus.Text = "-";
            this.AMinus.UseVisualStyleBackColor = true;
            this.AMinus.Click += new System.EventHandler(this.AMinus_Click);
            // 
            // APlus
            // 
            this.APlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.APlus.Location = new System.Drawing.Point(56, 64);
            this.APlus.Name = "APlus";
            this.APlus.Size = new System.Drawing.Size(41, 39);
            this.APlus.TabIndex = 11;
            this.APlus.Text = "+";
            this.APlus.UseVisualStyleBackColor = true;
            this.APlus.Click += new System.EventHandler(this.APlus_Click);
            // 
            // Close
            // 
            this.Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close.Location = new System.Drawing.Point(112, 149);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(144, 42);
            this.Close.TabIndex = 12;
            this.Close.Text = "OK";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // ScoreEditor
            // 
            this.ClientSize = new System.Drawing.Size(380, 203);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.APlus);
            this.Controls.Add(this.AMinus);
            this.Controls.Add(this.BMinus);
            this.Controls.Add(this.BPlus);
            this.Controls.Add(this.colon);
            this.Controls.Add(this.TeamBScore);
            this.Controls.Add(this.TeamAScore);
            this.Controls.Add(this.TeamB);
            this.Controls.Add(this.TeamA);
            this.Name = "ScoreEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private string aName;
        private string bName;
        public int aPoints;
        public int bPoints;

        public ScoreEditor(String ATeam, String BTeam, String aScore, String bScore)
        {
            InitializeComponent();

            aName = ATeam;
            bName = BTeam;
            aPoints = int.Parse(aScore);
            bPoints = int.Parse(bScore);

            TeamAScore.Text = aScore;
            TeamBScore.Text = bScore;

            Close.Text = Properties.Settings.Default.Save;
        }

        private void TeamA_Click(object sender, EventArgs e)
        {
            // do nothing
        }

        private void TeamB_Click(object sender, EventArgs e)
        {
            // do nothing
        }

        private void TeamAScore_Click(object sender, EventArgs e)
        {
            // do nothing
        }

        private void TeamBScore_Click(object sender, EventArgs e)
        {
            // do nothing
        }

        private void APlus_Click(object sender, EventArgs e)
        {
            aPoints++;
            TeamAScore.Text = aPoints.ToString();
        }

        private void AMinus_Click(object sender, EventArgs e)
        {
            if(aPoints != 0)
                aPoints--;
            TeamAScore.Text = aPoints.ToString();
        }

        private void BPlus_Click(object sender, EventArgs e)
        {
            bPoints++;
            TeamBScore.Text = bPoints.ToString();
        }

        private void BMinus_Click(object sender, EventArgs e)
        {
            if (bPoints != 0)
                bPoints--;
            TeamBScore.Text = bPoints.ToString();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int returnA()
        {
            return aPoints;
        }

        public int returnB()
        {
            return bPoints;
        }
    }
}

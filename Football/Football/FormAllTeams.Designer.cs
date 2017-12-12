namespace Football
{
    partial class FormAllTeams
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
            this.components = new System.ComponentModel.Container();
            this.dataSet1BindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewAll = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.victoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAll)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewAll
            // 
            this.dataGridViewAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAll.Location = new System.Drawing.Point(1, 27);
            this.dataGridViewAll.Name = "dataGridViewAll";
            this.dataGridViewAll.Size = new System.Drawing.Size(612, 460);
            this.dataGridViewAll.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(613, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.victoriesToolStripMenuItem,
            this.goalsToolStripMenuItem,
            this.bestToolStripMenuItem});
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.ToolStripMenuItem.Text = "Filter";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.allToolStripMenuItem.Text = "Teams";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // victoriesToolStripMenuItem
            // 
            this.victoriesToolStripMenuItem.Name = "victoriesToolStripMenuItem";
            this.victoriesToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.victoriesToolStripMenuItem.Text = "Order By Victories";
            this.victoriesToolStripMenuItem.Click += new System.EventHandler(this.victoriesToolStripMenuItem_Click);
            // 
            // goalsToolStripMenuItem
            // 
            this.goalsToolStripMenuItem.Name = "goalsToolStripMenuItem";
            this.goalsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.goalsToolStripMenuItem.Text = "Games";
            this.goalsToolStripMenuItem.Click += new System.EventHandler(this.goalsToolStripMenuItem_Click);
            // 
            // bestToolStripMenuItem
            // 
            this.bestToolStripMenuItem.Name = "bestToolStripMenuItem";
            this.bestToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.bestToolStripMenuItem.Text = "Best ";
            this.bestToolStripMenuItem.Click += new System.EventHandler(this.bestToolStripMenuItem_Click);
            // 
            // FormAllTeams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 486);
            this.Controls.Add(this.dataGridViewAll);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormAllTeams";
            this.Text = "FormAllTeams";
            this.Load += new System.EventHandler(this.FormAllTeams_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAll)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource dataSet1BindingSource;
        //private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource dataSet1BindingSource1;
        private System.Windows.Forms.DataGridView dataGridViewAll;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem victoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bestToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}
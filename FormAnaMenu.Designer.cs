namespace FormGiris
{
    partial class FormAnaMenu
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.stokKartlaritoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.stokKartıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cariKartlariToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cariKartlarıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çıkışToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stokKartlaritoolStripMenuItem1,
            this.cariKartlariToolStripMenuItem,
            this.çıkışToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // stokKartlaritoolStripMenuItem1
            // 
            this.stokKartlaritoolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.stokKartıToolStripMenuItem});
            this.stokKartlaritoolStripMenuItem1.Name = "stokKartlaritoolStripMenuItem1";
            this.stokKartlaritoolStripMenuItem1.Size = new System.Drawing.Size(52, 24);
            this.stokKartlaritoolStripMenuItem1.Text = "Stok";
            this.stokKartlaritoolStripMenuItem1.Click += new System.EventHandler(this.stokKartlaritoolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // stokKartıToolStripMenuItem
            // 
            this.stokKartıToolStripMenuItem.Name = "stokKartıToolStripMenuItem";
            this.stokKartıToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.stokKartıToolStripMenuItem.Text = "Stok Kartları";
            // 
            // cariKartlariToolStripMenuItem
            // 
            this.cariKartlariToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cariKartlarıToolStripMenuItem});
            this.cariKartlariToolStripMenuItem.Name = "cariKartlariToolStripMenuItem";
            this.cariKartlariToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.cariKartlariToolStripMenuItem.Text = "Cari";
            this.cariKartlariToolStripMenuItem.Click += new System.EventHandler(this.cariKartlariToolStripMenuItem_Click);
            // 
            // cariKartlarıToolStripMenuItem
            // 
            this.cariKartlarıToolStripMenuItem.Name = "cariKartlarıToolStripMenuItem";
            this.cariKartlarıToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.cariKartlarıToolStripMenuItem.Text = "Cari Kartları";
            // 
            // çıkışToolStripMenuItem
            // 
            this.çıkışToolStripMenuItem.Name = "çıkışToolStripMenuItem";
            this.çıkışToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.çıkışToolStripMenuItem.Text = "Çıkış";
            this.çıkışToolStripMenuItem.Click += new System.EventHandler(this.çıkışToolStripMenuItem_Click);
            // 
            // FormAnaMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormAnaMenu";
            this.Text = "Ana Menü";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem stokKartlaritoolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem stokKartıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cariKartlariToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cariKartlarıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem çıkışToolStripMenuItem;
    }
}
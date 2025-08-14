namespace FormGiris
{
    partial class FormAnaMenu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.stokKartlaritoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stokKartıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cariKartlariToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cariKartlarıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayarlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kullanıcıYönetimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dövizKurlarıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dövizKurlarıToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.ayarlarToolStripMenuItem,
            this.dövizKurlarıToolStripMenuItem,
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
            this.stokKartıToolStripMenuItem});
            this.stokKartlaritoolStripMenuItem1.Name = "stokKartlaritoolStripMenuItem1";
            this.stokKartlaritoolStripMenuItem1.Size = new System.Drawing.Size(52, 24);
            this.stokKartlaritoolStripMenuItem1.Text = "Stok";
            // Üst menü Click event kaldırıldı
            // 
            // stokKartıToolStripMenuItem
            // 
            this.stokKartıToolStripMenuItem.Name = "stokKartıToolStripMenuItem";
            this.stokKartıToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.stokKartıToolStripMenuItem.Text = "Stok Kartları";
            this.stokKartıToolStripMenuItem.Click += new System.EventHandler(this.stokKartlaritoolStripMenuItem1_Click);
            // 
            // cariKartlariToolStripMenuItem
            // 
            this.cariKartlariToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cariKartlarıToolStripMenuItem});
            this.cariKartlariToolStripMenuItem.Name = "cariKartlariToolStripMenuItem";
            this.cariKartlariToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.cariKartlariToolStripMenuItem.Text = "Cari";
            // Üst menü Click event kaldırıldı
            // 
            // cariKartlarıToolStripMenuItem
            // 
            this.cariKartlarıToolStripMenuItem.Name = "cariKartlarıToolStripMenuItem";
            this.cariKartlarıToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
            this.cariKartlarıToolStripMenuItem.Text = "Cari Kartları";
            this.cariKartlarıToolStripMenuItem.Click += new System.EventHandler(this.cariKartlariToolStripMenuItem_Click);
            // 
            // ayarlarToolStripMenuItem
            // 
            this.ayarlarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kullanıcıYönetimiToolStripMenuItem});
            this.ayarlarToolStripMenuItem.Name = "ayarlarToolStripMenuItem";
            this.ayarlarToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.ayarlarToolStripMenuItem.Text = "Ayarlar";
            // Üst menü Click event kaldırıldı
            // 
            // kullanıcıYönetimiToolStripMenuItem
            // 
            this.kullanıcıYönetimiToolStripMenuItem.Name = "kullanıcıYönetimiToolStripMenuItem";
            this.kullanıcıYönetimiToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.kullanıcıYönetimiToolStripMenuItem.Text = "Kullanıcı Yönetimi";
            this.kullanıcıYönetimiToolStripMenuItem.Click += new System.EventHandler(this.ayarlarToolStripMenuItem_Click);
            // 
            // dövizKurlarıToolStripMenuItem
            // 
            this.dövizKurlarıToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dövizKurlarıToolStripMenuItem1});
            this.dövizKurlarıToolStripMenuItem.Name = "dövizKurlarıToolStripMenuItem";
            this.dövizKurlarıToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.dövizKurlarıToolStripMenuItem.Text = "Döviz ";
            // Üst menü Click event kaldırıldı
            // 
            // dövizKurlarıToolStripMenuItem1
            // 
            this.dövizKurlarıToolStripMenuItem1.Name = "dövizKurlarıToolStripMenuItem1";
            this.dövizKurlarıToolStripMenuItem1.Size = new System.Drawing.Size(177, 26);
            this.dövizKurlarıToolStripMenuItem1.Text = "Döviz Kurları";
            this.dövizKurlarıToolStripMenuItem1.Click += new System.EventHandler(this.dövizKurlarıToolStripMenuItem1_Click);
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
        private System.Windows.Forms.ToolStripMenuItem stokKartıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cariKartlariToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cariKartlarıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayarlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kullanıcıYönetimiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dövizKurlarıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dövizKurlarıToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem çıkışToolStripMenuItem;
    }
}

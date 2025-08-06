using FormGiris.cs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormGiris
{
    public partial class FormAnaMenu : Form
    {
        public FormAnaMenu()
        {
            InitializeComponent();
            this.IsMdiContainer = true; // Ana menü içinden diğer formları açabilmek için
        }
        // Stok Kartları menüsüne tıklanınca
        private void stokKartlaritoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormStokKart frm = new FormStokKart();
            frm.MdiParent = this; // Ana menü penceresi içinde aç
            frm.WindowState = FormWindowState.Maximized; // Tam ekran aç
            frm.Show();
        }

        // Cari Kartları menüsüne tıklanınca
        private void cariKartlariToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCariKart form = new FormCariKart();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        // Çıkış menüsüne tıklanınca
        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

using FormGiris.cs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormGiris
{
    public partial class FormAnaMenu : Form
    {
        private Fis secilenFis = null;

        SqlConnection conn = new SqlConnection("Server=HATICE\\SQLEXPRESS;Database=MuhasebeDB;Trusted_Connection=True;");
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

        private void ayarlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormKullaniciYonetimi frm = new FormKullaniciYonetimi();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
        private void dövizKurlarıToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormDovizKurlari dovizForm = new FormDovizKurlari();
            dovizForm.MdiParent = this;
            dovizForm.WindowState=FormWindowState.Maximized;
            dovizForm.Show(); 
        }
        private void satışFaturasıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSatisFaturasi satisFormu = new FormSatisFaturasi();
            satisFormu.MdiParent = this;
            satisFormu.WindowState= FormWindowState.Maximized;
            satisFormu.Show();
        }

        private void bilgiFişiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBilgiFisi fisFormu = new FormBilgiFisi();
            fisFormu.MdiParent = this;
            fisFormu.WindowState= FormWindowState.Maximized;
            fisFormu.Show();
        }

        private void fişDetayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formBilgiFisi = this.MdiChildren
                             .OfType<FormBilgiFisi>()
                             .FirstOrDefault();

            if (formBilgiFisi == null)
            {
                MessageBox.Show("Önce Bilgi Fişi formunu açın.");
                return;
            }

            if (formBilgiFisi.SecilenFis == null)  // SecilenFis property olarak FormBilgiFisi içinde public yapılmalı
            {
                MessageBox.Show("Lütfen önce bir fiş seçin.");
                return;
            }

            var frmDetay = new FormFisDetay(formBilgiFisi.SecilenFis.Id)
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };

            // Callback ile toplamları bilgi fişi formuna bildir
            frmDetay.ToplamlarDegisti = (ara, kdv, genel) =>
            {
                formBilgiFisi.AraToplamGuncelle(ara, kdv, genel);
            };

            frmDetay.Show();
        }
    }
}

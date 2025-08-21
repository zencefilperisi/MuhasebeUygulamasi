using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormGiris.cs
{
    public partial class FormBilgiFisi : Form
    {
        private MuhasebeDBEntities2 db = new MuhasebeDBEntities2();

        public Fis SecilenFis { get; private set; }
        public Fis GetSecilenVeyaIlkFis()
        {
            if (SecilenFis != null) return SecilenFis;
            return FisListesi.FirstOrDefault();
        }
        private List<Fis> FisListesi = new List<Fis>();

        public FormBilgiFisi()
        {
            InitializeComponent();
        }

        private void FormBilgiFisi_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
            FisListesi = db.Fis.ToList();

            var kaynak = FisListesi
                         .Select(f => new
                         {
                             f.Id,
                             f.FisKodu,
                             f.Cari,
                             f.OdemeTuru,
                             f.Tarih
                         })
                         .ToList();

            dgvFis.DataSource = kaynak;

            if (dgvFis.Columns["Id"] != null)
                dgvFis.Columns["Id"].Visible = false;

            dgvFis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFis.RowTemplate.Height = 25;

            // Otomatik olarak ilk fişi seç
            if (FisListesi.Count > 0)
            {
                SecilenFis = FisListesi[0];
                ToplamlariGuncelle();
            }
        }

        public void AraToplamGuncelle(decimal ara, decimal kdv, decimal genel)
        {
            txtAraToplam.Text = ara.ToString("C2");
            txtKdvToplam.Text = kdv.ToString("C2");
            txtGenelToplam.Text = genel.ToString("C2");
        }

        private void ToplamlariGuncelle()
        {
            if (SecilenFis == null) return;

            var ara = db.FisDetay.Where(d => d.FisId == SecilenFis.Id)
                                  .Sum(d => (decimal?)(d.Miktar * d.BirimFiyat)) ?? 0;
            var kdv = db.FisDetay.Where(d => d.FisId == SecilenFis.Id)
                                  .Sum(d => (decimal?)(d.Miktar * d.BirimFiyat * d.Kdv / 100)) ?? 0;
            var genel = db.FisDetay.Where(d => d.FisId == SecilenFis.Id)
                                    .Sum(d => (decimal?)d.Toplam) ?? 0;

            AraToplamGuncelle(ara, kdv, genel);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFisKodu.Text) ||
                string.IsNullOrWhiteSpace(txtCari.Text) ||
                string.IsNullOrWhiteSpace(txtOdemeTuru.Text))
            {
                MessageBox.Show("Lütfen gerekli alanları doldurun!");
                return;
            }

            var yeniFis = new Fis
            {
                FisKodu = txtFisKodu.Text,
                Cari = txtCari.Text,
                OdemeTuru = txtOdemeTuru.Text,
                Tarih = dtpTarih.Value
            };

            db.Fis.Add(yeniFis);
            db.SaveChanges();

            SecilenFis = yeniFis;
            Listele();
            Temizle();
            MessageBox.Show("Fis başarıyla eklendi!");
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (SecilenFis != null)
            {
                SecilenFis.FisKodu = txtFisKodu.Text;
                SecilenFis.Cari = txtCari.Text;
                SecilenFis.OdemeTuru = txtOdemeTuru.Text;
                SecilenFis.Tarih = dtpTarih.Value;

                db.SaveChanges();
                Listele();
                MessageBox.Show("Fis başarıyla güncellendi!");
            }
            else
            {
                MessageBox.Show("Lütfen önce bir fiş seçin.");
            }
        }

        private void Temizle()
        {
            txtFisKodu.Clear();
            txtCari.Clear();
            txtOdemeTuru.Clear();
            txtAraToplam.Clear();
            txtKdvToplam.Clear();
            txtGenelToplam.Clear();
            dtpTarih.Value = DateTime.Now;
            SecilenFis = null;
        }

        private void fişDetayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SecilenFis == null)
            {
                MessageBox.Show("Önce bir fiş oluşturun.");
                return;
            }

            var frmDetay = new FormFisDetay(SecilenFis.Id)
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };

            frmDetay.ToplamlarDegisti = (ara, kdv, genel) =>
            {
                AraToplamGuncelle(ara, kdv, genel);
            };

            frmDetay.Show();
        }
    }
}


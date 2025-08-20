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
        private Fis secilenFis;
        public FormBilgiFisi()
        {
            InitializeComponent();
        }

        private void FormBilgiFisi_Load(object sender, EventArgs e)
        {
            Listele();
            if (db.Fis.Any())
            {
                dgvFis.Rows[0].Selected = true;
                dgvFis_CellClick(this, new DataGridViewCellEventArgs(0, 0));
            }
        }
        private void Listele()
        {
            var fisler = db.Fis
                           .Select(f => new
                           {
                               f.Id,
                               f.FisKodu,
                               f.Cari,
                               f.OdemeTuru,
                               f.Tarih
                           })
                           .ToList();

            dgvFis.DataSource = fisler;

            if (dgvFis.Columns["Id"] != null)
                dgvFis.Columns["Id"].Visible = false;

            dgvFis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFis.RowTemplate.Height = 25;
        }
        public Fis SecilenFis => secilenFis;

        public void AraToplamGuncelle(decimal ara, decimal kdv, decimal genel)
        {
            txtAraToplam.Text = ara.ToString("C2");
            txtKdvToplam.Text = kdv.ToString("C2");
            txtGenelToplam.Text = genel.ToString("C2");
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

            secilenFis = yeniFis;
            Listele();
            Temizle();
            MessageBox.Show("Fis başarıyla eklendi!");
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (secilenFis != null)
            {
                secilenFis.FisKodu = txtFisKodu.Text;
                secilenFis.Cari = txtCari.Text;
                secilenFis.OdemeTuru = txtOdemeTuru.Text;
                secilenFis.Tarih = dtpTarih.Value;

                db.SaveChanges();
                Listele();
                MessageBox.Show("Fis başarıyla güncellendi!");
            }
            else
            {
                MessageBox.Show("Lütfen önce bir fiş seçin.");
            }
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
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
            secilenFis = null;
        }
        private void dgvFis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int fisId = Convert.ToInt32(dgvFis.Rows[e.RowIndex].Cells["Id"].Value);
            secilenFis = db.Fis.FirstOrDefault(f => f.Id == fisId);

            if (secilenFis != null)
            {
                txtFisKodu.Text = secilenFis.FisKodu;
                txtCari.Text = secilenFis.Cari;
                txtOdemeTuru.Text = secilenFis.OdemeTuru;
                dtpTarih.Value = secilenFis.Tarih;

                HesaplaToplamlar();
            }
        }
        private void HesaplaToplamlar()
        {
            if (secilenFis == null) return;

            var urunler = db.FisDetay.Where(d => d.FisId == secilenFis.Id).ToList();
            decimal araToplam = urunler.Sum(u => u.Miktar * u.BirimFiyat);
            decimal kdvToplam = urunler.Sum(u => u.Miktar * u.BirimFiyat * (u.Kdv / 100));
            decimal genelToplam = araToplam + kdvToplam;

            txtAraToplam.Text = araToplam.ToString("C2");
            txtKdvToplam.Text = kdvToplam.ToString("C2");
            txtGenelToplam.Text = genelToplam.ToString("C2");
        } 
    }
}

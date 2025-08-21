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
    public partial class FormSatisFaturasi : Form
    {
        private MuhasebeDBEntities2 db = new MuhasebeDBEntities2();
        private SatisFatura secilenFatura;
        public FormSatisFaturasi()
        {
            InitializeComponent();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SatisFatura yeniFatura = new SatisFatura
            {
                FaturaNo = txtFaturaNo.Text,
                CariKodu = txtCariKodu.Text,
                UrunKodu = txtUrunKodu.Text,
                UrunAdi = txtUrunAdi.Text,
                Miktar = Convert.ToDecimal(txtMiktar.Text),
                BirimFiyat = Convert.ToDecimal(txtBirimFiyat.Text),
                Tarih = dtpTarih.Value
            };

            db.SatisFatura.Add(yeniFatura);
            db.SaveChanges();

            MessageBox.Show("Fatura başarıyla eklendi!");
            Temizle();
            Listele(); // DataGridView güncellensin
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (secilenFatura != null)
            {
                secilenFatura.FaturaNo = txtFaturaNo.Text;
                secilenFatura.CariKodu = txtCariKodu.Text;
                secilenFatura.UrunKodu = txtUrunKodu.Text;
                secilenFatura.UrunAdi = txtUrunAdi.Text;
                secilenFatura.Miktar = Convert.ToDecimal(txtMiktar.Text);
                secilenFatura.BirimFiyat = Convert.ToDecimal(txtBirimFiyat.Text);
                secilenFatura.Tarih = dtpTarih.Value;

                db.SaveChanges();
                MessageBox.Show("Fatura başarıyla güncellendi!");
                Temizle();
                Listele(); // Güncelleme sonrası listeyi yenile
            }
            else
            {
                MessageBox.Show("Lütfen önce bir fatura seçiniz.");
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        private void Temizle()
        {
            txtFaturaNo.Clear();
            txtCariKodu.Clear();
            txtUrunKodu.Clear();
            txtUrunAdi.Clear();
            txtMiktar.Clear();
            txtBirimFiyat.Clear();
            dtpTarih.Value = DateTime.Now;
            secilenFatura = null;
        }
        private void Listele()
        {
            var faturalar = db.SatisFatura.ToList();
            dgvFaturaDetay.DataSource = faturalar;
        }

        private void FormSatisFaturasi_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void dgvFaturaDetay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                secilenFatura = (SatisFatura)dgvFaturaDetay.Rows[e.RowIndex].DataBoundItem;

                txtFaturaNo.Text = secilenFatura.FaturaNo;
                txtCariKodu.Text = secilenFatura.CariKodu;
                txtUrunKodu.Text = secilenFatura.UrunKodu;
                txtUrunAdi.Text = secilenFatura.UrunAdi;
                txtMiktar.Text = secilenFatura.Miktar.ToString();
                txtBirimFiyat.Text = secilenFatura.BirimFiyat.ToString();
                dtpTarih.Value = secilenFatura.Tarih;
            }
        }
    }
}

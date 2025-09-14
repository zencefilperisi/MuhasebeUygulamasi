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
        private string FisKoduSecilen;
        private List<FisAll> FisAllListesi = new List<FisAll>();
        public FisAll SecilenFisAll { get; private set; }
        public FormBilgiFisi(SatisFatura fatura)
    : this(fatura != null ? fatura.FaturaNo : string.Empty)
        {

        }

        public FormBilgiFisi(string fisKodu)
        {
            InitializeComponent();
            FisKoduSecilen = fisKodu;
        }

        private void FormBilgiFisi_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
            FisAllListesi = db.FisAll.Where(f => f.FisKodu == FisKoduSecilen).ToList();

            var kaynak = FisAllListesi.Select(f => new
            {
                f.Id,
                f.FisKodu,
                f.Cari,
                f.OdemeTuru,
                f.Tarih,
                f.UrunKodu,
                f.UrunAdi,
                f.Miktar,
                f.BirimFiyat,
                f.Kdv,
                f.Toplam
            }).ToList();

            dgvFisAll.DataSource = kaynak;

            if (dgvFisAll.Columns["Id"] != null)
                dgvFisAll.Columns["Id"].Visible = false;

            dgvFisAll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFisAll.RowTemplate.Height = 25;

            if (FisAllListesi.Count > 0)
            {
                SecilenFisAll = FisAllListesi[0];
                ToplamlariGuncelle(SecilenFisAll.FisKodu);
            }
        }

        private void ToplamlariGuncelle(string fisKodu)
        {
            var ara = FisAllListesi.Where(f => f.FisKodu == fisKodu)
                                    .Sum(f => (decimal?)(f.Miktar * f.BirimFiyat)) ?? 0;
            var kdv = FisAllListesi.Where(f => f.FisKodu == fisKodu)
                                    .Sum(f => (decimal?)(f.Miktar * f.BirimFiyat * f.Kdv / 100)) ?? 0;
            var genel = FisAllListesi.Where(f => f.FisKodu == fisKodu)
                                      .Sum(f => (decimal?)f.Toplam) ?? 0;

            txtAraToplam.Text = ara.ToString("C2");
            txtKdvToplam.Text = kdv.ToString("C2");
            txtGenelToplam.Text = genel.ToString("C2");
        }

        private void dgvFisAll_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFisAll.CurrentRow == null) return;

            int id = (int)dgvFisAll.CurrentRow.Cells["Id"].Value;
            SecilenFisAll = FisAllListesi.FirstOrDefault(f => f.Id == id);

            if (SecilenFisAll != null)
            {
                txtFisKodu.Text = SecilenFisAll.FisKodu;
                txtCari.Text = SecilenFisAll.Cari;
                txtOdemeTuru.Text = SecilenFisAll.OdemeTuru;
                dtpTarih.Value = SecilenFisAll.Tarih;
                ToplamlariGuncelle(SecilenFisAll.FisKodu);
            }
        }
        private void Temizle(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Clear();
                if (ctrl is ComboBox)
                    ((ComboBox)ctrl).SelectedIndex = -1;
                if (ctrl.HasChildren)
                    Temizle(ctrl);
            }
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle(this);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Yeni FisAll nesnesi oluştur
            var yeniFis = new FisAll
            {
                FisKodu = txtFisKodu.Text,
                Cari = txtCari.Text,
                OdemeTuru = txtOdemeTuru.Text,
                Tarih = dtpTarih.Value
            };
            // Listeye ekle
            FisAllListesi.Add(yeniFis);

            // DataGridView'i güncelle
            dgvFisAll.DataSource = null;
            dgvFisAll.DataSource = FisAllListesi.Select(f => new
            {
                f.Id,
                f.FisKodu,
                f.Cari,
                f.OdemeTuru,
                f.Tarih
            }).ToList();

            ToplamlariGuncelle(yeniFis.FisKodu);
            Temizle(this);
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var fis in FisAllListesi)
                {
                    // Eğer veritabanında yoksa ekle
                    if (!db.FisAll.Any(f => f.Id == fis.Id && f.Id != 0))
                    {
                        db.FisAll.Add(fis);
                    }
                }
                db.SaveChanges();

                MessageBox.Show("Kayıtlar başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Yeniden listele ve temizle
                Listele();
                Temizle(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

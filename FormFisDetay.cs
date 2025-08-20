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
    public partial class FormFisDetay : Form
    {
        private readonly int _fisId;                // Hangi fişe detay giriyoruz?
        private int? _seciliDetayId = null;         // Güncelle/Sil için seçilen detay

        // İsteğe bağlı: Parent form toplamları güncellesin diye callback
        public Action<decimal, decimal, decimal> ToplamlarDegisti { get; set; }
        public FormFisDetay(int? fisId = null)
        {
            InitializeComponent();
            using (var db = new MuhasebeDBEntities2())
            {
                if (fisId.HasValue)
                {
                    _fisId = fisId.Value;
                }
                else
                {
                    // Yeni fiş oluştur
                    var yeniFis = new Fis
                    {
                        FisKodu = "FIS-" + DateTime.Now.Ticks,
                        Cari = "Varsayılan",
                        OdemeTuru = "Açık Hesap",
                        Tarih = DateTime.Now
                    };
                    db.Fis.Add(yeniFis);
                    db.SaveChanges();
                    _fisId = yeniFis.Id;  // readonly alana ata
                }
            }

            Listele(); // DataGridView’i doldur
        }

        private void FormFisDetay_Load(object sender, EventArgs e)
        {
        }
        private void Listele()
        {
            using (var db = new MuhasebeDBEntities2())
            {
                var detaylar = db.FisDetay
                                 .Where(d => d.FisId == _fisId)
                                 .Select(d => new
                                 {
                                     d.Id,
                                     d.UrunKodu,
                                     d.UrunAdi,
                                     d.Miktar,
                                     d.BirimFiyat,
                                     d.Kdv,
                                     d.Toplam
                                 })
                                 .ToList();

                dgvFisDetay.AutoGenerateColumns = true;
                dgvFisDetay.DataSource = detaylar;

                if (dgvFisDetay.Columns["Id"] != null)
                    dgvFisDetay.Columns["Id"].Visible = false;

                dgvFisDetay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvFisDetay.RowTemplate.Height = 28;
            }

            HesaplaVeBildirToplamlar();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Zorunlu kontroller
            if (string.IsNullOrWhiteSpace(txtUrunKodu.Text) ||
                string.IsNullOrWhiteSpace(txtUrunAdi.Text) ||
                string.IsNullOrWhiteSpace(txtMiktar.Text) ||
                string.IsNullOrWhiteSpace(txtBirimFiyat.Text) ||
                string.IsNullOrWhiteSpace(txtKdvToplam.Text))
            {
                MessageBox.Show("Lütfen ürün kodu, adı, miktar, birim fiyat ve KDV alanlarını doldurun.");
                return;
            }

            if (!decimal.TryParse(txtMiktar.Text, out var miktar) ||
                !decimal.TryParse(txtBirimFiyat.Text, out var birimFiyat) ||
                !decimal.TryParse(txtKdvToplam.Text, out var kdv))
            {
                MessageBox.Show("Miktar, Birim Fiyat ve KDV oranı sayısal olmalıdır.");
                return;
            }

            try
            {
                using (var db = new MuhasebeDBEntities2())
                {
                    var yeni = new FisDetay
                    {
                        FisId = _fisId,                   // txtFisNo yok; parent’tan gelen fiş Id
                        UrunKodu = txtUrunKodu.Text.Trim(),
                        UrunAdi = txtUrunAdi.Text.Trim(),
                        Miktar = miktar,
                        BirimFiyat = birimFiyat,
                        Kdv = kdv
                        // Toplam'ı SET etmiyoruz: SQL computed column
                    };

                    db.FisDetay.Add(yeni);
                    db.SaveChanges();
                }

                TemizleSatirGiris();
                Listele();
                MessageBox.Show("Detay eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata (Ekle): " + ex.Message);
            }
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (_seciliDetayId == null)
            {
                MessageBox.Show("Lütfen önce listeden bir satır seçin.");
                return;
            }

            if (!decimal.TryParse(txtMiktar.Text, out var miktar) ||
                !decimal.TryParse(txtBirimFiyat.Text, out var birimFiyat) ||
                !decimal.TryParse(txtKdvToplam.Text, out var kdv))
            {
                MessageBox.Show("Miktar, Birim Fiyat ve KDV oranı sayısal olmalıdır.");
                return;
            }

            try
            {
                using (var db = new MuhasebeDBEntities2())
                {
                    var detay = db.FisDetay.FirstOrDefault(d => d.Id == _seciliDetayId.Value && d.FisId == _fisId);
                    if (detay == null)
                    {
                        MessageBox.Show("Seçilen detay bulunamadı.");
                        return;
                    }

                    detay.UrunKodu = txtUrunKodu.Text.Trim();
                    detay.UrunAdi = txtUrunAdi.Text.Trim();
                    detay.Miktar = miktar;
                    detay.BirimFiyat = birimFiyat;
                    detay.Kdv = kdv;
                    // Toplam yine SQL tarafından hesaplanır

                    db.SaveChanges();
                }

                TemizleSatirGiris();
                Listele();
                MessageBox.Show("Detay güncellendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata (Güncelle): " + ex.Message);
            }
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (_seciliDetayId == null)
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.");
                return;
            }

            var onay = MessageBox.Show("Seçili detayı silmek istiyor musunuz?",
                                       "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onay != DialogResult.Yes) return;

            try
            {
                using (var db = new MuhasebeDBEntities2())
                {
                    var detay = db.FisDetay.FirstOrDefault(d => d.Id == _seciliDetayId.Value && d.FisId == _fisId);
                    if (detay == null)
                    {
                        MessageBox.Show("Seçilen detay bulunamadı.");
                        return;
                    }

                    db.FisDetay.Remove(detay);
                    db.SaveChanges();
                }

                TemizleSatirGiris();
                Listele();
                MessageBox.Show("Detay silindi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata (Sil): " + ex.Message);
            }
        }
        private void dgvFisDetay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvFisDetay.Rows[e.RowIndex];

            _seciliDetayId = Convert.ToInt32(row.Cells["Id"].Value);

            txtUrunKodu.Text = row.Cells["UrunKodu"]?.Value?.ToString();
            txtUrunAdi.Text = row.Cells["UrunAdi"]?.Value?.ToString();
            txtMiktar.Text = row.Cells["Miktar"]?.Value?.ToString();
            txtBirimFiyat.Text = row.Cells["BirimFiyat"]?.Value?.ToString();
            txtKdvToplam.Text = row.Cells["Kdv"]?.Value?.ToString();
        }
        private void TemizleSatirGiris()
        {
            _seciliDetayId = null;
            txtUrunKodu.Clear();
            txtUrunAdi.Clear();
            txtMiktar.Clear();
            txtBirimFiyat.Clear();
            txtKdvToplam.Clear();
        }
        private void HesaplaVeBildirToplamlar()
        {
            using (var db = new MuhasebeDBEntities2())
            {
                // AraToplam = Σ (Miktar * BirimFiyat)
                var araToplam = db.FisDetay
                                  .Where(d => d.FisId == _fisId)
                                  .Select(d => (decimal?)(d.Miktar * d.BirimFiyat))
                                  .Sum() ?? 0m;

                // KdvToplam = Σ (Miktar * BirimFiyat * Kdv/100)
                var kdvToplam = db.FisDetay
                                  .Where(d => d.FisId == _fisId)
                                  .Select(d => (decimal?)(d.Miktar * d.BirimFiyat * (d.Kdv / 100m)))
                                  .Sum() ?? 0m;

                // GenelToplam = SQL computed Toplam’ların toplamı
                var genelToplam = db.FisDetay
                                    .Where(d => d.FisId == _fisId)
                                    .Select(d => (decimal?)d.Toplam)
                                    .Sum() ?? 0m;

                // Parent form isterse totals’ı textBox’larına yazsın
                ToplamlarDegisti?.Invoke(araToplam, kdvToplam, genelToplam);
            }
        }
    }
}

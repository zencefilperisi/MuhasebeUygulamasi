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
        private readonly int _fisId;
        private int? _seciliDetayId = null;

        public Action<decimal, decimal, decimal> ToplamlarDegisti { get; set; }

        public FormFisDetay(int fisId)
        {
            InitializeComponent();
            _fisId = fisId;
            Listele();
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
            if (!decimal.TryParse(txtMiktar.Text, out var miktar) ||
                !decimal.TryParse(txtBirimFiyat.Text, out var birimFiyat) ||
                !decimal.TryParse(txtKdvToplam.Text, out var kdv))
            {
                MessageBox.Show("Miktar, Birim Fiyat ve KDV sayısal olmalıdır.");
                return;
            }

            using (var db = new MuhasebeDBEntities2())
            {
                var yeni = new FisDetay
                {
                    FisId = _fisId,
                    UrunKodu = txtUrunKodu.Text.Trim(),
                    UrunAdi = txtUrunAdi.Text.Trim(),
                    Miktar = miktar,
                    BirimFiyat = birimFiyat,
                    Kdv = kdv
                };
                db.FisDetay.Add(yeni);
                db.SaveChanges();
            }

            TemizleSatirGiris();
            Listele();
            HesaplaVeBildirToplamlar();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (_seciliDetayId == null) return;

            if (!decimal.TryParse(txtMiktar.Text, out var miktar) ||
                !decimal.TryParse(txtBirimFiyat.Text, out var birimFiyat) ||
                !decimal.TryParse(txtKdvToplam.Text, out var kdv))
            {
                MessageBox.Show("Miktar, Birim Fiyat ve KDV sayısal olmalıdır.");
                return;
            }

            using (var db = new MuhasebeDBEntities2())
            {
                var detay = db.FisDetay.FirstOrDefault(d => d.Id == _seciliDetayId && d.FisId == _fisId);
                if (detay == null) return;

                detay.UrunKodu = txtUrunKodu.Text.Trim();
                detay.UrunAdi = txtUrunAdi.Text.Trim();
                detay.Miktar = miktar;
                detay.BirimFiyat = birimFiyat;
                detay.Kdv = kdv;

                db.SaveChanges();
            }

            TemizleSatirGiris();
            Listele();
            HesaplaVeBildirToplamlar();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (_seciliDetayId == null) return;

            using (var db = new MuhasebeDBEntities2())
            {
                var detay = db.FisDetay.FirstOrDefault(d => d.Id == _seciliDetayId && d.FisId == _fisId);
                if (detay == null) return;

                db.FisDetay.Remove(detay);
                db.SaveChanges();
            }

            TemizleSatirGiris();
            Listele();
            HesaplaVeBildirToplamlar();
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
                var araToplam = db.FisDetay
                                  .Where(d => d.FisId == _fisId)
                                  .Select(d => (decimal?)(d.Miktar * d.BirimFiyat))
                                  .Sum() ?? 0m;

                var kdvToplam = db.FisDetay
                                  .Where(d => d.FisId == _fisId)
                                  .Select(d => (decimal?)(d.Miktar * d.BirimFiyat * (d.Kdv / 100m)))
                                  .Sum() ?? 0m;

                var genelToplam = db.FisDetay
                                    .Where(d => d.FisId == _fisId)
                                    .Select(d => (decimal?)d.Toplam)
                                    .Sum() ?? 0m;

                ToplamlarDegisti?.Invoke(araToplam, kdvToplam, genelToplam);
            }
        }
    }
}

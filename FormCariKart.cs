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
    public partial class FormCariKart : Form
    {
        public CariKart SecilenCari { get; private set; }
        public FormCariKart()
        {
            InitializeComponent();
        }

        private void FormCariKart_Load(object sender, EventArgs e)
        {
            CarileriListele();
        }
        private void CarileriListele()
        {
            using (var db = new MuhasebeDBEntities2())
            {
                var cariler = db.CariKart.Select(c => new
                {
                    c.Id,
                    c.CariKodu,
                    c.CariAdi,
                    c.Telefon,
                    c.Adres,
                    c.Email
                }).ToList();

                dataGridCariler.DataSource = cariler;
                dataGridCariler.Columns["Id"].Visible = false;
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new MuhasebeDBEntities2())
                {
                    var yeniCari = new CariKart
                    {
                        CariKodu = txtCariKodu.Text,
                        CariAdi = txtCariAdi.Text,
                        Telefon = txtTelefon.Text,
                        Adres = txtAdres.Text,
                        Email = txtEmail.Text
                    };

                    db.CariKart.Add(yeniCari);
                    db.SaveChanges(); // Veritabanına kaydet
                }

                MessageBox.Show("Cari başarıyla eklendi.");
                CarileriListele(); // DataGridView güncelle
                Temizle(); // Textboxları temizle
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private void Temizle()
        {
            txtCariKodu.Text = "";
            txtCariAdi.Text = "";
            txtTelefon.Text = "";
            txtAdres.Text = "";
            txtEmail.Text = "";
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dataGridCariler.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridCariler.SelectedRows[0].Cells["Id"].Value);

                using (var db = new MuhasebeDBEntities2())
                {
                    var cari = db.CariKart.FirstOrDefault(c => c.Id == id);

                    if (cari != null)
                    {
                        cari.CariKodu = txtCariKodu.Text;
                        cari.CariAdi = txtCariAdi.Text;
                        cari.Telefon = txtTelefon.Text;
                        cari.Adres = txtAdres.Text;
                        cari.Email = txtEmail.Text;

                        db.SaveChanges();
                        MessageBox.Show("Cari başarıyla güncellendi.");
                        CarileriListele();
                        Temizle();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek için bir satır seçin.");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridCariler.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridCariler.SelectedRows[0].Cells["Id"].Value);

                var onay = MessageBox.Show("Seçili cariyi silmek istediğinize emin misiniz?",
                                           "Silme Onayı",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Warning);

                if (onay == DialogResult.Yes)
                {
                    using (var db = new MuhasebeDBEntities2())
                    {
                        var cari = db.CariKart.FirstOrDefault(c => c.Id == id);
                        if (cari != null)
                        {
                            db.CariKart.Remove(cari);
                            db.SaveChanges();
                            MessageBox.Show("Cari başarıyla silindi.");
                            CarileriListele(); // Listeyi yenile
                        }
                        else
                        {
                            MessageBox.Show("Seçilen cari bulunamadı.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.");
            }
        }
        private void AramaYap()
        {
            using (var db = new MuhasebeDBEntities2())
            {
                string aranan = txtAra.Text.Trim();

                var sonuc = db.CariKart
                              .Where(c => c.CariAdi.Contains(aranan) || c.CariKodu.Contains(aranan) ||
                                          c.Telefon.Contains(aranan) || c.Email.Contains(aranan) ||
                                          c.Adres.Contains(aranan))
                              .ToList();

                dataGridCariler.DataSource = sonuc;

                if (dataGridCariler.Columns["Id"] != null)
                    dataGridCariler.Columns["Id"].Visible = false;
                // Satır ve sütunları otomatik boyutlandır
                dataGridCariler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridCariler.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                dataGridCariler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // tüm sütunları eşit dağıt
                dataGridCariler.RowTemplate.Height = 30; // Satır yüksekliğini minimum artır
                dataGridCariler.Refresh(); // DataGridView’i yeniden çiz
            }
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            AramaYap();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            AramaYap();
        }

        private void dataGridCariler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridCariler.Rows[e.RowIndex].Cells["Id"].Value);
                using (var db = new MuhasebeDBEntities2())
                {
                    SecilenCari = db.CariKart.FirstOrDefault(c => c.Id == id);
                }
                this.DialogResult = DialogResult.OK; // 🔹 Seçim yapıldığını belirt
                this.Close();
            }
        }
    }
}

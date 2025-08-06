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
    public partial class FormStokKart : Form
    {
        SqlConnection conn = new SqlConnection("Server=HATICE\\SQLEXPRESS;Database=MuhasebeDB;Trusted_Connection=True;");
        public FormStokKart()
        {
            InitializeComponent();
        }
        private void FormStokKart_Load(object sender, EventArgs e)
        {
            using (var db = new MuhasebeDBEntities2())
            {
                if (!db.StokKart.Any())
                {
                    db.StokKart.Add(new StokKart { StokKodu = "TEST1", StokAdi = "Deneme Stok", Birim = "Adet", Barkod = "1111", KDV = 18, Aciklama = "Test" });
                    db.SaveChanges();
                }
            }

            StoklariListele();

            SetPlaceholder(txtStokKodu, "Stok Kodu");
            SetPlaceholder(txtStokAdi, "Stok Adı");
            SetPlaceholder(txtBirim, "Birim");
            SetPlaceholder(txtBarkod, "Barkod");
            SetPlaceholder(txtKDV, "KDV");
            SetPlaceholder(txtAciklama, "Açıklama");
        }
        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }


        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new MuhasebeDBEntities2())
                {
                    var yeniStok = new StokKart
                    {
                        StokKodu = txtStokKodu.Text,
                        StokAdi = txtStokAdi.Text,
                        Birim = txtBirim.Text,
                        Barkod = txtBarkod.Text,
                        KDV = Convert.ToDecimal(txtKDV.Text),
                        Aciklama = txtAciklama.Text
                    };

                    db.StokKart.Add(yeniStok);
                    db.SaveChanges(); // Veritabanına kaydet
                }

                MessageBox.Show("Stok başarıyla eklendi.");
                StoklariListele(); // DataGridView'ı yenile
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void StoklariListele()
        {
            using (var db = new MuhasebeDBEntities2())
            {
                var stoklar = db.StokKart.Select(s => new
                {
                    s.Id,
                    s.StokKodu,
                    s.StokAdi,
                    s.Birim,
                    s.Barkod,
                    s.KDV,
                    s.Aciklama
                }).ToList();

                dataGridStoklar.DataSource = stoklar;

                if (dataGridStoklar.Columns["Id"] != null)
                    dataGridStoklar.Columns["Id"].Visible = false;
            }
        }

        private void Temizle()
        {
            txtStokKodu.Text = "";
            txtStokAdi.Text = "";
            txtBirim.Text = "";
            txtBarkod.Text = "";
            txtKDV.Text = "";
            txtAciklama.Text = "";
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridStoklar.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridStoklar.SelectedRows[0].Cells["Id"].Value);

                var onay = MessageBox.Show("Seçili stoğu silmek istediğinize emin misiniz?",
                                           "Silme Onayı",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Warning);

                if (onay == DialogResult.Yes)
                {
                    using (var db = new MuhasebeDBEntities2())
                    {
                        var stok = db.StokKart.FirstOrDefault(s => s.Id == id);
                        if (stok != null)
                        {
                            db.StokKart.Remove(stok);
                            db.SaveChanges();
                            MessageBox.Show("Stok başarıyla silindi.");
                            StoklariListele(); // Listeyi yenile
                        }
                        else
                        {
                            MessageBox.Show("Seçilen stok bulunamadı.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.");
            }
        }

        private void dataGridStoklar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtStokKodu.Text = dataGridStoklar.Rows[e.RowIndex].Cells["StokKodu"].Value.ToString();
                txtStokAdi.Text = dataGridStoklar.Rows[e.RowIndex].Cells["StokAdi"].Value.ToString();
                txtBirim.Text = dataGridStoklar.Rows[e.RowIndex].Cells["Birim"].Value.ToString();
                txtBarkod.Text = dataGridStoklar.Rows[e.RowIndex].Cells["Barkod"].Value.ToString();
                txtKDV.Text = dataGridStoklar.Rows[e.RowIndex].Cells["KDV"].Value.ToString();
                txtAciklama.Text = dataGridStoklar.Rows[e.RowIndex].Cells["Aciklama"].Value.ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dataGridStoklar.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridStoklar.SelectedRows[0].Cells["Id"].Value);

                using (var db = new MuhasebeDBEntities2())
                {
                    var stok = db.StokKart.FirstOrDefault(s => s.Id == id);
                    if (stok != null)
                    {
                        stok.StokAdi = txtStokAdi.Text;
                        stok.Birim = txtBirim.Text;
                        stok.Barkod = txtBarkod.Text;
                        stok.KDV = Convert.ToDecimal(txtKDV.Text);
                        stok.Aciklama = txtAciklama.Text;

                        db.SaveChanges();
                        MessageBox.Show("Stok başarıyla güncellendi.");
                        StoklariListele(); // Listeyi yenile
                    }
                    else
                    {
                        MessageBox.Show("Seçilen stok bulunamadı.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek için bir satır seçin.");
            }
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            using (var db = new MuhasebeDBEntities2())
            {
                string aranan = txtAra.Text.Trim();

                var sonuc = db.StokKart
                              .Where(s => s.StokAdi.Contains(aranan) || s.StokKodu.Contains(aranan))
                              .ToList();

                dataGridStoklar.DataSource = sonuc;
                if (dataGridStoklar.Columns["Id"] != null)
                    dataGridStoklar.Columns["Id"].Visible = false;
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            string aranan = txtAra.Text.Trim();

            using (var db = new MuhasebeDBEntities2())
            {
                var sonuc = db.StokKart
                              .Where(s => s.StokAdi.Contains(aranan) || s.StokKodu.Contains(aranan))
                              .ToList();
                dataGridStoklar.DataSource = sonuc;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Tüm stok textbox'larını temizle ve placeholder'ı geri yükle
            ResetPlaceholder(txtStokKodu, "Stok Kodu");
            ResetPlaceholder(txtStokAdi, "Stok Adı");
            ResetPlaceholder(txtBirim, "Birim");
            ResetPlaceholder(txtBarkod, "Barkod");
            ResetPlaceholder(txtKDV, "KDV");
            ResetPlaceholder(txtAciklama, "Açıklama");
        }
        private void ResetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;
        }
    }
}

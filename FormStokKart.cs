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

namespace FormStokKart
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
            using (var db = new MuhasebeModel())
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
                using (var db = new MuhasebeModel())
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
            try
            {
                using (var db = new MuhasebeModel())
                {
                    var stoklar = db.StokKart.ToList();
                    dataGridStoklar.DataSource = stoklar;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatası: " + ex.Message);
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
            if (dataGridStoklar.CurrentRow != null)
            {
                string stokKodu = dataGridStoklar.CurrentRow.Cells["StokKodu"].Value.ToString();

                using (var db = new MuhasebeModel())
                {
                    var stok = db.StokKart.FirstOrDefault(s => s.StokKodu == stokKodu);
                    if (stok != null)
                    {
                        db.StokKart.Remove(stok);
                        db.SaveChanges();
                        MessageBox.Show("Stok başarıyla silindi.");
                        StoklariListele();
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
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE StokKart SET StokAdi=@adi, Birim=@birim, Barkod=@barkod, KDV=@kdv, Aciklama=@aciklama WHERE StokKodu=@kod", conn);
                cmd.Parameters.AddWithValue("@kod", txtStokKodu.Text);
                cmd.Parameters.AddWithValue("@adi", txtStokAdi.Text);
                cmd.Parameters.AddWithValue("@birim", txtBirim.Text);
                cmd.Parameters.AddWithValue("@barkod", txtBarkod.Text);
                cmd.Parameters.AddWithValue("@kdv", Convert.ToDecimal(txtKDV.Text));
                cmd.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Stok başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme hatası: " + ex.Message);
            }
            finally
            {
                conn.Close();
                StoklariListele();
                Temizle();
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            string aranan = txtAra.Text.Trim();

            using (var db = new MuhasebeModel())
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

        private void btnTemizle_Click(object sender, EventArgs e)
        {

        }
    }
}

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
    }
}

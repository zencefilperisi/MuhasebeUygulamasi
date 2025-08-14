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

namespace FormGiris.cs
{
    public partial class FormKullaniciYonetimi : Form
    {
        public FormKullaniciYonetimi()
        {
            InitializeComponent();
        }
        private void FormKullaniciYonetimi_Load(object sender, EventArgs e)
        {
            KullaniciListele();
        }
        private void KullaniciListele()
        {
            using (var db = new MuhasebeDBEntities2())
            {
                var kullanicilar = db.Users.Select(u => new
                {
                    u.Id,
                    u.KullaniciAdi,
                    u.Sifre,
                    u.AdSoyad,
                    u.Yetki
                }).ToList();

                dgvKullanicilar.DataSource = kullanicilar;

                if (dgvKullanicilar.Columns["Id"] != null)
                    dgvKullanicilar.Columns["Id"].Visible = false;

                dgvKullanicilar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvKullanicilar.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvKullanicilar.RowTemplate.Height = 30;
                dgvKullanicilar.Refresh();
            }
        }
        private void Temizle()
        {
            txtId.Clear();
            txtKullaniciAdi.Clear();
            txtSifre.Clear();
            txtAdSoyad.Clear();
            txtYetki.Clear();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            using (var db = new MuhasebeDBEntities2())
            {
                var yeniKullanici = new Users
                {
                    KullaniciAdi = txtKullaniciAdi.Text,
                    Sifre = txtSifre.Text,
                    AdSoyad = string.IsNullOrWhiteSpace(txtAdSoyad.Text) ? null : txtAdSoyad.Text,
                    Yetki = string.IsNullOrWhiteSpace(txtYetki.Text) ? null : txtYetki.Text
                };

                db.Users.Add(yeniKullanici);
                db.SaveChanges();
            }

            MessageBox.Show("Kullanıcı başarıyla eklendi!");
            KullaniciListele();
            Temizle();
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Lütfen güncellenecek kullanıcıyı seçin.");
                return;
            }

            using (var db = new MuhasebeDBEntities2())
            {
                int id = Convert.ToInt32(txtId.Text);
                var kullanici = db.Users.FirstOrDefault(u => u.Id == id);
                if (kullanici != null)
                {
                    kullanici.KullaniciAdi = txtKullaniciAdi.Text;
                    kullanici.Sifre = txtSifre.Text;
                    kullanici.AdSoyad = string.IsNullOrWhiteSpace(txtAdSoyad.Text) ? null : txtAdSoyad.Text;
                    kullanici.Yetki = string.IsNullOrWhiteSpace(txtYetki.Text) ? null : txtYetki.Text;

                    db.SaveChanges();
                    MessageBox.Show("Kullanıcı güncellendi!");
                    KullaniciListele();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Seçilen kullanıcı bulunamadı.");
                }
            }
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Lütfen silinecek kullanıcıyı seçin.");
                return;
            }

            var onay = MessageBox.Show("Seçili kullanıcıyı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (onay != DialogResult.Yes) return;

            using (var db = new MuhasebeDBEntities2())
            {
                int id = Convert.ToInt32(txtId.Text);
                var kullanici = db.Users.FirstOrDefault(u => u.Id == id);
                if (kullanici != null)
                {
                    db.Users.Remove(kullanici);
                    db.SaveChanges();
                    MessageBox.Show("Kullanıcı silindi!");
                    KullaniciListele();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Seçilen kullanıcı bulunamadı.");
                }
            }
        }
        private void dgvKullanicilar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKullanicilar.Rows[e.RowIndex];
                txtId.Text = row.Cells["Id"].Value.ToString();
                txtKullaniciAdi.Text = row.Cells["KullaniciAdi"].Value.ToString();
                txtSifre.Text = row.Cells["Sifre"].Value.ToString();
                txtAdSoyad.Text = row.Cells["AdSoyad"].Value?.ToString();
                txtYetki.Text = row.Cells["Yetki"].Value?.ToString();
            }
        }
        private void AramaYap()
        {
            using (var db = new MuhasebeDBEntities2())
            {
                string aranan = txtAra.Text.Trim();
                var sonuc = db.Users
                    .Where(u => u.KullaniciAdi.Contains(aranan) || u.AdSoyad.Contains(aranan) || u.Yetki.Contains(aranan))
                    .ToList();

                dgvKullanicilar.DataSource = sonuc;
                if (dgvKullanicilar.Columns["Id"] != null)
                    dgvKullanicilar.Columns["Id"].Visible = false;

                dgvKullanicilar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvKullanicilar.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvKullanicilar.RowTemplate.Height = 30;
                dgvKullanicilar.Refresh();
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
    }
}

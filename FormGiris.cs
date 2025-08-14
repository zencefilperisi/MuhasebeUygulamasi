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
    public partial class FormGiris : Form
    {

        SqlConnection conn = new SqlConnection("Server=HATICE\\SQLEXPRESS;Database=MuhasebeDB;Trusted_Connection=True;");
        public FormGiris()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string kadi = txtKullaniciAdi.Text.Trim();
            string sifre = txtSifre.Text;

            if (kadi == "" || sifre == "")
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz.");
                return;
            }

            try
            {
                conn.Open();

                // Önce kullanıcı var mı kontrol et
                string checkSql = "SELECT COUNT(*) FROM Kullanici WHERE KullaniciAdi = @kadi";
                SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@kadi", kadi);
                int userCount = (int)checkCmd.ExecuteScalar();

                if (userCount == 0)
                {
                    // Kullanıcı yoksa yeni kayıt ekle
                    string insertSql = "INSERT INTO Kullanici (KullaniciAdi, Sifre) VALUES (@kadi, @sifre)";
                    SqlCommand insertCmd = new SqlCommand(insertSql, conn);
                    insertCmd.Parameters.AddWithValue("@kadi", kadi);
                    insertCmd.Parameters.AddWithValue("@sifre", sifre);
                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarılı!");
                }

                // Kullanıcı giriş kontrolü
                string loginSql = "SELECT COUNT(*) FROM Kullanici WHERE KullaniciAdi = @kadi AND Sifre = @sifre";
                SqlCommand loginCmd = new SqlCommand(loginSql, conn);
                loginCmd.Parameters.AddWithValue("@kadi", kadi);
                loginCmd.Parameters.AddWithValue("@sifre", sifre);

                int girisBasarili = (int)loginCmd.ExecuteScalar();

                if (girisBasarili > 0)
                {
                    MessageBox.Show("Giriş başarılı!");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            FormAnaMenu anaMenu = new FormAnaMenu();
            anaMenu.Show();
            this.Hide(); // Giriş ekranını gizle
        }

    }
}

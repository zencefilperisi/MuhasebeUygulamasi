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
    public partial class FormUrunSec : Form
    {
        public string Kodu { get; set; }
        public string Adi { get; set; }
        public decimal Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public FormUrunSec()
        {
            InitializeComponent();
        }
        private void btnSec_Click(object sender, EventArgs e)
        {
            Kodu = txtUrunKodu.Text;
            Adi = txtUrunAdi.Text;
            Miktar = decimal.Parse(txtMiktar.Text);
            BirimFiyat = decimal.Parse(txtBirimFiyat.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dataGridViewUrunler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Kodu = dgvUrunler.Rows[e.RowIndex].Cells["Kodu"].Value.ToString();
                Adi = dgvUrunler.Rows[e.RowIndex].Cells["Adi"].Value.ToString();
                txtMiktar.Text = txtMiktar.ToString();
                BirimFiyat = Convert.ToDecimal(dgvUrunler.Rows[e.RowIndex].Cells["BirimFiyat"].Value);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            string urunAdi = txtUrunAdi.Text.Trim().ToLower();
            string urunKodu = txtUrunKodu.Text.Trim().ToLower();

            // DataGridView'in adı dgvUrunler ise örnek filtreleme:
            foreach (DataGridViewRow row in dgvUrunler.Rows)
            {
                bool urunAdiEslesiyor = row.Cells["UrunAdi"].Value.ToString().ToLower().Contains(urunAdi);
                bool urunKoduEslesiyor = row.Cells["UrunKodu"].Value.ToString().ToLower().Contains(urunKodu);

                row.Visible = urunAdiEslesiyor && urunKoduEslesiyor;
            }
        }
    }
}

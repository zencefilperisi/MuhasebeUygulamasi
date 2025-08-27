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
        public FormUrunSec()
        {
            InitializeComponent();
        }

        private void dataGridViewUrunler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Kodu = dgvUrunler.Rows[e.RowIndex].Cells["Kodu"].Value.ToString();
                Adi = dgvUrunler.Rows[e.RowIndex].Cells["Adi"].Value.ToString();
                Miktar = Convert.ToInt32(dgvUrunler.Rows[e.RowIndex].Cells["Miktar"].Value);
                BirimFiyat = Convert.ToDecimal(dgvUrunler.Rows[e.RowIndex].Cells["BirimFiyat"].Value);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}

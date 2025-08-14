using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FormGiris.cs
{
    public partial class FormDovizKurlari : Form
    {
        private Timer timer;
        public FormDovizKurlari()
        {
            InitializeComponent();
            InitializeTimer();
        }
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 5 * 60 * 1000; // 5 dakika = 300000 ms
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            GuncelleKurlar();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            GuncelleKurlar();
        }
        private void GuncelleKurlar()
        {
            try
            {
                string url = "https://www.tcmb.gov.tr/kurlar/today.xml";
                DataTable dt = new DataTable();
                dt.Columns.Add("Döviz Cinsi");
                dt.Columns.Add("Alış");
                dt.Columns.Add("Satış");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(url);

                XmlNodeList currencyList = xmlDoc.GetElementsByTagName("Currency");

                foreach (XmlNode currency in currencyList)
                {
                    string isim = currency["Isim"].InnerText;
                    string alis = currency["ForexBuying"].InnerText;
                    string satis = currency["ForexSelling"].InnerText;

                    dt.Rows.Add(isim, alis, satis);
                }

                dgvKurlar.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kurlar çekilemedi: " + ex.Message);
            }
        }

        private void FormDovizKurlari_Load(object sender, EventArgs e)
        {
            GuncelleKurlar(); // Form açılır açılmaz güncelle
        }
    }
}

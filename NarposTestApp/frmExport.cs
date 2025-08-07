using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NarposTestApp
{
    public partial class frmExport : Form
    {
        SaveFileDialog sfd = new SaveFileDialog();
        BindingList<Section> bindinglist;

        public frmExport(BindingList<Section> bindinglist)
        {
            this.bindinglist = bindinglist;
            InitializeComponent();
        }

        private void btnCsv_Click(object sender, EventArgs e)
        {
            sfd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            sfd.FileName = "NarPosTestReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
            if (sfd.ShowDialog() == DialogResult.OK)
                SaveToCSV(sfd);
                MessageBox.Show("Csv formatında kayıt gerçekleşti.", "Kayıt Durumu", MessageBoxButtons.OK);
        }

        private void btnTxt_Click(object sender, EventArgs e)
        {
            sfd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            sfd.FileName = "NarPosTestReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            if (sfd.ShowDialog() == DialogResult.OK) {
                SaveToTxt(sfd);
                MessageBox.Show("Txt formatında kayıt gerçekleşti.","Kayıt Durumu",MessageBoxButtons.OK);
            }
        }
        public void SaveToCSV(SaveFileDialog sfd)
        {
            var sb = new StringBuilder();
            var props = typeof(Section).GetProperties();
            sb.AppendLine(string.Join(",", props.Select(p => p.Name)));
            foreach (var item in bindinglist)
            {
                var satir = props.Select(p =>
                {
                    var val = p.GetValue(item, null);
                    if (val != null)
                    {
                        string str = val.ToString();
                        if (str.Contains(",") || str.Contains("\""))
                        {
                            str = "\"" + str.Replace("\"", "\"\"") + "\"";
                        }
                        return str;
                    }
                    else
                    {
                        return "";
                    }
                });

                sb.AppendLine(string.Join(",", satir));
            }
            File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF32);
        }

        private void SaveToTxt(SaveFileDialog sfd)
        {
            if (bindinglist == null || string.IsNullOrEmpty(sfd.FileName))
            {
                // Bindinglist ya da dosya adı yoksa işleme devam etme
                return;
            }

            StringBuilder stringBuilder = new StringBuilder();
            int propertyIndex = 1;

            foreach (var item in bindinglist)
            {
                // Durumu null kontrollü
                string statusStr = item.Status.HasValue
                    ? (item.Status.Value ? "Başarılı" : "Başarısız")
                    : "Geçersiz";
                string baslikStr = item.Title?.ToUpper() ?? "";
                string baslangicZamaniStr = item.StartTime.ToString();
                string bitisZamanıStr = item.EndTime.ToString();
                string hataMesajiStr = item.ErrorMessage ?? "";
                string gecenSurestr = item.Duration.ToString();
                stringBuilder.AppendLine(propertyIndex.ToString() + " " + baslikStr);

                for (int i = 0; i < 10; i++)
                    stringBuilder.Append("*");

                stringBuilder.AppendLine("Durumu : " + statusStr);
                stringBuilder.AppendLine("Başlangıç Zamanı : " + baslangicZamaniStr);
                stringBuilder.AppendLine("Bitiş Zamanı : " + bitisZamanıStr);
                stringBuilder.AppendLine("Geçen Süre : " + gecenSurestr);
                stringBuilder.AppendLine("Hata Mesajı : " + hataMesajiStr);
                stringBuilder.AppendLine();  // ekstra boş satır

                propertyIndex++;
            }

            File.WriteAllText(sfd.FileName, stringBuilder.ToString());
        }

    }
}

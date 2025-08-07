using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NarposTestApp
{
    public partial class frmNetwork : Form
    {
        private int totalRequests = 0;
        private int successCount = 0;
        private int failCount = 0;

        public frmNetwork()
        {
            InitializeComponent();
            this.Shown += FrmNetwork_Shown;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            //dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            this.dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void FrmNetwork_Shown(object sender, EventArgs e)
        {
            if (SharedState.Logger != null)
            {
                SharedState.Logger.ResponseLogged += OnResponseLogged;
            }
            else
            {
                MessageBox.Show("Logger null!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            UpdateVisibleRowCountLabel();

        }

        private void OnResponseLogged(string url, string method, int code, string statusText, string payload, string response,string time)
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.BeginInvoke(new Action(() =>
                {
                    AddRowToGrid(url, method, code, statusText, payload, response,time);
                }));
            }
            else
            {
                AddRowToGrid(url, method, code, statusText, payload, response, time);
            }
        }


        private void AddRowToGrid(string url, string method, int code, string statusText, string payload, string response, string time)
        {
            dataGridView1.Rows.Add(url, method, code, statusText, payload, response, time);


            totalRequests++;
            if (statusText.ToUpper() == "BAŞARISIZ")
                failCount++;
            else
                successCount++;


            lblTotalRequests.Text = $"Toplam: {totalRequests}";
            lblSuccessCount.Text = $"Başarılı: {successCount}";
            lblFailCount.Text = $"Başarısız: {failCount}";

          
            var lastRowIndex = dataGridView1.Rows.Count - 1;

            if (dataGridView1.AllowUserToAddRows)
                lastRowIndex--;

            if (lastRowIndex < 0)
                return; 

            var row = dataGridView1.Rows[lastRowIndex];

            if (comboBox3.SelectedItem?.ToString().ToUpper() == "BAŞARISIZ" &&
                statusText.ToUpper() == "BAŞARISIZ")
            {
                row.DefaultCellStyle.BackColor = Color.Red;
                row.DefaultCellStyle.ForeColor = Color.White;
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.White;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            UpdateVisibleRowCountLabel();

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            string methodFilter = comboBox1.SelectedItem?.ToString()?.Trim();
            string statusCodeFilter = comboBox2.SelectedItem?.ToString()?.Trim();
            string statusTextFilter = comboBox3.SelectedItem?.ToString()?.Trim();
            string searchKeyword = search.Text.Trim().ToLower();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                string method = row.Cells["method"].Value?.ToString()?.Trim();
                string statusCode = row.Cells["code"].Value?.ToString()?.Trim();
                string statusText = row.Cells["statusText"].Value?.ToString()?.Trim();
                string url = row.Cells["url"].Value?.ToString()?.Trim().ToLower() ?? "";

                bool methodMatches = string.IsNullOrEmpty(methodFilter) || methodFilter == "TÜMÜ" || method == methodFilter;
                bool statusCodeMatches = string.IsNullOrEmpty(statusCodeFilter) || statusCodeFilter == "TÜMÜ" || statusCode == statusCodeFilter;
                bool statusTextMatches = string.IsNullOrEmpty(statusTextFilter) || statusTextFilter == "TÜMÜ" || statusText == statusTextFilter;
                bool searchMatches = string.IsNullOrEmpty(searchKeyword) || url.Contains(searchKeyword);

                row.Visible = methodMatches && statusCodeMatches && statusTextMatches && searchMatches;
            }
            UpdateVisibleRowCountLabel();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var columnName = dataGridView1.Columns[e.ColumnIndex].Name;
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string value = cell.Value?.ToString() ?? "(boş)";

                if (columnName == "payload" || columnName == "response")
                {
                    value = FormatJson(value);
                }

                richTextBox1.Text = value;
            }
        }

        private string FormatJson(string json)
        {
            try
            {
                var parsedJson = Newtonsoft.Json.Linq.JToken.Parse(json);
                return parsedJson.ToString(Newtonsoft.Json.Formatting.Indented);
            }
            catch
            {
                return json; 
            }
        }
        private void ExportDataGridViewToCsv(DataGridView dgv, string filePath)
        {
            var sb = new StringBuilder();

            var headers = dgv.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(",", headers.Select(column => $"\"{column.HeaderText}\"")));

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow && row.Visible)
                {
                    var cells = row.Cells.Cast<DataGridViewCell>();
                    sb.AppendLine(string.Join(",", cells.Select(cell =>
                    {
                        var val = cell.Value?.ToString()?.Replace("\"", "\"\"") ?? "";
                        return $"\"{val}\"";
                    })));
                }
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            MessageBox.Show("CSV başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV dosyası (*.csv)|*.csv";
            saveFileDialog.Title = ".CSV Olarak Kaydet";
            saveFileDialog.FileName = "network_log.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportDataGridViewToCsv(dataGridView1, saveFileDialog.FileName);
            }
        }


        private void search_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
            string keyword = search.Text.Trim().ToLower();
            string keyword2 = "";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (row.IsNewRow || !row.Visible) continue;

                string url = row.Cells["url"].Value?.ToString().ToLower() ?? "";
                string payload = row.Cells["payload"].Value?.ToString().ToLower() ?? "";
                string response = row.Cells["response"].Value?.ToString().ToLower() ?? "";

                bool matches = url.Contains(keyword) || payload.Contains(keyword) || response.Contains(keyword);
                row.Visible = matches;

                if (!string.IsNullOrEmpty(keyword) && matches)
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                }
                else if (!string.IsNullOrEmpty(keyword2) && url.Contains(keyword2))
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
            UpdateVisibleRowCountLabel();
        }

        private void UpdateVisibleRowCountLabel()
        {
            int visibleRowCount = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Count(row => row.Visible && !row.IsNewRow);

            lblVisibleCount.Text = $"Görünen Kayıt: {visibleRowCount}";
        }

    }
}

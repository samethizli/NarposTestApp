using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NarposTestApp
{
    public partial class frmSectionChooser : Form
    {
        frmTestScreen f3;
        bool allSelected = true;

        public frmSectionChooser()
        {
            InitializeComponent();

            this.Load += frmSectionChooser_Load;

            button1.Click += button1_Click;
            button2.Click += button2_Click;
        }

        private void frmSectionChooser_Load(object sender, EventArgs e)
        {
            f3 = new frmTestScreen();
            //SharedState.fn = new frmNetwork();
            dataGridView1.DataSource = f3.Methods;

            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[0].HeaderText = "ÖZELLİK ADI";
            dataGridView1.Columns[2].HeaderText = "SEÇİM";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            button2.Text = "SEÇİMLERİN TÜMÜNÜ KALDIR";
            allSelected = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Hide();
            f3.Show();

            await Task.Delay(1400);

            if (SharedState.Driver == null)
            {
                MessageBox.Show("ChromeDriver henüz başlatılamadı. Lütfen frmTestScreen içinde driver'ı başlattığınızdan emin olun.");
                Show();
                return;
            }

            //if (SharedState.Logger == null)
            //{
            //    SharedState.Logger = new NetworkLogger();
            //    await SharedState.Logger.StartAsync();
            //    await Task.Delay(200);
            //}

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (allSelected)
            {
                for (int i = 0; i < f3.Methods.Count; i++)
                {
                    f3.Methods[i].IsEnabled = false;
                }

                button2.Text = "SEÇİMLERİN TÜMÜNÜ SEÇ";
                allSelected = false;
            }
            else
            {
                for (int i = 0; i < f3.Methods.Count; i++)
                {
                    f3.Methods[i].IsEnabled = true;
                }

                button2.Text = "SEÇİMLERİN TÜMÜNÜ KALDIR";
                allSelected = true;
            }

            dataGridView1.Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

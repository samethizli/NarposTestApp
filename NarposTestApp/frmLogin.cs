using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NarposTestApp
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains(".")&& textBox1.Text.Contains("@"))
            {
                UserInfo.Email = textBox1.Text;
                UserInfo.Password = textBox2.Text;
                frmSectionChooser f3 = new frmSectionChooser();
                f3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı E-Posta girişi");
            }

           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}

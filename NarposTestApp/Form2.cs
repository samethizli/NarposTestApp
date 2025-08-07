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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains(".")&& textBox1.Text.Contains("@"))
            {
                PrgInfo.Eposta = textBox1.Text;
                PrgInfo.Sifre = textBox2.Text;
                Form3 f3 = new Form3();
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
            textBox1.Text = "caninceli2@gmail.com";
            textBox2.Text = "Mrb12345";
        }
    }
}

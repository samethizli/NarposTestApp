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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "pos.narpos.com.tr";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && textBox1.Text.Contains("."))
            {
                if (!textBox1.Text.Contains("https://"))
                    PrgInfo.Adres = "https://" + textBox1.Text + "/";
                else
                    PrgInfo.Adres = textBox1.Text;
                Form2 f2 = new Form2();
                f2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Girdiğiniz site adı geçersiz!!!");
            }
        }
    }
}

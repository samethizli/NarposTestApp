using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NarposTestApp
{
    public partial class frmSetUrl : Form
    {
        public frmSetUrl()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "https://costbeta.narpos.com.tr/";
        }



        private void button1_Click(object sender, EventArgs e)
        {

                    UserInfo.Adress = textBox1.Text;

                frmLogin f2 = new frmLogin();
                f2.Show();
                this.Hide();

        }
    }
}

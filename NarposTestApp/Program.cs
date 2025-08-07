using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NarposTestApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmSetUrl());
        }
    }
}

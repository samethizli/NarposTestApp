using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NarposTestApp
{
    public class Section
    {

        private static frmTestScreen f3;
        private static BindingList<Section> list;

        private bool HasIndex = false;
        public int Index { get; private set; }
        public string Title { get; private set; }
        public bool? Status { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Duration { get; private set; }
        public string ErrorMessage { get; private set; }
        public Section(String title)
        {
            StartTime = DateTime.Now;
            Title = title;
            Status = null;
        }
        static Section()
        {
            list = new BindingList<Section>();
        }
        public void SetIndexToObject(int index)
        {
            if (!HasIndex)
            {
                this.Index = index;
                HasIndex = true;
            }
        }
        public static void SendtheFormInstance(frmTestScreen _f3)
        {
            f3 = _f3;
        }
        public void MarkAsCompleted()
        {
            EndTime = DateTime.Now;
            Status = true;
            Section.Update(this);
            IncreaseProgress();
            Duration = string.Concat((EndTime - StartTime).TotalSeconds.ToString(), " saniye");

        }
        public void MarkAsFailed(Exception ex)
        {
            Status = false;
            ErrorMessage = ex.Message;
            Section.Update(this);
            if (Application.OpenForms["frmTestScreen"].InvokeRequired)
            {
                Application.OpenForms["frmTestScreen"].Invoke(new MethodInvoker(() =>
                {
                    MessageBox.Show(ex.Message);
                }));
                File.WriteAllText(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/HATA_" + DateTime.Now.ToString() + ".txt",
                ex.Message.ToString()); 
            }
            else
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void EndTheSection() {
            if (f3.GetEx() == null)
            {
                MarkAsCompleted();

            }
            else
            {
                MarkAsFailed(f3.GetEx());
                f3.SetExNull();
            }
        }
        public void IncreaseProgress()
        {
            if (f3.progressBar1.InvokeRequired)
            {
                f3.progressBar1.Invoke(new MethodInvoker(() =>
                {
                    f3.progressBar1.Value += 10;
                }));
            }
            else
            {
                f3.progressBar1.Value += 10;
            }
        }
        public static Section Create(string title)
        {
            return new Section(title);

        }
        public static Section Add(string title)
        {
            f3.SetExNull();
            Section sct = new Section(title);

            if (Application.OpenForms["frmTestScreen"].InvokeRequired)
            {
                Application.OpenForms["frmTestScreen"].Invoke(new MethodInvoker(() =>
                {
                    list.Add(sct);
                    sct.SetIndexToObject(list.IndexOf(sct));
                }));
            }
            else
            {
                list.Add(sct);
                sct.SetIndexToObject(list.IndexOf(sct));
            }
            ScrollToEnd();

            return sct;
        }
        public static void Update(Section sct)
        {
            if (Application.OpenForms["frmTestScreen"].InvokeRequired)
            {
                Application.OpenForms["frmTestScreen"].Invoke(new MethodInvoker(() =>
                {
                    list[sct.Index] = sct;
                }));
            }
            else
            {
                list[sct.Index] = sct;
            }
        }
        public static void ScrollToEnd() {

            try
            {
                if (f3.dataGridView1.InvokeRequired)
                {
                    f3.dataGridView1.Invoke(new MethodInvoker(() =>
                    {
                        f3.dataGridView1.FirstDisplayedScrollingRowIndex = f3.dataGridView1.Rows.Count - 1;
                    }));
                }
                else
                {
                    f3.dataGridView1.FirstDisplayedScrollingRowIndex = f3.dataGridView1.Rows.Count - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA :"+ex.Message.ToString());
            }
        }
        public static BindingList<Section> GetList()
        {
            return list;
        }
    }

}

using System;
using System.Windows.Forms;

namespace NarposTestApp
{
    public class MethodItem
    {
        public string Name { get; set; }
        public Action Method { get; set; }
        public bool IsEnabled { get; set; }
       
    }
}
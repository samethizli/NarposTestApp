using OpenQA.Selenium.Chrome;

namespace NarposTestApp
{
    public static class SharedState
    {
        public static NetworkLogger Logger { get; set; }
        public static ChromeDriver Driver { get; set; }
        public static frmNetwork fn { get; set; }
    }
}

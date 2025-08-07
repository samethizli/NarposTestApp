using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace NarposTestApp
{
    public partial class Form3 : Form
    {
        static ChromeDriver driver;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            btn_Bitir.Visible = false;

        }
        private void Form3_Shown(object sender, EventArgs e)
        {
            try
            {
                ChromeDriverService service = ChromeDriverService.CreateDefaultService("C:\\Chrome");
                driver = new ChromeDriver(service);
                driver.Navigate().GoToUrl(PrgInfo.Adres);
                GirisYap();
                MenuOlustur();
                BirUrunEkle();
                OzellikGruplari();
                Yazici();
                RolTanimlama();
                btn_Bitir.Visible = true;

            }
            catch (Exception ex)
            {

                MessageBox.Show("HATA:" + ex.Message);
            }


        }
        public void GirisYap()
        {
            driver.Manage().Window.Maximize();
            driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-login-screen/body/div/div/div/div/div[1]/div[1]/div/div[3]/form/div[1]/div/input"), PrgInfo.Eposta);
            driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-login-screen/body/div/div/div/div/div[1]/div[1]/div/div[3]/form/div[2]/div/input"), PrgInfo.Sifre);
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-login-screen/body/div/div/div/div/div[1]/div[1]/div/div[3]/form/div[3]/div[1]/div/div/input"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-login-screen/body/div/div/div/div/div[1]/div[1]/div/div[3]/form/div[4]/button"));
            lbl1.Text += "✅";
            progressBar1.Value += 10;
            progressBar1.Refresh();

        }
        public void MenuOlustur()
        {
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-home-page/body/div/div[2]/app-header-buttons/div/button[3]"));
            //AYARLAR MENÜSÜ AÇILDI
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-menu-settings/div/div/div/div[3]/div/div[1]/div"));
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div[1]/input"), "TestMenu_" + DateTime.Now.ToString());
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[3]/div/button[2]"));
            EkrandakiYaziyaTikla("Tamam");

            //MENÜ OLUŞTURULDU
            lbl2.Text += "✅";
            progressBar1.Value += 10;
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/div/button[1]"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/div/div[1]/button[2]"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-category-settings/div/div/div/div[3]/div/div[1]/div/div[2]/div/div"));
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[1]/div[1]/div[3]/div/div[1]/input"), "İçecekler");
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[1]/div[1]/div[3]/div/div[2]/button/span[1]"));
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[3]/p-button[2]/button"));
            EkrandakiYaziyaTikla("Tamam");

            //KATEGORİ OLUŞTURMA BAŞLANGICI
            lbl3.Text += "✅";
            progressBar1.Value += 10;
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-category-settings/div/div/div/div[3]/div/div[1]/div/div[3]/mat-accordion/mat-expansion-panel/mat-expansion-panel-header"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-category-settings/div/div/div/div[3]/div/div[1]/div/div[3]/mat-accordion/mat-expansion-panel/mat-expansion-panel-header/span[1]/mat-panel-description/button[2]"));
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[1]/div[1]/div[3]/div/div[1]/input"), "Alkolsüz İçecekler");
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[1]/div[1]/div[3]/div/div[2]/button/span[1]"));
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[1]/div[1]/div[3]/div/div[1]/input"), "Alkollü İçecekler");
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[1]/div[1]/div[3]/div/div[2]/button/span[1]"));
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[3]/p-button[2]/button"));
            EkrandakiYaziyaTikla("Tamam");
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/button"));
            //KATEGORİ OLUŞTURULDU
            lbl4.Text += "✅";
            progressBar1.Value += 10;
            

        }
        public void BirUrunEkle() {
            string[] icecekler = new string[] { "Kola", "Fanta", "Sprite", "Ayran", "Kahve", "Limonata", "Meyve Suyu" };
            int[] fiyatlar = new int[] { 20, 18, 18, 10, 12, 15, 14 };
            string[] alkolluIcecekler = new string[] { "Bira", "Şarap", "Votka"};
            int[] alkolluFiyatlar = new int[] {70, 60, 80};
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-home-page/body/div/div[2]/app-header-buttons/div/button[3]"));
            //AYARLAR MENÜSÜ AÇILDI
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/div/button[1]"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/div/div[1]/button[3]"));
            int i = 0;
            //ÜRÜN EKLEME BAŞLANGICI ALKOLSÜZ TEK ÜRÜN EKLE
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-product-settings/div/div/div/div[2]/div[1]/button"));
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div/mat-tab-group/div/mat-tab-body[1]/div/div/div/div[1]/input"), icecekler[i]);
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div/mat-tab-group/div/mat-tab-body[1]/div/div/div/div[2]/p-inputnumber/span/input"), fiyatlar[i].ToString());
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div/mat-tab-group/div/mat-tab-body[1]/div/div/div/div[5]/input"), icecekler[i].Substring(0, 2).ToUpper() + icecekler[i].Substring(icecekler[i].Length - 2).ToUpper() + "_" + DateTime.Now.ToString("ssfff"));
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[3]/p-button[2]/button"));
            EkrandakiYaziyaTikla("Tamam");
            i++;

            //TOPLU ÜRÜN EKLEME BAŞLANGICI
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-product-settings/div/div/div/div[2]/div[3]/button"));
            for (; i < icecekler.Length; i++)
            {

                driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div[1]/div/div/div[1]/input"), icecekler[i]);
                driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div[1]/div/div/div[3]/p-inputnumber/span/input"), fiyatlar[i].ToString());
                driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div[1]/div/div/div[2]/input"), icecekler[i].Substring(0, 2).ToUpper() + icecekler[i].Substring(icecekler[i].Length - 2).ToUpper() + "_" + DateTime.Now.ToString("ssfff"));
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[2]/div/div[1]/div/div/div[4]/p-button/button"));
            }

            driver.WaitAndClick(By.XPath("/html/body/div/div/div[3]/p-button[2]/button"));
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[3]/p-button[2]"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-product-settings/div/div/div/div[3]/div[2]/div[2]/button"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-product-settings/div/div/div/div[2]/div[3]/button"));
            for ( i=0; i < alkolluIcecekler.Length; i++)
            {

                driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div[1]/div/div/div[1]/input"), alkolluIcecekler[i]);
                driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div[1]/div/div/div[3]/p-inputnumber/span/input"), alkolluFiyatlar[i].ToString());
                driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div[1]/div/div/div[2]/input"), alkolluIcecekler[i].Substring(0, 2).ToUpper() + alkolluIcecekler[i].Substring(alkolluIcecekler[i].Length - 2).ToUpper() + "_" + DateTime.Now.ToString("ssfff"));
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[2]/div/div[1]/div/div/div[4]/p-button/button"));
            }
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[3]/p-button[2]"));
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[3]/p-button[2]/button"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/button"));
            lbl5.Text += "✅";
            progressBar1.Value += 10;


        }
        public void OzellikGruplari()
        {
            string[] boyut = new string[] { "Küçük", "Orta", "Büyük" };
            int[] ucret = new int[] {5,10,15};
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-home-page/body/div/div[2]/app-header-buttons/div/button[3]"));
            //AYARLAR MENÜSÜ AÇILDI
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/div/button[1]"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/div/div[1]/button[4]"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-feature-groups/div/div/div/div[3]/div/div[1]/div/div/div"));
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div/div/div[2]/input"),"İçecek Boyutları" + DateTime.Now.ToString("ssfff"));
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div/div/div[3]/input"), "ICECEKBOYUTLARI_"+ DateTime.Now.ToString("ssfff"));
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div/div/div[4]/input"), "İçecek Seçimi");
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[2]/div/div/div/div[7]/div/label/div/input"));

            int j = 11;
            for (int i = 0; i <= 2; i++) 
            {
                
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[2]/div/div/div/div[10]/button"));
                driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div/div/div["+j+"]/div[1]/input"), boyut[i]);
                driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div/div/div["+j+"]/div[2]/p-inputnumber/span/input"), ucret[i].ToString());
                j++;

            }

            driver.WaitAndClick(By.XPath("/html/body/div/div/div[3]/p-button[2]/button"));
            EkrandakiYaziyaTikla("Tamam");

            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/button"));
            lbl6.Text += "✅";
            progressBar1.Value += 10;
        }

        public void Yazici()
        {
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-home-page/body/div/div[2]/app-header-buttons/div/button[3]"));
            //AYARLAR MENÜSÜ AÇILDI
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/div/button[6]"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/div/div[6]/button[1]"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-printer-settings/div/div/div/div[2]/div/div/button"));
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div/div/div/div/div[1]/input"), "Yazici"+DateTime.Now);
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div/div/div/div/div[2]/input"),"192.168.1.102");
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[3]/p-button[2]/button"));
            //YeniYaziciEklendi
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/div/div[6]/button[3]"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-printer-redirection/div/div/div/div[2]/div/div[1]/p-dropdown/div/div[2]"));
            driver.WaitAndClick(By.XPath("/html/body/div/div/div/div/ul/p-dropdownitem[1]/li"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-printer-redirection/div/div/div/div[3]/div/div[1]/p-multiselect/div/div[3]"));
            driver.WaitAndClick(By.XPath("/html/body/div/div/div/div[2]/ul/p-multiselectitem[1]/li"));

            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-printer-redirection/div/div/div/div[3]/div/div[1]/button"));

            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-printer-redirection/div/div/div/div[3]/div/div[2]/p-multiselect/div/div[3]"));
            driver.WaitAndClick(By.XPath("/html/body/div/div/div/div[2]/ul/p-multiselectitem/li"));

            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-printer-redirection/div/div/div/div[2]/div/div[4]/button"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/button"));
            EkrandakiYaziyaTikla("Tamam");
            lbl7.Text += "✅";
            progressBar1.Value += 10;
        }
        public void RolTanimlama()
        {

            driver.WaitAndClick(By.XPath("/html/body/app-root/app-home-page/body/div/div[2]/app-header-buttons/div/button[3]"));
            //AYARLAR MENÜSÜ AÇILDI
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/div/button[5]"));

            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-permission-settings/div/div/div/div[2]/div[1]/div[1]/button"));
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div[1]/input"),"Rol"+DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"));
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[3]/p-button[2]/button"));

            ///ROL TANIMLANDIKTAN SONRA İZİNLERİ DEĞİŞTİRME
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-permission-settings/div/div/div/div[3]/div/div/div[2]/div[1]/div[2]/p-checkbox/div/div[2]"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-permission-settings/div/div/div/div[3]/div/div/div[2]/div[7]/div[2]/p-checkbox/div/div[2]"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-permission-settings/div/div/div/div[2]/div[2]/button"));

            ///ROL VE İLGİLİ İZİNLERİ TANIMLANDI*/
            ///KULLANICI TANIMLAMALARININ BAŞLANGICI
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/div/button[4]"));
            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav-content/div/app-user-settings/div/div/div/div[2]/div/div/button"));
            string name = string.Concat(DateTime.UtcNow.ToString("yyyyMMddHHmmssfff").Select(c => (char)('A' + (c - '0'))));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            string phoneNumber = "5" + new Random().Next(300000000, 599999999).ToString();
            var phoneInput = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div[3]/div/p-inputmask/input"));
            js.ExecuteScript(@"
    arguments[0].value = arguments[1];
    arguments[0].dispatchEvent(new Event('input'));
    arguments[0].dispatchEvent(new Event('change'));
", phoneInput, phoneNumber);

            // 2. 4 haneli rastgele PIN kodu
            string pinCode = new Random().Next(1000, 9999).ToString();
            var pinInput = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div[5]/p-inputmask/input"));
            js.ExecuteScript(@"
    arguments[0].value = arguments[1];
    arguments[0].dispatchEvent(new Event('input'));
    arguments[0].dispatchEvent(new Event('change'));
", pinInput, pinCode);


            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div[1]/input"), name);
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div[4]/input"),name+"@mail.com");
            driver.WaitAndSendKeys(By.XPath("/html/body/div/div/div[2]/div/div[6]/input"), SifreUret());
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[2]/div/div[2]/div/p-dropdown/div/div[2]"));
            driver.WaitAndClick(By.XPath("/html/body/div/div/div[2]/div/div[2]/div/p-dropdown/div/p-overlay/div/div/div/div/ul/p-dropdownitem[1]/li"));

            driver.WaitAndClick(By.XPath("/html/body/div/div/div[3]/p-button[2]/button"));//KAYDET BUTONU

            driver.WaitAndClick(By.XPath("/html/body/app-root/app-main-sidebar/mat-sidenav-container/mat-sidenav/div/button"));


            // 1. Telefon numarası (örneğin maske: (999) 999-9999, ama siz '0' istemiyorsunuz)



            lbl8.Text += "✅";
            progressBar1.Value += 10;
        }
        public void Cikis()
        {
           
            driver.Quit();
            driver.Dispose();

            
        }

        
        public void BringFormToFront()
        {
            this.TopMost = true;
            this.Activate();
            this.BringToFront();

        }

        public static string SifreUret(int uzunluk = 8)
        {
            if (uzunluk < 8)
                throw new ArgumentException("Şifre uzunluğu en az 8 karakter olmalıdır.");

            const string kucukHarfler = "abcdefghijklmnopqrstuvwxyz";
            const string buyukHarfler = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string rakamlar = "0123456789";
            string tumKarakterler = kucukHarfler + buyukHarfler + rakamlar;

            Random rnd = new Random();
            StringBuilder sifre = new StringBuilder();

            // Zorunlu karakterleri ekle
            sifre.Append(kucukHarfler[rnd.Next(1,kucukHarfler.Length)]);
            sifre.Append(buyukHarfler[rnd.Next(1,buyukHarfler.Length)]);
            sifre.Append(rakamlar[rnd.Next(1,rakamlar.Length)]);

            // Geri kalan karakterleri rastgele doldur
            for (int i = 3; i < uzunluk; i++)
            {
                sifre.Append(tumKarakterler[rnd.Next(tumKarakterler.Length)]);
            }

            // Karakterleri karıştır (daha rastgele görünmesi için)
            return Karistir(sifre.ToString(), rnd);
        }

        private static string Karistir(string input, Random rnd)
        {
            char[] array = input.ToCharArray();
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
            return new string(array);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Cikis();
            Application.Exit();
        }

        public void EkrandakiYaziyaTikla(string text)
        {
            // Buton hazır olana kadar bekle
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d =>
            {
                var js = (IJavaScriptExecutor)d;
                return (bool)js.ExecuteScript($@"
            return Array.from(document.querySelectorAll('button'))
                .some(btn => btn.innerText.trim() === '{text.Replace("'", "\\'")}');
        ");
            });

            // Butona tıkla
            IJavaScriptExecutor jsClick = (IJavaScriptExecutor)driver;
            jsClick.ExecuteScript($@"
        let buttons = document.querySelectorAll('button');
        for (let btn of buttons) {{
            if (btn.innerText.trim() === '{text.Replace("'", "\\'")}') {{
                btn.click();
                break;
            }}
        }}
    ");
        }

}
}

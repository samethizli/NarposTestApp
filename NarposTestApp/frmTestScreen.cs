using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NarposTestApp
{
    public partial class frmTestScreen : Form
    {
        public BindingList<MethodItem> Methods;
        public static ChromeDriver driver;
        public static DevToolsSession devToolsSession;
        public static DevToolsSessionDomains devTools;
        private static Section section;
        private static Exception _ex = null;
        Random rnd = new Random();

        public frmTestScreen()
        {
            InitializeComponent();
            methodsToList();

        }
        private void Form3_Load(object sender, EventArgs e)
        {
            menuStrip1.Enabled = false;
        }

        public void methodsToList()
        {
            Methods = new BindingList<MethodItem>
    {
        new MethodItem { Name = "Şube Oluştur", Method = AddBranch, IsEnabled = true },
        new MethodItem { Name = "Kullanıcı Oluştur", Method = AddUser, IsEnabled = true },
        new MethodItem { Name = "Rol Oluştur", Method = AddRole, IsEnabled = true },
        new MethodItem { Name = "Depo Oluştur", Method = AddWareHouse, IsEnabled = true },
        new MethodItem { Name = "Vergi Oluştur", Method = AddTax, IsEnabled = true },
        new MethodItem { Name = "Birim Oluştur", Method = AddUnit, IsEnabled = true },
        new MethodItem { Name = "Dönüşüm Oluştur", Method = AddConversion, IsEnabled = true },
        new MethodItem { Name = "Stok Grupları Oluştur", Method = AddStockGroup, IsEnabled = true },
        new MethodItem { Name = "Stok Kartları Oluştur", Method = AddStockCard, IsEnabled = true },
        new MethodItem { Name = "Cari Grubu Oluştur", Method = AddCariGroup, IsEnabled = true },
        new MethodItem { Name = "Cariler Oluştur", Method = AddCari, IsEnabled = true },
        new MethodItem { Name = "Liste Grubu Oluştur", Method = AddListGroup, IsEnabled = true },
        new MethodItem { Name = "Listeler Oluştur", Method = AddLists, IsEnabled = true },
        new MethodItem { Name = "Portföy Oluştur", Method = AddPortfolio, IsEnabled = true },
        new MethodItem { Name = "Alış Faturası Oluştur", Method = AddPurchaseİnvoice, IsEnabled = true },
        new MethodItem { Name = "Satış Faturası Oluştur", Method = AddSalesİnvoice, IsEnabled = true },
        new MethodItem { Name = "Zayi Grupları Oluştur", Method = AddLossGroup, IsEnabled = true },
        new MethodItem { Name = "Zayi Oluştur", Method = AddLoss, IsEnabled = true },
        new MethodItem { Name = "Üretim İşlemleri Oluştur", Method = AddProductionProcesses, IsEnabled = true },
        new MethodItem { Name = "Sayım İşlemleri Oluştur", Method = AddCountingOperations, IsEnabled = true },
        new MethodItem { Name = "Transfer İşlemleri Oluştur", Method = AddTransferTransactions, IsEnabled = true },
        //new MethodItem { Name = "Kasap İşlemleri Oluştur", Method = ButcherOperations, IsEnabled = true }



    };
        }
       
       
        private void EnsureFirmaYonetimiIsOpen()
        {
            var altMenuElement = driver.WaitAndFindElements(By.XPath("//*[@id='sidebar-menu']/li[1]/ul/li")).FirstOrDefault();

            if (altMenuElement != null && altMenuElement.Displayed)
                return;

            driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[1]/a"));
            Thread.Sleep(300);
        }

        private void EnsureAnaVerilerIsOpen()
        {
            var altMenuElement = driver.WaitAndFindElements(By.XPath("//*[@id='sidebar-menu']/li[2]/ul/li")).FirstOrDefault();

            if (altMenuElement != null && altMenuElement.Displayed)
                return;

            driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[2]/a"));
            Thread.Sleep(300);
        }

        private void EnsureMaliyetYonetimiIsOpen()
        {
            var altMenuElement = driver.WaitAndFindElements(By.XPath("//*[@id='sidebar-menu']/li[3]/ul/li")).FirstOrDefault();

            if (altMenuElement != null && altMenuElement.Displayed)
                return;

            driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[3]/a"));
            Thread.Sleep(300);
        }

        private void Login()
        {
            section = Section.Add("Giriş Yapıldı");
            try
            {
                driver.Manage().Window.Maximize();
                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-login/div[1]/div/div/div/div/div/div/div/div/div[1]/form/div[1]/input"), UserInfo.Email);
                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-login/div[1]/div/div/div/div/div/div/div/div/div[1]/form/div[2]/input"), UserInfo.Password);
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-login/div[1]/div/div/div/div/div/div/div/div/div[1]/form/div[4]/button"));
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }
        private void AddBranch()
        {
            section = Section.Add("Şubeler Tanımlandı");

            try
            {
                string randomVergiNo = rnd.Next(100000000, 999999999).ToString() + rnd.Next(10, 99).ToString();

                EnsureFirmaYonetimiIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[1]/ul/li[1]/a/span"));
                driver.WaitAndClick(By.XPath("//*[@id=\"pr_id_6\"]/div[1]/div/div/button[2]"));
                Thread.Sleep(350);

                string name = RandomNameHelper.GenerateRandomName();
                driver.WaitAndSendKeys(By.Id("name"), "Rssctgdaow");
                Thread.Sleep(600);
                driver.WaitAndSendKeys(By.Id("vkn"), randomVergiNo);
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-createbranch/p-dialog/div/div/div[4]/div/button[2]"));
                Thread.Sleep(1000);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                Thread.Sleep(400);

                
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-createbranch/p-dialog/div/div/div[4]/div/button[1]"));
                Thread.Sleep(700);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }



        private void AddUser()
        {
            section = Section.Add("Kullanıcı Tanımlandı");
            try
            {
                EnsureFirmaYonetimiIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[1]/ul/li[2]/a"));
                driver.WaitAndClick(By.XPath("//button[.//span[contains(text(), 'Yeni Ekle')]]\r\n"));

                string name = RandomNameHelper.GenerateRandomName();
                string surname = RandomNameHelper.GenerateRandomName();
                string email = RandomNameHelper.GenerateRandomName();
                string sifre = RandomPassHelper.GenPass();
                string not = RandomNameHelper.GenerateRandomName();
                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-add-sub-user/p-dialog/div/div/div[3]/div/div[1]/input"), name);
                Thread.Sleep(400);
                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-add-sub-user/p-dialog/div/div/div[3]/div/div[2]/input"), surname);
                Thread.Sleep(400);
                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-add-sub-user/p-dialog/div/div/div[3]/div/div[3]/input"), email + "@gmail.com");
                Thread.Sleep(600);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var phoneInput = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/app-root/app-add-sub-user/p-dialog/div/div/div[3]/div/div[4]/p-inputmask/input")));
                phoneInput.Clear();
                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-add-sub-user/p-dialog/div/div/div[3]/div/div[4]/p-inputmask/input"), "5" + new Random().Next(100000000, 999999999).ToString());
                Thread.Sleep(400);
                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-add-sub-user/p-dialog/div/div/div[3]/div/div[5]/input"), sifre);



                //var roleDropdown = driver.WaitAndFindElement(By.XPath("//label[contains(text(), 'Roller')]/following-sibling::p-multiselect//div[contains(@class, 'p-multiselect-trigger')]"));

                //roleDropdown?.Click();

                //var roleListContainer = driver.WaitAndFindElement(By.CssSelector("ul.p-multiselect-items"));

                //if (roleListContainer != null)
                //{
                //    var roleItems = roleListContainer.FindElements(By.CssSelector("li.p-multiselect-item")).ToList();

                //    if (roleItems.Any())
                //    {
                //        int selectCount = rnd.Next(1, roleItems.Count + 1);

                //        var selectedIndexes = Enumerable.Range(1, roleItems.Count)
                //                                        .OrderBy(x => rnd.Next())
                //                                        .Take(selectCount);

                //        foreach (var k in selectedIndexes)
                //        {
                //            try
                //            {
                //                var checkbox = roleItems[k].FindElement(By.CssSelector("div.p-checkbox-box"));
                //                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", checkbox);
                //                checkbox.Click();
                //            }
                //            catch
                //            {

                //            }
                //        }
                //    }
                //}
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-sub-user/p-dialog/div/div/div[3]/div/div[6]/p-multiselect"));

                var roller = driver.WaitAndFindElements(By.CssSelector("li.p-multiselect-item"));
                int kacTaneSecilecek = rnd.Next(1, roller.Count + 1);
                var secilecekler = roller.OrderBy(x => rnd.Next()).Take(kacTaneSecilecek).ToList();
                Thread.Sleep(400);

                foreach (var rol in secilecekler)
                {
                    var chechbox = rol.FindElement(By.CssSelector("li.p-multiselect-item .p-checkbox-box"));
                    driver.Click(rol);
                }

                Thread.Sleep(400);

                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-add-sub-user/p-dialog/div/div/div[3]/div/div[7]/input"), not);
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-sub-user/p-dialog/div/div/div[4]/div/button[2]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));

                Thread.Sleep(400);

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-sub-user/p-dialog/div/div/div[4]/div/button[1]/span[2]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }

        }
        private void AddRole()
        {
            section = Section.Add("Rol Tanımlandı");
            try
            {
                EnsureFirmaYonetimiIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[1]/ul/li[3]/a/span"));
                driver.WaitAndClick(By.XPath("//button[.//span[contains(text(), 'Yeni Ekle')]]\r\n"));
                driver.WaitAndClick(By.XPath("//*[@id='p-accordiontab-0']/div[1]"));


                string roleName = RandomNameHelper.GenerateRandomName();
                driver.WaitAndSendKeys(By.Id("majorGroupItemName"), roleName);
                Thread.Sleep(400);

                var allCheckboxes = driver.WaitAndFindElements(
                    By.XPath("//*[@id='p-accordiontab-0-content']//p-checkbox//div[contains(@class,'p-checkbox-box')]"));

                if (!allCheckboxes.Any())
                {
                    Console.WriteLine("Hiç checkbox bulunamadı! Accordion açık mı?");
                }
                else
                {
                    int selectCount = rnd.Next(1, allCheckboxes.Count + 1);

                    var randomCheckboxes = allCheckboxes.OrderBy(x => rnd.Next()).Take(selectCount);


                    foreach (var checkbox in randomCheckboxes)
                    {
                        try
                        {
                            driver.Click(checkbox);
                            Thread.Sleep(300);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Checkbox tıklanamadı: {ex.Message}");
                        }
                    }
                }

                Thread.Sleep(400);


                driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-role/p-dialog/div/div/div[3]/p-multiselect/div/div[2]/div"));

                var multiSelectListParent = driver.WaitAndFindElements(By.CssSelector("ul.p-multiselect-items"));
                Thread.Sleep(400);

                if (multiSelectListParent != null)
                {
                    var items = driver.WaitAndFindElements(By.CssSelector("li.p-multiselect-item")).ToList();

                    if (items.Any())
                    {
                        int selectBranchCount = rnd.Next(1, Math.Min(2, items.Count) + 1);

                        var selectedBranchIndexes = Enumerable.Range(0, items.Count)
                                                              .OrderBy(x => rnd.Next())
                                                              .Take(selectBranchCount);

                        foreach (int j in selectedBranchIndexes)
                        {
                            try
                            {
                                var checkbox = items[j].FindElement(By.CssSelector("div.p-checkbox-box"));
                                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", checkbox);
                                checkbox.Click();
                                Thread.Sleep(300);
                            }
                            catch
                            {
                            }
                        }
                    }
                }

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-role/p-dialog/div/div/div[4]/div/button[2]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                Thread.Sleep(400);

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-role/p-dialog/div/div/div[4]/div/button[1]/span[1]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));

            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }


        private void AddWareHouse()
        {
            section = Section.Add("Depo Tanımlandı");
            try
            {
                EnsureAnaVerilerIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[2]/ul/li[1]/a/span"));
                Thread.Sleep(300);
                driver.WaitAndClick(By.XPath("//button[.//span[contains(text(), 'Yeni Ekle')] and .//i[contains(@class, 'fa-square-plus')]]"));
                for (int i = 0; i < 2; i++)
                {
                    string name = RandomNameHelper.GenerateRandomName();
                    string Address = RandomNameHelper.GenerateRandomName();
                    driver.WaitAndSendKeys(By.Id("name"), name);

                    Thread.Sleep(400);

                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-createwarehouse/p-dialog/div/div/div[3]/div[3]/p-dropdown"));
                    Thread.Sleep(300);
                    var warehouse = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    Thread.Sleep(300);
                    var randomWareHouse = warehouse[rnd.Next(warehouse.Count)];
                    driver.Click(randomWareHouse);



                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-createwarehouse/p-dialog/div/div/div[3]/div[3]/p-dropdown"));
                    Thread.Sleep(400);
                    var affiliatedBranch = driver.WaitAndFindElements(
                    By.XPath("//h6[contains(text(), 'Bağlı Olduğu Şube')]/following-sibling::p-dropdown//div[contains(@class,'p-dropdown-trigger')]"));
                    Thread.Sleep(400);
                    var randomAffiliatedBranch = affiliatedBranch[rnd.Next(affiliatedBranch.Count)];
                    driver.Click(randomAffiliatedBranch);


                    Thread.Sleep(400);


                    driver.WaitAndSendKeys(By.Id("address"), Address);
                    Thread.Sleep(500);
                    driver.WaitAndSendKeys(By.Id("email"), name + "@gmail.com");
                    Thread.Sleep(400);

                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"no\"]/input"), "5" + new Random().Next(300000000, 599999999).ToString());


                    Thread.Sleep(1000);

                    var checkboxIds = new Dictionary<string, string>
{
    { "Transferde Üretim", "productionInTransfer" },
    { "Kullanım Deposu", "usageWarehouse" },
    { "Zayi Deposu", "wasteWarehouse" },
    { "Sayımda Kullan", "selectedCloseType" }
};

                    int strateji = rnd.Next(0, 3);

                    foreach (var id in checkboxIds.Values)
                    {
                        try
                        {
                            var checkboxInput = driver.FindElement(By.Id(id));
                            var isChecked = checkboxInput.GetAttribute("aria-checked") == "true" || checkboxInput.Selected;
                            var isDisabled = checkboxInput.GetAttribute("disabled") != null;

                            if (isChecked && !isDisabled)
                            {
                                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", checkboxInput);
                                Thread.Sleep(200);
                            }
                        }
                        catch { }
                    }

                    List<string> secilecekler = new List<string>();
                    if (strateji == 0)
                        secilecekler.Add("Sayımda Kullan");
                    else if (strateji == 1)
                    {
                        secilecekler.Add("Sayımda Kullan");
                        secilecekler.Add("Transferde Üretim");
                    }
                    else if (strateji == 2)
                    {
                        secilecekler.Add("Kullanım Deposu");
                        secilecekler.Add("Zayi Deposu");
                    }

                    foreach (var label in secilecekler)
                    {
                        try
                        {
                            var inputId = checkboxIds[label];
                            var checkboxInput = driver.FindElement(By.Id(inputId));
                            var isDisabled = checkboxInput.GetAttribute("disabled") != null;
                            var isChecked = checkboxInput.GetAttribute("aria-checked") == "true" || checkboxInput.Selected;

                            if (!isDisabled && !isChecked)
                            {
                                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", checkboxInput);
                                Thread.Sleep(300);
                            }
                        }
                        catch { }
                    }

                    if (secilecekler.Contains("Sayımda Kullan"))
                    {
                        try
                        {
                            var sayimParametre = driver.WaitAndFindElement(
                                By.XPath("//span[contains(text(),'Sayım Parametreleri')]"), timeout: 10
                            );

                            if (sayimParametre != null)
                            {
                                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", sayimParametre);
                                Thread.Sleep(300);

                                var wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                                wait2.Until(d => d.FindElements(By.CssSelector("li.p-dropdown-item")).Count > 0);

                                var items = driver.FindElements(By.CssSelector("li.p-dropdown-item")).ToList();
                                if (items.Any())
                                {
                                    int index = rnd.Next(0, items.Count);
                                    try
                                    {
                                        var item = items[index];
                                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", item);
                                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", item);
                                        Thread.Sleep(300);
                                    }
                                    catch { }
                                }
                            }
                        }
                        catch { }
                    }
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-createwarehouse/p-dialog/div/div/div[4]/div/button[2]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                    Thread.Sleep(400);

                }
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-createwarehouse/p-dialog/div/div/div[4]/div/button[1]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                Thread.Sleep(400);

            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }

        }

        private void AddTax()
        {
            section = Section.Add("Vergiler tanımlandı");

            try
            {
                EnsureAnaVerilerIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[2]/ul/li[2]/a"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("//*[@id=\"canvas-bookmark\"]/div/div[3]/main/app-tax/div/div/div/div/div[1]/div/button[2]"));

                for (int i = 0; i < 2; i++)
                {
                    string KDVname = RandomNameHelper.GenerateRandomName();
                    int taxRatio = rnd.Next(0, 100);
                    Thread.Sleep(400);
                    driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-taxadd/p-dialog/div/div/div[3]/div[1]/input"), KDVname);
                    Thread.Sleep(400);
                    driver.WaitAndSendKeys(By.Id("locale-german"), taxRatio.ToString());
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-taxadd/p-dialog/div/div/div[4]/div/button[2]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));

                }
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-taxadd/p-dialog/div/div/div[4]/div/button[1]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));

            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }

        }

        private void AddUnit()
        {
            section = Section.Add("Birim Tanımlandı");
            try
            {
                EnsureAnaVerilerIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[2]/ul/li[3]/a"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("//*[@id=\"canvas-bookmark\"]/div/div[3]/main/app-birimler/div/div/div/div/div[2]/div[2]/button[2]"));
                Thread.Sleep(450);
                for (int i = 0; i < 2; i++)
                {
                    string unitName = RandomNameHelper.GenerateRandomName();
                    Thread.Sleep(400);
                    driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-unitadd/p-dialog/div/div/div[3]/div/div[2]/input"), unitName);
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-unitadd/p-dialog/div/div/div[4]/div/button[2]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                    Thread.Sleep(400);
                }
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-unitadd/p-dialog/div/div/div[4]/div/button[1]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));

            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }

        private void AddConversion()
        {
            section = Section.Add("Dönüşüm Tanımlandı");
            try
            {
                EnsureAnaVerilerIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[2]/ul/li[3]/a"));
                driver.WaitAndClick(By.Id("p-tabpanel-1-label"));
                driver.WaitAndClick(By.XPath("//*[@id=\"canvas-bookmark\"]/div/div[3]/main/app-birimler/div/div/div/div/div[2]/div[2]/button[2]"));

                string ConversionName = RandomNameHelper.GenerateRandomName();
                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-unitadd/p-dialog/div/div/div[3]/div/div[2]/input"), ConversionName);

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-unitadd/p-dialog/div/div/div[3]/div/div[4]/p-dropdown"));
                Thread.Sleep(400);
                var unit = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                Thread.Sleep(400);
                var randomUnit = unit[rnd.Next(unit.Count)];
                driver.Click(randomUnit);


                driver.WaitAndClick(By.XPath("/html/body/app-root/app-unitadd/p-dialog/div/div/div[3]/div/div[5]/p-dropdown"));
                Thread.Sleep(400);
                var conversion = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                Thread.Sleep(400);
                var randomConversion = conversion[rnd.Next(conversion.Count())];
                driver.Click(randomConversion);

                float pay = (float)(rnd.NextDouble() * 1000);
                float payda = (float)(rnd.NextDouble() * 1000);


                driver.WaitAndSendKeys(By.XPath("//*[@id='quantity']"), pay.ToString());
                Thread.Sleep(400);
                driver.WaitAndSendKeys(By.XPath("//label[contains(text(),'Payda')]/following-sibling::p-inputnumber//input\r\n"), payda.ToString());

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-unitadd/p-dialog/div/div/div[4]/div/button[2]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));


                driver.WaitAndClick(By.XPath("/html/body/app-root/app-unitadd/p-dialog/div/div/div[4]/div/button[1]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }


        private void AddStockGroup()
        {
            section = Section.Add("Stok Grupları Tanımlandı");
            try
            {
                EnsureAnaVerilerIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[2]/ul/li[4]/a"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("//button[normalize-space(span) = 'Yeni Ekle']\r\n"));
                Thread.Sleep(400);
                for (int i = 0; i < 2; i++)
                {
                    Thread.Sleep(300);
                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-groupadd/p-dialog/div/div/div[3]/div[1]/p-dropdown"));


                    var itemsStock = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    var randomStock = itemsStock[rnd.Next(itemsStock.Count)];
                    driver.Click(randomStock);
                    Thread.Sleep(400);

                    string groupName = RandomNameHelper.GenerateRandomName();
                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"overGroupName\"]"), groupName);
                    Thread.Sleep(600);
                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-groupadd/p-dialog/div/div/div[4]/div/button[2]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                    Thread.Sleep(400);
                }
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-groupadd/p-dialog/div/div/div[4]/div/button[1]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));



            }
            catch (Exception ex)
            {

                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }

        private void AddStockCard()
        {
            section = Section.Add("Stok Kartları Tanımlandı");
            try
            {
                EnsureAnaVerilerIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[2]/ul/li[5]/a"));
                Thread.Sleep(300);
                for (int i = 0; i < 2; i++)
                {
                    driver.WaitAndClick(By.XPath("//button[contains(@class, 'mat-raised-button') and contains(normalize-space(.), 'Yeni Ekle')]\r\n"));


                    var randomStock = rnd.Next(1, 6);
                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-createstock/p-dialog/div/div/div[3]/div[1]/p-dropdown"));
                    Thread.Sleep(400);
                    var stoklar = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    var randomIndex = rnd.Next(stoklar.Count);
                    stoklar[randomIndex].Click();



                    string StockCardName = RandomNameHelper.GenerateRandomName();
                    Thread.Sleep(400);
                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"name\"]"), StockCardName);


                    driver.WaitAndClick(By.XPath("//span[text()='Grup 1']/ancestor::p-dropdown//div[contains(@class,'p-dropdown-trigger')]\r\n"));
                    Thread.Sleep(300);
                    var itemsGroup1 = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    if (!itemsGroup1.Any())
                        throw new Exception("Grup 1 dropdown'ında seçenek yok.");
                    var randomItemGroup1 = itemsGroup1[rnd.Next(itemsGroup1.Count)];
                    driver.Click(randomItemGroup1);

                    Thread.Sleep(500);
                    //var randomGroup = rnd.Next(2, 4);
                    driver.WaitAndClick(By.XPath("//span[text()='Grup 2']/ancestor::p-dropdown//div[contains(@class,'p-dropdown-trigger')]\r\n"));
                    Thread.Sleep(300);
                    var randomItemGroup2 = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    Thread.Sleep(300);
                    if (!randomItemGroup2.Any())
                        throw new Exception("Grup dropdown'ında seçenek yok.");
                    var randomItemm2 = randomItemGroup2[rnd.Next(randomItemGroup2.Count)];
                    driver.Click(randomItemm2);



                    driver.WaitAndClick(By.XPath("//span[text()='Grup 3']/ancestor::p-dropdown//div[contains(@class,'p-dropdown-trigger')]\r\n"));
                    Thread.Sleep(300);
                    var randomItemGroup3 = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    Thread.Sleep(300);
                    if (!randomItemGroup3.Any())
                        throw new Exception("Grup dropdown'ında seçenek yok.");
                    var randomItemm3 = randomItemGroup3[rnd.Next(randomItemGroup3.Count)];
                    driver.Click(randomItemm3);



                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-createstock/p-dialog/div/div/div[3]/div[5]/p-dropdown"));
                    Thread.Sleep(300);
                    var itemsGroupp = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    if (!itemsGroupp.Any())
                        throw new Exception("Vergi dropdown'ında seçenek yok.");
                    var randomItemm = itemsGroupp[rnd.Next(itemsGroupp.Count)];
                    driver.Click(randomItemm);



                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-createstock/p-dialog/div/div/div[3]/div[6]/p-dropdown"));
                    Thread.Sleep(300);
                    var items2 = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    Thread.Sleep(300);
                    var randomItem2 = items2[rnd.Next(items2.Count)];
                    driver.Click(randomItem2);


                    //driver.WaitAndClick(By.XPath("/html/body/app-root/app-createstock/p-dialog/div/div/div[3]/div[7]/p-dropdown"));
                    //Thread.Sleep(300);
                    //var items3 = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    //if (!items3.Any())
                    //    throw new Exception("Dönüşüm Birim Dropdown'ında seçenek yok.");
                    //var randomItem3 = items3[rnd.Next(items3.Count)];
                    //driver.Click(randomItem3);


                    //driver.WaitAndClick(By.XPath("/html/body/app-root/app-createstock/p-dialog/div/div/div[3]/div[8]/p-dropdown"));
                    //Thread.Sleep(500);
                    //var items4 = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));

                    //if (items4.Count > 1)
                    //{
                    //    var selectedItem = items4[rnd.Next(items4.Count)];
                    //    driver.Click(selectedItem);
                    //}
                    //else
                    //{
                    //    driver.WaitAndClick(By.Id("quantity"));
                    //}


                    float CriticStockLimit = (float)(rnd.NextDouble() * 1000);
                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"quantity\"]"), CriticStockLimit.ToString());
                    Thread.Sleep(500);


                    if (randomIndex == 1 || randomIndex == 2)
                    {
                        driver.WaitAndClick(By.XPath("/html/body/app-root/app-createstock/p-dialog/div/div/div[3]/p-accordion[1]"));
                        Thread.Sleep(400);
                        var checkboxes = driver.FindElements(By.CssSelector("p-checkbox input[type='checkbox']"));
                        if (checkboxes.Count == 0)
                        {
                            Console.WriteLine("Üretim Parametreleri chechkbox'ları bulunamadı");
                        }
                        else
                        {
                            int selectCount = rnd.Next(1, checkboxes.Count + 1);
                            HashSet<int> selectedIndexes = new HashSet<int>();
                            while (selectedIndexes.Count < selectCount)
                            {
                                int idx = rnd.Next(0, checkboxes.Count);
                                selectedIndexes.Add(idx);
                            }

                            foreach (int idx in selectedIndexes)
                            {
                                var checkbox = checkboxes[idx];

                                string checkboxId = checkbox.GetAttribute("id");
                                if (!string.IsNullOrEmpty(checkboxId))
                                {
                                    var label = driver.FindElement(By.CssSelector($"label[for='{checkboxId}']"));
                                    label.Click();
                                    Thread.Sleep(200);
                                }
                                else
                                {
                                    if (!checkbox.Selected)
                                        checkbox.Click();
                                }
                            }
                        }

                    }

                    driver.WaitAndClick(By.XPath("//a[@role='tab' and contains(.,'Detaylı Bilgiler')]\r\n"));

                    string barcode = string.Concat(Enumerable.Range(0, 13)
                                          .Select(_ => rnd.Next(0, 10).ToString()));

                    Thread.Sleep(400);
                    driver.WaitAndSendKeys(By.XPath("//input[@type='text' and @placeholder='Barkod Yazınız.']\r\n"), barcode);
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("//button[contains(@class,'p-button-success') and .//span[contains(@class,'pi pi-check')]]\r\n"));

                    string accountingCode = string.Concat(Enumerable.Range(0, 13)
                                          .Select(_ => rnd.Next(0, 10).ToString()));

                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"accountingCode\"]"), accountingCode);
                    Thread.Sleep(400);

                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-createstock/p-dialog/div/div/div[4]/div/button[2]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));

                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-createstock/p-dialog/div/div/div[4]/div/button[1]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));



                    if (randomIndex == 1 || randomIndex == 2)
                    {
                        driver.Navigate().Refresh();

                        driver.WaitAndClick(By.XPath("(//div[@role='button' and @aria-label='dropdown trigger'])[4]"));
                        driver.WaitAndClick(By.XPath("(//div[@role='button' and @aria-label='dropdown trigger'])[4]"));
                        driver.WaitAndClick(By.XPath("//p-dropdownitem/li[@role='option' and .//span[text()='50']]"));

                        int pageCounter = 1;
                        bool rowFound = false;

                        while (true)
                        {
                            var targetRows = driver.WaitAndFindElements(
                                By.XPath($"//tr[(td[contains(.,'Mamul')] or td[contains(.,'Yarı Mamul')]) and td[contains(.,'{StockCardName}')]]")
                            );

                            if (targetRows.Any())
                            {
                                var targetRow = targetRows.First();
                                targetRow.Click();
                                Thread.Sleep(400);

                                var menuIcon = targetRow.FindElement(
                                    By.XPath(".//td[contains(@class,'mat-menu-trigger')]//i[contains(@class,'fa-ellipsis-vertical')]")
                                );
                                menuIcon.Click();

                                rowFound = true;
                                break;
                            }

                            var nextButtons = driver.WaitAndFindElements(
                                By.XPath("//button[contains(@class,'p-paginator-next') and not(contains(@class,'p-disabled'))]")
                            );

                            if (nextButtons.Any())
                            {
                                nextButtons.First().Click();
                                pageCounter++;
                                Console.WriteLine($" {pageCounter}. sayfaya geçildi, arama devam ediyor...");
                                Thread.Sleep(800);
                            }
                            else
                            {
                                throw new Exception($"'{StockCardName}' içeren Mamul veya Yarı Mamul satırı bulunamadı (tüm sayfalar tarandı)!");
                            }
                        }

                        if (rowFound)
                        {
                            driver.WaitAndClick(By.XPath("//div[contains(@class,'mat-menu-panel')]//button[2]"));

                            driver.WaitAndSendKeys(By.XPath("//*[@id='scrollItems']/div/div/div[1]/div/p-autocomplete/span/input"), "a");
                            Thread.Sleep(400);

                            var options2 = driver.WaitAndFindElements(By.CssSelector("div.p-autocomplete-panel ul.p-autocomplete-items li.p-autocomplete-item"));
                            Thread.Sleep(400);

                            var randomOption = options2[rnd.Next(options2.Count)];
                            driver.Click(randomOption);
                            Thread.Sleep(400);

                            float miktar = (float)(rnd.NextDouble() * 1000);
                            driver.WaitAndSendKeys(By.XPath("//*[@id='scrollItems']/div/div/div[2]/div/p-inputnumber/span/input"), miktar.ToString());

                            Thread.Sleep(400);
                            driver.WaitAndClick(By.XPath("/html/body/app-root/app-create-recipe/p-dialog/div/div/div[4]/div/button[2]"));
                            Thread.Sleep(400);
                            driver.WaitAndClick(By.XPath("/html/body/div[2]/div/div[6]/button[1]"));
                        }
                    }


                }


            }
            catch (Exception ex)
            {

                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }

        private void AddCariGroup()
        {
            section = Section.Add("Cari Grubu Tanımlandı");
            try
            {
                EnsureAnaVerilerIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[2]/ul/li[6]/a"));
                driver.Navigate().Refresh();
                WebDriverExtensions.WaitUntilPageLoads(driver);
                Thread.Sleep(600); var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementIsVisible(
                    By.Id("p-tabpanel-1-label")));
                driver.WaitAndClick(By.Id("p-tabpanel-1-label"));
                driver.WaitAndClick(By.XPath("//button[span[contains(text(),'+ Cari Grubu Ekle')]]"));
                for (int i = 0; i < 2; i++)
                {
                    string CariGroupName = RandomNameHelper.GenerateRandomName();
                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"name\"]"), CariGroupName);
                    Thread.Sleep(300);
                    string VergiNo = "";
                    while (VergiNo.Length < 10)
                    {
                        VergiNo += rnd.Next(0, 10).ToString();
                    }
                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"vktcn\"]/span/input"), VergiNo);
                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addsuppliergroup/p-dialog/div/div/div[4]/div/button[2]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                    Thread.Sleep(400);
                }
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addsuppliergroup/p-dialog/div/div/div[4]/div/button[1]"));
            }
            catch (Exception ex)
            {

                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }

        private void AddCari()
        {
            section = Section.Add("Cariler Tanımlandı");
            try
            {
                EnsureAnaVerilerIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[2]/ul/li[6]/a"));
                Thread.Sleep(1000);
                driver.WaitAndClick(By.Id("p-tabpanel-0-label"));
                driver.WaitAndClick(By.XPath("//button[span[contains(text(),'Yeni Ekle')]]\r\n"));

                for (int i = 0; i < 2; i++)
                {

                    string CariName = RandomNameHelper.GenerateRandomName();
                    string ShortName = CariName.Length >= 3 ? CariName.Substring(0, 3) : CariName;
                    string Address = RandomNameHelper.GenerateRandomName();
                    string email = RandomNameHelper.GenerateRandomName();
                    string AuthorizedName = RandomNameHelper.GenerateRandomName();

                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"name\"]"), CariName);
                    Thread.Sleep(400);
                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"shortName\"]"), ShortName);

                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addsupplier/p-dialog/div/div/div[3]/div[4]/p-dropdown"));
                    var itemsCari = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    var randomCari = itemsCari[rnd.Next(itemsCari.Count)];
                    driver.Click(randomCari);


                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addsupplier/p-dialog/div/div/div[3]/div[5]/p-dropdown"));
                    Thread.Sleep(300);
                    var items2 = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    Thread.Sleep(300);
                    var random2 = items2[rnd.Next(items2.Count)];
                    driver.Click(random2);


                    string VergiNo = "";
                    while (VergiNo.Length < 10)
                    {
                        VergiNo += rnd.Next(0, 10).ToString();
                    }
                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"vktcn\"]/span/input"), VergiNo);
                    Thread.Sleep(400);
                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"address\"]"), Address);
                    Thread.Sleep(400);
                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"barcode\"]"), email + "@gmail.com");
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addsupplier/p-dialog/div/div/div[3]/div[8]/div/button"));
                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"no\"]/input"), "5" + new Random().Next(300000000, 599999999).ToString());
                    Thread.Sleep(400);
                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"competent\"]"), AuthorizedName);
                    Thread.Sleep(400);

                    driver.WaitAndClick(By.XPath("//label[text()='Ödeme Günü']/following::p-calendar[1]//input"));
                    var dayElements = driver.WaitAndFindElements(
                        By.XPath("//table[contains(@class,'p-datepicker-calendar')]//td[not(contains(@class,'p-disabled')) and not(contains(@class,'p-datepicker-other-month'))]/span")
                    );
                    if (dayElements.Any())
                    {
                        var randomIndex3 = dayElements[rnd.Next(dayElements.Count)];
                        driver.Click(randomIndex3); ;
                    }
                    else
                    {
                        Console.WriteLine("Tarih Hücreleri Bulunamadı.");
                    }
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("//*[@id=\"paymentPeriod\"]"));
                    Thread.Sleep(400);
                    var items3 = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    var random3 = items3[rnd.Next(items3.Count)];
                    Thread.Sleep(400);
                    driver.Click(random3);
                    string random3Text = random3.Text.Trim();


                    foreach (var option in items3)
                    {
                        if (option.Text.Trim().Equals(random3Text, StringComparison.OrdinalIgnoreCase))
                        {
                            option.Click();
                            break;
                        }
                    }

                    if (random3Text.Equals("Periyodik", StringComparison.OrdinalIgnoreCase))
                    {
                        Thread.Sleep(300);
                        driver.WaitAndClick(By.XPath("/html/body/app-root/app-addsupplier/p-dialog/div/div/div[3]/div[13]/div/div/div/div/div/button"));
                        int PerSayi = rnd.Next(1, 31);
                        int PerSayi2 = rnd.Next(PerSayi + 1, 32);
                        int PerSayi3 = rnd.Next(1, 31);
                        driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-addsupplier/p-dialog/div/div/div[3]/div[13]/div/div/div/div[2]/div[1]/p-inputnumber/span/input"), PerSayi.ToString());
                        Thread.Sleep(400);
                        driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-addsupplier/p-dialog/div/div/div[3]/div[13]/div/div/div/div[2]/div[2]/p-inputnumber/span/input"), PerSayi2.ToString());
                        Thread.Sleep(400);
                        driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-addsupplier/p-dialog/div/div/div[3]/div[13]/div/div/div/div[2]/div[3]/p-inputnumber/span/input"), PerSayi3.ToString());

                    }

                    else if (random3Text.Equals("Vadeli", StringComparison.OrdinalIgnoreCase))
                    {
                        Thread.Sleep(300);
                        driver.WaitAndClick(By.XPath("/html/body/app-root/app-addsupplier/p-dialog/div/div/div[3]/div[13]/div/div/div/p-inputnumber/span/input"));
                        int vadeSayi = rnd.Next(1, 100);
                        driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-addsupplier/p-dialog/div/div/div[3]/div[13]/div/div/div/p-inputnumber/span/input"), vadeSayi.ToString());

                    }

                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addsupplier/p-dialog/div/div/div[4]/div/button[2]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                    Thread.Sleep(400);

                }
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addsupplier/p-dialog/div/div/div[4]/div/button[1]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));

            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }

        private void AddListGroup()
        {
            section = Section.Add("Liste Grubu Tanımlandı");
            try
            {
                EnsureAnaVerilerIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[2]/ul/li[7]/a"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("//button[.//span[contains(text(),'Yeni Liste Grubu')]]\r\n"));
                for (int i = 0; i < 2; i++)
                {
                    string listname = RandomNameHelper.GenerateRandomName();
                    driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-add-list-group/p-dialog/div/div/div[3]/div/div[1]/input"), listname);
                    Thread.Sleep(400);

                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-list-group/p-dialog/div/div/div[3]/div/div[2]/p-dropdown"));
                    Thread.Sleep(400);
                    var listStarHour = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    var randomHour = listStarHour[rnd.Next(listStarHour.Count)];
                    driver.Click(randomHour);

                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-list-group/p-dialog/div/div/div[3]/div/div[3]/p-dropdown"));
                    Thread.Sleep(400);
                    var listEndHour = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    var randomHourr = listEndHour[rnd.Next(listEndHour.Count)];
                    driver.Click(randomHourr);

                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-list-group/p-dialog/div/div/div[4]/div/button[2]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                    Thread.Sleep(400);
                }
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-list-group/p-dialog/div/div/div[4]/div/button[1]"));
            }
            catch (Exception ex)
            {

                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }

        private void AddLists()
        {
            section = Section.Add("Liste Tanımlandı");
            try
            {
                EnsureAnaVerilerIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[2]/ul/li[8]/a"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("//button[.//span[contains(text(),'Yeni Liste')]]\r\n"));


                float miktar = (float)(rnd.NextDouble() * 10000);
                float birimFiyat = (float)(rnd.NextDouble() * 10000);

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-list/p-dialog/div/div/div[3]/div[1]/div[1]/div/div[2]/p-dropdown"));
                Thread.Sleep(400);
                var listCategory = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                var randomCategory = listCategory[rnd.Next(listCategory.Count)];
                Thread.Sleep(400);
                driver.Click(randomCategory);


                string listname = RandomNameHelper.GenerateRandomName();


                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-add-list/p-dialog/div/div/div[3]/div[1]/div[2]/input"), listname);
                Thread.Sleep(400);

                driver.WaitAndSendKeys(By.XPath("//*[@id=\"scrollItems\"]/div/div/div[1]/div/div/p-autocomplete/span/input"), "a");
                Thread.Sleep(400);
                var options2 = driver.WaitAndFindElements(By.CssSelector("div.p-autocomplete-panel ul.p-autocomplete-items li.p-autocomplete-item"));

                if (options2.Count == 0)
                {
                    Console.WriteLine("Hiç seçenek bulunamadı!");
                }
                else
                {
                    var randomOption = options2[rnd.Next(options2.Count)];
                    driver.Click(randomOption);
                }


                Thread.Sleep(400);
                var inputs = driver.WaitAndFindElements(By.Id("quantity"));
                driver.WaitAndSendKeys(By.XPath("(//input[@id='quantity'])[1]"), miktar.ToString());
                Thread.Sleep(400);
                driver.WaitAndSendKeys(By.XPath("(//input[@id='quantity'])[2]"), birimFiyat.ToString());
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("//*[@id=\"scrollItems\"]/div/div/div[3]/div/div/p-dropdown"));
                Thread.Sleep(400);
                var dropUnit = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                var randomUnit = dropUnit[rnd.Next(dropUnit.Count)];
                driver.Click(randomUnit);


                driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-list/p-dialog/div/div/div[4]/div/button[2]"));
                Thread.Sleep(400);

                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                Thread.Sleep(400);


                driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-list/p-dialog/div/div/div[4]/div/button[1]"));
                Thread.Sleep(400);

                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
            }
            catch (Exception ex)
            {

                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }

        private void AddPortfolio()
        {
            section = Section.Add("Portföy Tanımlandı");
            try
            {
                EnsureAnaVerilerIsOpen();
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[2]/ul/li[9]/a/i"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("//button[.//i[contains(@class,'fa-square-plus')] and contains(.,'Yeni Ekle')]\r\n"));

                var code = rnd.Next(1, 100000);
                var name = RandomNameHelper.GenerateRandomName();
                float miktar = (float)(rnd.NextDouble() * 10000);
                float birimFiyat = (float)(rnd.NextDouble() * 10000);

                driver.WaitAndSendKeys(By.Id("code"), code.ToString());
                driver.WaitAndSendKeys(By.Id("name"), name);

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addportfolio/p-dialog/div/div/div[3]/div[2]/div[1]/p-dropdown/div/div[2]/span"));
                Thread.Sleep(400);
                var listBranch = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                var randomBranch = listBranch[rnd.Next(listBranch.Count)];
                Thread.Sleep(400);
                driver.Click(randomBranch);

                driver.WaitAndClick(By.XPath("//button[normalize-space(.)='Stok Ekle']\r\n"));


                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-addportfolio/p-dialog/div/div/div[3]/div[5]/div[1]/p-autocomplete/span/input"), "a");
                Thread.Sleep(400);
                var options = driver.WaitAndFindElements(By.CssSelector("div.p-autocomplete-panel ul.p-autocomplete-items li.p-autocomplete-item"));

                if (options.Count == 0)
                {
                    Console.WriteLine("Hiç seçenek bulunamadı!");
                }
                else
                {
                    var randomStock = options[rnd.Next(options.Count)];
                    driver.Click(randomStock);
                }

                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-addportfolio/p-dialog/div/div/div[3]/div[5]/div[2]/p-inputnumber/span/input"), miktar.ToString());
                Thread.Sleep(400);
                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-addportfolio/p-dialog/div/div/div[3]/div[5]/div[3]/p-inputnumber/span/input"), birimFiyat.ToString());

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addportfolio/p-dialog/div/div/div[3]/div[6]/button[1]"));
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }
        private void AddPurchaseİnvoice()
        {
            section = Section.Add("Alış Faturası Tanımlandı");
            try
            {
                EnsureMaliyetYonetimiIsOpen();
                Thread.Sleep(500);
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[3]/ul/li[1]"));
                driver.WaitAndClick(By.XPath("//button[span[contains(normalize-space(.), 'Yeni Ekle')]]\r\n"));


                string not = RandomNameHelper.GenerateRandomName();
                Thread.Sleep(400);

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[3]/div/div[2]/p-dropdown"));
                Thread.Sleep(400);
                var depo = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                var randomDepo = depo[rnd.Next(depo.Count)];
                driver.Click(randomDepo);


                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[3]/div/div[3]/p-dropdown"));
                Thread.Sleep(400);
                var cari = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                var randomCari = cari[rnd.Next(cari.Count)];
                driver.Click(randomCari);

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[3]/div/div[4]/p-calendar/span/input"));
                Thread.Sleep(400);
                var dayCells1 = driver.WaitAndFindElements(By.XPath("//table[contains(@class,'p-datepicker-calendar')]//td[not(contains(@class,'p-disabled')) and not(contains(@class,'p-datepicker-other-month'))]/span"));


                if (dayCells1.Any())
                {
                    var randomIndex3 = dayCells1[rnd.Next(dayCells1.Count)];
                    driver.Click(randomIndex3); ;
                }
                else
                {
                    Console.WriteLine("Tarih Hücreleri Bulunamadı.");
                }

                driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[3]/div/div[5]/input"), not);



                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[3]/div[1]/div[6]/p-calendar/span/input"));
                Thread.Sleep(500);
                var dayCells2 = driver.WaitAndFindElements(By.XPath("//table[contains(@class,'p-datepicker-calendar')]//td[not(contains(@class,'p-disabled')) and not(contains(@class,'p-datepicker-other-month'))]/span"));

                if (dayCells2.Any())
                {
                    var randomIndex4 = dayCells2[rnd.Next(dayCells2.Count)];
                    driver.Click(randomIndex4); ;
                }
                else
                {
                    Console.WriteLine("Tarih Hücreleri Bulunamadı.");
                }
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[3]/div[1]/div[7]/div/label"));


                driver.WaitAndSendKeys(By.XPath("//*[@id=\"scroll-0\"]/div/div[1]/p-autocomplete/span/input"), "a");
                var options = driver.WaitAndFindElements(By.CssSelector("div.p-autocomplete-panel ul.p-autocomplete-items li.p-autocomplete-item"));

                if (options.Count == 0)
                {
                    Console.WriteLine("Hiç seçenek bulunamadı!");
                }
                else
                {
                    var randomOption = options[rnd.Next(options.Count)];
                    driver.Click(randomOption);
                }



                float miktar = (float)(rnd.NextDouble() * 1000);


                float birimFiyat = (float)(rnd.NextDouble() * 1000);

                var inputs2 = driver.WaitAndFindElements(By.Id("quantity"));
                driver.WaitAndSendKeys(By.XPath("(//input[@id='quantity'])[1]"), miktar.ToString());
                driver.WaitAndSendKeys(By.XPath("(//input[@id='quantity'])[2]"), birimFiyat.ToString());



                driver.WaitAndClick(By.XPath("//*[@id=\"scroll-0\"]/div/div[3]/p-dropdown"));
                var birim = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                var randomBirim = birim[rnd.Next(birim.Count)];
                driver.Click(randomBirim);

                driver.WaitAndClick(By.XPath("//*[@id=\"scroll-0\"]/div/div[4]/p-dropdown"));
                Thread.Sleep(400);
                var KDV = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                Thread.Sleep(400);
                var randomKDV = KDV[rnd.Next(KDV.Count)];
                driver.Click(randomKDV);

                var indirimOran = rnd.Next(1, 100);
                driver.WaitAndSendKeys(By.XPath("//input[@placeholder='invoiceAlisPage.modal.placeholder.totalDiscountRate']\r\n"), indirimOran.ToString());



                //driver.WaitAndClick(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[4]/div[1]/div[2]/div[1]/div/p-dropdown"));
                //Thread.Sleep(400);
                //var indirimTurler = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                //var randomTur = indirimTurler[rnd.Next(indirimTurler.Count)];
                //string selectedText = randomTur.GetAttribute("innerText").Trim();

                //driver.Click(randomTur);

                //WebDriverExtensions.WaitUntilPageLoads(driver);

                //var input = driver.WaitAndFindElement(By.CssSelector("p-inputnumber input#quantity"));

                //var oran = rnd.Next(1, 100);
                //float tutar = (float)(rnd.NextDouble() * 1000);
                //var inputs = driver.WaitAndFindElements(By.Id("quantity"));

                //if (selectedText.Equals("Tutar", StringComparison.OrdinalIgnoreCase))
                //{
                //    driver.WaitAndSendKeys(By.Id("generalDiscount"), tutar.ToString());
                //}
                //else if (selectedText.Equals("Oran", StringComparison.OrdinalIgnoreCase))
                //{
                //    driver.WaitAndSendKeys(By.Id("//input[@id='quantity' and @max='100']"), oran.ToString());
                //}

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[4]/div[2]/button[2]"));
                Thread.Sleep(400);

                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                Thread.Sleep(400);

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[4]/div/button[1]"));
                Thread.Sleep(400);

                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));

            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }

        }

        private void AddSalesİnvoice()
        {
            section = Section.Add("Satış Faturası Tanımlandı");

            try
            {
                EnsureMaliyetYonetimiIsOpen();
                Thread.Sleep(500);
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[3]/ul/li[2]"));
                Thread.Sleep(500);
                driver.WaitAndClick(By.XPath("//button[.//span[contains(normalize-space(.), 'Yeni Ekle')]]\r\n"));

                for (int i = 0; i < 2; i++)
                {
                    string not2 = RandomNameHelper.GenerateRandomName();

                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[3]/div[1]/div[2]/p-dropdown"));
                    var depo = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    Thread.Sleep(400);
                    var randomDepo = depo[rnd.Next(depo.Count)];
                    driver.Click(randomDepo);

                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[3]/div/div[3]/p-dropdown"));
                    Thread.Sleep(400);
                    var cariAdi = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    var randomCariadi = cariAdi[rnd.Next(cariAdi.Count)];
                    driver.Click(randomCariadi);



                    driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[3]/div/div[5]/input"), not2);
                    Thread.Sleep(400);


                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[3]/div/div[4]/p-calendar/span/input"));
                    var dayCells1 = driver.WaitAndFindElements(By.XPath("//table[contains(@class,'p-datepicker-calendar')]//td[not(contains(@class,'p-disabled')) and not(contains(@class,'p-datepicker-other-month'))]/span"));


                    if (dayCells1.Any())
                    {
                        var randomIndex3 = dayCells1[rnd.Next(dayCells1.Count)];
                        driver.Click(randomIndex3); ;
                    }
                    else
                    {
                        Console.WriteLine("Tarih Hücreleri Bulunamadı.");
                    }


                    Thread.Sleep(400);


                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"scroll-0\"]/div/div[1]/p-autocomplete/span/input"), "a");
                    Thread.Sleep(400);
                    var options2 = driver.WaitAndFindElements(By.CssSelector("div.p-autocomplete-panel ul.p-autocomplete-items li.p-autocomplete-item"));

                    if (options2.Count == 0)
                    {
                        Console.WriteLine("Seçenek bulunamadı!");
                    }
                    else
                    {
                        var randomOption = options2[rnd.Next(options2.Count)];
                        driver.Click(randomOption);
                    }

                    float miktar = (float)(rnd.NextDouble() * 1000);


                    float birimFiyat = (float)(rnd.NextDouble() * 1000);

                    var inputs2 = driver.WaitAndFindElements(By.Id("quantity"));
                    driver.WaitAndSendKeys(By.XPath("(//input[@id='quantity'])[1]"), miktar.ToString());
                    Thread.Sleep(400);

                    driver.WaitAndSendKeys(By.XPath("(//input[@id='quantity'])[2]"), birimFiyat.ToString());



                    driver.WaitAndClick(By.XPath("//*[@id=\"scroll-0\"]/div/div[3]/p-dropdown"));
                    var birim = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    var randomBirim = birim[rnd.Next(birim.Count)];
                    driver.Click(randomBirim);


                    driver.WaitAndClick(By.XPath("//*[@id=\"scroll-0\"]/div/div[4]/p-dropdown"));
                    Thread.Sleep(400);
                    var KDV = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    Thread.Sleep(400);
                    var randomKDV = KDV[rnd.Next(KDV.Count)];
                    driver.Click(randomKDV);


                    var indirimOran = rnd.Next(1, 100);
                    driver.WaitAndSendKeys(By.XPath("//input[@placeholder='invoiceAlisPage.modal.placeholder.totalDiscountRate']\r\n"), indirimOran.ToString());
                    Thread.Sleep(400);



                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[4]/div[2]/button[2]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                    Thread.Sleep(400);
                }

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addinvoice/p-dialog/div/div/div[4]/div/button[1]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
            }
            catch (Exception ex)
            {

                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }

        private void AddLossGroup()
        {
            section = Section.Add("Zayi Grubu Tanımlandı");
            try
            {
                EnsureMaliyetYonetimiIsOpen();
                Thread.Sleep(500);
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[3]/ul/li[3]/a"));
                driver.Navigate().Refresh();
                WebDriverExtensions.WaitUntilPageLoads(driver);
                Thread.Sleep(600); var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementIsVisible(
                    By.Id("p-tabpanel-1-label")));
                driver.WaitAndClick(By.Id("p-tabpanel-1-label"));
                driver.WaitAndClick(By.XPath("//button[contains(normalize-space(.), 'Zayi Grubu Ekle')]\r\n"));

                for (int i = 0; i < 2; i++)
                {
                    string zayiName = RandomNameHelper.GenerateRandomName();
                    var randomNumber = rnd.Next(1, 1000);
                    driver.WaitAndSendKeys(By.Id("wasteGroupName"), zayiName);
                    Thread.Sleep(400);
                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"wasteGroupCode\"]"), "ZG" + randomNumber.ToString());
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-waste-group-add/p-dialog/div/div/div[4]/div/button[2]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                    Thread.Sleep(400);
                }
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-waste-group-add/p-dialog/div/div/div[4]/div/button[1]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
            }
            catch (Exception ex)
            {

                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }

        }
        private void AddLoss()
        {
            section = Section.Add("Zayi Tanımlandı");
            try
            {
                EnsureMaliyetYonetimiIsOpen();
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[3]/ul/li[3]/a"));
                WebDriverExtensions.WaitUntilPageLoads(driver);
                Thread.Sleep(600); var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementIsVisible(
                    By.Id("p-tabpanel-0-label")));
                driver.WaitAndClick(By.Id("p-tabpanel-0-label"));
                driver.WaitAndClick(By.XPath("//button[.//span[normalize-space(text())='Yeni Ekle']]\r\n"));

                for (int i = 0; i < 2; i++)
                {
                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addwaste/p-dialog/div/div/div[3]/div/div[2]/div/div[2]/p-dropdown"));
                    var zayiGrup = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    var randomZayiGrup = zayiGrup[rnd.Next(zayiGrup.Count)];
                    driver.Click(randomZayiGrup);


                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addwaste/p-dialog/div/div/div[3]/div/div[3]/div/div[2]/p-dropdown"));
                    Thread.Sleep(400);
                    var depo = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    Thread.Sleep(400);
                    var randomDepo = depo[rnd.Next(depo.Count)];
                    driver.Click(randomDepo);



                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addwaste/p-dialog/div/div/div[3]/div/div[4]/div/p-calendar/span/input"));
                    var dayCells1 = driver.WaitAndFindElements(By.XPath("//table[contains(@class,'p-datepicker-calendar')]//td[not(contains(@class,'p-disabled')) and not(contains(@class,'p-datepicker-other-month'))]/span"));
                    var randomIndex3 = dayCells1[rnd.Next(dayCells1.Count)];
                    driver.Click(randomIndex3); ;

                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"scrollItems\"]/div/div/div[1]/div/div/p-autocomplete/span/input"), "a");
                    Thread.Sleep(400);
                    var options2 = driver.WaitAndFindElements(By.CssSelector("div.p-autocomplete-panel ul.p-autocomplete-items li.p-autocomplete-item"));

                    if (options2.Count == 0)
                    {
                        Console.WriteLine("Hiç seçenek bulunamadı!");
                    }
                    else
                    {
                        var randomOption = options2[rnd.Next(options2.Count)];
                        driver.Click(randomOption);
                    }

                    float miktar = (float)(rnd.NextDouble() * 1000);
                    driver.WaitAndSendKeys(By.Id("locale-german"), miktar.ToString());

                    driver.WaitAndClick(By.XPath("//*[@id=\"scrollItems\"]/div/div/div[3]/div/div/p-dropdown"));
                    var zayi = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    var randomZayi = zayi[rnd.Next(zayi.Count)];
                    driver.Click(randomZayi);


                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addwaste/p-dialog/div/div/div[4]/div[2]/button[2]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                    Thread.Sleep(400);
                }
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addwaste/p-dialog/div/div/div[3]/div/div[5]/button"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));

            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }

        private void AddProductionProcesses()
        {
            section = Section.Add("Üretim İşlemleri Tanımlandı");
            try
            {
                EnsureMaliyetYonetimiIsOpen();
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[3]/ul/li[4]/a"));
                driver.WaitAndClick(By.XPath("//button[span[contains(., 'Yeni Ekle')]]\r\n"));
                float miktar = (float)(rnd.NextDouble() * 1000);


                for (int i = 0; i < 2; i++)
                {
                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-production/p-dialog/div/div/div[3]/div/div[2]/div/div[2]/p-dropdown"));
                    var depo = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    var randomDepo = depo[rnd.Next(depo.Count)];
                    driver.Click(randomDepo);


                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-production/p-dialog/div/div/div[3]/div/div[3]/div/p-calendar/span/input"));
                    var dayCells1 = driver.WaitAndFindElements(By.XPath("//table[contains(@class,'p-datepicker-calendar')]//td[not(contains(@class,'p-disabled')) and not(contains(@class,'p-datepicker-other-month'))]/span"));
                    var randomIndex3 = dayCells1[rnd.Next(dayCells1.Count)];
                    driver.Click(randomIndex3);


                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"scrollItems\"]/div/div/div[1]/div/div/p-autocomplete/span/input"), "a");
                    Thread.Sleep(400);
                    var options2 = driver.WaitAndFindElements(By.CssSelector("div.p-autocomplete-panel ul.p-autocomplete-items li.p-autocomplete-item"));

                    if (options2.Count == 0)
                    {
                        Console.WriteLine("Hiç seçenek bulunamadı!");
                    }
                    else
                    {
                        var randomOption = options2[rnd.Next(options2.Count)];
                        driver.Click(randomOption);
                    }

                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"locale-german\"]"), miktar.ToString());


                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-production/p-dialog/div/div/div[4]/div/button[2]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                    Thread.Sleep(400);

                }

                driver.WaitAndClick(By.XPath("/html/body/app-root/app-add-production/p-dialog/div/div/div[3]/div/div[4]/button"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));

            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }

        }
        private void AddCountingOperations()
        {
            section = Section.Add("Sayım İşlemleri Tanımlandı");

            try
            {
                EnsureMaliyetYonetimiIsOpen();
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[3]/ul/li[5]/a"));
                Thread.Sleep(400);
                var firstButtons = driver.WaitAndFindElements(
      By.XPath("//button[.//i[contains(@class,'fa-square-plus')] and contains(.,'Sayımları Oluştur')]")
  );

                if (firstButtons.Any())
                {
                    Thread.Sleep(400);

                    var firstButton = firstButtons.First();

                    bool isDisabledAttr = firstButton.GetAttribute("disabled") != null;
                    bool isDisabledClass = firstButton.GetAttribute("class").Contains("mat-button-disabled");
                    bool isEnabled = !(isDisabledAttr || isDisabledClass);
                    Thread.Sleep(400);
                    if (isEnabled)
                    {
                        Thread.Sleep(400);

                        driver.WaitAndClick(
                            By.XPath("//button[.//i[contains(@class,'fa-square-plus')] and contains(.,'Sayımları Oluştur')]")

                        );
                        Thread.Sleep(400);

                    }
                    else
                    {
                        Thread.Sleep(400);

                        driver.WaitAndClick(
                            By.XPath("//button[.//i[contains(@class,'fa-lock')] and contains(.,'Mühürle')]")
                        );
                        Thread.Sleep(400);

                    }
                }
                else
                {
                    Thread.Sleep(400);

                    driver.WaitAndClick(
                        By.XPath("//button[.//i[contains(@class,'fa-lock')] and contains(.,'Mühürle')]")
                    ); Thread.Sleep(400);

                }

                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("//*[@id=\"pr_id_17\"]/div[1]/div/div/button[3]"));
                Thread.Sleep(400);

            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }
        }

        private void AddTransferTransactions()
        {
            section = Section.Add("Tranfer İşlemleri Tanımlandı");
            try
            {
                EnsureMaliyetYonetimiIsOpen();
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[3]/ul/li[6]/a"));
                driver.WaitAndClick(By.XPath("//button[.//i[contains(@class,'fa-square-plus')] and contains(normalize-space(.),'Yeni Ekle')]\r\n"));
                for (int i = 0; i < 2; i++)
                {
                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addtransfer/p-dialog/div/div/div[3]/div/div[2]/div/div[2]/p-dropdown"));
                    Thread.Sleep(400);
                    var transfer = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    Thread.Sleep(400);
                    var randomTransfer = transfer[rnd.Next(transfer.Count)];
                    driver.Click(randomTransfer);

                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addtransfer/p-dialog/div/div/div[3]/div/div[3]/div/div[2]/p-dropdown"));
                    Thread.Sleep(400);
                    var wareHouse = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    Thread.Sleep(400);
                    var randomWareHouse = wareHouse[rnd.Next(wareHouse.Count)];
                    driver.Click(randomWareHouse);


                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addtransfer/p-dialog/div/div/div[3]/div/div[4]/div/p-calendar/span/input"));
                    var dayElements = driver.WaitAndFindElements(
                        By.XPath("//table[contains(@class,'p-datepicker-calendar')]//td[not(contains(@class,'p-disabled')) and not(contains(@class,'p-datepicker-other-month'))]/span")
                    );
                    if (dayElements.Any())
                    {
                        var randomIndex3 = dayElements[rnd.Next(dayElements.Count)];
                        driver.Click(randomIndex3); ;
                    }
                    else
                    {
                        Console.WriteLine("Tarih Hücreleri Bulunamadı.");
                    }
                    if (i > 0)
                    {
                        driver.WaitAndClick(By.XPath("/html/body/app-root/app-addtransfer/p-dialog/div/div/div[3]/div[2]/div/div/div/div[1]/div[5]/button"));
                    }

                    driver.WaitAndSendKeys(By.XPath("//*[@id=\"scrollItems\"]/div/div/div[1]/div/div/p-autocomplete/span/input"), "a");
                    Thread.Sleep(400);
                    var options2 = driver.WaitAndFindElements(By.CssSelector("div.p-autocomplete-panel ul.p-autocomplete-items li.p-autocomplete-item"));

                    if (options2.Count == 0)
                    {
                        Console.WriteLine("Hiç seçenek bulunamadı!");
                    }
                    else
                    {
                        var randomOption = options2[rnd.Next(options2.Count)];
                        driver.Click(randomOption);
                    }

                    float miktar = (float)(rnd.NextDouble() * 10000);
                    float birimFiyat = (float)(rnd.NextDouble() * 10000);

                    driver.WaitAndSendKeys(By.XPath("(//input[@id='quantity'])[1]"), miktar.ToString());
                    Thread.Sleep(400);
                    driver.WaitAndSendKeys(By.XPath("(//input[@id='quantity'])[2]"), birimFiyat.ToString());
                    Thread.Sleep(400);

                    driver.WaitAndClick(By.XPath("//*[@id=\"scrollItems\"]/div/div/div[3]/div/div[1]/p-dropdown"));
                    var unit = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
                    var randomUnit = unit[rnd.Next(unit.Count)];
                    driver.Click(randomUnit);


                    driver.WaitAndClick(By.XPath("/html/body/app-root/app-addtransfer/p-dialog/div/div/div[4]/div/button[2]"));
                    Thread.Sleep(400);
                    driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));
                    Thread.Sleep(400);
                }
                driver.WaitAndClick(By.XPath("/html/body/app-root/app-addtransfer/p-dialog/div/div/div[3]/div/div[5]/button"));
                Thread.Sleep(400);
                driver.WaitAndClick(By.XPath("/html/body/div/div/div[6]/button[1]"));

            }

            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                section.EndTheSection();
            }

        }

        //private void ButcherOperations()
        //{
        //    section = Section.Add("Kasap İşlemleri Tanımlandı");
        //    try
        //    {
        //        EnsureMaliyetYonetimiIsOpen();
        //        driver.WaitAndClick(By.XPath("//*[@id=\"sidebar-menu\"]/li[3]/ul/li[9]/a"));
        //        driver.WaitAndClick(By.XPath("//button[.//span[contains(normalize-space(.), 'Yeni Ekle')]]\r\n"));
        //        var code = rnd.Next(1, 1000);
        //        float karkasAgırlık = (float)(rnd.NextDouble() * 1000);
        //        float zayiAgırlık = (float)(rnd.NextDouble() * 1000);
        //        string desc = RandomNameHelper.GenerateRandomName();
        //        driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-addbutcher/p-dialog/div/div/div[3]/div[1]/div[1]/div/div[1]/input"),"DŞ"+code);
        //        driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-addbutcher/p-dialog/div/div/div[3]/div[1]/div[1]/div/div[2]/p-inputnumber/span/input"),karkasAgırlık.ToString());


        //        driver.WaitAndClick(By.XPath("/html/body/app-root/app-addbutcher/p-dialog/div/div/div[3]/div[1]/div[1]/div/div[4]/p-dropdown"));
        //        var portfolia = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
        //        var randomPortfolia = portfolia[rnd.Next(portfolia.Count)];
        //        driver.Click(randomPortfolia);


        //        driver.WaitAndClick(By.XPath("/html/body/app-root/app-addbutcher/p-dialog/div/div/div[3]/div[1]/div[2]/div/div[1]/p-dropdown"));
        //        var wareHouse = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
        //        var randomwareHouse = wareHouse[rnd.Next(wareHouse.Count)];
        //        driver.Click(randomwareHouse);


        //        driver.WaitAndClick(By.XPath("/html/body/app-root/app-addbutcher/p-dialog/div/div/div[3]/div[1]/div[3]/div/div[1]/p-dropdown"));
        //        var convertedProduct = driver.WaitAndFindElements(By.CssSelector("li.p-dropdown-item"));
        //        var randomconvertedProduct = convertedProduct[rnd.Next(convertedProduct.Count)];
        //        driver.Click(randomconvertedProduct);


        //        //driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-addbutcher/p-dialog/div/div/div[3]/div[1]/div[3]/div/div[3]/p-inputnumber/span/input"),zayiAgırlık);

        //        driver.WaitAndSendKeys(By.XPath("/html/body/app-root/app-addbutcher/p-dialog/div/div/div[3]/div[1]/div[3]/div/div[4]/input"),desc);
        //        driver.WaitAndClick(By.XPath("/html/body/app-root/app-addbutcher/p-dialog/div/div/div[3]/div[3]/button[2]"));
        //    }
        //    catch (Exception ex)
        //    {
        //        _ex = ex;
        //    }
        //    finally
        //    { 
        //        section.EndTheSection();
        //    }
        //}





        public void SetExNull()
        {
            _ex = null;
        }
        public Exception GetEx()
        {
            return _ex;
        }


        private void GridBinding()
        {

            dataGridView1.AutoGenerateColumns = false;

            DataGridViewCheckBoxColumn statuscheck = new DataGridViewCheckBoxColumn();
            DataGridViewTextBoxColumn titletext = new DataGridViewTextBoxColumn();

            statuscheck.Name = "Status";
            statuscheck.HeaderText = "Durumu";
            statuscheck.DataPropertyName = "Status";
            statuscheck.ThreeState = true;

            statuscheck.TrueValue = true;
            statuscheck.FalseValue = false;
            statuscheck.IndeterminateValue = DBNull.Value;



            titletext.Name = "Title";
            titletext.HeaderText = "Özellik";
            titletext.DataPropertyName = "Title";

            dataGridView1.Columns.Add(titletext);
            dataGridView1.Columns.Add(statuscheck);
            statuscheck.Width = 100;
            dataGridView1.DataSource = Section.GetList();
        }
        public static frmTestScreen Instance { get; private set; }
        private async void Form3_Shown(object sender, EventArgs e)
        {
            Instance = this;
            try
            {
                Section.SendtheFormInstance(this);
                ChromeDriverService service = ChromeDriverService.CreateDefaultService("C:\\Chrome");
                driver = new ChromeDriver(service);
                SharedState.Driver = driver;

                SharedState.fn = new frmNetwork();
                SharedState.fn.Show();
                SharedState.fn.Hide();

                SharedState.Logger = new NetworkLogger();
                await SharedState.Logger.StartAsync();

                driver.Navigate().GoToUrl(UserInfo.Adress);
                GridBinding();
            }
            catch (Exception ex)
            {
                _ex = ex;
            }

            await Task.Run(() => ExecuteTestModules());
            TestSonu();
        }



        private void ExecuteTestModules()
        {
            Login();
            foreach (var item in Methods)
            {
                if (item.IsEnabled)
                {
                    item.Method();
                }
            }
        }


        private void Exit()
        {

            driver.Quit();
            driver.Dispose();
        }



        private void TestSonu()
        {
            if (MessageBox.Show(new Form { TopMost = true }, "Test sonuçlandı. Programa devam etmek ister misiniz ?", "Test Sonucu", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                menuStrip1.Enabled = true;
                this.Hide();

                if (SharedState.fn != null && !SharedState.fn.IsDisposed)
                {
                    SharedState.fn.Show();
                    SharedState.fn.TopMost = true;
                }
                else
                {
                    MessageBox.Show("frmNetwork formu yok veya kapanmış.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                Exit();
                Application.Exit();
            }
        }




        private void raporuKaydetToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmExport frmExport = new frmExport(Section.GetList());
            frmExport.ShowDialog();
        }

        private void yazdırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            {
                int x = e.MarginBounds.Left;
                int y = e.MarginBounds.Top;
                int rowHeight = 25;
                Font font = new Font("Arial", 10);
                Pen pen = Pens.Black;

                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    Rectangle rect = new Rectangle(x, y, col.Width, rowHeight);
                    e.Graphics.DrawRectangle(pen, rect);
                    e.Graphics.DrawString(col.HeaderText, font, Brushes.Black, rect);
                    x += col.Width;
                }

                y += rowHeight;
                x = e.MarginBounds.Left;

                int currentRow = 0;

                while (currentRow < dataGridView1.Rows.Count)
                {
                    DataGridViewRow row = dataGridView1.Rows[currentRow];
                    x = e.MarginBounds.Left;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        Rectangle rect = new Rectangle(x, y, cell.OwningColumn.Width, rowHeight);
                        e.Graphics.DrawRectangle(pen, rect);
                        e.Graphics.DrawString(cell.Value?.ToString(), font, Brushes.Black, rect);
                        x += cell.OwningColumn.Width;
                    }

                    y += rowHeight;
                    currentRow++;

                    if (y + rowHeight > e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                }

                currentRow = 0;
                e.HasMorePages = false;
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
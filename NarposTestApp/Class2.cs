using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace NarposTestApp
{
    public static class WebDriverExtensions
    {
        private static void SafeLog(string msg)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msg}");
        }

        private static void ShowExceptionMessage(Exception ex, string context)
        {
            string message = $"{context}\nHata Türü: {ex.GetType().Name}\nMesaj: {ex.Message}";
            SafeLog(message);
            MessageBox.Show(message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static bool TryWait(Func<IWebDriver, bool> condition, IWebDriver driver, int timeoutSeconds)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                return wait.Until(condition);
            }
            catch
            {
                return false;
            }
        }
       
        public static void WaitUntilPageLoads(IWebDriver driver, int timeoutSeconds = 5)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(d =>
                ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").ToString() == "complete"
            );
        }

        private static bool WaitForNoOverlays(IWebDriver driver, int timeoutSeconds = 2)
        {
            return TryWait(drv =>
                drv.FindElements(By.CssSelector(".overlay, .swal2-container, .loading")).Count == 0,
                driver,
                timeoutSeconds);
        }
     
        public static IWebElement WaitAndFindElement(this IWebDriver driver, By by, int timeout = 10)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    WaitUntilPageLoads(driver);
                    WaitForNoOverlays(driver);

                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                    return wait.Until(ExpectedConditions.ElementIsVisible(by));
                }
                catch (StaleElementReferenceException)
                {
                    SafeLog("[WaitAndFindElement] Stale element. Yeniden deneniyor...");
                }
                catch (Exception ex)
                {
                    ShowExceptionMessage(ex, "[WaitAndFindElement]");
                    break;
                }
            }

            return null;
        }
        public static List<IWebElement> WaitAndFindElements(this IWebDriver driver, By by, int timeout=10)
        {
            
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    WaitUntilPageLoads(driver);
                    WaitForNoOverlays(driver);

                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                    var elements = wait.Until(d =>
                    {
                        var found = d.FindElements(by).ToList();
                        return found.Count > 0 ? found : null;
                    });

                    return elements;
                }
                catch (StaleElementReferenceException)
                {
                    SafeLog("[WaitAndFindElements] Stale element. Yeniden deneniyor...");
                }
                catch (Exception ex)
                {
                    ShowExceptionMessage(ex, "[WaitAndFindElements]");
                    break;
                }
            }

            return new List<IWebElement>();
        }

        public static bool WaitAndClick(this IWebDriver driver, By by, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(by));

                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", element);

                try
                {
                    element.Click();
                    return true;
                }
                catch (Exception)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool WaitAndSendKeys(this IWebDriver driver, By by, string text, int timeout = 10)
        {
            WaitUntilPageLoads(driver);
            WaitForNoOverlays(driver);

            for (int attempt = 1; attempt <= 3; attempt++)
            {
                try
                {
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                    var element = wait.Until(ExpectedConditions.ElementIsVisible(by));

                    element.Clear();
                    element.SendKeys(text);
                    SafeLog("[WaitAndSendKeys] Metin gönderildi.");
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    SafeLog("[WaitAndSendKeys] Stale element. Yeniden deneniyor...");
                }
                catch (Exception ex)
                {
                    ShowExceptionMessage(ex, $"[WaitAndSendKeys] Deneme {attempt}");
                }
            }

            SafeLog("[WaitAndSendKeys] Metin gönderme başarısız.");
            return false;
        }
        public static void Click(this IWebDriver driver, IWebElement element)
        {
            try
            {
                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'center'});", element);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(ExpectedConditions.ElementToBeClickable(element));

                element.Click();
            }
            catch (Exception)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
            }
        }

    }
}

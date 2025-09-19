using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace WebDriver_Automation_Framework_Demoblaze.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected IWebElement WaitAndFindElement(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d => d.FindElement(locator));
        }

        protected void WaitAndClick(By locator, int timeoutSeconds = 10)
        {
            WaitAndFindElement(locator, timeoutSeconds).Click();
        }

        protected void WaitAndSendKeys(By locator, string text, int timeoutSeconds = 10)
        {
            var element = WaitAndFindElement(locator, timeoutSeconds);
            element.Clear();
            element.SendKeys(text);
        }

        protected bool IsElementVisible(By locator, int timeoutSeconds = 5)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
                return wait.Until(d => d.FindElement(locator).Displayed);
            }
            catch
            {
                return false;
            }
        }

        protected void ScrollToElement(By locator)
        {
            var element = WaitAndFindElement(locator);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        protected void WaitForPageLoad(int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
using OpenQA.Selenium;
using DriverManager = WebDriverAutomationFramework.Driver.WebDriverManager;
using WebDriverAutomationFramework.Utils;

namespace WebDriverAutomationFramework.Pages
{
    public abstract class BasePage
    {
        private static readonly DriverManager _webDriverManager = new DriverManager();
        protected IWebDriver Driver => _webDriverManager.GetDriver();

        protected void NavigateToUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        protected void WaitAndClick(By locator)
        {
            Driver.WaitAndClick(locator);
        }

        protected void WaitAndSendKeys(By locator, string text)
        {
            Driver.WaitAndSendKeys(locator, text);
        }

        protected string WaitAndGetText(By locator)
        {
            return Driver.WaitAndGetText(locator);
        }

        protected bool IsElementVisible(By locator, int timeoutInSeconds = 10)
        {
            return Driver.WaitForElementToBeVisible(locator, timeoutInSeconds);
        }

        protected void ScrollToElement(By locator)
        {
            var element = Driver.FindElement(locator);
            Driver.ScrollToElement(element);
        }

        public string GetPageTitle()
        {
            return Driver.Title;
        }

        public string GetCurrentUrl()
        {
            return Driver.Url;
        }
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverAutomationFramework.Config;

namespace WebDriverAutomationFramework.Utils
{
    public static class WebElementExtensions
    {
        private static readonly TestConfiguration _config = TestConfiguration.Instance;

        public static void WaitAndClick(this IWebDriver driver, By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(_config.ExplicitWait));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            element.Click();
        }

        public static void WaitAndSendKeys(this IWebDriver driver, By locator, string text)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(_config.ExplicitWait));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            element.Clear();
            element.SendKeys(text);
        }

        public static string WaitAndGetText(this IWebDriver driver, By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(_config.ExplicitWait));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return element.Text;
        }

        public static bool WaitForElementToBeVisible(this IWebDriver driver, By locator, int timeoutInSeconds = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public static void ScrollToElement(this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Thread.Sleep(500); // Small delay to ensure scroll completes
        }
    }
}

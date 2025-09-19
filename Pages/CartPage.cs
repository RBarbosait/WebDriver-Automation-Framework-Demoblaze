using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace WebDriver_Automation_Framework_Demoblaze.Pages
{
    public class CartPage : BasePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly By _cartRows = By.XPath("//*[@id='tbodyid']/tr");

        public CartPage(IWebDriver driver) : base(driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(25))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            _wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        }

        public void EnsureCartPageLoaded()
        {
            if (!_driver.Url.Contains("cart.html"))
            {
                _driver.Navigate().GoToUrl("https://www.demoblaze.com/cart.html");
            }

            _wait.Until(d => ((IJavaScriptExecutor)d)
                .ExecuteScript("return document.readyState").Equals("complete"));
        }

        public int GetCartProductCount(int expectedCount = 1, int timeoutSeconds = 25)
        {
            EnsureCartPageLoaded();

            try
            {
                var localWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
                localWait.Until(d => d.FindElements(_cartRows).Count >= expectedCount);
            }
            catch { /* ignoramos timeout, devolvemos lo que haya */ }

            return _driver.FindElements(_cartRows).Count;
        }
    }
}
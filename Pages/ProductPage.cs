using OpenQA.Selenium;
using WebDriverAutomationFramework.Hooks;



namespace WebDriver_Automation_Framework_Demoblaze.Pages
{
    public class ProductPage:BasePage
    {
        private readonly IWebDriver _driver;

        public ProductPage(): base(WebDriverAutomationFramework.Hooks.Hooks.Driver){}

        public void AddToCart()
        {
            _driver.FindElement(By.CssSelector(".btn.btn-success.btn-lg")).Click();
            _driver.SwitchTo().Alert().Accept();
        }
    }
}
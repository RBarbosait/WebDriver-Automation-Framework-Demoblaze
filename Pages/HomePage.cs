using OpenQA.Selenium;
using WebDriver_Automation_Framework_Demoblaze.Hooks;

namespace WebDriver_Automation_Framework_Demoblaze.Pages
{
    public class ProductPage : BasePage
    {
        public ProductPage() : base(Hooks.Hooks.Driver) { }

        public void AddToCart()
        {
            WaitAndClick(By.CssSelector(".btn.btn-success.btn-lg"));
            Driver.SwitchTo().Alert().Accept();
        }
    }
}
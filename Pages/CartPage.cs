using OpenQA.Selenium;
using WebDriver_Automation_Framework_Demoblaze.Hooks;
using System.Linq;

namespace WebDriver_Automation_Framework_Demoblaze.Pages
{
    public class CartPage : BasePage
    {
        public CartPage() : base(Hooks.Hooks.Driver) { }

        private By CartLink => By.Id("cartur");
        private By ProductRows => By.CssSelector("#tbodyid tr");
        private By PlaceOrderButton => By.CssSelector(".btn.btn-success");

        public void GoToCart() => WaitAndClick(CartLink);

        public int GetProductCount() => Driver.FindElements(ProductRows).Count;

        public void OpenPlaceOrder() => WaitAndClick(PlaceOrderButton);
    }
}
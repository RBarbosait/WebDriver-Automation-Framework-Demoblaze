using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebDriverAutomationFramework.Hooks;

namespace WebDriver_Automation_Framework_Demoblaze.Pages
{
    public class CheckoutPage : BasePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public CheckoutPage() : base(WebDriverAutomationFramework.Hooks.Hooks.Driver) { }

        private By NameField => By.Id("name");
        private By CountryField => By.Id("country");
        private By CityField => By.Id("city");
        private By CardField => By.Id("card");
        private By MonthField => By.Id("month");
        private By YearField => By.Id("year");
        private By PurchaseButton => By.CssSelector("button[onclick='purchaseOrder()']");
        private By ConfirmationMessage => By.CssSelector(".sweet-alert h2");

        public void FillPurchaseForm(string name, string country, string city, string card, string month, string year)
{
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

    // 1️⃣ Clic en Place Order para abrir el modal
    var placeOrderButton = wait.Until(drv => drv.FindElement(By.XPath("//*[@id='page-wrapper']/div/div[2]/button")));
    placeOrderButton.Click();

    // 2️⃣ Esperar a que el modal se muestre
    var modal = wait.Until(drv => drv.FindElement(By.Id("orderModal")));
    wait.Until(drv => modal.Displayed && modal.Enabled);

    // 3️⃣ Llenar los campos del formulario dentro del modal
    modal.FindElement(By.Id("name")).SendKeys(name);
    modal.FindElement(By.Id("country")).SendKeys(country);
    modal.FindElement(By.Id("city")).SendKeys(city);
    modal.FindElement(By.Id("card")).SendKeys(card);
    modal.FindElement(By.Id("month")).SendKeys(month);
    modal.FindElement(By.Id("year")).SendKeys(year);
}

        public void Purchase() => WaitAndClick(PurchaseButton);

        public string GetConfirmationMessage() => WaitAndFindElement(ConfirmationMessage).Text;
    }
}
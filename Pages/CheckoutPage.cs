using OpenQA.Selenium;
using WebDriver_Automation_Framework_Demoblaze.Hooks;

namespace WebDriver_Automation_Framework_Demoblaze.Pages
{
    public class CheckoutPage : BasePage
    {
        public CheckoutPage() : base(Hooks.Hooks.Driver) { }

        private By NameField => By.Id("name");
        private By CountryField => By.Id("country");
        private By CityField => By.Id("city");
        private By CardField => By.Id("card");
        private By MonthField => By.Id("month");
        private By YearField => By.Id("year");
        private By PurchaseButton => By.CssSelector("button[onclick='purchaseOrder()']");
        private By ConfirmationMessage => By.CssSelector(".sweet-alert h2");

        public void FillForm(string name, string country, string city, string card, string month, string year)
        {
            WaitAndSendKeys(NameField, name);
            WaitAndSendKeys(CountryField, country);
            WaitAndSendKeys(CityField, city);
            WaitAndSendKeys(CardField, card);
            WaitAndSendKeys(MonthField, month);
            WaitAndSendKeys(YearField, year);
        }

        public void Purchase() => WaitAndClick(PurchaseButton);

        public string GetConfirmationMessage() => WaitAndFindElement(ConfirmationMessage).Text;
    }
}
using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebDriver_Automation_Framework_Demoblaze.Pages
{
    public class HomePage : BasePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private By productItems = By.XPath("//*[@id='tbodyid']/div[1]/div/a/img"); 
        private By addToCartButton = By.XPath("//*[@id='tbodyid']/div[2]/div/a");

        public HomePage(IWebDriver driver) : base(driver)
    {
        _driver = driver;
    }

        // Navegar a la página principal
        public void NavigateToHomePage()
        {
            _driver.Navigate().GoToUrl("https://www.demoblaze.com/");
        }

        // Agregar producto al carrito por índice
 public void AddProductToCart(int index)
{
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

    // Usar XPath dinámico basado en el índice (los divs empiezan en 1, no en 0)
    string productXPath = $"//*[@id='tbodyid']/div[{index + 1}]/div/a";

    // Esperar que el producto esté visible
    var productLink = wait.Until(d => d.FindElement(By.XPath(productXPath)));
    productLink.Click();

    // Esperar el botón Add to cart en la página de detalle
    var addToCartButton = wait.Until(d => d.FindElement(By.XPath("//a[text()='Add to cart']")));
    addToCartButton.Click();

    // Esperar el alert y aceptarlo
    try
    {
        wait.Until(ExpectedConditions.AlertIsPresent());
        IAlert alert = _driver.SwitchTo().Alert();
        alert.Accept();
    }
    catch (WebDriverTimeoutException)
    {
        throw new Exception("No apareció la alerta después de agregar el producto al carrito.");
    }

    // Volver al home para poder agregar otro producto
    _driver.Navigate().GoToUrl("https://www.demoblaze.com/");
}

        // Abrir carrito
        public void OpenCart()
        {
            _driver.FindElement(By.Id("cartur")).Click();
        }

        // Contar productos en carrito
        public int GetCartProductCount()
        {
            var products = _driver.FindElements(By.CssSelector("#tbodyid > tr"));
            return products.Count;
        }

        // Obtener lista de productos en carrito
        public List<string> GetCartProducts()
        {
            var productElements = _driver.FindElements(By.CssSelector("#tbodyid > tr > td:nth-child(2)"));
            List<string> products = new List<string>();
            foreach (var p in productElements)
                products.Add(p.Text);
            return products;
        }

        // Completar formulario de compra
       public void FillPurchaseForm(string name, string country, string city, string card, string month, string year)
{
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

    // 1️⃣ Clic en Place Order para abrir el modal
    var placeOrderButton = wait.Until(drv => drv.FindElement(By.XPath("//*[@id='page-wrapper']/div/div[2]/button")));
    placeOrderButton.Click();

    // 2️⃣ Esperar explícitamente a que el modal sea visible
    var modal = wait.Until(drv =>
    {
        var m = drv.FindElement(By.Id("orderModal"));
        return (m.Displayed && m.Enabled) ? m : null;
    });

    // 3️⃣ Esperar a que cada input dentro del modal sea interactuable
    wait.Until(drv => modal.FindElement(By.Id("name")).Enabled);
    modal.FindElement(By.Id("name")).SendKeys(name);

    wait.Until(drv => modal.FindElement(By.Id("country")).Enabled);
    modal.FindElement(By.Id("country")).SendKeys(country);

    wait.Until(drv => modal.FindElement(By.Id("city")).Enabled);
    modal.FindElement(By.Id("city")).SendKeys(city);

    wait.Until(drv => modal.FindElement(By.Id("card")).Enabled);
    modal.FindElement(By.Id("card")).SendKeys(card);

    wait.Until(drv => modal.FindElement(By.Id("month")).Enabled);
    modal.FindElement(By.Id("month")).SendKeys(month);

    wait.Until(drv => modal.FindElement(By.Id("year")).Enabled);
    modal.FindElement(By.Id("year")).SendKeys(year);
}

        // Verificar que el formulario está listo para comprar
        public bool IsPurchaseFormAccepted()
        {
            var purchaseButton = _driver.FindElement(By.CssSelector("#orderModal .btn-primary"));
            return purchaseButton.Enabled;
        }

        // Confirmar compra
        public void ConfirmPurchase()
        {
            _driver.FindElement(By.CssSelector("#orderModal .btn-primary")).Click();
        }

        // Obtener mensaje de confirmación
       public string GetConfirmationMessage()
{
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

    // Espera a que el modal de confirmación sea visible
    var modal = wait.Until(drv =>
    {
        var m = drv.FindElement(By.ClassName("sweet-alert"));
        return (m.Displayed && m.Enabled) ? m : null;
    });

    // Espera a que el mensaje dentro del modal sea visible
    var messageElement = wait.Until(drv =>
    {
        var el = modal.FindElement(By.TagName("h2")); // El texto de confirmación normalmente está en <h2>
        return (el.Displayed) ? el : null;
    });

    return messageElement.Text;
}
    }
}
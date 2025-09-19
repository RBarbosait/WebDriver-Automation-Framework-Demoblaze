using System;
using TechTalk.SpecFlow;
using WebDriver_Automation_Framework_Demoblaze.Pages;
using OpenQA.Selenium;
using NUnit.Framework;

namespace WebDriver_Automation_Framework_Demoblaze.StepDefinitions
{
    [Binding]
    public class DemoblazeSteps
    {
        private readonly IWebDriver _driver;
        private readonly HomePage _homePage;

        public DemoblazeSteps(ScenarioContext scenarioContext)
        {
            _driver = scenarioContext.Get<IWebDriver>("WebDriver");
            _homePage = new HomePage(_driver); // ✅ pasa el driver
        }

        [Then(@"se listan los productos agregados")]
        public void ThenSeListanLosProductosAgregados()
        {
            // Usamos el mismo driver que levantamos en el scenario
            var cartPage = new CartPage(_driver);

            // Esperamos al menos 1 producto (puedes parametrizar si quieres)
            int productsCount = cartPage.GetCartProductCount(expectedCount: 1, timeoutSeconds: 25);

            if (productsCount == 0)
                throw new Exception("No hay productos en el carrito");
        }

        [When(@"el usuario accede al carrito")]
        public void WhenElUsuarioAccedeAlCarrito()
        {
            var cartPage = new CartPage(_driver);
            cartPage.EnsureCartPageLoaded(); 
        }

        [Given(@"el usuario navega a la página principal")]
        public void GivenElUsuarioNavegaALaPaginaPrincipal() => _homePage.NavigateToHomePage();

        [When(@"agrega el primer producto al carrito")]
        public void WhenAgregaElPrimerProductoAlCarrito() => _homePage.AddProductToCart(0);

        [When(@"agrega el segundo producto al carrito")]
        public void WhenAgregaElSegundoProductoAlCarrito() => _homePage.AddProductToCart(1);


        [Then(@"el carrito muestra (.*) productos")]
        public void ThenElCarritoMuestraProductos(int expectedCount)
        {
            var cartPage = new CartPage(_driver); // <-- pasamos el mismo driver
            int actualCount = cartPage.GetCartProductCount(expectedCount, 25);

            if (actualCount != expectedCount)
                throw new Exception($"Se esperaban {expectedCount} productos, pero hay {actualCount}");
        }

        [Given(@"el usuario tiene productos en el carrito")]
        public void GivenElUsuarioTieneProductosEnElCarrito()
        {
            _homePage.NavigateToHomePage();
            _homePage.AddProductToCart(0);
            _homePage.AddProductToCart(1);
        }

      

       

        [Given(@"el usuario está en el carrito con productos")]
        public void GivenElUsuarioEstaEnElCarritoConProductos()
        {
            GivenElUsuarioTieneProductosEnElCarrito();
            WhenElUsuarioAccedeAlCarrito();
        }

        [When(@"completa el formulario de compra con datos válidos")]
        public void WhenCompletaElFormularioDeCompraConDatosValidos()
        {
            _homePage.FillPurchaseForm("Rodrigo Test", "Uruguay", "Montevideo", "1234567890123456", "12", "2025");
        }

        [Then(@"el sistema acepta la información ingresada")]
        public void ThenElSistemaAceptaLaInformacionIngresada()
        {
            Assert.IsTrue(_homePage.IsPurchaseFormAccepted(), "El formulario de compra no fue aceptado");
        }

        [Given(@"el usuario completó el formulario de compra")]
        public void GivenElUsuarioCompletoElFormularioDeCompra()
        {
            GivenElUsuarioEstaEnElCarritoConProductos();
            WhenCompletaElFormularioDeCompraConDatosValidos();
        }

        [When(@"confirma la compra")]
        public void WhenConfirmaLaCompra() => _homePage.ConfirmPurchase();

        [Then(@"se muestra un mensaje de confirmación de compra exitosa")]
        public void ThenSeMuestraUnMensajeDeConfirmacionDeCompraExitosa()
        {
            string message = _homePage.GetConfirmationMessage();
            Assert.IsFalse(string.IsNullOrEmpty(message), "No se mostró el mensaje de confirmación");
        }
        
    }
}
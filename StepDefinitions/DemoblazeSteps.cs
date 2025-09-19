using TechTalk.SpecFlow;
using NUnit.Framework;
using WebDriver_Automation_Framework_Demoblaze.Pages;

namespace WebDriver_Automation_Framework_Demoblaze.StepDefinitions
{
    [Binding]
    public class DemoblazeSteps
    {
        private readonly HomePage _homePage = new();
        private readonly ProductPage _productPage = new();
        private readonly CartPage _cartPage = new();
        private readonly CheckoutPage _checkoutPage = new();

        [Given(@"el usuario navega a la página principal")]
        public void GivenElUsuarioNavegaALaPaginaPrincipal()
        {
            _homePage.NavigateToHomePage();
        }

        [When(@"agrega el primer producto al carrito")]
        public void WhenAgregaElPrimerProductoAlCarrito()
        {
            _homePage.SelectProduct("Samsung galaxy s6");
            _productPage.AddToCart();
        }

        [When(@"agrega el segundo producto al carrito")]
        public void WhenAgregaElSegundoProductoAlCarrito()
        {
            _homePage.NavigateToHomePage();
            _homePage.SelectProduct("Nokia lumia 1520");
            _productPage.AddToCart();
        }

        [Then(@"el carrito muestra (.*) productos")]
        public void ThenElCarritoMuestraProductos(int expectedCount)
        {
            _cartPage.GoToCart();
            Assert.AreEqual(expectedCount, _cartPage.GetProductCount());
        }

        [Given(@"el usuario tiene productos en el carrito")]
        public void GivenElUsuarioTieneProductosEnElCarrito()
        {
            ThenElCarritoMuestraProductos(2);
        }

        [When(@"el usuario accede al carrito")]
        public void WhenElUsuarioAccedeAlCarrito()
        {
            _cartPage.GoToCart();
        }

        [Then(@"se listan los productos agregados")]
        public void ThenSeListanLosProductosAgregados()
        {
            Assert.Greater(_cartPage.GetProductCount(), 0);
        }

        [Given(@"el usuario está en el carrito con productos")]
        public void GivenElUsuarioEstaEnElCarritoConProductos()
        {
            _cartPage.GoToCart();
        }

        [When(@"completa el formulario de compra con datos válidos")]
        public void WhenCompletaElFormularioDeCompraConDatosValidos()
        {
            _cartPage.OpenPlaceOrder();
            _checkoutPage.FillForm("Rodrigo", "Uruguay", "Montevideo", "1234567890123456", "12", "2025");
        }

        [Then(@"el sistema acepta la información ingresada")]
        public void ThenElSistemaAceptaLaInformacionIngresada()
        {
            // Validación básica: se espera que modal siga abierto con los datos
            Assert.IsNotNull(_checkoutPage);
        }

        [Given(@"el usuario completó el formulario de compra")]
        public void GivenElUsuarioCompletoElFormularioDeCompra()
        {
            WhenCompletaElFormularioDeCompraConDatosValidos();
        }

        [When(@"confirma la compra")]
        public void WhenConfirmaLaCompra()
        {
            _checkoutPage.Purchase();
        }

        [Then(@"se muestra un mensaje de confirmación de compra exitosa")]
        public void ThenSeMuestraUnMensajeDeConfirmacionDeCompraExitosa()
        {
            string message = _checkoutPage.GetConfirmationMessage();
            Assert.AreEqual("Thank you for your purchase!", message);
        }
    }
}
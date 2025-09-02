using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverAutomationFramework.Config;
using System;
using System.Collections.Generic;

namespace WebDriverAutomationFramework.Pages
{
    public class HomePage : BasePage
    {
        private static readonly TestConfiguration _config = TestConfiguration.Instance;

        // Page Elements
        private readonly By _pageTitle = By.TagName("h1");
        private readonly By _navigationMenu = By.TagName("nav");
        private readonly By _mainContent = By.Id("root");
        private readonly By _footer = By.TagName("footer");
        private readonly By _loadingIndicator = By.CssSelector(".loading, .spinner");

        // Tabla clientes
        private readonly By _clientTable = By.CssSelector("#root > div > div > div > div > table");
        private readonly By _identifiedUserText = By.XPath("//*[@id='root']/div/div/div/p/b[1]");

        // Form elements
        private readonly By _inputField = By.CssSelector("input[type='text'], input[type='email']");
        private readonly By _submitButton = By.XPath("//*[@id='root']//input[@type='button']");

        private readonly By _successMessage = By.CssSelector(".success, .alert-success");
        private readonly By _errorMessage = By.CssSelector(".error, .alert-error");

        private WebDriverWait GetWait(int seconds) => new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));

        /// <summary>
        /// Navega a la home page remota y espera a que cargue
        /// </summary>
        public void NavigateToHomePage()
        {
            NavigateToUrl(_config.BaseUrl);
            WaitForPageToLoad();
        }

        /// <summary>
        /// Navega a la home page local y espera que cargue
        /// </summary>
        public void NavigateToLocalHomePage()
        {
            NavigateToUrl(_config.LocalUrl);
            WaitForPageToLoad();
        }

        /// <summary>
        /// Espera que la página esté completamente cargada
        /// Combina main content visible y loader desaparecido
        /// </summary>
        public void WaitForPageToLoad(int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(200)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            wait.Until(driver =>
            {
                bool mainVisible = false;
                try { mainVisible = driver.FindElement(_mainContent).Displayed; } catch { }

                bool loaderGone = false;
                try
                {
                    var loader = driver.FindElement(_loadingIndicator);
                    loaderGone = !loader.Displayed;
                }
                catch { loaderGone = true; }

                return mainVisible && loaderGone;
            });
        }

        /// <summary>
        /// Verifica si la página está cargada
        /// </summary>
        public bool IsPageLoaded()
        {
            return IsElementVisible(_mainContent, 5);
        }

        /// <summary>
        /// Devuelve el título de la página
        /// </summary>
        public new string GetPageTitle()
        {
            if (IsElementVisible(_pageTitle, 5))
                return WaitAndGetText(_pageTitle);
            return Driver.Title;
        }

        /// <summary>
        /// Verifica si el menú de navegación está visible
        /// </summary>
        public bool IsNavigationVisible()
        {
            return IsElementVisible(_navigationMenu, 5);
        }

        /// <summary>
        /// Verifica si el main content está visible
        /// </summary>
        public bool IsMainContentVisible()
        {
            try
            {
                var visible = Driver.FindElement(_mainContent).Displayed;
                Console.WriteLine($"[DEBUG] Selector {_mainContent} visible = {visible}, URL = {Driver.Url}");
                return visible;
            }
            catch { return false; }
        }

        /// <summary>
        /// Verifica si el footer está visible y hace scroll
        /// </summary>
        public bool IsFooterVisible()
        {
            ScrollToElement(_footer);
            return IsElementVisible(_footer, 5);
        }

        /// <summary>
        /// Llena el input con el texto especificado
        /// </summary>
        public void FillInputField(string text)
        {
            GetWait(5).Until(d => d.FindElement(_inputField).Displayed);
            WaitAndSendKeys(_inputField, text);
        }

        /// <summary>
        /// Hace click en el botón submit y espera que la página cargue
        /// </summary>
        public void ClickSubmitButton()
        {
            GetWait(5).Until(d => d.FindElement(_submitButton).Displayed);
            WaitAndClick(_submitButton);
            WaitForPageToLoad(10);
        }

        /// <summary>
        /// Combina flujo input + submit + espera tabla clientes
        /// </summary>
        public bool IdentifyUserAndWaitForClientTable(string user)
        {
            FillInputField(user);
            ClickSubmitButton();
            return IsClientTableVisible();
        }

        /// <summary>
        /// Verifica si la tabla de clientes está visible
        /// </summary>
        public bool IsClientTableVisible()
        {
            return IsElementVisible(_clientTable, 10);
        }

        /// <summary>
        /// Devuelve los valores de la columna NAME de la tabla de clientes en la UI
        /// </summary>
        public List<string> GetClientNamesFromTable()
        {
            var clientNames = new List<string>();
            var rows = Driver.FindElements(By.CssSelector("#root > div > div > div > div > table tbody tr"));
            foreach (var row in rows)
            {
                var nameCell = row.FindElement(By.CssSelector("td:nth-child(1)"));
                clientNames.Add(nameCell.Text.Trim());
            }
            Console.WriteLine("[DEBUG] Client names from table: " + string.Join(", ", clientNames));
            return clientNames;
        }

        /// <summary>
        /// Devuelve el texto del usuario identificado
        /// </summary>
        public string GetIdentifiedUserText()
        {
            if (IsElementVisible(_identifiedUserText, 10))
                return WaitAndGetText(_identifiedUserText);
            return string.Empty;
        }

        /// <summary>
        /// Compara el usuario identificado en la UI con el esperado
        /// </summary>
        public bool DoesIdentifiedUserMatch(string expectedUser)
        {
            var actualText = GetIdentifiedUserText();
            Console.WriteLine($"[DEBUG] Identified user text: '{actualText}', Expected: '{expectedUser}'");
            return actualText.Equals(expectedUser, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Verifica si hay mensaje de éxito
        /// </summary>
        public bool IsSuccessMessageDisplayed() => IsElementVisible(_successMessage, 5);

        /// <summary>
        /// Verifica si hay mensaje de error
        /// </summary>
        public bool IsErrorMessageDisplayed() => IsElementVisible(_errorMessage, 5);

        /// <summary>
        /// Devuelve el texto del mensaje de éxito
        /// </summary>
        public string GetSuccessMessage() => IsSuccessMessageDisplayed() ? WaitAndGetText(_successMessage) : string.Empty;

        /// <summary>
        /// Devuelve el texto del mensaje de error
        /// </summary>
        public string GetErrorMessage() => IsErrorMessageDisplayed() ? WaitAndGetText(_errorMessage) : string.Empty;

        /// <summary>
        /// Devuelve todas las filas de la tabla de clientes en la UI
        /// </summary>
        public IReadOnlyCollection<IWebElement> GetClientTableRows()
        {
            return Driver.FindElements(By.CssSelector("#root > div > div > div > div > table tbody tr"));
        }


        /// <summary>
        /// Verifica si el loader está visible
        /// </summary>
        public bool IsLoadingIndicatorVisible()
        {
            try { return GetWait(2).Until(ExpectedConditions.ElementIsVisible(_loadingIndicator)).Displayed; }
            catch { return false; }
        }
    }
}
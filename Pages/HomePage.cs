using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverAutomationFramework.Config;
using System;

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

        //tabla clientes

        //private readonly By _clientTable = By.CssSelector("table#clients, .clients-table");
        private readonly By _clientTable = By.CssSelector("#root > div > div > div > div > table");


        // Form elements
        private readonly By _inputField = By.CssSelector("input[type='text'], input[type='email']");
        // private readonly By _submitButton = By.CssSelector("button[type='submit'], .submit-btn");
        private readonly By _submitButton = By.XPath("//*[@id='root']//input[@type='button']");

        private readonly By _successMessage = By.CssSelector(".success, .alert-success");
        private readonly By _errorMessage = By.CssSelector(".error, .alert-error");

        // Helper for explicit wait
        private WebDriverWait GetWait(int seconds) => new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));

        public void NavigateToHomePage()
        {
            NavigateToUrl(_config.BaseUrl);
            WaitForLoadingToComplete();
        }

        public void NavigateToLocalHomePage()
        {
            NavigateToUrl(_config.LocalUrl);
            WaitForLoadingToComplete();
        }

        public bool IsClientTableVisible()
        {
            return IsElementVisible(_clientTable, 15);
        }
       
           public bool IsPageLoaded(){
            /* By[] possibleMainContainers =
             {
                 By.TagName("main"),
                 By.Id("content"),
                 By.CssSelector(".container"),
                 By.Id("root")
             };

             foreach (var locator in possibleMainContainers)
             {
                 if (IsElementVisible(locator, 10))
                 {
                     return true;
                 }
             }*/

            return IsElementVisible(_mainContent, 15);

        }

        public void ClickSubmitButton()
        {
            Console.WriteLine("[DEBUG] Trying to click submit button...");
            if (IsElementVisible(_submitButton, 5))
            {
                WaitAndClick(_submitButton);
                Console.WriteLine("[DEBUG] Clicked submit button OK");
                WaitForLoadingToComplete();
            }
            else
            {
                Console.WriteLine("[DEBUG] Submit button not found with selector");
            }
        }

        public new string GetPageTitle()
        {
            if (IsElementVisible(_pageTitle, 5))
            {
                return WaitAndGetText(_pageTitle);
            }
            return Driver.Title;
        }

        public bool IsNavigationVisible()
        {
            return IsElementVisible(_navigationMenu, 5);
        }

       //resumi flujo input click verify
        public bool IdentifyUserAndWaitForClientTable(string user)
        {
            FillInputField(user);
            ClickSubmitButton();
            return IsClientTableVisible();
        }


    
            public bool IsMainContentVisible()
        {
            bool result = IsElementVisible(_mainContent, 10);
            Console.WriteLine($"[DEBUG] Selector {_mainContent} visible = {result}, URL = {Driver.Url}");
            return result;
        }
           // return IsElementVisible(_mainContent, 10);
        

        public bool IsFooterVisible()
        {
            ScrollToElement(_footer);
            return IsElementVisible(_footer, 5);
        }

        public void FillInputField(string text)
        {
            if (IsElementVisible(_inputField, 5))
            {
                WaitAndSendKeys(_inputField, text);
            }
        }

        /*public void ClickSubmitButton()
        {
            if (IsElementVisible(_submitButton, 5))
            {
                WaitAndClick(_submitButton);
                WaitForLoadingToComplete();
            }
        }*/

        public bool IsSuccessMessageDisplayed()
        {
            return IsElementVisible(_successMessage, 10);
        }


        public bool IsErrorMessageDisplayed()
        {
            return IsElementVisible(_errorMessage, 10);
        }

        public string GetSuccessMessage()
        {
            if (IsSuccessMessageDisplayed())
            {
                return WaitAndGetText(_successMessage);
            }
            return string.Empty;
        }

        public string GetErrorMessage()
        {
            if (IsErrorMessageDisplayed())
            {
                return WaitAndGetText(_errorMessage);
            }
            return string.Empty;
        }

        public bool IsLoadingIndicatorVisible()
        {
            try
            {
                return GetWait(2).Until(ExpectedConditions.ElementIsVisible(_loadingIndicator)).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void WaitForLoadingToComplete(int timeoutSeconds = 30)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(driver =>
            {
                try
                {
                    return !driver.FindElement(_loadingIndicator).Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true; // El loader no existe → ya cargó
                }
                catch (StaleElementReferenceException)
                {
                    return true; // Se eliminó el loader del DOM
                }
            });
        }
    }
}

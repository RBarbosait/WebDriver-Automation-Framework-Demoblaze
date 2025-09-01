using OpenQA.Selenium;
using WebDriverAutomationFramework.Config;

namespace WebDriverAutomationFramework.Pages
{
    public class HomePage : BasePage
    {
        private static readonly TestConfiguration _config = TestConfiguration.Instance;

        // Page Elements
        private readonly By _pageTitle = By.TagName("h1");
        private readonly By _navigationMenu = By.TagName("nav");
        private readonly By _mainContent = By.TagName("main");
        private readonly By _footer = By.TagName("footer");
        private readonly By _loadingIndicator = By.CssSelector(".loading, .spinner");
        
        // Form elements (assuming there might be a form)
        private readonly By _inputField = By.CssSelector("input[type='text'], input[type='email']");
        private readonly By _submitButton = By.CssSelector("button[type='submit'], .submit-btn");
        private readonly By _successMessage = By.CssSelector(".success, .alert-success");
        private readonly By _errorMessage = By.CssSelector(".error, .alert-error");

        public void NavigateToHomePage()
        {
            NavigateToUrl(_config.BaseUrl);
        }

        public void NavigateToLocalHomePage()
        {
            NavigateToUrl(_config.LocalUrl);
        }

        public bool IsPageLoaded()
        {
            return IsElementVisible(_mainContent, 15);
        }

        public string GetPageTitle()
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

        public bool IsMainContentVisible()
        {
            return IsElementVisible(_mainContent, 10);
        }

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

        public void ClickSubmitButton()
        {
            if (IsElementVisible(_submitButton, 5))
            {
                WaitAndClick(_submitButton);
            }
        }

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
            return IsElementVisible(_loadingIndicator, 2);
        }

        public void WaitForLoadingToComplete()
        {
            // Wait for loading indicator to disappear
            var maxWaitTime = 30;
            var waitTime = 0;
            
            while (IsLoadingIndicatorVisible() && waitTime < maxWaitTime)
            {
                Thread.Sleep(1000);
                waitTime++;
            }
        }
    }
}

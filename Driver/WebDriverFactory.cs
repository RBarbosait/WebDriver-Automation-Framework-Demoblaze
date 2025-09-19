using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriverAutomationFramework.Drivers
{
    public static class WebDriverFactory
    {
        // Crea y devuelve un WebDriver configurado
        public static IWebDriver CreateWebDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-popup-blocking");

            return new ChromeDriver(options);
        }
    }
}
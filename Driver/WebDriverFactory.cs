using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace WebDriver_Automation_Framework_Demoblaze.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver(string browser = "chrome")
        {
            IWebDriver driver;

            switch (browser.ToLower())
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "chrome":
                default:
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--start-maximized");
                    driver = new ChromeDriver(chromeOptions);
                    break;
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }
    }
}
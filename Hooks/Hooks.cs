using OpenQA.Selenium;
using TechTalk.SpecFlow;
using WebDriver_Automation_Framework_Demoblaze.Drivers;

namespace WebDriver_Automation_Framework_Demoblaze.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private static IWebDriver _driver;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver = WebDriverFactory.CreateDriver();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Quit();
        }

        public static IWebDriver Driver => _driver;
    }
}
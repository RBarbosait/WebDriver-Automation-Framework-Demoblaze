using TechTalk.SpecFlow;
using OpenQA.Selenium;
using WebDriverAutomationFramework.Drivers;

namespace WebDriverAutomationFramework.Hooks
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver Driver { get; set; }
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
               // Crear WebDriver y agregar al contenedor de SpecFlow
               _driver = WebDriverFactory.CreateWebDriver();
               _scenarioContext["WebDriver"] = _driver;
            
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}
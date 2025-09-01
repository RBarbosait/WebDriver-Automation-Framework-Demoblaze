using TechTalk.SpecFlow;
using WebDriverAutomationFramework.Driver;

namespace WebDriverAutomationFramework.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly ScenarioContext _scenarioContext;

        public TestHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Console.WriteLine($"Starting scenario: {_scenarioContext.ScenarioInfo.Title}");
            WebDriverManager.InitializeDriver();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.TestError != null)
            {
                Console.WriteLine($"Scenario failed: {_scenarioContext.TestError.Message}");
                WebDriverManager.TakeScreenshot(_scenarioContext.ScenarioInfo.Title);
            }

            Console.WriteLine($"Completed scenario: {_scenarioContext.ScenarioInfo.Title}");
            WebDriverManager.QuitDriver();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Console.WriteLine($"Starting feature: {featureContext.FeatureInfo.Title}");
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            Console.WriteLine($"Completed feature: {featureContext.FeatureInfo.Title}");
        }
    }
}

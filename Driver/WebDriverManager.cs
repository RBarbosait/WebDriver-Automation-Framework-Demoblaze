using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverAutomationFramework.Config;
using WDM = WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace WebDriverAutomationFramework.Driver
{
    public class WebDriverManager
    {
        private static readonly ThreadLocal<IWebDriver> _driver = new();
        private static readonly TestConfiguration _config = TestConfiguration.Instance;

        public static IWebDriver GetDriver()
        {
            return _driver.Value!;
        }

        public static void InitializeDriver()
        {
            if (_driver.Value == null)
            {
                _driver.Value = CreateDriver(_config.Browser);
                ConfigureDriver();
            }
        }

        private static IWebDriver CreateDriver(string browserName)
        {
            return browserName.ToLower() switch
            {
                "chrome" => CreateChromeDriver(),
                "firefox" => CreateFirefoxDriver(),
                "edge" => CreateEdgeDriver(),
                _ => throw new ArgumentException($"Browser '{browserName}' is not supported")
            };
        }

        private static IWebDriver CreateChromeDriver()
        {
            var driverManager = new WDM.DriverManager();
            driverManager.SetUpDriver(new ChromeConfig(), "LATEST");
            
            var options = new ChromeOptions();
            
            if (_config.RunHeadless)
            {
                options.AddArgument("--headless");
            }
            
            options.AddArguments(
                "--no-sandbox",
                "--disable-dev-shm-usage",
                "--disable-gpu",
                "--window-size=1920,1080",
                "--disable-extensions",
                "--disable-web-security",
                "--allow-running-insecure-content",
                "--remote-debugging-port=9222"
            );

            return new ChromeDriver(options);
        }

        private static IWebDriver CreateFirefoxDriver()
        {
            new WDM.DriverManager().SetUpDriver(new FirefoxConfig(), "LATEST");
            
            var options = new FirefoxOptions();
            
            if (_config.RunHeadless)
            {
                options.AddArgument("--headless");
            }
            
            return new FirefoxDriver(options);
        }

        private static IWebDriver CreateEdgeDriver()
        {
            new WDM.DriverManager().SetUpDriver(new EdgeConfig(), "LATEST");
            
            var options = new EdgeOptions();
            
            if (_config.RunHeadless)
            {
                options.AddArgument("--headless");
            }
            
            return new EdgeDriver(options);
        }

        private static void ConfigureDriver()
        {
            var driver = GetDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_config.ImplicitWait);
        }

        public static void QuitDriver()
        {
            if (_driver.Value != null)
            {
                _driver.Value.Quit();
                _driver.Value = null;
            }
        }

        public static void TakeScreenshot(string testName)
        {
            if (!_config.TakeScreenshotOnFailure) return;

            try
            {
                var driver = GetDriver();
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                var filePath = Path.Combine("Screenshots", fileName);
                
                Directory.CreateDirectory("Screenshots");
                screenshot.SaveAsFile(filePath);
                
                Console.WriteLine($"Screenshot saved: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to take screenshot: {ex.Message}");
            }
        }
    }
}

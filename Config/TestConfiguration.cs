using Microsoft.Extensions.Configuration;

namespace WebDriverAutomationFramework.Config
{
    public class TestConfiguration
    {
        private static TestConfiguration? _instance;
        private readonly IConfiguration _configuration;

        private TestConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            _configuration = builder.Build();
        }

        public static TestConfiguration Instance => _instance ??= new TestConfiguration();

        public string BaseUrl => _configuration["TestSettings:BaseUrl"] ?? "https://frontend-and-backend-jet.vercel.app";
        public string LocalUrl => _configuration["TestSettings:LocalUrl"] ?? "http://localhost:3000";
        public string Browser => _configuration["TestSettings:Browser"] ?? "Chrome";
        public int ImplicitWait => int.Parse(_configuration["TestSettings:ImplicitWait"] ?? "10");
        public int ExplicitWait => int.Parse(_configuration["TestSettings:ExplicitWait"] ?? "30");
        public bool RunHeadless => bool.Parse(_configuration["TestSettings:RunHeadless"] ?? "false");
        public bool TakeScreenshotOnFailure => bool.Parse(_configuration["TestSettings:TakeScreenshotOnFailure"] ?? "true");
    }
}

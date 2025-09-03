using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;
using WebDriverAutomationFramework.Pages;

namespace WebDriverAutomationFramework.StepDefinitions
{

    [Binding]
    public class WebApplicationSteps
    {
        private readonly HomePage _homePage;
        private readonly ScenarioContext _scenarioContext;

        public WebApplicationSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _homePage = new HomePage();
        }

        [When(@"I fill the input field with ""(.*)""")]
        public void WhenIFillTheInputFieldWith(string text)
        {
            _homePage.FillInputField(text);
        }


        [Given(@"the web application is running")]
        public static void GivenTheWebApplicationIsRunning()
        {
            // This step assumes the application running a real scenario server is running
            Console.WriteLine("Assuming web application is running and accessible");
        }

        [Given(@"I am on the home page")]
        public void GivenIAmOnTheHomePage()
        {
            _homePage.NavigateToLocalHomePage();
            _homePage.IsPageLoaded().Should().BeTrue("Home page should load successfully");
        }

        [When(@"I navigate to the home page")]
        public void WhenINavigateToTheHomePage()
        {
            _homePage.NavigateToLocalHomePage();
        }

        
        [When(@"the page loads completely")]
        public void WhenThePageLoadsCompletely()
        {
            // _homePage.WaitForLoadingToComplete();
            _homePage.IsPageLoaded().Should().BeTrue("Page should load completely");

        }

        [Then(@"the page should load completely")]
        public void ThenThePageShouldLoadCompletely()
        {
            // _homePage.WaitForLoadingToComplete();
            _homePage.IsPageLoaded().Should().BeTrue("Page should load completely");

        }

        [Then(@"the main content should be visible")]
        public void ThenTheMainContentShouldBeVisible()
        {
            bool isVisible = _homePage.IsMainContentVisible();
            isVisible.Should().BeTrue("the main content area must be displayed");
        }

        [When(@"I leave the input field empty")]
        public void WhenILeaveTheInputFieldEmpty()
        {
            // Aseguramos limpiar el campo si tiene algo
            _homePage.FillInputField(""); 
        }

        /* [When(@"I fill the input field with ""(.*)""")]
         public void WhenIFillTheInputFieldWith(string text)
         {
             _homePage.FillInputField(text);
         }*/



        //click submit standard:

        [When(@"I click the submit button")]
        public void WhenIClickTheSubmitButton()
        {
            _homePage.ClickSubmitButton();
        }
        // click submit negative alert:
        [When(@"I click the submit button expecting an alert")]
        public void WhenIClickTheSubmitButtonExpectingAlert()
        {
            // Para el escenario con input vacío
            _homePage.ClickSubmitButton(expectAlert: true);
        }


        [Then(@"the client list table should be visible")]
        public void ThenTheClientListTableShouldBeVisible()
        {
            _homePage.IsClientTableVisible().Should().BeTrue("Client list table should be visible after login");
        }

        
        [Then(@"the client list table should be visible and the identified user should match ""(.*)""")]
        public void ThenTheClientListTableShouldBeVisibleAndTheIdentifiedUserShouldMatch(string expectedUser)
        {
            // 1. Verificar que la tabla de clientes es visible
            bool isTableVisible = _homePage.IsClientTableVisible();
            isTableVisible.Should().BeTrue("Client list table should be visible after login");

            // 2. Verificar que el usuario identificado coincide
            bool userMatches = _homePage.DoesIdentifiedUserMatch(expectedUser);
            userMatches.Should().BeTrue($"Identified user should match the expected value '{expectedUser}'");
        }


        [Then(@"the page title should be displayed")]
        public void ThenThePageTitleShouldBeDisplayed()
        {
            string title = _homePage.GetPageTitle();
            title.Should().NotBeNullOrEmpty("Page title should be displayed");
            Console.WriteLine($"[DEBUG] Page title = '{title}'");
        }


        [Then(@"the client table ""Size"" column values should match employee counts")]
        public void ThenTheClientTableSizeColumnValuesShouldMatchEmployeeCounts()
        {
            var rows = _homePage.GetClientTableRows();

            int rowIndex = 0;
            foreach (var row in rows)
            {
                rowIndex++;

                var nameText = row.FindElement(By.CssSelector("td:nth-child(1)")).Text.Trim(); // columna Name
                var employeesText = row.FindElement(By.CssSelector("td:nth-child(2)")).Text.Trim(); // columna Employees
                var sizeText = row.FindElement(By.CssSelector("td:nth-child(3)")).Text.Trim(); // columna Size

                if (!int.TryParse(employeesText, out int employees))
                {
                    throw new Exception($"[ERROR] No se pudo parsear Employees en fila {rowIndex} (Cliente={nameText}): '{employeesText}'");
                }

                // Nueva lógica de negocio
                string expectedSize = employees switch
                {
                    <= 100 => "Small",
                    >= 101 and <= 999 => "Medium",
                    _ => "Big"
                };

                try
                {
                    sizeText.Should().Be(expectedSize,
                        $"Employee count {employees} debería corresponder con Size '{expectedSize}' según la regla de negocio");
                    Console.WriteLine($"[OK] Row={rowIndex}, Client={nameText}, Employees={employees}, Size={sizeText}, Expected={expectedSize}");
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[FAIL] Row={rowIndex}, Client={nameText}, Employees={employees}, Size from UI='{sizeText}', Expected='{expectedSize}'");
                    Console.ResetColor();
                    throw;
                }
            }
        }

         [Then(@"an alert should be displayed with message ""(.*)""")]
        public void ThenAnAlertShouldBeDisplayedWithMessage(string expectedMessage)
        {
            var wait = new WebDriverWait(_homePage.GetWebDriver(), TimeSpan.FromSeconds(5));
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

            string alertText = alert.Text;
            Console.WriteLine($"[DEBUG] Alert text: {alertText}");

            alertText.Should().Be(expectedMessage, $"because alert should say '{expectedMessage}' when input is empty");

            // Cerramos el alert
            alert.Accept();
        }

      /*   [When(@"I leave the input field empty")]
        public void WhenILeaveTheInputFieldEmpty()
        {
            // Aseguramos limpiar el campo si tiene algo
            _homePage.FillInputField(""); 
        }*/


        [Then(@"the client names in the table should match the backend response")]
        public async Task ThenTheClientNamesInTheTableShouldMatchBackendResponse()
        {
            // 1️⃣ Nombres de la UI
            var uiClientNames = _homePage.GetClientNamesFromTable();

            // 2️⃣ Request al backend
            using var client = new HttpClient();
            var requestBody = new StringContent("{}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:3001/", requestBody);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var customers = JObject.Parse(jsonResponse)["customers"];

            var backendClientNames = customers.Select(c => c["name"].ToString()).ToList();

            Console.WriteLine("[DEBUG] Client names from backend: " + string.Join(", ", backendClientNames));

            // 3️⃣ Comparación
            uiClientNames.Should().BeEquivalentTo(backendClientNames, options => options.WithStrictOrdering(),
                "The client names displayed in the UI should match the backend response");
        }

        //check the server response

        [Then(@"the server response status code should be 200")]
        public async Task ThenTheServerResponseStatusCodeShouldBe200()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3001"); // backend URL

            var requestBody = new { email = "test@example.com" }; // opcional: puedes parametrizar
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestBody),
                                            System.Text.Encoding.UTF8,
                                            "application/json");

            var response = await client.PostAsync("/", content);

            int statusCode = (int)response.StatusCode;
            Console.WriteLine($"[DEBUG] Backend response status code: {statusCode}");

            statusCode.Should().Be(200, "the backend should respond with 200 OK on valid form submission");
        }

    }
}
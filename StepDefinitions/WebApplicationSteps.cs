using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
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

        /* [When(@"I fill the input field with ""(.*)""")]
         public void WhenIFillTheInputFieldWith(string text)
         {
             _homePage.FillInputField(text);
         }*/

        [When(@"I click the submit button")]
        public void WhenIClickTheSubmitButton()
        {
            _homePage.ClickSubmitButton();
        }

        [Then(@"the client list table should be visible")]
        public void ThenTheClientListTableShouldBeVisible()
        {
            _homePage.IsClientTableVisible().Should().BeTrue("Client list table should be visible after login");
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
    }
}
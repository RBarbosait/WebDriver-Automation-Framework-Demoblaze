using System;
using RestSharp;
using TechTalk.SpecFlow;
using FluentAssertions;
using TechTalk.SpecFlow.Assist;

namespace WebDriver_Automation_Framework_Demoblaze.Features.Api.StepDefinitions
{
    [Binding]
    public class ApiSteps
    {
        private string _baseUrl;
        private RestClient _client;
        private RestResponse _response;

        [Given(@"the API base URL is ""(.*)""")]
        public void GivenTheApiBaseUrlIs(string baseUrl)
        {
            _baseUrl = baseUrl;
            _client = new RestClient(_baseUrl);
        }

        [When(@"I send POST request to ""(.*)"" with body")]
        public void WhenISendPostRequestToWithBody(string endpoint, Table table)
        {
            var body = table.CreateInstance<RequestBody>(); // Convierte a objeto
            var request = new RestRequest(endpoint, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(body);

            _response = _client.Execute(request);
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            ((int)_response.StatusCode).Should().Be(statusCode);
        }

        [Then(@"the response should contain message ""(.*)""")]
        public void ThenTheResponseShouldContainMessage(string expectedMessage)
        {
            _response.Content.Should().Contain(expectedMessage);
        }

        // Clase para mapear el body del POST
        private class RequestBody
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}
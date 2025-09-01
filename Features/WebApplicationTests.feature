Feature: Web Application Testing
    As a user
    I want to interact with the web application
    So that I can verify its functionality

Background:
    Given the web application is running

@smoke @regression
Scenario: Verify home page loads successfully
    When I navigate to the home page
    Then the page should load completely
    And the main content should be visible
    And the page title should be displayed

@functional @regression
Scenario: Verify page navigation and elements
    Given I am on the home page
    When the page loads completely
    Then the navigation menu should be visible
    And the main content area should be displayed
    And the footer should be visible at the bottom
    And the page should have a valid title

@form @regression
Scenario Outline: Verify form interaction functionality
    Given I am on the home page
    When I fill the input field with "<input_text>"
    And I click the submit button
    Then I should see a response within 10 seconds
    And the form interaction should be completed

Examples:
    | input_text          |
    | test@example.com    |
    | Hello World         |
    | 123456789          |

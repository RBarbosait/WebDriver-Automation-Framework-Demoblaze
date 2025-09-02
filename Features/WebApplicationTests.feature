Feature: Web Application Testing
    As a user
    I want to interact with the web application
    So that I can verify its functionality

Background:
    Given the web application is running

@smoke @regression
Scenario: Verify client list displayed after system identification
    When I navigate to the home page
    Then the page should load completely
    And the main content should be visible
    When I fill the input field with "test@example.com"
    And I click the submit button
    Then the client list table should be visible and the identified user should match "test@example.com"
    And the page title should be displayed

@functional @regression
Scenario: Verify page navigation and elements
    Given I am on the home page
    When the page loads completely
    When I fill the input field with "test@example.com"
    And I click the submit button
    Then the client list table should be visible
    And the client names in the table should match the backend response

@functional2 @regression
Scenario: Verify client size classification matches employees count - business size rule
    Given I am on the home page
    When the page loads completely
    When I fill the input field with "test@example.com"
    And I click the submit button
    Then the client list table should be visible
    And the client table "Size" column values should match employee counts 
        # the client table Size column value should correspond with the # of Employees applying the business rule:
        # 0–100 → Small
        # 101–999 → Medium
        # ≥1000 → Big


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

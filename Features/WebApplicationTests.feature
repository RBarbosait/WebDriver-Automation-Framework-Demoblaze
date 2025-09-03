Feature: Web Application Testing
    As a user
    I want to interact with the web application
    So that I can verify its functionality

Background:
    Given the web application is running

@form @regression @positive
Scenario Outline: Verify client list displayed after system identification and welcome msg
    When I navigate to the home page
    Then the page should load completely
    And the main content should be visible
    When I fill the input field with "<input_text>"
    And I click the submit button
    Then the client list table should be visible and the identified user should match "<input_text>"
    And the page title should be displayed

    Examples:
    | input_text          |
    | test@example.com    |
    | Rodrigo Test        |

@form @regression @negative
Scenario: Verify alert message when input is left empty
    When I navigate to the home page
    Then the page should load completely
    And the main content should be visible
    When I leave the input field empty
    And I click the submit button expecting an alert
    Then an alert should be displayed with message "Please provide your name"

@functional0 @ClientListBackendVsFrontend  @regression
Scenario: Verify clients table display and client name data table content match backend response
    Given I am on the home page
    When the page loads completely
    When I fill the input field with "test@example.com"
    And I click the submit button
    Then the client list table should be visible
    And the client names in the table should match the backend response

@functional1 @ClientSizeBusinessRule @regression
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


@smoke @regression
Scenario: Verify form submission returns 200 OK
    Given I am on the home page
    When I fill the input field with "test@example.com"
    And I click the submit button
    Then the server response status code should be 200


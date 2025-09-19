WebDriver Automation Framework – Demoblaze QA Project
📖 Overview

This project demonstrates QA automation for the Demoblaze web application
, a demo e-commerce platform.

The automation validates realistic user scenarios including product selection, cart management, and purchase workflow:

Navigate to the home page and browse products.

Add one or more products to the shopping cart.

Verify the cart contains the correct number of items.

Complete the purchase form with valid data.

Confirm the order and verify the success message.

This ensures end-to-end UI and functional validation of the e-commerce flow.

⚙️ Project Setup

Clone the repository:

git clone https://github.com/RBarbosait/WebDriver-Automation-Framework-Demoblaze.git
cd WebDriver-Automation-Framework-Demoblaze


Install .NET (if not installed):
https://dotnet.microsoft.com/es-es/download

Add ChromeDriver:

dotnet add package Selenium.WebDriver.ChromeDriver --version 139.0.7339.128


(adjust version to match your local Chrome version)

Restore and build the solution:

dotnet restore WebDriverAutomationFramework.csproj
dotnet build WebDriverAutomationFramework.csproj


Run All Automated Tests:

dotnet test WebDriverAutomationFramework.csproj


Run Tests by Category:

dotnet test --filter "TestCategory=form"
dotnet test --filter "TestCategory=smoke"
dotnet test --filter "TestCategory=finalizar"


Utilities:

kill-chrome-processes.bat → closes all running ChromeDriver processes.

📋 Automated Test Scenarios

Home & Cart Tests

Navigate to home page and verify products load.

Add one or more products to the cart.

Validate the cart displays the correct number of items.

Purchase Form Tests

Complete the purchase form with valid data.

Verify the system accepts the input.

Confirm the purchase success message appears.

End-to-End Purchase

Select products, complete the form, finalize purchase.

Validate success messages and flow consistency.

📂 Project Structure
WebDriver-Automation-Framework-Demoblaze/
├── Config/
│   └── TestConfiguration.cs      # Centralized test settings
├── Driver/
│   └── WebDriverManager.cs       # Thread-safe WebDriver management
├── Features/
│   └── DemoblazePurchase.feature # BDD scenarios in Gherkin
├── Hooks/
│   └── Hooks.cs                  # Automatic Setup/Teardown
├── Pages/
│   ├── BasePage.cs               # Base Page Object
│   ├── HomePage.cs               # Home page interactions
│   └── CartPage.cs               # Cart page interactions
├── StepDefinitions/
│   └── DemoblazeSteps.cs         # SpecFlow step implementations
├── Utils/
│   └── WebElementExtensions.cs   # Custom Selenium helpers
├── appsettings.json              # Project configuration
├── specflow.json                 # SpecFlow configuration
└── WebDriverAutomationFramework.csproj

🛠️ Tech Stack

Automation: C#, .NET, Selenium WebDriver, SpecFlow (Gherkin), NUnit, FluentAssertions

Browser: Chrome (via ChromeDriver)

Version Control: Git

CI/CD Compatible – Automation can be executed in pipelines


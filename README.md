# Customer App – QA Automation Project

## 📖 Overview
This project demonstrates **QA automation** for the [frontend-and-backend app](https://github.com/wlsf82/frontend-and-backend).

The application was selected because of its **clean UI and backend integration**, allowing the design of a realistic **QA Story**:
- A user enters their name to access the system.
- A **header** is displayed with the welcome message and date.
- A **customer list** is shown, including name, number of employees, and size classification (Small, Medium, Big).
- Each customer has a detail page with contact information.

---

## ⚙️ Project Setup

Clone the repository:
```bash
git clone https://github.com/wlsf82/frontend-and-backend.git
cd frontend-and-backend
```

Frontend:
```bash
cd frontend
npm install
npm start
```

Backend:
```bash
cd backend
npm install
npm start
```

The app will now be available locally, serving both frontend and backend components.

🤖 QA Automation

Automation scripts are implemented in C# with SpecFlow, Selenium WebDriver, NUnit, and FluentAssertions.
They validate both UI flows and backend data consistency.

Clone and Setup:
```bash
git clone https://github.com/RBarbosait/WebDriver-Automation-Framework-with-C-and-SpecFlow-d6
cd WebDriver-Automation-Framework-with-C-and-SpecFlow-d6
```

Install .NET:
Download and install from: https://dotnet.microsoft.com/es-es/

Add ChromeDriver:
```bash
dotnet add package Selenium.WebDriver.ChromeDriver --version 139.0.7258.15400
``` 
*(change to the correct version if needed)

Build the Solution:
```bash
dotnet restore WebDriver-Automation-Framework-with-C-and-SpecFlow-d6.sln
dotnet build WebDriver-Automation-Framework-with-C-and-SpecFlow-d6.sln
```

Run All Automated Tests:
```bash
dotnet test WebDriverAutomationFramework.csproj
```

Run Tests by Category:
You can filter and run specific categories of tests:
```bash
dotnet test --filter "TestCategory=smoke"
dotnet test --filter "TestCategory=functional"
dotnet test --filter "TestCategory=form"
```
Utils:
kill-chrome-processes.bat → closes all ChromeDriver processes.

📋 Automated Test Cases

Positive form submission – verifies header, welcome message, and client list load.
Negative form submission – validates alert message when input is empty.
Client data validation – ensures UI customer data matches backend response.
Business rules validation – confirms size classification rules (0–100 Small, 101–999 Medium, ≥1000 Big).
Smoke test – checks that form submission returns a 200 OK status.

📂 Project Structure
```bash
graphql
```
WebDriver-Automation-Framework-with-C-and-SpecFlow-d6/
├── Config/
│   └── TestConfiguration.cs      # Centralized configuration
├── Driver/
│   └── WebDriverManager.cs       # Thread-safe WebDriver management
├── Features/
│   └── WebApplicationTests.feature # BDD scenarios in Gherkin
├── Hooks/
│   └── TestHooks.cs              # Automatic Setup/Teardown
├── Pages/
│   ├── BasePage.cs               # Base class for Page Objects
│   └── HomePage.cs               # Page Object for the home page
├── StepDefinitions/
│   └── WebApplicationSteps.cs    # Cucumber step implementations
├── Utils/
│   └── WebElementExtensions.cs   # Custom extensions
├── appsettings.json              # Application configuration
├── specflow.json                 # SpecFlow configuration
└── WebDriverAutomationFramework.csproj

🛠️ Tech Stack

Frontend & Backend: Node.js + npm

Automation: C#, SpecFlow (Gherkin syntax), Selenium WebDriver, NUnit, FluentAssertions

Version control: Git



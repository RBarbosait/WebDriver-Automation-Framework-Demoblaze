# WebDriver Automation Framework – Demoblaze QA Project

## 📖 Overview
This project demonstrates **QA automation** for the [Demoblaze web application](https://www.demoblaze.com/).

It covers the main **purchase workflow**:
- Navigate to homepage
- Add products to cart
- Verify cart contents
- Complete the purchase form
- Confirm purchase success

---

## ⚙️ Project Setup

# 1️⃣ Clone repository
```bash
git clone https://github.com/RBarbosait/WebDriver-Automation-Framework-Demoblaze.git
cd WebDriver-Automation-Framework-Demoblaze
2️⃣ Install .NET SDK
Download and install from: https://dotnet.microsoft.com/es-es/download/dotnet
3️⃣ Install ChromeDriver
bash
Copiar código
dotnet add package Selenium.WebDriver.ChromeDriver --version 139.0.7339.128
4️⃣ Restore and build the solution
bash
Copiar código
dotnet restore WebDriverAutomationFramework.csproj
dotnet build WebDriverAutomationFramework.csproj
▶️ Running Tests
Run all tests
bash
Copiar código
dotnet test WebDriverAutomationFramework.csproj
Run tests by category
bash
Copiar código
dotnet test --filter "TestCategory=agregar"
dotnet test --filter "TestCategory=visualizar"
dotnet test --filter "TestCategory=formulario"
dotnet test --filter "TestCategory=finalizar"
🛠️ Utilities
Kill all ChromeDriver/Chrome processes
bash
Copiar código
.\Utils\kill-chrome-processes.bat
Screenshots are automatically saved on test failure in the Screenshots/ folder
📂 Project Structure
bash
Copiar código
WebDriver-Automation-Framework-Demoblaze/
├── Config/
│   └── TestConfiguration.cs      # Configuration settings
├── Driver/
│   └── WebDriverManager.cs       # Thread-safe WebDriver manager
├── Features/
│   └── DemoblazePurchase.feature # BDD scenarios in Gherkin
├── Hooks/
│   └── Hooks.cs                  # Setup/Teardown
├── Pages/
│   ├── BasePage.cs
│   ├── HomePage.cs
│   └── CartPage.cs
├── StepDefinitions/
│   └── DemoblazeSteps.cs
├── Utils/
│   └── WebElementExtensions.cs
├── appsettings.json
├── specflow.json
└── WebDriverAutomationFramework.csproj
📋 Automated Test Cases
Agregar productos al carrito – verifies items added correctly.

Visualizar el carrito – ensures products are listed in cart.

Completar el formulario – validates form submission.

Finalizar la compra – confirms success message is displayed.

🛠️ Tech Stack
Frontend: Demoblaze web app

Automation: C#, SpecFlow (Gherkin syntax), Selenium WebDriver, NUnit, FluentAssertions

Version control: Git
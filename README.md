# WebDriver Automation Framework con C# y SpecFlow

Framework de automatización de pruebas utilizando Selenium WebDriver, C#, Page Object Model y SpecFlow (Cucumber para .NET).

## Características

- **Selenium WebDriver** con C# (.NET 6.0)
- **Page Object Model** para mantenibilidad del código
- **SpecFlow/Cucumber** para BDD (Behavior Driven Development)
- **Configuración flexible** para múltiples navegadores y entornos
- **Screenshots automáticos** en caso de fallos
- **Ejecución paralela** con WebDriver thread-safe
- **3 tipos de tests**: Smoke, Functional y Form testing

## Instalación y Configuración

### Prerrequisitos
- .NET 6.0 SDK o superior
- Visual Studio Code con extensión C# Dev Kit
- Node.js (para la aplicación objetivo)
- Git

### 1. Clonar el Framework
\`\`\`bash
# <CHANGE> Actualizado para usar repositorio Git propio
git clone [TU_REPOSITORIO_GIT_URL]
cd WebDriverAutomationFramework
\`\`\`

### 2. Configurar la Aplicación Objetivo
\`\`\`bash
# En otra terminal, clonar el repositorio objetivo
git clone https://github.com/wlsf82/frontend-and-backend.git
cd frontend-and-backend

# Instalar dependencias
npm install

# Ejecutar backend (puerto 3001)
npm run start:backend &

# Ejecutar frontend (puerto 3000)  
npm run start:frontend
\`\`\`

### 3. Configurar el Framework
\`\`\`bash
# Volver a la carpeta del framework
cd ../WebDriverAutomationFramework

# Restaurar dependencias NuGet
dotnet restore

# Compilar el proyecto
dotnet build
\`\`\`

## Ejecución de Tests

### Comandos Básicos
\`\`\`bash
# Ejecutar todos los tests
dotnet test

# Ejecutar con logging detallado
dotnet test --logger "console;verbosity=detailed"

# Ejecutar tests por categoría
dotnet test --filter "TestCategory=smoke"
dotnet test --filter "TestCategory=functional"
dotnet test --filter "TestCategory=form"
\`\`\`

### Script de Ejecución Rápida
\`\`\`bash
# Windows
run-tests.bat

# Linux/Mac
chmod +x run-tests.sh && ./run-tests.sh
\`\`\`

## Estructura del Proyecto

\`\`\`
WebDriverAutomationFramework/
├── Config/
│   └── TestConfiguration.cs      # Configuración centralizada
├── Driver/
│   └── WebDriverManager.cs       # Gestión thread-safe del WebDriver
├── Features/
│   └── WebApplicationTests.feature # Escenarios BDD en Gherkin
├── Hooks/
│   └── TestHooks.cs              # Setup/Teardown automático
├── Pages/
│   ├── BasePage.cs               # Clase base para Page Objects
│   └── HomePage.cs               # Page Object de la página principal
├── StepDefinitions/
│   └── WebApplicationSteps.cs    # Implementación de pasos Cucumber
├── Utils/
│   └── WebElementExtensions.cs   # Extensiones personalizadas
├── appsettings.json              # Configuración de la aplicación
├── specflow.json                 # Configuración de SpecFlow
└── WebDriverAutomationFramework.csproj
\`\`\`

## Configuración

### appsettings.json
\`\`\`json
{
  "TestSettings": {
    "BaseUrl": "http://localhost:3000",
    "Browser": "Chrome",
    "RunHeadless": false,
    "ImplicitWait": 10,
    "PageLoadTimeout": 30
  }
}
\`\`\`

### Navegadores Soportados
- Chrome (por defecto)
- Firefox
- Edge

## Tests Implementados

### 1. Smoke Test
Verificación básica de que la aplicación carga correctamente.

### 2. Functional Test  
Validación de elementos de navegación y estructura de la página.

### 3. Form Test
Pruebas de interacción con formularios y elementos de entrada.

## Extensibilidad

### Agregar nuevos Page Objects
```csharp
public class NewPage : BasePage
{
    private readonly By _newElement = By.Id("new-element");
    
    public void InteractWithNewElement()
    {
        WaitAndClick(_newElement);
    }
}

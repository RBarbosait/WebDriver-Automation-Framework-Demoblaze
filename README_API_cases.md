# Demoblaze API – QA Automation

## 📖 Overview
Este proyecto incluye **pruebas automatizadas de los servicios API** de [Demoblaze](https://www.demoblaze.com/), utilizando **C#, SpecFlow, NUnit y RestSharp**.  

Se prueban los endpoints:  
- **Signup:** `https://api.demoblaze.com/signup`  
- **Login:** `https://api.demoblaze.com/login`  

Los escenarios contemplan:  
- Crear un nuevo usuario.  
- Intentar crear un usuario ya existente.  
- Login con usuario y contraseña correctos.  
- Login con usuario o contraseña incorrectos.  

---

## ⚙️ Project Setup

Clonar el repositorio:

```bash
git clone https://github.com/RBarbosait/WebDriver-Automation-Framework-Demoblaze.git
cd WebDriver-Automation-Framework-Demoblaze
Instalar .NET
Descargar e instalar .NET desde:
https://dotnet.microsoft.com/es-es/

Instalar RestSharp
En la carpeta del proyecto, agregar RestSharp:

bash
Copiar código
dotnet add package RestSharp --version 118.0.3
(cambiar versión si es necesario)

🏗️ Build y Restore
bash
Copiar código
dotnet restore WebDriver-Automation-Framework-Demoblaze.sln
dotnet build WebDriver-Automation-Framework-Demoblaze.sln
🤖 Ejecutar Tests de API
Correr todos los tests:

bash
Copiar código
dotnet test WebDriverAutomationFramework.csproj
Filtrar por categoría  @signup, @signup_existing, @login_success, @login_fail

bash
Copiar código
dotnet test --filter "TestCategory=api" o 
"TestCategory= @signup, @signup_existing, @login_success, @login_fail"

📋 Estructura del proyecto (API)
bash
Copiar código
WebDriver-Automation-Framework-Demoblaze/
├── Features/
│   └── SignupLogin.feature       # Scenarios API Signup/Login
├── StepDefinitions/
│   └── ApiSteps.cs               # Implementación de Steps API
├── Utils/
│   └── (si aplica helpers API)
├── WebDriverAutomationFramework.csproj
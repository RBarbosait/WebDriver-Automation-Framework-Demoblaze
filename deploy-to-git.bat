@echo off
echo ========================================
echo  SUBIENDO FRAMEWORK A REPOSITORIO GIT
echo ========================================

echo Inicializando repositorio local...
git init

echo Agregando archivos al staging...
git add .

echo Creando commit inicial...
git commit -m "Initial commit: WebDriver Automation Framework with C# and SpecFlow

- Page Object Model implementation
- SpecFlow/Cucumber BDD tests
- 3 test scenarios: Smoke, Functional, Form
- Flexible configuration (browsers, headless mode)
- Automatic screenshots on failures
- Thread-safe WebDriver management"

echo Conectando con repositorio remoto...
git remote add origin https://github.com/RBarbosait/WebDriver-Automation-Framework-with-C-and-SpecFlow.git

echo Subiendo archivos...
git branch -M main
git push -u origin main

echo ========================================
echo  FRAMEWORK SUBIDO EXITOSAMENTE!
echo ========================================
echo.
echo Tu repositorio está disponible en:
echo https://github.com/RBarbosait/WebDriver-Automation-Framework-with-C-and-SpecFlow
echo.
pause

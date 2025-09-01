@echo off
echo ========================================
echo  WEBDRIVER AUTOMATION FRAMEWORK
echo  Test Execution Commands
echo ========================================
echo.

echo 1. Ejecutar todos los tests:
echo dotnet test
echo.

echo 2. Ejecutar solo Smoke Tests:
echo dotnet test --filter "TestCategory=smoke"
echo.

echo 3. Ejecutar solo Functional Tests:
echo dotnet test --filter "TestCategory=functional"
echo.

echo 4. Ejecutar solo Form Tests:
echo dotnet test --filter "TestCategory=form"
echo.

echo 5. Ejecutar tests con reporte detallado:
echo dotnet test --logger "console;verbosity=detailed"
echo.

echo 6. Ejecutar tests en modo headless:
echo dotnet test -e TestSettings__RunHeadless=true
echo.

echo 7. Compilar proyecto:
echo dotnet build
echo.

echo 8. Restaurar dependencias:
echo dotnet restore
echo.

echo Presiona cualquier tecla para continuar...
pause > nul

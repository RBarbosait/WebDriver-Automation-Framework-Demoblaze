@echo off
echo Cerrando procesos de Chrome y ChromeDriver...

echo Cerrando procesos chromedriver.exe...
taskkill /f /im chromedriver.exe 2>nul
if %errorlevel% == 0 (
    echo ChromeDriver processes cerrados exitosamente.
) else (
    echo No se encontraron procesos ChromeDriver ejecutandose.
)

echo Cerrando procesos chrome.exe...
taskkill /f /im chrome.exe 2>nul
if %errorlevel% == 0 (
    echo Chrome processes cerrados exitosamente.
) else (
    echo No se encontraron procesos Chrome ejecutandose.
)

echo Esperando 2 segundos...
timeout /t 2 /nobreak >nul

echo Limpieza completada. Ahora puedes ejecutar dotnet build.
pause

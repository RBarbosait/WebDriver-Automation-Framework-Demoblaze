@echo off
echo === Limpiando procesos de Chrome ===
call kill-chrome-processes.bat

echo.
echo === Ejecutando Build ===
dotnet clean
dotnet build

if %errorlevel% == 0 (
    echo.
    echo === Build exitoso! Ejecutando tests ===
    dotnet test
) else (
    echo.
    echo === Build falló ===
    pause
)

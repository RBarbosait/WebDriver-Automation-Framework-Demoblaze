@echo off
echo Clearing WebDriverManager cache...

REM Clear WebDriverManager cache directory
if exist "%USERPROFILE%\.cache\selenium" (
    rmdir /s /q "%USERPROFILE%\.cache\selenium"
    echo Selenium cache cleared
)

if exist "%USERPROFILE%\.wdm" (
    rmdir /s /q "%USERPROFILE%\.wdm"
    echo WebDriverManager cache cleared
)

echo Cache cleared successfully!
echo Now run: dotnet test --filter "TestCategory=smoke"
pause

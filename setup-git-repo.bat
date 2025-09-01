@echo off
echo ========================================
echo CONFIGURACION DE REPOSITORIO GIT
echo ========================================

echo.
echo 1. Inicializando repositorio Git...
git init

echo.
echo 2. Agregando archivos al staging...
git add .

echo.
echo 3. Realizando commit inicial...
git commit -m "Initial commit: WebDriver Automation Framework with C# and SpecFlow"

echo.
echo 4. Configurando rama principal...
git branch -M main

echo.
echo SIGUIENTE PASO:
echo 1. Crear repositorio en GitHub/GitLab/Bitbucket
echo 2. Copiar la URL del repositorio
echo 3. Ejecutar: git remote add origin [URL_DEL_REPOSITORIO]
echo 4. Ejecutar: git push -u origin main

echo.
echo ========================================
echo REPOSITORIO LOCAL CONFIGURADO
echo ========================================
pause

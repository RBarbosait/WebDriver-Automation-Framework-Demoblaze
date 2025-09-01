# Instrucciones de Deployment

## Pasos para Subir el Framework a Git

### 1. Preparar el Entorno
\`\`\`bash
# Asegúrate de estar en la carpeta del proyecto
cd WebDriverAutomationFramework

# Verificar que Git está instalado
git --version
\`\`\`

### 2. Ejecutar Script Automático
\`\`\`bash
# Ejecutar el script de deployment
deploy-to-git.bat
\`\`\`

### 3. Verificar en GitHub
- Ir a: https://github.com/RBarbosait/WebDriver-Automation-Framework-with-C-and-SpecFlow
- Verificar que todos los archivos están subidos
- El README.md debe mostrarse automáticamente

### 4. Clonar en Otra Máquina (Para Demostración)
\`\`\`bash
git clone https://github.com/RBarbosait/WebDriver-Automation-Framework-with-C-and-SpecFlow.git
cd WebDriver-Automation-Framework-with-C-and-SpecFlow
dotnet restore
dotnet build
dotnet test
\`\`\`

## Comandos Manuales (Si el Script Falla)

\`\`\`bash
git init
git add .
git commit -m "Initial commit: WebDriver Automation Framework"
git remote add origin https://github.com/RBarbosait/WebDriver-Automation-Framework-with-C-and-SpecFlow.git
git branch -M main
git push -u origin main
\`\`\`

## Para la Entrevista

### Demostrar el Repositorio
1. Mostrar la estructura en GitHub
2. Explicar el README.md
3. Clonar en vivo durante la presentación
4. Ejecutar tests desde el repositorio clonado

### Puntos Clave a Mencionar
- Control de versiones con Git
- Documentación completa
- Framework listo para CI/CD
- Fácil setup para nuevos desarrolladores

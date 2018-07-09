SonarScanner.MSBuild.exe begin /k:"tsqlparser" /d:sonar.organization="bruce-dunwiddie-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="1b72f10a1bd41a9e5351695d24db6d9d6e8ae08c"

"C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\MsBuild.exe" ..\TSQL_Parser.sln /t:Rebuild

SonarScanner.MSBuild.exe end /d:sonar.login="1b72f10a1bd41a9e5351695d24db6d9d6e8ae08c"

pause

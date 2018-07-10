SonarScanner.MSBuild.exe begin /k:"tsqlparser" /d:sonar.organization="bruce-dunwiddie-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login=%SONARQUBE_TOKEN% /d:sonar.cs.opencover.reportsPaths="..\Tests\bin\Debug\CodeCoverageResult.xml"

"C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\MsBuild.exe" ..\TSQL_Parser.sln /t:Rebuild

SonarScanner.MSBuild.exe end /d:sonar.login=%SONARQUBE_TOKEN%

pause

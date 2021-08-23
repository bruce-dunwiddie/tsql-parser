nuget SetApiKey %NUGET_KEY%
nuget push TSQL.Parser.1.5.3.snupkg -Source https://api.nuget.org/v3/index.json
nuget push TSQL.Parser.1.5.3.nupkg -Source https://api.nuget.org/v3/index.json
pause
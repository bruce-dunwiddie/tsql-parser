nuget SetApiKey %NUGET_KEY%
nuget push TSQL.Parser.2.2.1.snupkg -Source https://api.nuget.org/v3/index.json
nuget push TSQL.Parser.2.2.1.nupkg -Source https://api.nuget.org/v3/index.json
pause
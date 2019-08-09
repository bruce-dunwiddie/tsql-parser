# tsql-parser
Library Written in C# For Parsing SQL Server T-SQL Scripts in .Net

Available on Nuget, [TSQL.Parser](https://www.nuget.org/packages/TSQL.Parser/).

    Install-Package TSQL.Parser


[![NuGet](https://img.shields.io/nuget/dt/TSQL.Parser.svg)](https://www.nuget.org/packages/TSQL.Parser/)

[![Travis](https://travis-ci.org/bruce-dunwiddie/tsql-parser.svg)](https://travis-ci.org/bruce-dunwiddie/tsql-parser/)

[![AppVeyor](https://ci.appveyor.com/api/projects/status/lcfjc4jox76dia8q?svg=true)](https://ci.appveyor.com/project/bruce-dunwiddie/tsql-parser)

[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=tsqlparser&metric=coverage)](https://sonarcloud.io/component_measures?id=tsqlparser&metric=coverage)

[![Code Quality](https://sonarcloud.io/api/project_badges/measure?project=tsqlparser&metric=alert_status)](https://sonarcloud.io/dashboard?id=tsqlparser)

[![Coverity](https://scan.coverity.com/projects/9334/badge.svg)](https://scan.coverity.com/projects/bruce-dunwiddie-tsql-parser)

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

### Current Features
- Full T-SQL implementation.
- Streaming tokenizer for parsing scripts into tokens.
- Returns tokens of type characters, comments, identifiers, keywords, literals, variables, and operators.
- Handles both single line and multi line comments.
- Select statement parser.
- .Net Framework 4.0+ and .Net Core 2.0+ compatible.

### Some Possible Current Uses
- [Colorization](<https://github.com/bruce-dunwiddie/tsql-color>)
- Parsing comments.
- Find and replace.
- Script validation.

### Future Additions
- More statement parsers built on top of tokenizer.

### Code Samples
- See [wiki](<https://github.com/bruce-dunwiddie/tsql-parser/wiki/TSQL-Parser-Code-Samples>) for simple code examples and output.
- See [test cases](<https://github.com/bruce-dunwiddie/tsql-parser/tree/master/TSQL_Parser/Tests>) in project.

### Class Documentation
- [bruce-dunwiddie.github.io/tsql-parser/](<http://bruce-dunwiddie.github.io/tsql-parser/>)


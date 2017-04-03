# tsql-parser
Library For Parsing SQL Server T-SQL Scripts

Available on Nuget, [TSQL.Parser](https://www.nuget.org/packages/TSQL.Parser/).

    Install-Package TSQL.Parser

[![Travis](https://travis-ci.org/bruce-dunwiddie/tsql-parser.svg)](https://travis-ci.org/bruce-dunwiddie/tsql-parser/)

[![AppVeyor](https://ci.appveyor.com/api/projects/status/lcfjc4jox76dia8q?svg=true)](https://ci.appveyor.com/project/bruce-dunwiddie/tsql-parser)

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

### Current Features
- Full T-SQL implementation.
- Streaming tokenizer for parsing scripts into tokens.
- Returns tokens of type characters, comments, identifiers, keywords, literals, variables, and operators.
- Handles both single line and multi line comments.
- Select statement parser.

### Some Possible Current Uses
- Parsing comments.
- Find and replace.
- Script validation.

### Future Additions
- More statement parsers built on top of tokenizer.

### Code Samples
- See [wiki](<https://github.com/bruce-dunwiddie/tsql-parser/wiki>) for simple code example and output.
- See test cases in project.

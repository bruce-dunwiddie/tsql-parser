# tsql-parser
Library for parsing SQL Server TSQL scripts

[![Travis](https://travis-ci.org/bruce-dunwiddie/tsql-parser.svg)](https://travis-ci.org/bruce-dunwiddie/tsql-parser/)

[![AppVeyor](https://ci.appveyor.com/api/projects/status/lcfjc4jox76dia8q?svg=true)](https://ci.appveyor.com/project/bruce-dunwiddie/tsql-parser)

### Current Features
- Full TSQL implementation.
- Streaming tokenizer for parsing scripts into tokens.
- Returns tokens of type characters, comments, identifiers, keywords, literals, variables, and operators.
- Handles both single line and multi line comments.

### Some Possible Current Uses
- Parsing comments.
- Find and replace.
- Script validation.

### Future Additions
- Statement parsers built on top of tokenizer.

### Code Samples
- See [wiki] for simple code example and output.
- See test cases in project.

[wiki]: <https://github.com/bruce-dunwiddie/tsql-parser/wiki>

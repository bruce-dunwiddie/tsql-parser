# tsql-parser
Library for parsing SQL Server TSQL scripts

### Current Features
- Full TSQL implementation.
- Streaming tokenizer for parsing letter by letter.
- Streaming lexer for combining letters into tokens.
- Returns tokens of type comments, identifiers, keywords, literals, and operators.
- Handles both single line and multi line comments.

### Some Possible Current Uses
- Parsing comments.
- Find and replace.
- Script validation.

### Future Additions
- Statement parsers built on top of lexer.

### Code Samples
- See [wiki] for simple code example and output.
- See test cases in project.

[wiki]: <https://github.com/bruce-dunwiddie/tsql-parser/wiki>

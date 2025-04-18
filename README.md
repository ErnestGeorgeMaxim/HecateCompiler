# Mini Language Compiler

## Description

This project implements a compiler for a custom **mini-programming language**. It reads a source program from a text file, analyzes it lexically and syntactically, and validates it according to the defined grammar.

## Features

- Lexical analysis (tokenization)
- Syntax and semantic validation
- Error detection and reporting:
  - Lexical errors
  - Syntactic errors
  - Semantic errors
- Ignores whitespace and both single-line (`//`) and block (`/* ... */`) comments
- Generates structured outputs:
  - Token list
  - Global variables
  - Function details
  - Control structures

## Language Elements

- **Keywords:** `int`, `float`, `double`, `string`, `void`, `if`, `else`, `for`, `while`, `return`
- **Identifiers**
- **Constants:** Numeric and string literals (`"text"`)
- **Operators:**
  - Arithmetic: `+`, `-`, `*`, `/`, `%`
  - Relational: `<`, `>`, `<=`, `>=`, `==`, `!=`
  - Logical: `&&`, `||`, `!`
  - Assignment: `=`, `+=`, `-=`, `*=`, `/=`, `%=`
  - Increment/Decrement: `++`, `--`
- **Delimiters:** `(`, `)`, `{`, `}`, `,`, `;`

## Output Files

- `tokens.txt`: List of tokens in the format `<token, lexeme, line number>`
- `globals.txt`: Global variables with types and initialization values
- `functions.txt`: Function definitions with:
  - Type (iterative, recursive, `main`)
  - Return type, name, and parameter list
  - Local variables
  - Control structures with line numbers

## Error Reporting

The compiler reports:
- Lexical errors (invalid tokens)
- Syntax errors (incorrect grammar usage)
- Semantic errors (invalid declarations, type mismatches)

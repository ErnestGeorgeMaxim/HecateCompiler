grammar HecateLang;

// Parser Rules
program
    : (globalVar | function)*
    ;

globalVar
    : type ID ('=' expression)? SEMICOLON
    ;

localVar
    : type ID ('=' expression)? SEMICOLON
    ;

function
    : type ID LPAREN parameters? RPAREN block
    ;

parameters
    : param (COMMA param)*
    ;

param
    : type ID
    ;

statement
    : assignment
    | ifStatement
    | loop
    | returnStatement
    | expression SEMICOLON
    ;

assignment
    : ID (ASSIGN | PLUS_ASSIGN | MINUS_ASSIGN | STAR_ASSIGN | SLASH_ASSIGN) expression SEMICOLON
    ;

ifStatement
    : IF LPAREN expression RPAREN block (ELSE block)?
    ;

loop
    : FOR LPAREN forInitialization SEMICOLON forCondition SEMICOLON forIncrement RPAREN block
    | WHILE LPAREN expression RPAREN block
    ;

forInitialization
    : localVar
    | assignment
    | incrementOrDecrement
    ;

forCondition
    : expression
    ;

forIncrement
    : incrementOrDecrement
    | expression
    ;

incrementOrDecrement
    : ID PLUS_PLUS
    | ID MINUS_MINUS
    | PLUS_PLUS ID
    | MINUS_MINUS ID
    ;

returnStatement
    : RETURN expression? SEMICOLON
    ;

block
    : LBRACE (statement | localVar)* RBRACE
    ;

expression
    : term ((PLUS | MINUS | STAR | SLASH | LT | GT | LE | GE | EQUAL | NE | AND | OR | NOT) term)*
    | incrementOrDecrement
    ;

term
    : ID
    | NUMBER
    | STRING
    | LPAREN expression RPAREN
    | ID LPAREN (expression (COMMA expression)*)? RPAREN
    | incrementOrDecrement
    ;

type
    : INT
    | FLOAT
    | DOUBLE 
    | STRING_TYPE
    | VOID
    ;

// Lexer Rules
// Keywords
INT             : 'int';
FLOAT           : 'float';
DOUBLE          : 'double';
STRING_TYPE     : 'string';
VOID            : 'void';
IF              : 'if';
ELSE            : 'else';
FOR             : 'for';
WHILE           : 'while';
RETURN          : 'return';

// Identifiers and Literals
ID              : [a-zA-Z_][a-zA-Z0-9_]*;
NUMBER          : [+/-]?[0-9]+ ('.' [0-9]+)?;
STRING          : '"' ( ~["\\\r\n] | '\\' . )* '"';

// Arithmetic Operators
PLUS            : '+';
MINUS           : '-';
STAR            : '*';
SLASH           : '/';
PLUS_PLUS       : '++';
MINUS_MINUS     : '--';

// Relational Operators
LT              : '<';
GT              : '>';
LE              : '<=';
GE              : '>=';
EQUAL           : '==';
NE              : '!=';

// Logical Operators
AND             : '&&';
OR              : '||';
NOT             : '!';

// Assignment Operators
ASSIGN          : '=';
PLUS_ASSIGN     : '+=';
MINUS_ASSIGN    : '-=';
STAR_ASSIGN     : '*=';
SLASH_ASSIGN    : '/=';

// Delimiters
LPAREN          : '(';
RPAREN          : ')';
LBRACE          : '{';
RBRACE          : '}';
SEMICOLON       : ';';
COMMA           : ',';

// Comments and Whitespace
COMMENT         : '//' ~[\r\n]* -> skip;
BLOCK_COMMENT   : '/*' .*? '*/' -> skip;
WS              : [ \t\r\n]+ -> skip;
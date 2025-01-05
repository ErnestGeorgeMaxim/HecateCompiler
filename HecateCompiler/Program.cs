using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;

namespace HecateCompiler
{
    public class TokenInfo
    {
        public string TokenType { get; set; }
        public string Lexeme { get; set; }
        public int Line { get; set; }

        public override string ToString()
        {
            return $"(Token: {TokenType}, Lexeme: '{Lexeme}', Line: {Line})";
        }
    }

    public class ErrorCollector
    {
        public List<string> Errors { get; private set; } = new List<string>();

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public void SaveErrorsToFile(string filePath, string errorType)
        {
            if (Errors.Count > 0)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"{errorType}:");
                    foreach (var error in Errors)
                    {
                        writer.WriteLine(error);
                    }
                }
            }
        }
    }

    public class CustomErrorListener : IAntlrErrorListener<int>, IAntlrErrorListener<IToken>
    {
        private readonly ErrorCollector _errorCollector;

        public CustomErrorListener(ErrorCollector errorCollector)
        {
            _errorCollector = errorCollector;
        }

        public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            string errorMessage = $"Lexer Error: '{msg}' at line {line}, col {charPositionInLine}";
            _errorCollector.AddError(errorMessage);
        }

        public void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            string errorMessage = $"Parser Error: '{msg}' at line {line}, col {charPositionInLine}";
            _errorCollector.AddError(errorMessage);
        }
    }

    public class HecateLangVisitor : HecateLangBaseVisitor<object>
    {
        private Dictionary<string, object> globalVariables = new Dictionary<string, object>();

        public override object VisitGlobalVar(HecateLangParser.GlobalVarContext context)
        {
            string type = context.type().GetText();
            string id = context.ID().GetText();
            object value = null;

            if (context.expression() != null)
            {
                value = Visit(context.expression());
            }

            globalVariables[id] = value;

            using (StreamWriter writer = new StreamWriter("D:\\clion_projects\\C#\\HecateCompiler\\HecateCompiler\\globalVariables.txt", append: true))
            {
                writer.WriteLine($"Type: {type}, Name: {id}, Initial Value: {value}");
            }

            return null;
        }

        public override object VisitFunction(HecateLangParser.FunctionContext context)
        {
            string returnType = context.type().GetText();
            string functionName = context.ID().GetText();
            var parameters = new List<(string type, string name)>();

            if (context.parameters() != null)
            {
                foreach (var param in context.parameters().param())
                {
                    string paramType = param.type().GetText();
                    string paramName = param.ID().GetText();
                    parameters.Add((paramType, paramName));
                }
            }

            string parameterList = string.Join(", ", parameters.Select(p => $"{p.type} {p.name}"));

            using (StreamWriter writer = new StreamWriter("D:\\clion_projects\\C#\\HecateCompiler\\HecateCompiler\\functions.txt", append: true))
            {
                writer.WriteLine($"Return Type: {returnType}, Name: {functionName}, Parameters: {parameterList}");
            }

            return null;
        }

        public override object VisitIfStatement(HecateLangParser.IfStatementContext context)
        {
            using (StreamWriter writer = new StreamWriter("D:\\clion_projects\\C#\\HecateCompiler\\HecateCompiler\\controlStructures.txt", append: true))
            {
                writer.WriteLine($"<If Statement, Line: {context.Start.Line}>");
            }

            return base.VisitIfStatement(context);
        }

        public override object VisitLoop(HecateLangParser.LoopContext context)
        {
            string loopType = context.FOR() != null ? "For Loop" : "While Loop";

            using (StreamWriter writer = new StreamWriter("D:\\clion_projects\\C#\\HecateCompiler\\HecateCompiler\\controlStructures.txt", append: true))
            {
                writer.WriteLine($"<{loopType}, Line: {context.Start.Line}>");
            }

            return base.VisitLoop(context);
        }
    }

    internal class Program
    {
        static List<TokenInfo> GetTokens(string code, ErrorCollector lexicalErrors)
        {
            var tokenList = new List<TokenInfo>();

            var inputStream = new AntlrInputStream(code);
            var lexer = new HecateLangLexer(inputStream);

            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new CustomErrorListener(lexicalErrors));

            foreach (var token in lexer.GetAllTokens())
            {
                string tokenType = lexer.Vocabulary.GetSymbolicName(token.Type);
                if (string.IsNullOrEmpty(tokenType))
                {
                    tokenType = lexer.Vocabulary.GetDisplayName(token.Type);
                }

                tokenList.Add(new TokenInfo
                {
                    TokenType = tokenType,
                    Lexeme = token.Text,
                    Line = token.Line
                });
            }

            return tokenList;
        }

        static void SaveTokensToFile(List<TokenInfo> tokens, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var token in tokens)
                {
                    writer.WriteLine($"<Token: {token.TokenType}, Lexeme: '{token.Lexeme}', Line: {token.Line}>");
                }
            }
        }

        static void Main(string[] args)
        {
            string outputPath = "D:\\clion_projects\\C#\\HecateCompiler\\HecateCompiler";
            string sourcePath = Path.Combine(outputPath, "test.txt");

            if (!File.Exists(sourcePath))
            {
                Console.WriteLine($"File {sourcePath} not found. Please create a source file with valid HecateLang code.");
                return;
            }

            string code = File.ReadAllText(sourcePath);
            var lexicalErrors = new ErrorCollector();
            var syntaxErrors = new ErrorCollector();

            try
            {
                // Lexical Analysis
                var inputStream = new AntlrInputStream(code);
                var lexer = new HecateLangLexer(inputStream);
                
                lexer.RemoveErrorListeners();
                lexer.AddErrorListener(new CustomErrorListener(lexicalErrors));
                
                var tokenStream = new CommonTokenStream(lexer);
                var parser = new HecateLangParser(tokenStream);

                parser.RemoveErrorListeners();
                parser.AddErrorListener(new CustomErrorListener(syntaxErrors));

                var tree = parser.program();

                // Create visitor with output path
                var visitor = new HecateLangVis(outputPath);
                visitor.Visit(tree);

                Console.WriteLine("Compilation completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Compilation Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            // Save any errors
            if (lexicalErrors.Errors.Any())
            {
                lexicalErrors.SaveErrorsToFile(
                    Path.Combine(outputPath, "lexical_errors.txt"),
                    "Lexical Errors"
                );
            }
            
            if (syntaxErrors.Errors.Any())
            {
                syntaxErrors.SaveErrorsToFile(
                    Path.Combine(outputPath, "syntax_errors.txt"),
                    "Syntax Errors"
                );
            }
        }
    }
}

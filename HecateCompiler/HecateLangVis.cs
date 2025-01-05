using Antlr4.Runtime.Misc;
using System.Collections.Generic;
using System.Text;

namespace HecateCompiler
{
    public class HecateLangVis : HecateLangBaseVisitor<object>
    {
        private readonly string _outputPath;
        private Dictionary<string, object> _globalVariables = new Dictionary<string, object>();
        private Dictionary<string, object> _localVariables = new Dictionary<string, object>();
        private Dictionary<string, FunctionDefinition> _functions = new Dictionary<string, FunctionDefinition>();
        private string _currentFunction = null;
        private List<string> _semanticErrors = new List<string>();

        public HecateLangVis(string outputPath)
        {
            _outputPath = outputPath;
            // Clear existing files
            File.WriteAllText(Path.Combine(_outputPath, "semantic_errors.txt"), "");
        }

        private class FunctionDefinition
        {
            public string ReturnType { get; set; }
            public List<(string type, string name)> Parameters { get; set; }
            public HecateLangParser.BlockContext Body { get; set; }
            public bool IsMain { get; set; }
            public List<(string type, string name, object initialValue)> LocalVariables { get; set; } = new List<(string type, string name, object initialValue)>();
            public List<string> ControlStructures { get; set; } = new List<string>();
            public HashSet<string> ParameterNames { get; set; } = new HashSet<string>();
        }

        private void AddSemanticError(string error)
        {
            _semanticErrors.Add(error);
            File.AppendAllText(Path.Combine(_outputPath, "semantic_errors.txt"), error + "\n");
        }
        
        private bool IsTypeCompatible(string expectedType, object value)
        {
            if (value == null) return true;

            return (expectedType, value) switch
            {
                ("int", int _) => true,
                ("double", double _) => true,
                ("double", int _) => true,
                ("float", float _) => true,
                ("float", int _) => true,
                ("string", string _) => true,
                _ => false
            };
        }

        public override object VisitProgram([NotNull] HecateLangParser.ProgramContext context)
        {
            // Clear existing files
            File.WriteAllText(Path.Combine(_outputPath, "globalVariables.txt"), "");
            File.WriteAllText(Path.Combine(_outputPath, "functions.txt"), "");
            File.WriteAllText(Path.Combine(_outputPath, "controlStructures.txt"), "");
            
            foreach (var child in context.children)
            {
                Visit(child);
            }

            WriteFunctionInformation();
            return null;
        }

        private void WriteFunctionInformation()
        {
            var sb = new StringBuilder();
            foreach (var func in _functions)
            {
                sb.AppendLine($"Function Information:");
                sb.AppendLine($"  Return Type: {func.Value.ReturnType}");
                sb.AppendLine($"  Name: {func.Key}");
                sb.AppendLine($"  Is Main: {func.Value.IsMain}");
                sb.AppendLine($"  Parameters: {string.Join(", ", func.Value.Parameters.Select(p => $"{p.type} {p.name}"))}");
                
                sb.AppendLine("  Local Variables:");
                foreach (var localVar in func.Value.LocalVariables)
                {
                    string valueStr = localVar.initialValue?.ToString() ?? "null";
                    sb.AppendLine($"    - Type: {localVar.type}, Name: {localVar.name}, Initial Value: {valueStr}");
                }
                
                sb.AppendLine("  Control Structures:");
                foreach (var control in func.Value.ControlStructures)
                {
                    sb.AppendLine($"    - {control}");
                }
                sb.AppendLine();
            }

            File.WriteAllText(Path.Combine(_outputPath, "functions.txt"), sb.ToString());
        }

        public override object VisitGlobalVar([NotNull] HecateLangParser.GlobalVarContext context)
        {
            string type = context.type().GetText();
            string id = context.ID().GetText();
            object value = null;

            if (_globalVariables.ContainsKey(id))
            {
                AddSemanticError($"Line {context.Start.Line}: Duplicate global variable '{id}'");
                return null;
            }

            if (context.expression() != null)
            {
                value = EvaluateExpression(context.expression());
        
                if (!IsTypeCompatible(type, value))
                {
                    AddSemanticError($"Line {context.Start.Line}: Type mismatch in global variable '{id}'. Expected {type}, got {value?.GetType().Name ?? "null"}");
                    return null;
                }
            }
            else
            {
                value = GetDefaultValue(type);
            }

            _globalVariables[id] = value;
    
            File.AppendAllText(
                Path.Combine(_outputPath, "globalVariables.txt"), 
                $"Type: {type}, Name: {id}, Initial Value: {value?.ToString() ?? "null"}\n"
            );

            return null;
        }
        
        public override object VisitLocalVar([NotNull] HecateLangParser.LocalVarContext context)
        {
            string type = context.type().GetText();
            string id = context.ID().GetText();
            object value = null;

            if (_currentFunction != null && _functions.ContainsKey(_currentFunction))
            {
                if (_functions[_currentFunction].ParameterNames.Contains(id))
                {
                    AddSemanticError($"Line {context.Start.Line}: Local variable '{id}' conflicts with parameter name in function '{_currentFunction}'");
                    return null;
                }

                if (_localVariables.ContainsKey(id))
                {
                    AddSemanticError($"Line {context.Start.Line}: Duplicate local variable '{id}' in function '{_currentFunction}'");
                    return null;
                }
            }

            if (context.expression() != null)
            {
                value = EvaluateExpression(context.expression());
        
                if (!IsTypeCompatible(type, value))
                {
                    AddSemanticError($"Line {context.Start.Line}: Type mismatch in local variable '{id}'. Expected {type}, got {value?.GetType().Name ?? "null"}");
                    return null;
                }
            }
            else
            {
                value = GetDefaultValue(type);
            }

            _localVariables[id] = value;

            if (_currentFunction != null && _functions.ContainsKey(_currentFunction))
            {
                _functions[_currentFunction].LocalVariables.Add((type, id, value));
            }

            return null;
        }

        public override object VisitFunction([NotNull] HecateLangParser.FunctionContext context)
        {
            string returnType = context.type().GetText();
            string functionName = context.ID().GetText();
            var parameters = new List<(string type, string name)>();
            var parameterNames = new HashSet<string>();

            if (context.parameters() != null)
            {
                foreach (var param in context.parameters().param())
                {
                    string paramType = param.type().GetText();
                    string paramName = param.ID().GetText();

                    if (parameterNames.Contains(paramName))
                    {
                        AddSemanticError($"Line {param.Start.Line}: Duplicate parameter name '{paramName}' in function '{functionName}'");
                        return null;
                    }

                    parameters.Add((paramType, paramName));
                    parameterNames.Add(paramName);
                }
            }

            if (_functions.ContainsKey(functionName))
            {
                var existing = _functions[functionName];
                if (existing.Parameters.Count == parameters.Count)
                {
                    bool identical = true;
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        if (existing.Parameters[i].type != parameters[i].type)
                        {
                            identical = false;
                            break;
                        }
                    }
                    if (identical)
                    {
                        AddSemanticError($"Line {context.Start.Line}: Duplicate function definition '{functionName}' with identical parameter types");
                        return null;
                    }
                }
            }

            _currentFunction = functionName;
            _localVariables.Clear();

            _functions[functionName] = new FunctionDefinition
            {
                ReturnType = returnType,
                Parameters = parameters,
                Body = context.block(),
                IsMain = functionName.ToLower() == "main",
                ParameterNames = parameterNames
            };

            object result = Visit(context.block());
            _currentFunction = null;
            
            return result;
        }

        public override object VisitAssignment([NotNull] HecateLangParser.AssignmentContext context)
        {
            string id = context.ID().GetText();
            object value = Visit(context.expression());
            string op = context.GetChild(1).GetText();

            object currentValue = _localVariables.ContainsKey(id) ? _localVariables[id] : _globalVariables[id];

            switch (op)
            {
                case "=":
                    if (_localVariables.ContainsKey(id))
                        _localVariables[id] = value;
                    else
                        _globalVariables[id] = value;
                    break;
                case "+=":
                    value = Add(currentValue, value);
                    break;
                case "-=":
                    value = Subtract(currentValue, value);
                    break;
                case "*=":
                    value = Multiply(currentValue, value);
                    break;
                case "/=":
                    value = Divide(currentValue, value);
                    break;
            }

            return null;
        }

        public override object VisitIfStatement([NotNull] HecateLangParser.IfStatementContext context)
        {
            File.AppendAllText(
                Path.Combine(_outputPath, "controlStructures.txt"),
                $"<If Statement, Line: {context.Start.Line}>\n"
            );

            bool condition = IsTruthy(Visit(context.expression()));

            if (condition)
            {
                File.AppendAllText(
                    Path.Combine(_outputPath, "controlStructures.txt"),
                    $"  - Entered 'if' block at line {context.block(0).Start.Line}\n"
                );
                Visit(context.block(0));
            }
            else
            {
                if (context.ELSE() != null)
                {
                    File.AppendAllText(
                        Path.Combine(_outputPath, "controlStructures.txt"),
                        $"  - Entered 'else' block at line {context.block(1).Start.Line}\n"
                    );
                    Visit(context.block(1));
                }
                else
                {
                    File.AppendAllText(
                        Path.Combine(_outputPath, "controlStructures.txt"),
                        "  - No 'else' block provided\n"
                    );
                }
            }

            return null;
        }


        public override object VisitLoop([NotNull] HecateLangParser.LoopContext context)
        {
            string loopType = context.FOR() != null ? "For Loop" : "While Loop";
            string controlStructure = $"<{loopType}, Line: {context.Start.Line}>";
            RecordControlStructure(controlStructure);

            if (context.FOR() != null)
            {
                Visit(context.forInitialization());
                while (IsTruthy(Visit(context.forCondition())))
                {
                    Visit(context.block());
                    Visit(context.forIncrement());
                }
            }
            else
            {
                while (IsTruthy(Visit(context.expression())))
                {
                    Visit(context.block());
                }
            }
            
            return null;
        }

        private void RecordControlStructure(string structure)
        {
            if (_currentFunction != null && _functions.ContainsKey(_currentFunction))
            {
                _functions[_currentFunction].ControlStructures.Add(structure);
            }

            File.AppendAllText(
                Path.Combine(_outputPath, "controlStructures.txt"),
                $"{structure}\n"
            );
        }

        public override object VisitIncrementOrDecrement([NotNull] HecateLangParser.IncrementOrDecrementContext context)
        {
            string id = context.ID().GetText();
            bool isPrefix = context.GetChild(0).GetText() == "++" || context.GetChild(0).GetText() == "--";
            bool isIncrement = context.PLUS_PLUS() != null;

            object currentValue = _localVariables.ContainsKey(id) ? _localVariables[id] : _globalVariables[id];
            object newValue = isIncrement ? Add(currentValue, 1) : Subtract(currentValue, 1);

            if (_localVariables.ContainsKey(id))
                _localVariables[id] = newValue;
            else
                _globalVariables[id] = newValue;

            return isPrefix ? newValue : currentValue;
        }

        public override object VisitReturnStatement([NotNull] HecateLangParser.ReturnStatementContext context)
        {
            if (context.expression() != null)
            {
                return Visit(context.expression());
            }
            return null;
        }
        
        private object EvaluateExpression(HecateLangParser.ExpressionContext context)
        {
            if (context.term().Length == 1 && context.term(0).NUMBER() != null)
            {
                string numberText = context.term(0).NUMBER().GetText();
                if (numberText.Contains("."))
                {
                    return double.Parse(numberText);
                }
                return int.Parse(numberText);
            }
    
            if (context.term().Length == 1 && context.term(0).STRING() != null)
            {
                string str = context.term(0).STRING().GetText();
                return str.Substring(1, str.Length - 2);
            }

            return Visit(context);
        }
        
        private object GetDefaultValue(string type)
        {
            return type.ToLower() switch
            {
                "int" => 0,
                "float" => 0.0f,
                "double" => 0.0d,
                "string" => "",
                _ => null
            };
        }

        public override object VisitExpression([NotNull] HecateLangParser.ExpressionContext context)
        {
            if (context.incrementOrDecrement() != null)
            {
                return Visit(context.incrementOrDecrement());
            }

            object result = Visit(context.term(0));

            for (int i = 1; i < context.term().Length; i++)
            {
                string op = context.GetChild(2 * i - 1).GetText();
                object right = Visit(context.term(i));

                switch (op)
                {
                    case "+": result = Add(result, right); break;
                    case "-": result = Subtract(result, right); break;
                    case "*": result = Multiply(result, right); break;
                    case "/": result = Divide(result, right); break;
                    case "%": result = Modulo(result, right); break;
                    case "<": result = LessThan(result, right); break;
                    case ">": result = GreaterThan(result, right); break;
                    case "<=": result = LessThanOrEqual(result, right); break;
                    case ">=": result = GreaterThanOrEqual(result, right); break;
                    case "==": result = Equal(result, right); break;
                    case "!=": result = NotEqual(result, right); break;
                    case "&&": result = And(result, right); break;
                    case "||": result = Or(result, right); break;
                    case "!": result = Not(result); break;
                    default: throw new Exception($"Unknown operator: {op}");
                }
            }

            return result;
        }


        public override object VisitTerm([NotNull] HecateLangParser.TermContext context)
        {
            if (context.ID() != null && context.LPAREN() != null)
            {
                string functionName = context.ID().GetText();
                
                if (!_functions.ContainsKey(functionName))
                {
                    AddSemanticError($"Line {context.Start.Line}: Call to undefined function '{functionName}'");
                    return null;
                }

                var args = new List<object>();
                foreach (var expr in context.expression())
                {
                    args.Add(Visit(expr));
                }

                if (args.Count != _functions[functionName].Parameters.Count)
                {
                    AddSemanticError($"Line {context.Start.Line}: Function '{functionName}' called with wrong number of arguments. Expected {_functions[functionName].Parameters.Count}, got {args.Count}");
                    return null;
                }

                return ExecuteFunction(functionName, args);
            }

            return base.VisitTerm(context);
        }



        private object ExecuteFunction(string name, List<object> args)
        {
            if (!_functions.ContainsKey(name))
            {
                throw new System.Exception($"Function {name} not found");
            }

            var function = _functions[name];
    
            var previousLocals = new Dictionary<string, object>(_localVariables);
            string previousFunction = _currentFunction;
    
            _currentFunction = name;
            _localVariables.Clear();

            for (int i = 0; i < function.Parameters.Count; i++)
            {
                _localVariables[function.Parameters[i].name] = args[i];
            }

            object result = Visit(function.Body);

            _localVariables = previousLocals;
            _currentFunction = previousFunction;

            return result;
        }

        private object Add(object left, object right)
        {
            if (left == null) left = 0;
            if (right == null) right = 0;

            return (left, right) switch
            {
                (int l, int r) => l + r,
                (double l, double r) => l + r,
                (int l, double r) => l + r,
                (double l, int r) => l + r,

                (float l, float r) => l + r,
                (float l, int r) => l + r,
                (int l, float r) => l + r,
                (float l, double r) => l + r,
                (double l, float r) => l + r,

                (string l, string r) => l + r,
                (string l, var r) => l + r?.ToString(),
                (var l, string r) => l?.ToString() + r,

                _ => throw new ArgumentException(
                    $"Invalid addition between types {left.GetType().Name} and {right.GetType().Name}: {left} + {right}")
            };
        }

        private object Subtract(object left, object right)
        {
            if (left == null || right == null)
            {
                throw new Exception($"Cannot perform subtraction with null values: {left} - {right}");
            }

            if (left is int l && right is int r)
                return l - r;
            if (left is double ld && right is double rd)
                return ld - rd;
            if (left is int li && right is double rd2)
                return li - rd2;
            if (left is double ld2 && right is int ri)
                return ld2 - ri;
            if (left is float lf && right is float rf)
                return lf - rf;
            if (left is float lf2 && right is int ri2)
                return lf2 - ri2;
            if (left is int li2 && right is float rf2)
                return li2 - rf2;

            throw new Exception($"Invalid subtraction between types {left?.GetType().Name ?? "null"} and {right?.GetType().Name ?? "null"}: {left} - {right}");
        }

        private object Multiply(object left, object right)
        {
            if (left == null || right == null)
            {
                throw new Exception($"Cannot perform multiplication with null values: {left} * {right}");
            }

            if (left is int l && right is int r)
                return l * r;
            if (left is double ld && right is double rd)
                return ld * rd;
            if (left is int li && right is double rd2)
                return li * rd2;
            if (left is double ld2 && right is int ri)
                return ld2 * ri;
            if (left is float lf && right is float rf)
                return lf * rf;
            if (left is float lf2 && right is int ri2)
                return lf2 * ri2;
            if (left is int li2 && right is float rf2)
                return li2 * rf2;

            throw new Exception($"Invalid multiplication between types {left?.GetType().Name ?? "null"} and {right?.GetType().Name ?? "null"}: {left} * {right}");
        }

        private object Divide(object left, object right)
        {
            if (left == null || right == null)
            {
                left ??= 1;
                right ??= 1;
            }   

            if ((right is int ri && ri == 0) || 
                (right is double rd && rd == 0.0) || 
                (right is float rf && rf == 0.0f))
                throw new DivideByZeroException("Attempted division by zero.");

            return (left, right) switch
            {
                (int l, int r) => l / r,
                (double l, double r) => l / r,
                (int l, double r) => l / r,
                (double l, int r) => l / r,
                (float l, float r) => l / r,
                (int l, float r) => l / r,
                (float l, int r) => l / r,
                (float l, double r) => l / r,
                (double l, float r) => l / r,
                _ => throw new ArgumentException($"Unsupported types for division: {left.GetType().Name} / {right.GetType().Name}")
            };
        }
        
        private object Modulo(object left, object right)
        {
            if (left == null || right == null)
            {
                throw new Exception($"Cannot perform modulo with null values: {left} % {right}");
            }

            if (left is int l && right is int r)
                return l % r;
            if (left is double ld && right is double rd)
                return ld % rd;
            if (left is int li && right is double rd2)
                return li % rd2;
            if (left is double ld2 && right is int ri)
                return ld2 % ri;
            if (left is float lf && right is float rf)
                return lf % rf;
            if (left is float lf2 && right is int ri2)
                return lf2 % ri2;
            if (left is int li2 && right is float rf2)
                return li2 % rf2;

            throw new Exception($"Invalid modulo operation between types {left?.GetType().Name ?? "null"} and {right?.GetType().Name ?? "null"}: {left} % {right}");
        }


        private bool LessThan(object left, object right)
        {
            // Handle null cases
            if (left == null || right == null)
            {
                if (left == null) left = 0;
                if (right == null) right = 0;
            }

            return (left, right) switch
            {
                (int l, int r) => l < r,
                (double ld, double rd) => ld < rd,
                (int li, double rd2) => li < rd2,
                (double ld2, int ri) => ld2 < ri,
                (float lf, float rf) => lf < rf,
                (float lf2, int ri2) => lf2 < ri2,
                (int li2, float rf2) => li2 < rf2,
                _ => throw new Exception($"Invalid comparison between types {left?.GetType().Name ?? "null"} and {right?.GetType().Name ?? "null"}: {left} < {right}")
            };
        }

        private bool GreaterThan(object left, object right)
        {
            // Handle null cases
            if (left == null || right == null)
            {
                if (left == null) left = 0;
                if (right == null) right = 0;
            }

            return (left, right) switch
            {
                (int l, int r) => l > r,
                (double ld, double rd) => ld > rd,
                (int li, double rd2) => li > rd2,
                (double ld2, int ri) => ld2 > ri,
                (float lf, float rf) => lf > rf,
                (float lf2, int ri2) => lf2 > ri2,
                (int li2, float rf2) => li2 > rf2,
                _ => throw new Exception($"Invalid comparison between types {left?.GetType().Name ?? "null"} and {right?.GetType().Name ?? "null"}: {left} > {right}")
            };
        }

        private bool LessThanOrEqual(object left, object right)
        {
            if (left == null || right == null)
            {
                if (left == null) left = 0;
                if (right == null) right = 0;
            }

            return (left, right) switch
            {
                (int l, int r) => l <= r,
                (double ld, double rd) => ld <= rd,
                (int li, double rd2) => li <= rd2,
                (double ld2, int ri) => ld2 <= ri,
                (float lf, float rf) => lf <= rf,
                (float lf2, int ri2) => lf2 <= ri2,
                (int li2, float rf2) => li2 <= rf2,
                _ => throw new Exception($"Invalid comparison between types {left?.GetType().Name ?? "null"} and {right?.GetType().Name ?? "null"}: {left} <= {right}")
            };
        }

        private bool GreaterThanOrEqual(object left, object right)
        {
            if (left == null || right == null)
            {
                if (left == null) left = 0;
                if (right == null) right = 0;
            }

            return (left, right) switch
            {
                (int l, int r) => l >= r,
                (double ld, double rd) => ld >= rd,
                (int li, double rd2) => li >= rd2,
                (double ld2, int ri) => ld2 >= ri,
                (float lf, float rf) => lf >= rf,
                (float lf2, int ri2) => lf2 >= ri2,
                (int li2, float rf2) => li2 >= rf2,
                _ => throw new Exception($"Invalid comparison between types {left?.GetType().Name ?? "null"} and {right?.GetType().Name ?? "null"}: {left} >= {right}")
            };
        }

        private bool Equal(object left, object right)
        {
            if (left == null || right == null)
            {
                return left == right;
            }

            return (left, right) switch
            {
                (int l, int r) => l == r,
                (double ld, double rd) => Math.Abs(ld - rd) < 1e-10,
                (int li, double rd) => Math.Abs(li - rd) < 1e-10,
                (double ld, int ri) => Math.Abs(ld - ri) < 1e-10,
                (float lf, float rf) => Math.Abs(lf - rf) < 1e-6f,
                (string ls, string rs) => ls == rs,
                _ => left.Equals(right)
            };
        }

        private bool NotEqual(object left, object right)
        {
            return !Equal(left, right);
        }

        private bool And(object left, object right) => IsTruthy(left) && IsTruthy(right);

        private bool Or(object left, object right) => IsTruthy(left) || IsTruthy(right);

        private bool Not(object value) => !IsTruthy(value);

        private bool IsTruthy(object value) => value switch
        {
            bool b => b,
            int i => i != 0,
            double d => d != 0.0,
            string s => !string.IsNullOrEmpty(s),
            null => false,
            _ => throw new Exception($"Unknown truthiness: {value}")
        };

    }
}
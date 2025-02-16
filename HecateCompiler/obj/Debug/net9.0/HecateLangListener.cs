﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from D:\clion_projects\C#\HecateCompiler\HecateCompiler\HecateLang.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace HecateCompiler {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="HecateLangParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IHecateLangListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] HecateLangParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] HecateLangParser.ProgramContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.globalVar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGlobalVar([NotNull] HecateLangParser.GlobalVarContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.globalVar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGlobalVar([NotNull] HecateLangParser.GlobalVarContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.localVar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLocalVar([NotNull] HecateLangParser.LocalVarContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.localVar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLocalVar([NotNull] HecateLangParser.LocalVarContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction([NotNull] HecateLangParser.FunctionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction([NotNull] HecateLangParser.FunctionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.parameters"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParameters([NotNull] HecateLangParser.ParametersContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.parameters"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParameters([NotNull] HecateLangParser.ParametersContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.param"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParam([NotNull] HecateLangParser.ParamContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.param"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParam([NotNull] HecateLangParser.ParamContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] HecateLangParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] HecateLangParser.StatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssignment([NotNull] HecateLangParser.AssignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssignment([NotNull] HecateLangParser.AssignmentContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfStatement([NotNull] HecateLangParser.IfStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfStatement([NotNull] HecateLangParser.IfStatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.loop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLoop([NotNull] HecateLangParser.LoopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.loop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLoop([NotNull] HecateLangParser.LoopContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.forInitialization"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterForInitialization([NotNull] HecateLangParser.ForInitializationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.forInitialization"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitForInitialization([NotNull] HecateLangParser.ForInitializationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.forCondition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterForCondition([NotNull] HecateLangParser.ForConditionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.forCondition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitForCondition([NotNull] HecateLangParser.ForConditionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.forIncrement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterForIncrement([NotNull] HecateLangParser.ForIncrementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.forIncrement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitForIncrement([NotNull] HecateLangParser.ForIncrementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.incrementOrDecrement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIncrementOrDecrement([NotNull] HecateLangParser.IncrementOrDecrementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.incrementOrDecrement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIncrementOrDecrement([NotNull] HecateLangParser.IncrementOrDecrementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.returnStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReturnStatement([NotNull] HecateLangParser.ReturnStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.returnStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReturnStatement([NotNull] HecateLangParser.ReturnStatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] HecateLangParser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] HecateLangParser.BlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] HecateLangParser.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] HecateLangParser.ExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTerm([NotNull] HecateLangParser.TermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTerm([NotNull] HecateLangParser.TermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType([NotNull] HecateLangParser.TypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType([NotNull] HecateLangParser.TypeContext context);
}
} // namespace HecateCompiler

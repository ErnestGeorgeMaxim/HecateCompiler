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
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IHecateLangListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class HecateLangBaseListener : IHecateLangListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterProgram([NotNull] HecateLangParser.ProgramContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitProgram([NotNull] HecateLangParser.ProgramContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.globalVar"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterGlobalVar([NotNull] HecateLangParser.GlobalVarContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.globalVar"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitGlobalVar([NotNull] HecateLangParser.GlobalVarContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.localVar"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLocalVar([NotNull] HecateLangParser.LocalVarContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.localVar"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLocalVar([NotNull] HecateLangParser.LocalVarContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.function"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunction([NotNull] HecateLangParser.FunctionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.function"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunction([NotNull] HecateLangParser.FunctionContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.parameters"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParameters([NotNull] HecateLangParser.ParametersContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.parameters"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParameters([NotNull] HecateLangParser.ParametersContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.param"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParam([NotNull] HecateLangParser.ParamContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.param"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParam([NotNull] HecateLangParser.ParamContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement([NotNull] HecateLangParser.StatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement([NotNull] HecateLangParser.StatementContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.assignment"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAssignment([NotNull] HecateLangParser.AssignmentContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.assignment"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAssignment([NotNull] HecateLangParser.AssignmentContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.ifStatement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIfStatement([NotNull] HecateLangParser.IfStatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.ifStatement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIfStatement([NotNull] HecateLangParser.IfStatementContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.loop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLoop([NotNull] HecateLangParser.LoopContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.loop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLoop([NotNull] HecateLangParser.LoopContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.forInitialization"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterForInitialization([NotNull] HecateLangParser.ForInitializationContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.forInitialization"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitForInitialization([NotNull] HecateLangParser.ForInitializationContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.forCondition"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterForCondition([NotNull] HecateLangParser.ForConditionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.forCondition"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitForCondition([NotNull] HecateLangParser.ForConditionContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.forIncrement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterForIncrement([NotNull] HecateLangParser.ForIncrementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.forIncrement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitForIncrement([NotNull] HecateLangParser.ForIncrementContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.incrementOrDecrement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIncrementOrDecrement([NotNull] HecateLangParser.IncrementOrDecrementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.incrementOrDecrement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIncrementOrDecrement([NotNull] HecateLangParser.IncrementOrDecrementContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.returnStatement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterReturnStatement([NotNull] HecateLangParser.ReturnStatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.returnStatement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitReturnStatement([NotNull] HecateLangParser.ReturnStatementContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBlock([NotNull] HecateLangParser.BlockContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBlock([NotNull] HecateLangParser.BlockContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpression([NotNull] HecateLangParser.ExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpression([NotNull] HecateLangParser.ExpressionContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.term"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTerm([NotNull] HecateLangParser.TermContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.term"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTerm([NotNull] HecateLangParser.TermContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="HecateLangParser.type"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterType([NotNull] HecateLangParser.TypeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HecateLangParser.type"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitType([NotNull] HecateLangParser.TypeContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
} // namespace HecateCompiler
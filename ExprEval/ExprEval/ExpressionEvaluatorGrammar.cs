using Irony.Parsing;
using ExprEval.Node;
using Irony.Interpreter;
using System;

namespace ExprEval
{
    public class ExpressionEvaluatorGrammar : InterpretedLanguageGrammar
    {
        public ExpressionEvaluatorGrammar()
            : base(false)
        { //false means case insensitive

            //Non-Terminal Node
            var program = new NonTerminal("program", typeof(ProgramNode));
            var expression = new NonTerminal("expression", typeof(ExpressionNode));
            var binaryOperator = new NonTerminal("binaryOperator", typeof(BinaryOperatorNode));
           

            //Identifier for Number and Variable
            var number = TerminalFactory.CreateCSharpNumber("number");
            number.AstConfig.NodeType = typeof(MyNumber);

            var Id = new NonTerminal("Id");
            var Id_simple = TerminalFactory.CreateSqlExtIdentifier(this, "id_simple");
            Id.AstConfig.NodeType = typeof(MyVariable);

            var stringLiteral = new StringLiteral("string", "\'", StringOptions.None);


            Id.Rule = MakePlusRule(Id, Id_simple);

            this.Root = program;
            //Expression
            program.Rule = expression;

            expression.Rule = number | Id | stringLiteral
               | expression + binaryOperator + expression
               | "(" + expression + ")";


            binaryOperator.Rule = ToTerm("+") | "-" | "*" | "/" | "%" //arithmetic
                       | "&" | "|" | "^"                     //bit
                       | "=" | ">" | "<" | ">=" | "<=" | "<>" | "!=" | "!<" | "!>" // comparator
                       | "AND" | "OR" | "CONTAINS" | ToTerm("NOT") + "CONTAINS" | ToTerm("NOT"); //others

            //Operators precedence
            RegisterOperators(10, "*", "/", "%");
            RegisterOperators(9, "+", "-");
            RegisterOperators(8, "=", ">", "<", ">=", "<=", "<>", "!=", "!<", "!>");
            RegisterOperators(7, "^", "&", "|");
            RegisterOperators(6, "NOT");
            RegisterOperators(5, "AND");
            RegisterOperators(4, "OR", "CONTAINS");


            this.LanguageFlags = LanguageFlags.CreateAst;

            this.MarkPunctuation("(", ")");

        }

       

    }//class
}

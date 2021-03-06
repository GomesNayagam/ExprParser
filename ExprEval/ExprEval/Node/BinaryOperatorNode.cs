﻿using System.Text;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace ExprEval.Node
{
    public class BinaryOperatorNode : AstNode, IScriptGenerator
    {
        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            TreeNode = treeNode;
        }

        private ParseTreeNode TreeNode { get; set; }

        private Token Token
        {
            get
            { // there is only one child, which is a binary operator
                return TreeNode.ChildNodes[0].Token;
            }
        }

        public void GenerateScript(StringBuilder builder)
        {
            // Seems redundant.  Sean Kearon, March 2014.  AstNode childNode = ChildNodes[0];

            // The binary operators for FL and Javascript are the same,
            // except for the equality operator.
            string op = Token.Text;
            if (op.ToLower() == "contains")
            {
                builder.Append(" like ");
            }
            else
            {
                builder.Append(" "+ op + " ");
            }
        }

    }
}

using System.Text;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;
using System;

namespace ExprEval.Node
{
    public class ExpressionNode : AstNode, IScriptGenerator
    {
        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            TreeNode = treeNode;
        }

        private ParseTreeNode TreeNode { get; set; }

        public void GenerateScript(StringBuilder builder)
        {
            // different expression types have different number of children
            foreach (ParseTreeNode child in TreeNode.ChildNodes)
            {

                var jsChild = child.AstNode as IScriptGenerator;
                if (jsChild != null)
                {
                    jsChild.GenerateScript(builder);
                }
                else
                {
                    Token token = child.Token;
                    if (token != null)
                    {
                        // Just send the text that the user entered straight to javascript.
                        // In most languages, it is not as simple as this (some sort of 
                        // transformation from the source to the destination language is needed).
                        string expressionAsIS = token.Text;

                        builder.Append(expressionAsIS);
                    }
                    else if(child.AstNode.GetType() == typeof(MyVariable))
                    {
                        string variable = child.ChildNodes[0].Token.Text;
                        builder.Append(variable);
                    }
                    else
                    {
                        throw new InvalidOperationException("Was not expected a child of type " + child.GetType().FullName);
                    }
                }
            }

        }

    }
}

using System.Text;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;
using ExprEval.Node;

namespace ExprEval.Node
{
    public class MyNumber : IAstNodeInit
    {
        public void Init(AstContext context, ParseTreeNode parseNode)
        {

        }
    }
    public class MyVariable : IAstNodeInit
    {
        public void Init(AstContext context, ParseTreeNode parseNode)
        {

        }
    }
    public class ProgramNode : AstNode, IScriptGenerator
    {
        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            TreeNode = treeNode;
        }

        private ParseTreeNode TreeNode { get; set; }

        private ExpressionNode expressionNode
        {
            get
            {
                return (ExpressionNode)TreeNode.ChildNodes[0].AstNode;
            }
        }

      
        public void GenerateScript(StringBuilder builder)
        {

            builder.AppendLine("Where (");

            expressionNode.GenerateScript(builder);
         
            builder.AppendLine(")");

        }



    }
}

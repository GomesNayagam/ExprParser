using System.Text;

namespace ExprEval.Node
{
   public interface IScriptGenerator
    {
        void GenerateScript(StringBuilder builder);

    }
}

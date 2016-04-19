
using ExprEval;
using Irony.Parsing;
using System;
using System.Text;
using ExprEval.Node;

namespace TestExpEval
{
    class Program
    {
        static void Main(string[] args)
        {
            String input = @"(Type = 'Subject' AND ()Anatomical Region = 'Kidney' AND Type = 'Raw Image' AND Type Contains 'Clinical Study Data'))";

            ExpressionEvaluatorGrammar grammar = new ExpressionEvaluatorGrammar();
            Parser compiler = new Parser(grammar);
            var program = compiler.Parse(input);

           if (program.HasErrors())
           {
               Console.WriteLine(input);
               foreach (var err in program.ParserMessages)
               {
                   Console.WriteLine(string.Format("Line {0} , Column {1}", err.Location.Line, err.Location.Column));
                   Console.WriteLine(err.Message); //print message
               }
               System.Console.ReadLine();
               return;
		   }

            var generator = (IScriptGenerator)program.Root.AstNode;
			var js = new StringBuilder();
			generator.GenerateScript(js);

            System.Console.WriteLine(js.ToString());

            System.Console.ReadLine();
        }
    }
}

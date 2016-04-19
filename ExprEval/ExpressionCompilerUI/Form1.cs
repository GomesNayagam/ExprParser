using ExprEval;
using ExprEval.Node;
using Irony.Ast;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressionCompilerUI
{
    public partial class Form1 : Form
    {
        private ExpressionEvaluatorGrammar grammar;
        private Parser compiler;
        private ParseTree program;


        public Form1()
        {
            InitializeComponent();

            grammar = new ExpressionEvaluatorGrammar();
            compiler = new Parser(grammar);

        }

        private void button1_Click(object sender, EventArgs e)
        {
           // String input = @"(Type = 'Subject' AND ()Anatomical Region = 'Kidney' AND Type = 'Raw Image' AND Type = 'Clinical Study Data'))";
            button2.Enabled = true;
            button3.Enabled = true;
            richTextBox2.Clear();

            String input = richTextBox1.Text;
            
            program = compiler.Parse(input);

            if (program.HasErrors())
            {
                button2.Enabled = false;
                button3.Enabled = false;


                foreach (var err in program.ParserMessages)
                {
                    richTextBox2.AppendText(string.Format("Line {0} , Column {1}", err.Location.Line, err.Location.Column));
                    richTextBox2.AppendText("\n" + err.Message); 
                }
                return;
            }

            richTextBox2.AppendText("0 Compilation errors");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox3.Clear();

            var generator = (IScriptGenerator)program.Root.AstNode;
            var js = new StringBuilder();
            generator.GenerateScript(js);

            richTextBox3.AppendText(js.ToString());


        }

        private void button3_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            if (program == null) return;
            AddParseNodeRec(null, program.Root);
        }

        private void AddParseNodeRec(TreeNode parent, ParseTreeNode node)
        {
            if (node == null) return;
            string txt = node.ToString();
            TreeNode tvNode = (parent == null ? treeView1.Nodes.Add(txt) : parent.Nodes.Add(txt));
            tvNode.Tag = node;
            foreach (var child in node.ChildNodes)
                AddParseNodeRec(tvNode, child);
        }

    }
}

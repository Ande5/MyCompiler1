using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCompiler
{
    public partial class CompilerGUI : Form
    {
        LLParserLoading ll_loading = new LLParserLoading();
        LRParserLoading lr_loading = new LRParserLoading();

        public CompilerGUI()
        {
            InitializeComponent();
            CompilerEvent.PrintCompileInfoLLParser = new CompilerEvent.PrintResult(PrintCompileInfoLLParser);
            CompilerEvent.PrintMessageLLParser = new CompilerEvent.PrintResult(PrintMessage);
            CompilerEvent.PrintCompileInfoLRParser = new CompilerEvent.PrintResult(PrintCompileInfoLRParser);
            CompilerEvent.PrintMessageLRParser = new CompilerEvent.PrintResult(PrintMessage);
            CompilerEvent.PrintCompileResult = new CompilerEvent.PrintResult(PrintCompileResult);
            textInputData.Text = "else else := id const then := id const if > id id then else := id const then := id const if > id id if > const const else := id const then := id const if & id id";
        }

        private void PrintMessage(string text)
        {
            MessageBox.Show(text);
        }
        private void PrintCompileInfoLLParser(string text)
        {
            if (!InvokeRequired)
            {
                richTextLLParser.AppendText(text + "\r");
                richTextLLParser.ScrollToCaret();
            }
            else
                Invoke(new CompilerEvent.PrintResult(PrintCompileInfoLLParser), new object[] { text });
        }

        private void PrintCompileInfoLRParser(string text)
        {
            if (!InvokeRequired)
            {  
                richTextLRParser.AppendText(text + "\r");
                richTextLRParser.ScrollToCaret();
            }
            else
                Invoke(new CompilerEvent.PrintResult(PrintCompileInfoLRParser), new object[] { text });

        }
        private void PrintCompileResult(string text)
        {
            if (!InvokeRequired)
                textCompiler.Text = text;
            else
                Invoke(new CompilerEvent.PrintResult(PrintCompileResult), new object[] { text });
        }
        private void button1_Click(object sender, EventArgs e)
        {
            richTextLLParser.Clear();
            LLParser ll_parser = new LLParser(ll_loading);
            ll_parser.Run(textInputData.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextLRParser.Clear();
            textCompiler.Clear();
            LRParser lr_parser = new LRParser(lr_loading);
            string str = textInputData.Text;
            Thread compileThread = new Thread(lr_parser.Run);
            compileThread.Start(str);
        }

        private void button3_Click(object sender, EventArgs e)
        {
           

            richTextLRParser.Clear();
            textCompiler.Clear();
            LRParser lr_parser = new LRParser(lr_loading);
            string str = textInputData.Text;
            Thread compileThread = new Thread(lr_parser.Run);
            compileThread.Start(str);

            richTextLLParser.Clear();
            LLParser ll_parser = new LLParser(ll_loading);
          //  ll_parser.Run(textBox1.Text);
            Thread compileThread1 = new Thread(ll_parser.Run);
            compileThread1.Start(str);
            
        }

        private void butRun_Click(object sender, EventArgs e)
        {
            if (checkLLParser.Checked)
            {
                LLParser ll_parser = new LLParser(ll_loading);
                Thread compileThread1 = new Thread(ll_parser.Run);
                compileThread1.Start(textInputData.Text);
            }
            if (checkLRParser.Checked)
            {
                LRParser lr_parser = new LRParser(lr_loading);
                Thread compileThread = new Thread(lr_parser.Run);
                compileThread.Start(textInputData.Text);
            }
        }
    }
}

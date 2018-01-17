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
        public LrParser Analysis;
        public CompilerGUI()
        {
            InitializeComponent();
            Analysis = new LrParser(lr_loading);
            Analysis.PrintCompileInfo += AnalysisOnPrintCompileInfo1;
            Analysis.PrintCompileResult += AnalysisOnPrintCompileResult1;
            Analysis.PrintMessage += AnalysisOnPrintMessage;
            LLParserEvent.PrintCompileInfo = new LLParserEvent.PrintResult(AnalysisOnPrintCompileInfo);
            LLParserEvent.PrintMessage = new LLParserEvent.PrintResult(AnalysisOnPrintMessage);
            textBox1.Text = "else else := id const then := id const if > id id then else := id const then := id const if > id id if > const const else := id const then := id const if & id id";
        }

        private void AnalysisOnPrintMessage(string text)
        {
            MessageBox.Show(text);
        }
        private void AnalysisOnPrintCompileInfo(string text)
        {
            if (!InvokeRequired)
            {
                richTextBox1.AppendText(text + "\r");
                richTextBox1.ScrollToCaret();
            }
            else
                Invoke(new LrParser.PrintDelegate(AnalysisOnPrintCompileInfo), new object[] { text });
        }

        private void AnalysisOnPrintCompileInfo1(string text)
        {
            if (!InvokeRequired)
            {  
                richTextBox2.AppendText(text + "\r");
                richTextBox2.ScrollToCaret();
            }
            else
                Invoke(new LrParser.PrintDelegate(AnalysisOnPrintCompileInfo1), new object[] { text });

        }
        private void AnalysisOnPrintCompileResult1(string text)
        {
            if (!InvokeRequired)
                textBox2.Text = text;
            else
                Invoke(new LrParser.PrintDelegate(AnalysisOnPrintCompileResult1), new object[] { text });
        }
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            LLParser ll_parser = new LLParser(ll_loading);
            ll_parser.Run(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            textBox2.Clear();

            string str = textBox1.Text;
            Thread compileThread = new Thread(Analysis.Run);
            compileThread.Start(str);
        }

        private void button3_Click(object sender, EventArgs e)
        {
           

            richTextBox2.Clear();
            textBox2.Clear();

            string str = textBox1.Text;
            Thread compileThread = new Thread(Analysis.Run);
            compileThread.Start(str);

            richTextBox1.Clear();
            LLParser ll_parser = new LLParser(ll_loading);
          //  ll_parser.Run(textBox1.Text);
            Thread compileThread1 = new Thread(ll_parser.Run);
            compileThread1.Start(str);
            
        }
    }
}

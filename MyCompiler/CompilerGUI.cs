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
        public CompilerGUI()
        {
            InitializeComponent();
            CompilerEvent.PrintCompileInfoLLParser = new CompilerEvent.PrintResult(PrintCompileInfoLLParser);
            CompilerEvent.PrintMessageLLParser = new CompilerEvent.PrintResult(PrintMessage);
            CompilerEvent.PrintCompileInfoLRParser = new CompilerEvent.PrintResult(PrintCompileInfoLRParser);
            CompilerEvent.PrintMessageLRParser = new CompilerEvent.PrintResult(PrintMessage);
            CompilerEvent.PrintCompileResult = new CompilerEvent.PrintResult(PrintCompileResult);
           //Примеры правил
            textInputData.Text = "#else := t [ true ] false #then := t [ 0xAB ] true #if > t min";
            textInputData.Text = "#else #else := x 0xABF #then := y 0xFB #if > x y #then #else := index [ 0x0 ] 0xAB #then := k 0x15A #if & max s #if > Max number1 #else := s + s 0x1 #then := number [ 0x1 ] * number [ 0x2 ] 0xFFF #if | n true";
            textInputData.Text = "#else #else := x 0xABF #then := y 0xFB #if > x y #then #else := index [ 0x0 ] 0xAB #then := k 0x15A #if & max s #if > Max number1 #else := s + s 0x1 #then := number [ 0x1 ] * number [ 0x2 ] 0xFFF #if | n true #else := t [ true ] false #then := t [ 0xAB ] true #if > t min";
        }

        private void PrintMessage(string text)
        {
            MessageBox.Show(text, "Обноружена ошибка!");
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
        private void butRun_Click(object sender, EventArgs e)
        {
                  ClearParser();
            if (checkLLParser.Checked)
            {
                LLParser ll_parser = new LLParser(new LLParserLoading());
                Thread compileThread1 = new Thread(ll_parser.Run);
                compileThread1.Start(textInputData.Text);
            }
            if (checkLRParser.Checked)
            {
                LRParser lr_parser = new LRParser(new LRParserLoading());
                Thread compileThread = new Thread(lr_parser.Run);
                compileThread.Start(textInputData.Text);
            }
        }
        
        private void ClearParser()
        {
            richTextLLParser.Clear();
            richTextLRParser.Clear();
            textCompiler.Clear();
        }

        private void labelInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выполнил: студент группы 1490\n\t  Щелоков А.С.\nПроектирование компиляторов Вариант №6", "О программе!");
        }
    }
}

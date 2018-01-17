namespace MyCompiler
{
    partial class CompilerGUI
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textInputData = new System.Windows.Forms.TextBox();
            this.richTextLLParser = new System.Windows.Forms.RichTextBox();
            this.richTextLRParser = new System.Windows.Forms.RichTextBox();
            this.textCompiler = new System.Windows.Forms.TextBox();
            this.groupControl = new System.Windows.Forms.GroupBox();
            this.butRun = new System.Windows.Forms.Button();
            this.checkLRParser = new System.Windows.Forms.CheckBox();
            this.checkLLParser = new System.Windows.Forms.CheckBox();
            this.labelInput = new System.Windows.Forms.Label();
            this.labelLine = new System.Windows.Forms.Label();
            this.labelLLParser = new System.Windows.Forms.Label();
            this.labelLine1 = new System.Windows.Forms.Label();
            this.labelLRParser = new System.Windows.Forms.Label();
            this.labelLine2 = new System.Windows.Forms.Label();
            this.labelOutputC = new System.Windows.Forms.Label();
            this.labelLine3 = new System.Windows.Forms.Label();
            this.groupControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // textInputData
            // 
            this.textInputData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textInputData.Location = new System.Drawing.Point(13, 33);
            this.textInputData.Multiline = true;
            this.textInputData.Name = "textInputData";
            this.textInputData.Size = new System.Drawing.Size(721, 95);
            this.textInputData.TabIndex = 1;
            // 
            // richTextLLParser
            // 
            this.richTextLLParser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextLLParser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextLLParser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.richTextLLParser.Location = new System.Drawing.Point(13, 160);
            this.richTextLLParser.Name = "richTextLLParser";
            this.richTextLLParser.Size = new System.Drawing.Size(721, 96);
            this.richTextLLParser.TabIndex = 2;
            this.richTextLLParser.Text = "";
            // 
            // richTextLRParser
            // 
            this.richTextLRParser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextLRParser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextLRParser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.richTextLRParser.Location = new System.Drawing.Point(11, 291);
            this.richTextLRParser.Name = "richTextLRParser";
            this.richTextLRParser.Size = new System.Drawing.Size(721, 96);
            this.richTextLRParser.TabIndex = 3;
            this.richTextLRParser.Text = "";
            // 
            // textCompiler
            // 
            this.textCompiler.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textCompiler.Location = new System.Drawing.Point(12, 416);
            this.textCompiler.Multiline = true;
            this.textCompiler.Name = "textCompiler";
            this.textCompiler.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textCompiler.Size = new System.Drawing.Size(488, 113);
            this.textCompiler.TabIndex = 4;
            // 
            // groupControl
            // 
            this.groupControl.Controls.Add(this.butRun);
            this.groupControl.Controls.Add(this.checkLRParser);
            this.groupControl.Controls.Add(this.checkLLParser);
            this.groupControl.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupControl.Location = new System.Drawing.Point(507, 398);
            this.groupControl.Name = "groupControl";
            this.groupControl.Size = new System.Drawing.Size(227, 131);
            this.groupControl.TabIndex = 7;
            this.groupControl.TabStop = false;
            this.groupControl.Text = "Управление";
            // 
            // butRun
            // 
            this.butRun.Location = new System.Drawing.Point(42, 83);
            this.butRun.Name = "butRun";
            this.butRun.Size = new System.Drawing.Size(139, 29);
            this.butRun.TabIndex = 2;
            this.butRun.Text = "Выполнить";
            this.butRun.UseVisualStyleBackColor = true;
            this.butRun.Click += new System.EventHandler(this.butRun_Click);
            // 
            // checkLRParser
            // 
            this.checkLRParser.AutoSize = true;
            this.checkLRParser.Location = new System.Drawing.Point(22, 47);
            this.checkLRParser.Name = "checkLRParser";
            this.checkLRParser.Size = new System.Drawing.Size(167, 23);
            this.checkLRParser.TabIndex = 1;
            this.checkLRParser.Text = "Восходящий разбор";
            this.checkLRParser.UseVisualStyleBackColor = true;
            // 
            // checkLLParser
            // 
            this.checkLLParser.AutoSize = true;
            this.checkLLParser.Location = new System.Drawing.Point(22, 23);
            this.checkLLParser.Name = "checkLLParser";
            this.checkLLParser.Size = new System.Drawing.Size(169, 23);
            this.checkLLParser.TabIndex = 0;
            this.checkLLParser.Text = "Нисходящий разбор";
            this.checkLLParser.UseVisualStyleBackColor = true;
            // 
            // labelInput
            // 
            this.labelInput.AutoSize = true;
            this.labelInput.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInput.Location = new System.Drawing.Point(28, 7);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(135, 19);
            this.labelInput.TabIndex = 8;
            this.labelInput.Text = "Исходные данные";
            // 
            // labelLine
            // 
            this.labelLine.AutoSize = true;
            this.labelLine.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLine.Location = new System.Drawing.Point(15, 12);
            this.labelLine.Name = "labelLine";
            this.labelLine.Size = new System.Drawing.Size(708, 16);
            this.labelLine.TabIndex = 9;
            this.labelLine.Text = "_________________________________________________________________________________" +
    "___________________";
            // 
            // labelLLParser
            // 
            this.labelLLParser.AutoSize = true;
            this.labelLLParser.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLLParser.Location = new System.Drawing.Point(26, 136);
            this.labelLLParser.Name = "labelLLParser";
            this.labelLLParser.Size = new System.Drawing.Size(150, 19);
            this.labelLLParser.TabIndex = 10;
            this.labelLLParser.Text = "Нисходящий разбор";
            // 
            // labelLine1
            // 
            this.labelLine1.AutoSize = true;
            this.labelLine1.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLine1.Location = new System.Drawing.Point(18, 141);
            this.labelLine1.Name = "labelLine1";
            this.labelLine1.Size = new System.Drawing.Size(708, 16);
            this.labelLine1.TabIndex = 11;
            this.labelLine1.Text = "_________________________________________________________________________________" +
    "___________________";
            // 
            // labelLRParser
            // 
            this.labelLRParser.AutoSize = true;
            this.labelLRParser.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLRParser.Location = new System.Drawing.Point(28, 265);
            this.labelLRParser.Name = "labelLRParser";
            this.labelLRParser.Size = new System.Drawing.Size(148, 19);
            this.labelLRParser.TabIndex = 12;
            this.labelLRParser.Text = "Восходящий разбор";
            // 
            // labelLine2
            // 
            this.labelLine2.AutoSize = true;
            this.labelLine2.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLine2.Location = new System.Drawing.Point(18, 271);
            this.labelLine2.Name = "labelLine2";
            this.labelLine2.Size = new System.Drawing.Size(708, 16);
            this.labelLine2.TabIndex = 13;
            this.labelLine2.Text = "_________________________________________________________________________________" +
    "___________________";
            // 
            // labelOutputC
            // 
            this.labelOutputC.AutoSize = true;
            this.labelOutputC.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOutputC.Location = new System.Drawing.Point(28, 388);
            this.labelOutputC.Name = "labelOutputC";
            this.labelOutputC.Size = new System.Drawing.Size(87, 19);
            this.labelOutputC.TabIndex = 14;
            this.labelOutputC.Text = "Текст на C";
            // 
            // labelLine3
            // 
            this.labelLine3.AutoSize = true;
            this.labelLine3.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLine3.Location = new System.Drawing.Point(18, 397);
            this.labelLine3.Name = "labelLine3";
            this.labelLine3.Size = new System.Drawing.Size(477, 16);
            this.labelLine3.TabIndex = 15;
            this.labelLine3.Text = "___________________________________________________________________";
            // 
            // CompilerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(746, 541);
            this.Controls.Add(this.labelOutputC);
            this.Controls.Add(this.labelLine3);
            this.Controls.Add(this.labelLRParser);
            this.Controls.Add(this.labelLine2);
            this.Controls.Add(this.labelLLParser);
            this.Controls.Add(this.labelLine1);
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.groupControl);
            this.Controls.Add(this.textCompiler);
            this.Controls.Add(this.richTextLRParser);
            this.Controls.Add(this.richTextLLParser);
            this.Controls.Add(this.textInputData);
            this.Controls.Add(this.labelLine);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CompilerGUI";
            this.ShowIcon = false;
            this.Text = "Компилятор";
            this.groupControl.ResumeLayout(false);
            this.groupControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textInputData;
        private System.Windows.Forms.RichTextBox richTextLLParser;
        private System.Windows.Forms.RichTextBox richTextLRParser;
        private System.Windows.Forms.TextBox textCompiler;
        private System.Windows.Forms.GroupBox groupControl;
        private System.Windows.Forms.CheckBox checkLRParser;
        private System.Windows.Forms.CheckBox checkLLParser;
        private System.Windows.Forms.Button butRun;
        private System.Windows.Forms.Label labelInput;
        private System.Windows.Forms.Label labelLine;
        private System.Windows.Forms.Label labelLLParser;
        private System.Windows.Forms.Label labelLine1;
        private System.Windows.Forms.Label labelLRParser;
        private System.Windows.Forms.Label labelLine2;
        private System.Windows.Forms.Label labelOutputC;
        private System.Windows.Forms.Label labelLine3;
    }
}


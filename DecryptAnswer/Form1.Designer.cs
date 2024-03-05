namespace DecryptAnswer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1=new Label();
            privateKey=new TextBox();
            label2=new Label();
            button1=new Button();
            openFileDialog1=new OpenFileDialog();
            txtPath=new TextBox();
            button2=new Button();
            Panel=new Panel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize=true;
            label1.Location=new Point(49, 31);
            label1.Name="label1";
            label1.Size=new Size(107, 15);
            label1.TabIndex=0;
            label1.Text="請輸入Private Key:";
            label1.Click+=label1_Click;
            // 
            // privateKey
            // 
            privateKey.Location=new Point(162, 28);
            privateKey.Name="privateKey";
            privateKey.Size=new Size(522, 23);
            privateKey.TabIndex=1;
            // 
            // label2
            // 
            label2.AutoSize=true;
            label2.Location=new Point(38, 74);
            label2.Name="label2";
            label2.Size=new Size(118, 15);
            label2.TabIndex=2;
            label2.Text="請選擇要匯入的檔案:";
            // 
            // button1
            // 
            button1.Location=new Point(162, 70);
            button1.Name="button1";
            button1.Size=new Size(75, 23);
            button1.TabIndex=3;
            button1.Text="瀏覽檔案";
            button1.UseVisualStyleBackColor=true;
            button1.Click+=button1_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName="openFileDialog1";
            // 
            // txtPath
            // 
            txtPath.Location=new Point(245, 70);
            txtPath.Name="txtPath";
            txtPath.Size=new Size(439, 23);
            txtPath.TabIndex=4;
            // 
            // button2
            // 
            button2.Location=new Point(312, 112);
            button2.Name="button2";
            button2.Size=new Size(128, 32);
            button2.TabIndex=5;
            button2.Text="Decrypt Answer";
            button2.UseVisualStyleBackColor=true;
            button2.Click+=button2_Click;
            // 
            // Panel
            // 
            Panel.BackColor=SystemColors.ButtonHighlight;
            Panel.Location=new Point(36, 157);
            Panel.Name="Panel";
            Panel.Size=new Size(659, 226);
            Panel.TabIndex=6;
            // 
            // Form1
            // 
            AutoScaleDimensions=new SizeF(7F, 15F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(800, 450);
            Controls.Add(Panel);
            Controls.Add(button2);
            Controls.Add(txtPath);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(privateKey);
            Controls.Add(label1);
            Name="Form1";
            Text="Form1";
            Load+=Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TextBox privateKey;
        private Label label2;
        private Button button1;
        private OpenFileDialog openFileDialog1;
        private TextBox txtPath;
        private Button button2;
        private Panel Panel;
    }
}
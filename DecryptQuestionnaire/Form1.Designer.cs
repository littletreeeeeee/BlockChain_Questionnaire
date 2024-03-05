namespace DecryptQuestionnaire
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
            txtLotteryQty=new Label();
            txtLottery=new Label();
            txtOpenReward=new Label();
            txtReward=new Label();
            txtPublishQty=new Label();
            txtDeadline=new Label();
            txtStartDate=new Label();
            txtDesc=new Label();
            txtTitle=new Label();
            txtId=new Label();
            label12=new Label();
            label11=new Label();
            label10=new Label();
            label9=new Label();
            label8=new Label();
            label7=new Label();
            label6=new Label();
            label5=new Label();
            label4=new Label();
            label3=new Label();
            panel1=new Panel();
            UserInfoPanel=new Panel();
            Panel.SuspendLayout();
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
            Panel.Controls.Add(txtLotteryQty);
            Panel.Controls.Add(txtLottery);
            Panel.Controls.Add(txtOpenReward);
            Panel.Controls.Add(txtReward);
            Panel.Controls.Add(txtPublishQty);
            Panel.Controls.Add(txtDeadline);
            Panel.Controls.Add(txtStartDate);
            Panel.Controls.Add(txtDesc);
            Panel.Controls.Add(txtTitle);
            Panel.Controls.Add(txtId);
            Panel.Controls.Add(label12);
            Panel.Controls.Add(label11);
            Panel.Controls.Add(label10);
            Panel.Controls.Add(label9);
            Panel.Controls.Add(label8);
            Panel.Controls.Add(label7);
            Panel.Controls.Add(label6);
            Panel.Controls.Add(label5);
            Panel.Controls.Add(label4);
            Panel.Controls.Add(label3);
            Panel.Location=new Point(36, 157);
            Panel.Name="Panel";
            Panel.Size=new Size(627, 151);
            Panel.TabIndex=6;
            // 
            // txtLotteryQty
            // 
            txtLotteryQty.AutoSize=true;
            txtLotteryQty.Location=new Point(263, 123);
            txtLotteryQty.Margin=new Padding(2, 0, 2, 0);
            txtLotteryQty.Name="txtLotteryQty";
            txtLotteryQty.Size=new Size(10, 15);
            txtLotteryQty.TabIndex=20;
            txtLotteryQty.Text=" ";
            // 
            // txtLottery
            // 
            txtLottery.AutoSize=true;
            txtLottery.Location=new Point(78, 123);
            txtLottery.Margin=new Padding(2, 0, 2, 0);
            txtLottery.Name="txtLottery";
            txtLottery.Size=new Size(0, 15);
            txtLottery.TabIndex=19;
            // 
            // txtOpenReward
            // 
            txtOpenReward.AutoSize=true;
            txtOpenReward.Location=new Point(449, 96);
            txtOpenReward.Margin=new Padding(2, 0, 2, 0);
            txtOpenReward.Name="txtOpenReward";
            txtOpenReward.Size=new Size(10, 15);
            txtOpenReward.TabIndex=18;
            txtOpenReward.Text=" ";
            // 
            // txtReward
            // 
            txtReward.AutoSize=true;
            txtReward.Location=new Point(267, 96);
            txtReward.Margin=new Padding(2, 0, 2, 0);
            txtReward.Name="txtReward";
            txtReward.Size=new Size(10, 15);
            txtReward.TabIndex=17;
            txtReward.Text=" ";
            // 
            // txtPublishQty
            // 
            txtPublishQty.AutoSize=true;
            txtPublishQty.Location=new Point(78, 96);
            txtPublishQty.Margin=new Padding(2, 0, 2, 0);
            txtPublishQty.Name="txtPublishQty";
            txtPublishQty.Size=new Size(10, 15);
            txtPublishQty.TabIndex=16;
            txtPublishQty.Text=" ";
            // 
            // txtDeadline
            // 
            txtDeadline.AutoSize=true;
            txtDeadline.Location=new Point(290, 68);
            txtDeadline.Margin=new Padding(2, 0, 2, 0);
            txtDeadline.Name="txtDeadline";
            txtDeadline.Size=new Size(10, 15);
            txtDeadline.TabIndex=15;
            txtDeadline.Text=" ";
            // 
            // txtStartDate
            // 
            txtStartDate.AutoSize=true;
            txtStartDate.Location=new Point(110, 68);
            txtStartDate.Margin=new Padding(2, 0, 2, 0);
            txtStartDate.Name="txtStartDate";
            txtStartDate.Size=new Size(10, 15);
            txtStartDate.TabIndex=14;
            txtStartDate.Text=" ";
            // 
            // txtDesc
            // 
            txtDesc.AutoSize=true;
            txtDesc.Location=new Point(62, 43);
            txtDesc.Margin=new Padding(2, 0, 2, 0);
            txtDesc.Name="txtDesc";
            txtDesc.Size=new Size(10, 15);
            txtDesc.TabIndex=13;
            txtDesc.Text=" ";
            // 
            // txtTitle
            // 
            txtTitle.AutoSize=true;
            txtTitle.Location=new Point(248, 16);
            txtTitle.Margin=new Padding(2, 0, 2, 0);
            txtTitle.Name="txtTitle";
            txtTitle.Size=new Size(10, 15);
            txtTitle.TabIndex=12;
            txtTitle.Text=" ";
            // 
            // txtId
            // 
            txtId.AutoSize=true;
            txtId.Location=new Point(51, 16);
            txtId.Margin=new Padding(2, 0, 2, 0);
            txtId.Name="txtId";
            txtId.Size=new Size(10, 15);
            txtId.TabIndex=11;
            txtId.Text=" ";
            // 
            // label12
            // 
            label12.AutoSize=true;
            label12.Location=new Point(205, 125);
            label12.Margin=new Padding(2, 0, 2, 0);
            label12.Name="label12";
            label12.Size=new Size(58, 15);
            label12.TabIndex=10;
            label12.Text="抽獎數量:";
            // 
            // label11
            // 
            label11.AutoSize=true;
            label11.Location=new Point(20, 125);
            label11.Margin=new Padding(2, 0, 2, 0);
            label11.Name="label11";
            label11.Size=new Size(58, 15);
            label11.TabIndex=9;
            label11.Text="抽獎獎勵:";
            // 
            // label10
            // 
            label10.AutoSize=true;
            label10.Location=new Point(390, 96);
            label10.Margin=new Padding(2, 0, 2, 0);
            label10.Name="label10";
            label10.Size=new Size(58, 15);
            label10.TabIndex=8;
            label10.Text="公開獎勵:";
            // 
            // label9
            // 
            label9.AutoSize=true;
            label9.Location=new Point(205, 96);
            label9.Margin=new Padding(2, 0, 2, 0);
            label9.Name="label9";
            label9.Size=new Size(61, 15);
            label9.TabIndex=7;
            label9.Text="填答獎勵: ";
            label9.Click+=label9_Click;
            // 
            // label8
            // 
            label8.AutoSize=true;
            label8.Location=new Point(20, 96);
            label8.Margin=new Padding(2, 0, 2, 0);
            label8.Name="label8";
            label8.Size=new Size(58, 15);
            label8.TabIndex=6;
            label8.Text="發行數量:";
            // 
            // label7
            // 
            label7.AutoSize=true;
            label7.Location=new Point(205, 68);
            label7.Margin=new Padding(2, 0, 2, 0);
            label7.Name="label7";
            label7.Size=new Size(85, 15);
            label7.TabIndex=5;
            label7.Text="問卷結束時間: ";
            // 
            // label6
            // 
            label6.AutoSize=true;
            label6.Location=new Point(20, 68);
            label6.Margin=new Padding(2, 0, 2, 0);
            label6.Name="label6";
            label6.Size=new Size(85, 15);
            label6.TabIndex=4;
            label6.Text="問卷開始時間: ";
            label6.Click+=label6_Click;
            // 
            // label5
            // 
            label5.AutoSize=true;
            label5.Location=new Point(20, 43);
            label5.Margin=new Padding(2, 0, 2, 0);
            label5.Name="label5";
            label5.Size=new Size(40, 15);
            label5.TabIndex=3;
            label5.Text="Desc :";
            // 
            // label4
            // 
            label4.AutoSize=true;
            label4.Location=new Point(205, 16);
            label4.Margin=new Padding(2, 0, 2, 0);
            label4.Name="label4";
            label4.Size=new Size(40, 15);
            label4.TabIndex=2;
            label4.Text="Title : ";
            // 
            // label3
            // 
            label3.AutoSize=true;
            label3.Location=new Point(20, 16);
            label3.Margin=new Padding(2, 0, 2, 0);
            label3.Name="label3";
            label3.Size=new Size(28, 15);
            label3.TabIndex=0;
            label3.Text="ID : ";
            // 
            // panel1
            // 
            panel1.AutoScroll=true;
            panel1.AutoScrollMargin=new Size(0, 240);
            panel1.AutoSize=true;
            panel1.BackColor=SystemColors.ButtonHighlight;
            panel1.Location=new Point(36, 323);
            panel1.Margin=new Padding(2);
            panel1.Name="panel1";
            panel1.Size=new Size(628, 231);
            panel1.TabIndex=7;
            // 
            // UserInfoPanel
            // 
            UserInfoPanel.AutoScroll=true;
            UserInfoPanel.AutoScrollMargin=new Size(0, 240);
            UserInfoPanel.AutoSize=true;
            UserInfoPanel.BackColor=SystemColors.ButtonHighlight;
            UserInfoPanel.Location=new Point(707, 161);
            UserInfoPanel.Name="UserInfoPanel";
            UserInfoPanel.Size=new Size(459, 393);
            UserInfoPanel.TabIndex=8;
            // 
            // Form1
            // 
            AutoScaleDimensions=new SizeF(7F, 15F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(1227, 577);
            Controls.Add(UserInfoPanel);
            Controls.Add(panel1);
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
            Panel.ResumeLayout(false);
            Panel.PerformLayout();
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
        private Label label3;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label txtLotteryQty;
        private Label txtLottery;
        private Label txtOpenReward;
        private Label txtReward;
        private Label txtPublishQty;
        private Label txtDeadline;
        private Label txtStartDate;
        private Label txtDesc;
        private Label txtTitle;
        private Label txtId;
        private Label label12;
        private Label label11;
        private Label label10;
        private Panel panel1;
        private Panel UserInfoPanel;
    }
}
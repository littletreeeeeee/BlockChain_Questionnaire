using DecryptQuestionnaire.Enum;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sodium;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace DecryptQuestionnaire
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            this.txtPath.Text = file.FileName;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.privateKey.Text))
            {
                MessageBox.Show("請輸入Private Key", "Warning");
            }
            else if (string.IsNullOrEmpty(this.txtPath.Text))
            {
                MessageBox.Show("請選擇要匯入的檔案!", "warning");
            }

            StreamReader str = new StreamReader(this.txtPath.Text);
            string text = str.ReadToEnd();
            str.Close();

            Label textControl = new System.Windows.Forms.Label();
            textControl.Text = text;
            FinalExportAnswer answer = JsonConvert.DeserializeObject<FinalExportAnswer>(text);
            this.txtId.Text=answer?.Questionnaire?._id;
            this.txtTitle.Text=answer?.Questionnaire?.Title;
            this.txtDesc.Text=answer?.Questionnaire?.Desc;
            this.txtStartDate.Text=answer?.Questionnaire?.StartDate?.ToString("yyyy/MM/dd");
            this.txtDeadline.Text=answer?.Questionnaire?.Deadline?.ToString("yyyy/MM/dd");
            this.txtReward.Text=answer?.Questionnaire?.Reward;
            this.txtOpenReward.Text=answer?.Questionnaire?.OpenReward;
            this.txtLottery.Text=answer?.Questionnaire?.Lottery;
            this.txtLotteryQty.Text=answer?.Questionnaire?.LotteryQty;
            this.txtPublishQty.Text=answer?.Questionnaire?.Circulation;

            int y = 20;
            List<AnswerViewModel> answerList = new List<AnswerViewModel>(); ;
            List<MemberInfoViewModel> userInfo = new List<MemberInfoViewModel>(); ;
            foreach (var item in answer.Answers)
            {
                //答案要解密
                var userAnswer = DecodeAnswer(item);
                if (userAnswer.Answer!=null)
                    answerList.AddRange(userAnswer.Answer);
                if (userAnswer.UserInfo!=null)
                    userInfo.Add(userAnswer.UserInfo);
            }
            this.panel1.AutoScroll=true;
            foreach (var item in answer.Questionnaire.Questions)
            {
                this.panel1.Controls.Add(new System.Windows.Forms.Label
                {
                    Text=$"[{(item.Seq+1)}] {item.Question}  ({GetQuestionType(item.QuestionType)})",
                    Name="qLabel"+item.Seq,
                    Size = new Size(500, 40),
                    Location=new Point(20, y)
                });
                if (item.QuestionType!="text")
                {
                    foreach (var desc in item.ItemDesc)
                    {
                        y=y+50;
                        panel1.Controls.Add(new Label
                        {
                            Text=$"      ◆ {desc}",
                            Name="qLabel"+item.Seq+item.ItemDesc.IndexOf(desc),
                            Size = new Size(300, 40),
                            Location=new Point(20, y)
                        });
                        panel1.Controls.Add(new Label
                        {
                            Text=$"[{answerList.Where(x => x.Seq==item.Seq && x.Answer==desc).Count()}]",
                            Name="qAnswerCount"+item.Seq,
                            Size = new Size(100, 40),
                            ForeColor= Color.FromKnownColor(KnownColor.Blue),
                            Location=new Point(400, y)
                        });
                    }
                }
                else
                {
                    foreach (var ans in answerList.Where(x => x.Seq==item.Seq).ToList())
                    {
                        y=y+50;

                        panel1.Controls.Add(new Label
                        {
                            Text=$"      ◆ {ans.Answer}",
                            Name="qTestAnswer"+item.Seq+answerList.Where(x => x.Seq==item.Seq).ToList().IndexOf(ans).ToString(),
                            Size = new Size(300, 40),
                            ForeColor= Color.FromKnownColor(KnownColor.Blue),
                            Location=new Point(20, y)
                        }); ;
                    }
                }
                y=y+50;

            }

            //放基本資料ㄉ
            int userInfoY = 20;
            if (userInfo==null || userInfo.Count==0)
            {
                UserInfoPanel.Controls.Add(new Label
                {
                    Text=$"尚無人公開基本資料!",
                    Name="uLabel",
                    Size = new Size(300, 40),
                    Location=new Point(20, userInfoY)
                });
            }
            else
            {


                UserInfoPanel.Controls.Add(new Label
                {
                    Text=$"性別 : ",
                    Name="uLabelGender",
                    Size = new Size(80, 40),
                    Location=new Point(20, userInfoY)
                });
                userInfoY=userInfoY+50;

                var genderList = userInfo.Select(x => x.Gender).Distinct().ToList();
                foreach (var gender in genderList)
                {
                    UserInfoPanel.Controls.Add(new Label
                    {
                        Text=$"     - {gender} : {userInfo.Where(x => x.Gender==gender).Count()}",
                        Name="genderLabel"+genderList.IndexOf(gender),
                        Size = new Size(200, 40),
                        Location=new Point(110, userInfoY),
                        ForeColor= Color.FromKnownColor(KnownColor.Blue),

                    });
                    userInfoY=userInfoY+50;
                }

                UserInfoPanel.Controls.Add(new Label
                {
                    Text=$"學歷 : ",
                    Name="uLabelEdu",
                    Size = new Size(80, 40),
                    Location=new Point(20, userInfoY)
                });
                userInfoY=userInfoY+50;

                var eduactionList = userInfo.Select(x => x.Eduaction_Level).Distinct().ToList();
                foreach (var edu in eduactionList)
                {
                    UserInfoPanel.Controls.Add(new Label
                    {
                        Text=$"     - {EduactionLevelEnum.GetDesc(edu)} : {userInfo.Where(x => x.Eduaction_Level==edu).Count()}",
                        Name="eduabel"+eduactionList.IndexOf(edu),
                        Size = new Size(200, 40),
                        Location=new Point(110, userInfoY),
                        ForeColor= Color.FromKnownColor(KnownColor.Blue),

                    });
                    userInfoY=userInfoY+50;
                }


                UserInfoPanel.Controls.Add(new Label
                {
                    Text=$"婚姻狀況 : ",
                    Name="uLabelMarriage",
                    Size = new Size(80, 40),
                    Location=new Point(20, userInfoY)
                });
                userInfoY=userInfoY+50;

                var marriageList = userInfo.Select(x => x.Marital_Status).Distinct().ToList();
                foreach (var marr in marriageList)
                {
                    UserInfoPanel.Controls.Add(new Label
                    {
                        Text=$"     - {MaritalStatusEnum.GetDesc(marr)} : {userInfo.Where(x => x.Marital_Status==marr).Count()}",
                        Name="marrLabel"+marriageList.IndexOf(marr),
                        Size = new Size(200, 40),
                        Location=new Point(110, userInfoY),
                        ForeColor= Color.FromKnownColor(KnownColor.Blue),

                    });
                    userInfoY=userInfoY+50;
                    
                }


                UserInfoPanel.Controls.Add(new Label
                {
                    Text=$"年齡分布 : ",
                    Name="uLabelAge",
                    Size = new Size(80, 40),
                    Location=new Point(20, userInfoY)
                });
                userInfoY=userInfoY+50;

                int under20 = 0;
                int bet2130 = 0;
                int bet3140 = 0;
                int bet4150 = 0;
                int bet5160 = 0;
                int over60 = 0;
                foreach (var u in userInfo)
                {
                    var age = DateTime.Now.Year-Convert.ToDateTime(u.Birthday).Year;
                    if (age<=20)
                        under20++;
                    if (age>20 && age<=30)
                        bet2130++;
                    if (age>30 && age<=40)
                        bet3140++;
                    if (age>40 && age<=50)
                        bet4150++;
                    if (age>50 && age<=60)
                        bet5160++;
                    if (age>60)
                        over60++;


                }
                UserInfoPanel.Controls.Add(new Label
                {
                    Text=$"     - 年齡 <= 20 : {under20}",
                    Name="ageLabel20",
                    Size = new Size(200, 40),
                    Location=new Point(110, userInfoY),
                    ForeColor= Color.FromKnownColor(KnownColor.Blue),

                });
                userInfoY=userInfoY+50;

                UserInfoPanel.Controls.Add(new Label
                {
                    Text=$"     - 20<年齡<=30 : {bet2130}",
                    Name="ageLabel2130",
                    Size = new Size(200, 40),
                    Location=new Point(110, userInfoY),
                    ForeColor= Color.FromKnownColor(KnownColor.Blue),

                });
                userInfoY=userInfoY+50;

                UserInfoPanel.Controls.Add(new Label
                {
                    Text=$"     - 30<年齡<=40 : {bet3140}",
                    Name="ageLabel3140",
                    Size = new Size(200, 40),
                    Location=new Point(110, userInfoY),
                    ForeColor= Color.FromKnownColor(KnownColor.Blue),

                });
                userInfoY=userInfoY+50;


                UserInfoPanel.Controls.Add(new Label
                {
                    Text=$"     - 40<年齡<=50 : {bet4150}",
                    Name="ageLabel4150",
                    Size = new Size(200, 40),
                    Location=new Point(110, userInfoY),
                    ForeColor= Color.FromKnownColor(KnownColor.Blue),

                });
                userInfoY=userInfoY+50;
                UserInfoPanel.Controls.Add(new Label
                {
                    Text=$"     - 40<年齡<=60 : {bet5160}",
                    Name="ageLabel5160",
                    Size = new Size(200, 40),
                    Location=new Point(110, userInfoY),
                    ForeColor= Color.FromKnownColor(KnownColor.Blue),

                });
                userInfoY=userInfoY+50;
                UserInfoPanel.Controls.Add(new Label
                {
                    Text=$"     - 年齡>60 : {over60}",
                    Name="ageLabelover60",
                    Size = new Size(200, 40),
                    Location=new Point(110, userInfoY),
                    ForeColor= Color.FromKnownColor(KnownColor.Blue),

                });


                /*
                 
        public string? Eduaction_Level { get; set; }
        public string? Marital_Status { get; set; }
        public string? Birthday { get; set; }
                 */
            }


        }
        private string GetQuestionType(string type)
        {
            switch (type)
            {
                case "text":
                    return "問答題";
                case "select":
                    return "下拉選項";
                case "radio":
                    return "單選按鈕";
                case "checkbox":
                    return "複選按鈕";
                default:
                    return type;
            }
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private UserAnswerViewModel DecodeAnswer(string answer)
        {
            string privateKeyHex = this.privateKey.Text;
            byte[] privateKey = HexStringToByteArray(privateKeyHex.StartsWith("0x") ? privateKeyHex.Substring(2) : privateKeyHex);

            // ?入加密后的消息（? JavaScript ?出）
            string encryptedMessageJson = Encoding.UTF8.GetString(HexStringToByteArray(answer.StartsWith("0x") ? answer.Substring(2) : answer));
            JObject encryptedMessageObj = JObject.Parse(encryptedMessageJson);

            byte[] nonce = Convert.FromBase64String(encryptedMessageObj["nonce"].ToString());
            byte[] ephemPublicKey = Convert.FromBase64String(encryptedMessageObj["ephemPublicKey"].ToString());
            byte[] cipherText = Convert.FromBase64String(encryptedMessageObj["ciphertext"].ToString());

            byte[] decryptedMessageBytes = PublicKeyBox.Open(cipherText, nonce, privateKey, ephemPublicKey);
            string decryptedMessage = System.Text.Encoding.UTF8.GetString(decryptedMessageBytes);

            Console.WriteLine("Decrypted message: " + decryptedMessage);

            return JsonConvert.DeserializeObject<UserAnswerViewModel>(decryptedMessage);

        }
        private static byte[] HexStringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

    }
}
namespace DecryptAnswer
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
                MessageBox.Show("�п�JPrivate Key", "Warning");
            }
            else if (string.IsNullOrEmpty(this.txtPath.Text))
            {
                MessageBox.Show("�п�ܭn�פJ���ɮ�!", "warning");
            }

            StreamReader str = new StreamReader(this.txtPath.Text);
            var text = str.ReadToEnd();
            str.Close();

            Label textControl = new Label();
            textControl.Text = text;
            Panel.Controls.Add(textControl);
        }
    }
}
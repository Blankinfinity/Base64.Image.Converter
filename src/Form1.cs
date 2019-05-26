using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base64_Image_Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Svg File(*.svg)|*.svg";
            saveFileDialog1.ShowDialog();            
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string name = saveFileDialog1.FileName;
            ImageFromBase64(textBox1.Text, name);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Svg File(*.svg)|*.svg";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var name = openFileDialog1.FileName;
                ImageToBase64(name);
            }
        }

        private void ImageFromBase64(string base64Img, string fileName)
        {
            var bytes = Convert.FromBase64String(base64Img);
            using (var img = new FileStream(fileName, FileMode.Create))
            {
                img.Write(bytes, 0, bytes.Length);
                img.Flush();
            }
        }

        private void ImageToBase64(string name)
        {
            byte[] binaryData;
            using (var svgfile = new FileStream(name, FileMode.Open, FileAccess.Read))
            {
                binaryData = new byte[svgfile.Length];
                long bytesRead = svgfile.Read(binaryData, 0, (int) svgfile.Length);

                svgfile.Close();
            }

            string base64String;
            try
            {
                base64String = Convert.ToBase64String(binaryData, 0, binaryData.Length);
                textBox1.Text = base64String;
            }
            catch (ArgumentNullException)
            {
                return;
            }
        }
    }
}

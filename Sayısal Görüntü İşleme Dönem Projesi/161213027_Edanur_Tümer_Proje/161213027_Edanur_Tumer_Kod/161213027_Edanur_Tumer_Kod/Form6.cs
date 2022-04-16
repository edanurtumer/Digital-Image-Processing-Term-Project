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

namespace _161213027_Edanur_Tumer_Kod
{
    
    public partial class Form6 : Form
    {
    public void ResimDegistir(Image resim)
    {
        pictureBox2.Image = resim;
    }
        public Form6()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ResimDegistir(pictureBox1.Image);
            form5.Show();
            this.Hide();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox2.Image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "jpeg Resmi|*.jpg|Bitmap Resmi|*.bmp|Gif Resmi|*.gif";
            saveFileDialog1.Title = "Resmi Kaydet";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != ""); //Dosya adı boş değilse kaydedecek.
            {
                //FileStream nesnesi ile kayıtı gerçekleştirecek.
                FileStream DosyaAkisi = (FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        pictureBox2.Image.Save(DosyaAkisi, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case 2:
                        pictureBox2.Image.Save(DosyaAkisi, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case 3:
                        pictureBox2.Image.Save(DosyaAkisi, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }
                DosyaAkisi.Close();
            }
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _161213027_Edanur_Tumer_Kod
{
    public partial class Form1 : Form
    {
        public void ResimDegistir(Image resim)
        {
            pictureBox1.Image = resim;
        }
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            OpenFileDialog resim = new OpenFileDialog();
            resim.Filter = "Resim Dosyası|*.jpg;*.nef;*.png| Video|*.avi|Tüm Dosyalar |*.*";
            resim.Title = "YENİ RESİM AÇMA";

            string dosyaYolu = resim.FileName;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog resim = new OpenFileDialog();
            resim.Filter = "Resim Dosyası |*.jpg;*.nef;*.png| Video|*.avi| Tüm Dosyalar |*.*";
            resim.Title = "RESİM AÇMA";
            resim.ShowDialog();
            string dosyaYolu = resim.FileName;
            pictureBox1.ImageLocation = dosyaYolu;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ResimDegistir(pictureBox1.Image);
            form2.Show();
            this.Hide();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _161213027_Edanur_Tumer_Kod
{
    public partial class Form5 : Form
    {
        public void ResimDegistir(Image resim)
        {
            pictureBox2.Image = resim;
        }

        public Form5()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            if (radioButton2.Checked)
            {
                comboBox1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.ResimDegistir(pictureBox1.Image);
            form6.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ResimDegistir(pictureBox1.Image);
            form4.Show();
            this.Hide();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox2.Image;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        int[] histogram = new int[256];
        void BuildPixelArray()
        {
            Bitmap GirisResmi;
            GirisResmi = new Bitmap(pictureBox2.Image);

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            int[,] pixelArray = new int[pictureBox2.Image.Height, pictureBox1.Image.Width];
            int[,] greyPixelArray = new int[pictureBox2.Image.Height, pictureBox1.Image.Width];
            pixelArray = new int[ResimGenisligi, ResimYuksekligi];
            greyPixelArray = new int[ResimGenisligi, ResimYuksekligi];
            Rectangle rect = new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height);
            Bitmap temp = new Bitmap(pictureBox2.Image);
            BitmapData bmpData = temp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int remain = bmpData.Stride - bmpData.Width * 3;
            unsafe
            {
                byte* ptr = (byte*)bmpData.Scan0;
                for (int j = 0; j < bmpData.Height; j++)
                {
                    for (int i = 0; i < bmpData.Width; i++)
                    {
                        pixelArray[i, j] = ptr[0] + ptr[1] * 256 + ptr[2] * 256 * 256;
                        greyPixelArray[i, j] = (int)((double)ptr[0] * 0.11 + (double)ptr[1] * 0.59 + (double)ptr[2] * 0.3);
                        histogram[greyPixelArray[i, j]]++;
                        ptr += 3;
                    }
                    ptr += remain;
                }
            }
            temp.UnlockBits(bmpData);
        }
        int otsuValue;

        void EsikDegeriBulma()
        {
            double fmax = -1.0;
            double m1, m2, S, toplam1 = 0.0, toplam2 = 0.0;
            int nTop = 0, n1 = 0, n2;

            for (int i = 0; i < 256; i++)
            {
                toplam1 += (double)i * (double)histogram[i];
                nTop += histogram[i];
            }

            for (int i = 0; i < 256; i++)
            {
                n1 += histogram[i];
                if (n1 == 0)
                    continue;
                n2 = nTop - n1;
                if (n2 == 0)
                    break;
                toplam2 += (double)i * (double)histogram[i];
                m1 = toplam2 / n1;
                m2 = (toplam1 - toplam2) / n2;
                S = (double)n1 * (double)n2 * (m1 - m2) * (m1 - m2);
                if (S > fmax)
                {
                    fmax = S;
                    otsuValue = i;
                }
            }
        }
        void Binary()
        {
            Bitmap GirisResmi,CikisResmi;
            GirisResmi = new Bitmap(pictureBox2.Image);

            int[,] pixelArray = new int[pictureBox1.Image.Height, pictureBox1.Image.Width];
            int[,] greyPixelArray = new int[pictureBox1.Image.Height, pictureBox1.Image.Width];
            int[,] binaryPixelArray = new int[pictureBox1.Image.Height, pictureBox1.Image.Width];

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            binaryPixelArray = new int[ResimGenisligi, ResimYuksekligi];
            for (int i = 0; i < ResimGenisligi; i++)
            {
                for (int j = 0; j < ResimYuksekligi; j++)
                {
                    if (greyPixelArray[i, j] < 0)
                        binaryPixelArray[i, j] = 0;
                    else
                        binaryPixelArray[i, j] = 255;

                    Color OkunanRenk, DonusenRenk;
                    OkunanRenk = GirisResmi.GetPixel(0, 255);
                    DonusenRenk = OkunanRenk;
                    CikisResmi.SetPixel(i, j, DonusenRenk);
                }
            }
            pictureBox1.Image = CikisResmi;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                BuildPixelArray();
                EsikDegeriBulma();
                Binary();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                
            }
            if (comboBox1.SelectedIndex == 2)
            {
                
            }
            if (comboBox1.SelectedIndex == 3)
            {
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _161213027_Edanur_Tumer_Kod
{
    public partial class Form4 : Form
    {
        public void ResimDegistir(Image resim)
        {
            pictureBox2.Image = resim;
        }
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox2.Image;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            if (radioButton2.Checked)
            {
                comboBox1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ResimDegistir(pictureBox1.Image);
            form3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ResimDegistir(pictureBox1.Image);
            form5.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        void Genisletme()
        {
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox2.Image);

            //Kolaylık sağlamak için görüntü boyut değişkenleri oluştur.
            int width = GirisResmi.Width;
            int height = GirisResmi.Height;

            //Hızlı işlem için bitleri sistem belleğine kilitle.
            Rectangle canvas = new Rectangle(0, 0, width, height);
            BitmapData srcData = GirisResmi.LockBits(canvas, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int stride = srcData.Stride;
            int bytes = stride * srcData.Height;

            //Tüm piksel verilerini tutacak bayt dizileri oluşturun, biri işleme için, biri çıktı için.
            byte[] pixelBuffer = new byte[bytes];
            byte[] resultBuffer = new byte[bytes];

            //Piksel verilerini işleme amaçlı diziye yaz.
            Marshal.Copy(srcData.Scan0, pixelBuffer, 0, bytes);
            GirisResmi.UnlockBits(srcData);

            //Convert to grayscale
            float rgb = 0;
            for (int i = 0; i < bytes; i += 4)
            {
                rgb = pixelBuffer[i] * .071f;
                rgb += pixelBuffer[i + 1] * .71f;
                rgb += pixelBuffer[i + 2] * .21f;
                pixelBuffer[i] = (byte)rgb;
                pixelBuffer[i + 1] = pixelBuffer[i];
                pixelBuffer[i + 2] = pixelBuffer[i];
                pixelBuffer[i + 3] = 255;
            }

            //Bu, çekirdeğin sınırından Merkez pikselin ofsetidir.
            int kernelOffset = (3 - 1) / 2;
            int calcOffset = 0;
            int byteOffset = 0;
            for (int y = kernelOffset; y < height - kernelOffset; y++)
            {
                for (int x = kernelOffset; x < width - kernelOffset; x++)
                {
                    byte value = 0;
                    byteOffset = y * stride + x * 4;

                    //Uyguluyor.
                    for (int ykernel = -kernelOffset; ykernel <= kernelOffset; ykernel++)
                    {
                        for (int xkernel = -kernelOffset; xkernel <= kernelOffset; xkernel++)
                        {
                            if (shape[ykernel + kernelOffset, xkernel + kernelOffset] == 1)
                            {
                                calcOffset = byteOffset + ykernel * stride + xkernel * 4;
                                value = Math.Max(value, pixelBuffer[calcOffset]);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    //İşlenmiş verileri ikinci diziye yaz.
                    resultBuffer[byteOffset] = value;
                    resultBuffer[byteOffset + 1] = value;
                    resultBuffer[byteOffset + 2] = value;
                    resultBuffer[byteOffset + 3] = 255;
                }
            }
            //Bu işlevin çıktı bitmap oluştur.
            Bitmap rsltImg = new Bitmap(width, height);
            BitmapData rsltData = rsltImg.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            //İşlenmiş verileri bitmap formuna yaz.
            Marshal.Copy(resultBuffer, 0, rsltData.Scan0, bytes);
            rsltImg.UnlockBits(rsltData);
            

            pictureBox1.Image=rsltImg;
        }
        private byte[,] shape
        {
            get
            {
                return new byte[,]
                {
            { 0, 1, 0 },
            { 1, 1, 1 },
            { 0, 1, 0 }
                };
            }
        }




        public void Erozyon()
        {
            Bitmap GirisResmi;
            GirisResmi = new Bitmap(pictureBox2.Image);

            //Kolaylık sağlamak için görüntü boyut değişkenleri oluştur.
            int width = GirisResmi.Width;
            int height = GirisResmi.Height;

            //Hızlı işlem için bitleri sistem belleğine kilitle.
            Rectangle canvas = new Rectangle(0, 0, width, height);
            BitmapData srcData = GirisResmi.LockBits(canvas, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int stride = srcData.Stride;
            int bytes = stride * srcData.Height;

            //Tüm piksel verilerini tutacak bayt dizileri oluşturun, biri işleme için, biri çıktı için.
            byte[] pixelBuffer = new byte[bytes];
            byte[] resultBuffer = new byte[bytes];

            //Piksel verilerini işleme amaçlı diziye yaz.
            Marshal.Copy(srcData.Scan0, pixelBuffer, 0, bytes);
            GirisResmi.UnlockBits(srcData);

            //Convert to grayscale

            float rgb = 0;
            for (int i = 0; i < bytes; i += 4)
            {
                rgb = pixelBuffer[i] * .071f;
                rgb += pixelBuffer[i + 1] * .71f;
                rgb += pixelBuffer[i + 2] * .21f;
                pixelBuffer[i] = (byte)rgb;
                pixelBuffer[i + 1] = pixelBuffer[i];
                pixelBuffer[i + 2] = pixelBuffer[i];
                pixelBuffer[i + 3] = 255;
            }


            //int[] kernel;
            int kernelSize = 3;
            int kernelOffset = (kernelSize - 1) / 2;
            int calcOffset = 0;
            int byteOffset = 0;

            for (int y = kernelOffset; y < height - kernelOffset; y++)
            {
                for (int x = kernelOffset; x < width - kernelOffset; x++)
                {
                    byte value = 255;
                    byteOffset = y * srcData.Stride + x * 4;
                    for (int ykernel = -kernelOffset; ykernel <= kernelOffset; ykernel++)
                    {
                        for (int xkernel = -kernelOffset; xkernel <= kernelOffset; xkernel++)
                        {
                            if (shape[ykernel + kernelOffset, xkernel + kernelOffset] == 1)
                            {
                                calcOffset = byteOffset + ykernel * srcData.Stride + xkernel * 4;
                                value = Math.Min(value, pixelBuffer[calcOffset]);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    resultBuffer[byteOffset] = value;
                    resultBuffer[byteOffset + 1] = value;
                    resultBuffer[byteOffset + 2] = value;
                    resultBuffer[byteOffset + 3] = 255;
                }
            }
            //Bu işlevin çıktı bitmap oluştur.
            Bitmap rsltImg = new Bitmap(width, height);
            BitmapData rsltData = rsltImg.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            //İşlenmiş verileri bitmap formuna yaz.
            Marshal.Copy(resultBuffer, 0, rsltData.Scan0, bytes);
            rsltImg.UnlockBits(rsltData);


            pictureBox1.Image = rsltImg;
        }
        public void Acma()
        {
            Bitmap GirisResmi;
            GirisResmi = new Bitmap(pictureBox2.Image);

            //Kolaylık sağlamak için görüntü boyut değişkenleri oluştur.
            int width = GirisResmi.Width;
            int height = GirisResmi.Height;

            //Hızlı işlem için bitleri sistem belleğine kilitle.
            Rectangle canvas = new Rectangle(0, 0, width, height);
            BitmapData srcData = GirisResmi.LockBits(canvas, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int stride = srcData.Stride;
            int bytes = stride * srcData.Height;

            //Tüm piksel verilerini tutacak bayt dizileri oluşturun, biri işleme için, biri çıktı için.
            byte[] pixelBuffer = new byte[bytes];
            byte[] resultBuffer = new byte[bytes];

            //Piksel verilerini işleme amaçlı diziye yaz.
            Marshal.Copy(srcData.Scan0, pixelBuffer, 0, bytes);
            GirisResmi.UnlockBits(srcData);

            //Convert to grayscale

            float rgb = 0;
            for (int i = 0; i < bytes; i += 4)
            {
                rgb = pixelBuffer[i] * .071f;
                rgb += pixelBuffer[i + 1] * .71f;
                rgb += pixelBuffer[i + 2] * .21f;
                pixelBuffer[i] = (byte)rgb;
                pixelBuffer[i + 1] = pixelBuffer[i];
                pixelBuffer[i + 2] = pixelBuffer[i];
                pixelBuffer[i + 3] = 255;
            }


            //int[] kernel;
            int kernelSize = 3;
            int kernelOffset = (kernelSize - 1) / 2;
            int calcOffset = 0;
            int byteOffset = 0;

            for (int y = kernelOffset; y < height - kernelOffset; y++)
            {
                for (int x = kernelOffset; x < width - kernelOffset; x++)
                {
                    byte value = 255;
                    byteOffset = y * srcData.Stride + x * 4;
                    for (int ykernel = -kernelOffset; ykernel <= kernelOffset; ykernel++)
                    {
                        for (int xkernel = -kernelOffset; xkernel <= kernelOffset; xkernel++)
                        {
                            if (shape[ykernel + kernelOffset, xkernel + kernelOffset] == 1)
                            {
                                calcOffset = byteOffset + ykernel * srcData.Stride + xkernel * 4;
                                value = Math.Min(value, pixelBuffer[calcOffset]);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    resultBuffer[byteOffset] = value;
                    resultBuffer[byteOffset + 1] = value;
                    resultBuffer[byteOffset + 2] = value;
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            //Bu, çekirdeğin sınırından Merkez pikselin ofsetidir.
            int kernelOffset1 = (3 - 1) / 2;
            int calcOffset1 = 0;
            int byteOffset1 = 0;
            for (int y = kernelOffset1; y < height - kernelOffset1; y++)
            {
                for (int x = kernelOffset1; x < width - kernelOffset1; x++)
                {
                    byte value = 0;
                    byteOffset1 = y * stride + x * 4;

                    //Uyguluyor.
                    for (int ykernel = -kernelOffset1; ykernel <= kernelOffset1; ykernel++)
                    {
                        for (int xkernel = -kernelOffset1; xkernel <= kernelOffset1; xkernel++)
                        {
                            if (shape[ykernel + kernelOffset1, xkernel + kernelOffset1] == 1)
                            {
                                calcOffset = byteOffset1 + ykernel * stride + xkernel * 4;
                                value = Math.Max(value, pixelBuffer[calcOffset1]);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    //İşlenmiş verileri ikinci diziye yaz.
                    resultBuffer[byteOffset] = value;
                    resultBuffer[byteOffset + 1] = value;
                    resultBuffer[byteOffset + 2] = value;
                    resultBuffer[byteOffset + 3] = 255;
                }
            }
            //Bu işlevin çıktı bitmap oluştur.
            Bitmap rsltImg = new Bitmap(width, height);
            BitmapData rsltData = rsltImg.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            //İşlenmiş verileri bitmap formuna yaz.
            Marshal.Copy(resultBuffer, 0, rsltData.Scan0, bytes);
            rsltImg.UnlockBits(rsltData);


            pictureBox1.Image = rsltImg;

        }
        public void Kapama()
        {
            Bitmap GirisResmi;
            GirisResmi = new Bitmap(pictureBox2.Image);

            //Kolaylık sağlamak için görüntü boyut değişkenleri oluştur.
            int width = GirisResmi.Width;
            int height = GirisResmi.Height;

            //Hızlı işlem için bitleri sistem belleğine kilitle.
            Rectangle canvas = new Rectangle(0, 0, width, height);
            BitmapData srcData = GirisResmi.LockBits(canvas, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int stride = srcData.Stride;
            int bytes = stride * srcData.Height;

            //Tüm piksel verilerini tutacak bayt dizileri oluşturun, biri işleme için, biri çıktı için.
            byte[] pixelBuffer = new byte[bytes];
            byte[] resultBuffer = new byte[bytes];

            //Piksel verilerini işleme amaçlı diziye yaz.
            Marshal.Copy(srcData.Scan0, pixelBuffer, 0, bytes);
            GirisResmi.UnlockBits(srcData);

          

            float rgb = 0;
            for (int i = 0; i < bytes; i += 4)
            {
                rgb = pixelBuffer[i] * .071f;
                rgb += pixelBuffer[i + 1] * .71f;
                rgb += pixelBuffer[i + 2] * .21f;
                pixelBuffer[i] = (byte)rgb;
                pixelBuffer[i + 1] = pixelBuffer[i];
                pixelBuffer[i + 2] = pixelBuffer[i];
                pixelBuffer[i + 3] = 255;
            }

            //Bu, çekirdeğin sınırından Merkez pikselin ofsetidir.
            int kernelOffset = (3 - 1) / 2;
            int calcOffset = 0;
            int byteOffset = 0;
            for (int y = kernelOffset; y < height - kernelOffset; y++)
            {
                for (int x = kernelOffset; x < width - kernelOffset; x++)
                {
                    byte value = 0;
                    byteOffset = y * stride + x * 4;

                    //Uyguluyor.
                    for (int ykernel = -kernelOffset; ykernel <= kernelOffset; ykernel++)
                    {
                        for (int xkernel = -kernelOffset; xkernel <= kernelOffset; xkernel++)
                        {
                            if (shape[ykernel + kernelOffset, xkernel + kernelOffset] == 1)
                            {
                                calcOffset = byteOffset + ykernel * stride + xkernel * 4;
                                value = Math.Max(value, pixelBuffer[calcOffset]);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    //İşlenmiş verileri ikinci diziye yaz.
                    resultBuffer[byteOffset] = value;
                    resultBuffer[byteOffset + 1] = value;
                    resultBuffer[byteOffset + 2] = value;
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            //int[] kernel;
            int kernelSize1 = 3;
            int kernelOffset1 = (kernelSize1 - 1) / 2;
            int calcOffset1 = 0;
            int byteOffset1 = 0;

            for (int y = kernelOffset1; y < height - kernelOffset1; y++)
            {
                for (int x = kernelOffset1; x < width - kernelOffset1; x++)
                {
                    byte value = 255;
                    byteOffset = y * srcData.Stride + x * 4;
                    for (int ykernel = -kernelOffset1; ykernel <= kernelOffset1; ykernel++)
                    {
                        for (int xkernel = -kernelOffset1; xkernel <= kernelOffset1; xkernel++)
                        {
                            if (shape[ykernel + kernelOffset1, xkernel + kernelOffset1] == 1)
                            {
                                calcOffset1 = byteOffset1 + ykernel * srcData.Stride + xkernel * 4;
                                value = Math.Min(value, pixelBuffer[calcOffset]);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    resultBuffer[byteOffset] = value;
                    resultBuffer[byteOffset + 1] = value;
                    resultBuffer[byteOffset + 2] = value;
                    resultBuffer[byteOffset + 3] = 255;
                }
            }
            //Bu işlevin çıktı bitmap oluştur.
            Bitmap rsltImg = new Bitmap(width, height);
            BitmapData rsltData = rsltImg.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            //İşlenmiş verileri bitmap formuna yaz.
            Marshal.Copy(resultBuffer, 0, rsltData.Scan0, bytes);
            rsltImg.UnlockBits(rsltData);


            pictureBox1.Image = rsltImg;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            { 
                Genisletme();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                Erozyon();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                Acma();
            }
            if (comboBox1.SelectedIndex == 3)
            {
                Kapama();
            }
        }
    }
}

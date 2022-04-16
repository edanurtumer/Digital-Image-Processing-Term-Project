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
    public partial class Form3 : Form
    {
        public void ResimDegistir(Image resim)
        {
            pictureBox2.Image = resim;
        }
        public Form3()
        {
            InitializeComponent();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ResimDegistir(pictureBox1.Image);
            form4.Show();
            this.Hide();

        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ResimDegistir(pictureBox1.Image);
            form2.Show();
            this.Hide();
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            if (radioButton2.Checked)
            {
                comboBox1.Enabled = false;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox2.Image;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
        public void Bulaniklastirma()
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 7; //şablon boyutu 3 den büyük tek rakam olmalıdır (3,5,7 gibi).
            int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            toplamR = toplamR + OkunanRenk.R;
                            toplamG = toplamG + OkunanRenk.G;
                            toplamB = toplamB + OkunanRenk.B;
                        }
                    }
                    ortalamaR = toplamR / (SablonBoyutu * SablonBoyutu);
                    ortalamaG = toplamG / (SablonBoyutu * SablonBoyutu);
                    ortalamaB = toplamB / (SablonBoyutu * SablonBoyutu);
                    CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));

                }
            }
            pictureBox1.Image = CikisResmi;
        }
        public void Keskinlestirme()
        {
            
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi; 
            GirisResmi = new Bitmap(pictureBox1.Image); 
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y, i, j, toplamR, toplamG, toplamB;
            int R, G, B;
            int[] Matris = { 0, -2, 0, -2, 11, -2, 0, -2, 0 };
            int MatrisToplami = 0 + -2 + 0 + -2 + 11 + -2 + 0 + -2 + 0;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++) //Resmi taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.
            {
                for(y = (SablonBoyutu -1) / 2; y <ResimYuksekligi -(SablonBoyutu -1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;//Şablon bölgesi (çekirdek matris) içindeki pikselleri tarıyor.
                    int k = 0;
                    //matris içindeki elemanları sırayla okurken kullanılacak.
                    for(i = -((SablonBoyutu -1) / 2); i <= (SablonBoyutu -1) / 2; i++)
                    {
                        for(j = -((SablonBoyutu -1) / 2); j <= (SablonBoyutu -1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            toplamR = toplamR + OkunanRenk.R * Matris[k];
                            toplamG = toplamG+ OkunanRenk.G * Matris[k];
                            toplamB = toplamB + OkunanRenk.B * Matris[k];
                            R = toplamR / MatrisToplami;
                            G = toplamG / MatrisToplami;
                            B = toplamB / MatrisToplami;
                            
                            //Renkler sınırların dışına çıktıysa, sınır değer alınacak.
                            if(R > 255) R = 255;
                            if(G > 255) G = 255;
                            if(B > 255) B = 255;
                            if(R < 0) R = 0;
                            if(G < 0) G = 0;
                            if(B < 0) B = 0;
                            
                            CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                            k++;
                        }
                    }
                }
            }
            pictureBox1.Image = CikisResmi;
        }
        public void Ortanca()
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox2.Image);

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int SablonBoyutu = 7; //Şablon boyutu 3'ten büyük tek rakam olmalıdır.
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;

            int[] R = new int[ElemanSayisi];
            int[] G = new int[ElemanSayisi];
            int[] B = new int[ElemanSayisi];
            int[] Gri = new int[ElemanSayisi];

            int x, y, i, j;

            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    int k = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            R[k] = OkunanRenk.R;
                            G[k] = OkunanRenk.G;
                            B[k] = OkunanRenk.B;

                            Gri[k] = Convert.ToInt16(R[k] * 0.299 + G[k] * 0.587 + B[k] * 0.114); //Gri ton formülü
                            k++;
                        }
                    }
                    //Gri tona göre sıralama yapıyor. Aynı anda üç rengide değiştiriyor.
                    int GeciciSayi = 0;
                    for (i = 0; i < ElemanSayisi; i++)
                    {
                        for (j = i + 1; j < ElemanSayisi; j++)
                        {
                            if (Gri[j] < Gri[i])
                            {
                                GeciciSayi = Gri[i];
                                Gri[i] = Gri[j];
                                Gri[j] = GeciciSayi;
                                GeciciSayi = R[i];
                                R[i] = R[j];
                                R[j] = GeciciSayi;
                                GeciciSayi = G[i];
                                G[i] = G[j];
                                G[j] = GeciciSayi;
                                GeciciSayi = B[i];
                                B[i] = B[j];
                                B[j] = GeciciSayi;
                            }
                        }
                    }
                    //Sıralama sonrası ortadaki değeri çıkış resminin piksel değeri olarak atıyor.
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R[(ElemanSayisi - 1) / 2], G[(ElemanSayisi - 1) / 2], B[(ElemanSayisi - 1) / 2]));
                }           
            }
            pictureBox1.Image = CikisResmi;
        }
        public void Laplace()
        {

        }
        public void KenarBulma()
        {
            Color renk1;
            //maskelerimiz.
            int [,] GX = new int[3, 3];
            int [,] GY = new int[3, 3];

            

            //yatay yönde kenar
            GX[0, 0] = -1; GX[0, 1] = 0; GX[0, 2] = 1;
            GX[1, 0] = -2; GX[1, 1] = 0; GX[1, 2] = 2;
            GX[2, 0] = -1; GX[2, 1] = 0; GX[2, 2] = 1;

            //düsey yönde kenar
            GY[0, 0] = -1; GY[0, 1] = -2; GY[0, 2] = -1;
            GY[1, 0] = 0; GY[1, 1] = 0; GY[1, 2] = 0;
            GY[2, 0] = 1; GY[2, 1] = 2; GY[2, 2] = 1;

            int valx, valy,gradient; // Yatayda bulduğumuz kenarla için.

            Bitmap gri = new Bitmap(pictureBox2.Image);   //Burda resmimizi bir bitmap ortamına alıyoruz
            for (int i = 0; i < gri.Height - 1; i++)                            // ardından enlem ve boylam şeklinde tarama yapıcağımız için resmin boyuna göre bir döngü oluşturuyoruz
            {
                for (int j = 0; j < gri.Width - 1; j++)                           // birde enine döngü oluşturuyoruz
                {
                    int deger = (gri.GetPixel(j, i).R + gri.GetPixel(j, i).G + gri.GetPixel(j, i).B) / 3;  // ardından yukarıda da bahsettiğim pikseldeki RGB değerinin aritmatik ortalamasını alıyoruz ve deger değişkenine atıyoruz
                    Color renk;
                    renk = Color.FromArgb(deger, deger, deger);  //burda ise bir üst satırda oluşturduğumuz renk değişkeninin RGB değerlerine aritmetik ortalamasını aldırdığımız yeni rengi veriyoruz
                    gri.SetPixel(j, i, renk);     //ve pikselimizi bitmapimizin j boylamında i enlemindeki noktaya yerleştiriyoruz
                }
            }
            Bitmap CikisResmi = new Bitmap(gri.Width, gri.Height); //Aynı boyutlara sahip boş bir görüntümüz olacak.
            for(int x=0; x< gri.Height; x++)
            {
                for (int y = 0; y < gri.Width; y++)
                {
                    if (x==0 || x==gri.Height-1 || y==0 || y==gri.Width-1)
                    {
                        renk1 = Color.FromArgb(255, 255, 255);
                        CikisResmi.SetPixel(y, x, renk1);

                        valx = 0;
                        valy = 0;
                    }
                    else
                    {
                        valx = gri.GetPixel(y-1, x-1).R * GX[0, 0] +
                            gri.GetPixel(y,x-1).R*GX[0,1]+
                            gri.GetPixel(y+1,x-1).R*GX[0,2]+
                            gri.GetPixel(y - 1, x).R * GX[1, 0]+
                            gri.GetPixel(y, x).R * GX[1, 1]+
                            gri.GetPixel(y + 1,x ).R * GX[1, 2]+
                            gri.GetPixel(y - 1, x + 1).R * GX[2, 0]+
                            gri.GetPixel(y, x+1).R * GX[2, 1]+
                            gri.GetPixel(y+ 1, x + 1).R * GX[2, 2];

                        valy = gri.GetPixel(y - 1, x - 1).R * GY[0, 0] +
                            gri.GetPixel(y, x - 1).R * GY[0, 1] +
                            gri.GetPixel(y + 1, x - 1).R * GY[0, 2] +
                            gri.GetPixel(y - 1, x).R * GY[1, 0] +
                            gri.GetPixel(y, x).R * GY[1, 1] +
                            gri.GetPixel(y + 1, x).R * GY[1, 2] +
                            gri.GetPixel(y - 1, x + 1).R * GY[2, 0] +
                            gri.GetPixel(y, x + 1).R * GY[2, 1] +
                            gri.GetPixel(y + 1, x + 1).R * GY[2, 2];

                        gradient = (int)(Math.Abs(valx) + Math.Abs(valy)); //Mutlak değerini buluyoruz.
                        
                        if (gradient < 0) gradient = 0;
                        if (gradient > 255) gradient = 255;

                        //Bulduğumuz değeri x,y pikseline yükleyeceğiz.
                        renk1 = Color.FromArgb(gradient, gradient, gradient);
                        CikisResmi.SetPixel(y, x,renk1);
                    }
                }
                    
            }
            pictureBox1.Image = CikisResmi;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                Bulaniklastirma();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                Keskinlestirme();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                Ortanca();
            }
            if (comboBox1.SelectedIndex == 3)
            {
                Laplace();
            }
            if (comboBox1.SelectedIndex == 4)
            {
                KenarBulma();
            }
        }
    }
}

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
    public partial class Form2 : Form
    {
        public void ResimDegistir(Image resim)
        {
            pictureBox2.Image = resim;
        }
         private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox2.Image;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ResimDegistir(pictureBox1.Image);
            form3.Show();
            this.Hide();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
             Form1 form1 = new Form1();
             form1.ResimDegistir(pictureBox1.Image);
             form1.Show();
             this.Hide();
        }


        public void GriyeCevirme()
        {
            Bitmap resim = new Bitmap(pictureBox2.Image);   //Burda resmimizi bir bitmap ortamına alıyoruz
            for (int i = 0; i < resim.Height - 1; i++)                            // ardından enlem ve boylam şeklinde tarama yapıcağımız için resmin boyuna göre bir döngü oluşturuyoruz
            {
                for (int j = 0; j < resim.Width - 1; j++)                           // birde enine döngü oluşturuyoruz
                {
                    int deger = (resim.GetPixel(j, i).R + resim.GetPixel(j, i).G + resim.GetPixel(j, i).B) / 3;  // ardından yukarıda da bahsettiğim pikseldeki RGB değerinin aritmatik ortalamasını alıyoruz ve deger değişkenine atıyoruz
                    Color renk;
                    renk = Color.FromArgb(deger, deger, deger);  //burda ise bir üst satırda oluşturduğumuz renk değişkeninin RGB değerlerine aritmetik ortalamasını aldırdığımız yeni rengi veriyoruz
                    resim.SetPixel(j, i, renk);     //ve pikselimizi bitmapimizin j boylamında i enlemindeki noktaya yerleştiriyoruz
                }
            }
            pictureBox1.Image = resim;         // ve resmimizi picturebox1 nesnemize ekliyoruz

        }
        public void Buyultme()
        {
            Color OkunanRenk, DonusenRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox2.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int x2 = 0, y2 = 0; //Çıkış resminin x ve y'si olacak.
            int BuyultmeKatsayisi = 1;
            for (int x1 = 0; x1 > ResimGenisligi; x1 = x1 + BuyultmeKatsayisi)
            {
                y2 = 0;
                for (int y1 = 0; y1 > ResimYuksekligi; y1 = y1 + BuyultmeKatsayisi)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);
                    DonusenRenk = OkunanRenk;
                    CikisResmi.SetPixel(x2, y2, DonusenRenk);
                    y2++;
                }
                x2++;
            }
            pictureBox1.Image = CikisResmi;
        }
        public void Kucultme()
        {
            Color OkunanRenk, DonusenRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox2.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int x2 = 0, y2 = 0; //Çıkış resminin x ve y'si olacak.
            int KucultmeKatsayisi = 2;
            for (int x1 = 0; x1 < ResimGenisligi; x1 = x1 + KucultmeKatsayisi)
            {
                y2 = 0;
                for (int y1 = 0; y1 < ResimYuksekligi; y1 = y1 + KucultmeKatsayisi)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);
                    DonusenRenk = OkunanRenk;
                    CikisResmi.SetPixel(x2, y2, DonusenRenk);
                    y2++;
                }
                x2++;
            }
            pictureBox1.Image = CikisResmi;

        }
        public void YenidenBoyutlandırma()
        {
            Bitmap resimBoyutlandır = new Bitmap(pictureBox2.Image, 50, 50); // Yeniden boyutlandırmak için Bitmap sınıfı kullanılır.Picturebox da yüklü olan resim 200'e 50 boyutunda yeniden boyutlandırılıyor.
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.Image = resimBoyutlandır;
        }
        public Form2()
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
       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                GriyeCevirme();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                Kucultme();
               // Buyultme();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                YenidenBoyutlandırma();
            }
            if (comboBox1.SelectedIndex == 3)
            {

            }
            if (comboBox1.SelectedIndex == 4)
            {
                
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}

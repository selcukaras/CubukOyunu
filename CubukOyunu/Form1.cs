using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CubukOyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int LR, tick, carpma, puan;
        private int[] kutuHareket;
        private int[] kutuCarpan;
        private PictureBox[] kutular;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                LR = 2;
            }
            else if (e.KeyCode == Keys.Left)
            {
                LR = 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (LR == 1)
            {
                CubukSola();
            }
            else if (LR == 2)
            {
                CubukSaga();
            }
            if (tick == 40)
            {
                KutuGonder();
                bool gecti = KutularGectimi();
                if (gecti)
                {
                    label2.Text = "Kazandınız:))";
                    label2.Visible = true;
                    timer1.Enabled = false;
                }
                tick = 0;
            }
            int i = 0;
            while (i < 10)
            {
                if (kutuHareket[i] == 1)
                {
                    kutular[i].Top += 10;
                }
                i++;
            }
            Carpma();
            tick++;
        }

        private bool Carpma()
        {
            int i = 0;
            Rectangle cubuk = new Rectangle(boxCubuk.Location.X, boxCubuk.Location.Y, boxCubuk.Width, boxCubuk.Height);
            
            while (i < 10)
            {
                Rectangle kutu = new Rectangle(kutular[i].Location.X, kutular[i].Location.Y, kutular[i].Width, kutular[i].Height);
                if (kutuCarpan[i]==0 && !Rectangle.Intersect(cubuk, kutu).IsEmpty)
                {
                    kutuCarpan[i] = 1; carpma++;
                    label1.Text = "Çarpma: " + carpma.ToString();
                    if (carpma==4)
                    {
                        label2.Visible = true;
                        timer1.Enabled = false;
                    }
                }
                if (kutuCarpan[i]==0 && kutu.Top>cubuk.Bottom)
                {
                    puan += 10;
                    label3.Text = "Puan: " + puan.ToString();
                    kutuCarpan[i] = 2;
                }
                //int kl = kutular[i].Left, kr = kutular[i].Right, ku = kutular[i].Top, kd = kutular[i].Bottom;
                //int cl = boxCubuk.Left, cr = boxCubuk.Right, cu = boxCubuk.Top, cd = boxCubuk.Bottom;
                //bool kutu_sol_dahil = kl < cr && kl > cl, 
                //    kutu_sag_dahil = kr < cr && kr > cl,
                //    cubuk_ust_dahil = kd > cu && ku < cu,
                //    cubuk_alt_dahil = kd > cd && ku < cu;
                //if (kutuCarpan[i] == 0 && (kutu_sag_dahil || kutu_sol_dahil) && (cubuk_ust_dahil || cubuk_alt_dahil))
                //{
                //    kutuCarpan[i] = 1; carpma++;
                //}
                i++;
            }
            return false;
        }

        private bool KutularGectimi()
        {
            bool ret=true;
            foreach (var kutu in kutular)
            {
                if (kutu.Top<boxCubuk.Bottom)
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            LR = 0;
        }

        private void CubukSola()
        {
            if (boxCubuk.Left > 10) { boxCubuk.Left -= 10; }
        }

        private void CubukSaga()
        {
            if (boxCubuk.Left + boxCubuk.Width < this.Width - 20) { boxCubuk.Left += 10; }
        }

        private void KutuGonder()
        {
            Random rnd = new Random();
            int _r,_j=0;
            while (true)
            {
                _r = rnd.Next(10);
                if (kutuHareket[_r] == 0)
                {
                    kutuHareket[_r] = 1; break;
                }
                _j++;
                if (_j==100)
                {
                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            puan = 0;
            kutuHareket = new int[10];
            kutular = new PictureBox[10];
            kutuCarpan = new int[10];
            kutular[0] = kutu1; kutular[1] = pictureBox1;
            kutular[2] = pictureBox2; kutular[3] = pictureBox3;
            kutular[4] = pictureBox4; kutular[5] = pictureBox5;
            kutular[6] = pictureBox6; kutular[7] = pictureBox7;
            kutular[8] = pictureBox8; kutular[9] = pictureBox9;
            int i = 0; while (i < 10) { kutuHareket[i] = 0; i++; }
        }


    }
}

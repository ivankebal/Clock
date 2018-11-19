using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        Timer t = new Timer();

        int WIDTH = 300, HEIGHT = 300, secHAND = 140, minHAND = 110, hrHAND = 80;

         int cx, cy;
        Bitmap bmp;
        Graphics g;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(WIDTH+1, HEIGHT+1);
            cx = WIDTH / 2;
            cy = HEIGHT / 2;

            this.BackColor = Color.Aqua;

            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();



        }

        private void t_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp);

            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            int [] handCoord = new int[2];

            g.Clear(Color.Aqua);

            g.DrawEllipse(new Pen(Color.Black, 1f), 0,0, WIDTH,HEIGHT );
            g.DrawString("12", new Font ("Arial", 12), Brushes.Black, new PointF(140,2) );
            g.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(286, 140));
            
            g.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(142, 282));
            g.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, 140));

            handCoord = msCoord(ss, secHAND);
            g.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = msCoord(mm, minHAND);
            g.DrawLine(new Pen(Color.Orange, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = hrCoord(hh%12, mm, hrHAND);
            g.DrawLine(new Pen(Color.Fuchsia, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            pictureBox1.Image = bmp;

            this.Text = "Clock - " + hh + ":" + mm + ":" + ss;

            g.Dispose();

        }

        private int[] msCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val *= 6;
            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int) (hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int) (hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int) (hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int) (hlen * Math.Cos(Math.PI * val / 180));
            }

            return coord;
        }

        private int[] hrCoord(int hval, int mval, int hlen)
            {
                int[] coord = new int[2];

                int val = (int) ((hval * 30) + (mval * 0.5));
                

                if (val >= 0 && val <= 180)
                {
                    coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                    coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
                }
                else
                {
                    coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                    coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
                }

                return coord;
            }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

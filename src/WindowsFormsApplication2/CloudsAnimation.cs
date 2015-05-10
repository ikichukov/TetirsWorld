using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public class CloudsAnimation
    {
        public Graphics clouds;
        public Image image;
        public float y;
        public Timer timer1;

        public CloudsAnimation(Graphics g, Image i)
        {
            clouds = g;
            image = i;
            y = (float)-0.5;
            timer1 = new Timer();
            timer1.Interval = 100;
            timer1.Tick += new System.EventHandler(this.timer1_Tick);
            timer1.Start();
        }

        private void timer1_Tick (object sender, EventArgs e)
        {
            y += (float)0.5;
            clouds.DrawImage(image, y, 0);
            clouds.DrawImage(image, y - image.Width - 25, 0);
            if (y >= image.Width) y = (float)(y - image.Width - 25);
        }
    }
}

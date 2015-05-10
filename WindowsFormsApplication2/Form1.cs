using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public CloudsAnimation animation;
        public Form1()
        {
            InitializeComponent();
            animation = new CloudsAnimation(this.CreateGraphics(), Properties.Resources.clouds);
            this.DoubleBuffered = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SinglePlayer sp = new SinglePlayer();
            sp.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TwoPlayer tp = new TwoPlayer();
            tp.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CustomMode cm = new CustomMode();
            cm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

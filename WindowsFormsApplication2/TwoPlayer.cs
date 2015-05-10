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
    public partial class TwoPlayer : Form
    {
        public Game pl1Game;
        public Game pl2Game;
        public CloudsAnimation animation;
        public TwoPlayer()
        {
            InitializeComponent();
            //timer1.Interval = pl2Game.speed;
            //timer1.Start();
            this.DoubleBuffered = true;
            StartGame();
            animation = new CloudsAnimation(this.CreateGraphics(), Properties.Resources.cloudsBig);
        }

        public void StartGame()
        {
            Label[] labels = new Label[3];
            (labels[0] = label5).Text = "1" ;
            (labels[1] = label4).Text = "0";
            (labels[2] = label3).Text = "0000";
            pl2Game = new Game(pictureBox2, pictureBox4, labels);
            Label[] labelsx = new Label[3];
            (labelsx[0] = labelLevel).Text = "1";
            (labelsx[1] = labelLines).Text = "0";
            (labelsx[2] = labelPoints).Text = "0000";
            pl1Game = new Game(pictureBox1, pictureBox3, labelsx);
            timer2.Start();
            //timer1.Start();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (pl1Game == null || pl2Game == null) return false;
            if (keyData == Keys.Left)
            {
                pl1Game.MoveLeft();
                return true;
            }
            if (keyData == Keys.Right)
            {
                pl1Game.MoveRight();
                return true;
            }
            if (keyData == Keys.Down)
            {
                pl1Game.MoveDown(pl1Game.currentShape);
                return true;
            }
            if (keyData == Keys.Up)
            {
                pl1Game.Rotate();
                return true;
            }
            if (keyData == Keys.Space)
            {
                pl1Game.FastDrop();
            }
            ////pl2
            if (keyData == Keys.A)
            {
                pl2Game.MoveLeft();
                return true;
            }
            if (keyData == Keys.D)
            {
                pl2Game.MoveRight();
                return true;
            }
            if (keyData == Keys.S)
            {
                pl2Game.MoveDown(pl2Game.currentShape);
                return true;
            }
            if (keyData == Keys.W)
            {
                pl2Game.Rotate();
                return true;
            }
            if (keyData == Keys.J)
            {
                pl2Game.FastDrop();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void TwoPlayer_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer2.Stop();
            animation.timer1.Stop();
            pl1Game.timer1.Stop();
            pl2Game.timer1.Stop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (pl1Game.started == false || pl2Game.started == false)
            {
                timer2.Stop();

                pl1Game.timer1.Stop();
                pl2Game.timer1.Stop();

                pl1Game.currentShape = null;
                pl2Game.currentShape = null;

                if (System.Windows.Forms.DialogResult.Yes == MessageBox.Show("Нова игра?", "Крај", MessageBoxButtons.YesNo))
                {
                    StartGame();
                }
                else
                {
                    TwoPlayer_FormClosed(null, null);
                    this.Hide();
                }
            }
        }
    }
}

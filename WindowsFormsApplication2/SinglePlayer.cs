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
    public partial class SinglePlayer : Form
    {
        public Game game;
        public CloudsAnimation animation;
        
        public SinglePlayer()
        {
            InitializeComponent();
            animation = new CloudsAnimation(this.CreateGraphics(), Properties.Resources.clouds);
            this.DoubleBuffered = true;
            startGame();
            timer1.Start();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (game == null) return false;
            if (keyData == Keys.Left)
            {
                game.MoveLeft();
                return true;
            }
            if (keyData == Keys.Right)
            {
                game.MoveRight();
                return true;
            }
            if (keyData == Keys.Down)
            {
                game.MoveDown(game.currentShape);
                return true;
            }
            if (keyData == Keys.Up)
            {
                game.Rotate();
                return true;
            }
            if (keyData == Keys.Space)
            {
                game.FastDrop();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void startGame()
        {
            Label[] labels = new Label[3];
            (labels[0] = labelLevel).Text = "1";
            (labels[1] = labelLines).Text = "0";
            (labels[2] = labelPoints).Text = "0000";
            game = new Game(pictureBox1, pictureBox2, labels);
            timer1.Start();
        }

        private void SinglePlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            animation.timer1.Stop();
            game.timer1.Stop();
            game.timer2.Stop();
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (game.started == false)
            {
                timer1.Stop();
                if (System.Windows.Forms.DialogResult.Yes == MessageBox.Show("Нова игра?", "Крај", MessageBoxButtons.YesNo))
                {
                    startGame();
                }
                else
                {
                    SinglePlayer_FormClosing(null, null);
                    this.Hide();
                }

            }
        }
    }
}

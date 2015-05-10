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
    public partial class CustomMode : Form
    {
        public Game game;
        public CloudsAnimation animation;
        public int seconds;
        
        public CustomMode()
        {
            InitializeComponent();
            animation = new CloudsAnimation(this.CreateGraphics(), Properties.Resources.clouds);
            this.DoubleBuffered = true;
            startGame();
            seconds = 60;
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
            labels[0] = null;
            (labels[1] = labelLines).Text = "0";
            (labels[2] = labelPoints).Text = "0000";
            game = new Game(pictureBox1, pictureBox2, labels);
            game.timer1.Interval = 100;
            timer1.Start();
            game.level = 13;
            seconds = 60;
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            --seconds;
            labelTime.Text = String.Format("{0:00}:{1:00}", seconds / 60, seconds % 60);
            if (seconds == 0 || game.started == false)
            {
                timer1.Stop();
                game.timer1.Stop();
                game.timer2.Stop();
                game.currentShape = null;
                if (System.Windows.Forms.DialogResult.Yes == MessageBox.Show("Нова игра?", "Крај", MessageBoxButtons.YesNo))
                {
                    labelTime.Text = "01:00";
                    startGame();
                }
                else
                {
                    CustomMode_FormClosed(null, null);
                    this.Hide();
                }
            }
        }

        private void CustomMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            animation.timer1.Stop();
            game.timer1.Stop();
            game.timer2.Stop();
        }
    }
}

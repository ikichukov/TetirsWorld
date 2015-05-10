using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
//Today
namespace WindowsFormsApplication2
{
    public class Game
    {
        public static Color BACKGROUND_COLOR = Color.FromArgb(181, 215, 240);
        public static int NUMBER_ROWS = 19;
        public static int NUMBER_COLUMNS = 10;
        public static int BLOCK_SIDE_LENGTH = 20;
        public static int INITIAL_SPEED = 400;
        public int speed, level, points, deletedRows, toggleTicks;
        public Block[,] matrix;
        public Graphics g1, g2;
        public Shape currentShape, shade;
        public int next;
        public List<int> RowsToDelete;
        public Timer timer1, timer2;
        public Form window;
        public PictureBox pb1, pb2;
        public Label[] labels;
        public bool started;

        public enum Piece
        {
            OPIECE, IPIECE, TPIECE, JPIECE, LPIECE, ZPIECE, SPIECE
        }
        public Game(PictureBox pictureBox1, PictureBox pictureBox2, Label[] labels)
        {
            Random random = new Random();
            next = random.Next(7);
            this.labels = labels;
            pb1 = pictureBox1;
            pb2 = pictureBox2;
            pictureBox1.Width =  NUMBER_COLUMNS * BLOCK_SIDE_LENGTH;
            pictureBox1.Height = NUMBER_ROWS * BLOCK_SIDE_LENGTH;
            g1 = pictureBox1.CreateGraphics();
            pictureBox2.Width = 6 * 17;
            pictureBox2.Height = 6 * 17;
            g2 = pictureBox2.CreateGraphics();
            matrix = new Block[NUMBER_ROWS, NUMBER_COLUMNS];
            currentShape = null;
            RowsToDelete = new List<int>();
            Empty();
            level = 1;
            speed = INITIAL_SPEED;
            timer1 = new Timer();
            timer1.Interval = speed;
            timer1.Tick += new System.EventHandler(this.timer1_Tick);
            timer1.Enabled = true;
            timer2 = new Timer();
            timer2.Interval = 80;
            timer2.Tick += new System.EventHandler(this.timer2_Tick);
            toggleTicks = 0;
            points = 0;
            deletedRows = 0;
            started = true;
        }

        // го бои секое делче од матрицата со позадинската боја
        public void Empty()
        {
            //g1 = pb1.CreateGraphics();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = new Block(BACKGROUND_COLOR, i, j);
                    DrawAt(i, j);
                }
            }
            currentShape = null;
            g2.Clear(BACKGROUND_COLOR);
        }

        //ја исцртува матрицата со тековните податоци
        public void Draw()
        {
            //g1.Clear(BACKGROUND_COLOR);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j].taken)
                    {
                        DrawAt(i, j, matrix[i, j].Color);
                    }
                    else DrawAt(i, j);
                }
            }
        }

        //го бои блокот на позиција [i, j] со позадинската боја
        public void DrawAt(int x, int y)
        {
            Brush brush = new SolidBrush(matrix[x, y].Color);
            this.g1.FillRectangle(brush, y * BLOCK_SIDE_LENGTH, x * BLOCK_SIDE_LENGTH, BLOCK_SIDE_LENGTH, BLOCK_SIDE_LENGTH);
            matrix[x, y].Color = BACKGROUND_COLOR;
            matrix[x, y].taken = false;
            brush.Dispose();
        }

        //го бои блокот на позиција [i, j] со бојата дадена како аргумент
        public void DrawAt(int x, int y, Color color)
        {
            Brush tmpbr = new SolidBrush(Color.Transparent);
            Brush brush = new SolidBrush(color);
            g1.FillRectangle(tmpbr, y * BLOCK_SIDE_LENGTH, x * BLOCK_SIDE_LENGTH, BLOCK_SIDE_LENGTH, BLOCK_SIDE_LENGTH);
            g1.FillRectangle(brush, (float) (y * BLOCK_SIDE_LENGTH + 0.5), (float) (x * BLOCK_SIDE_LENGTH + 0.5), BLOCK_SIDE_LENGTH - 1, BLOCK_SIDE_LENGTH - 1);
            matrix[x, y].Color = color;
            brush.Dispose();
            tmpbr.Dispose();
        }

        //ја исцртува следната форма
        public void DrawNext(int i)
        {
            Shape shape = null;
            switch (i)
            {
                case 0:
                    shape = new OPiece(6);
                    break;
                case 1:
                    shape = new IPiece(6);
                    break;
                case 2:
                    shape = new TPiece(6);
                    break;
                case 3:
                    shape = new JPiece(6);
                    break;
                case 4:
                    shape = new LPiece(6);
                    break;
                case 5:
                    shape = new ZPiece(6);
                    break;
                case 6:
                    shape = new SPiece(6);
                    break;
            }
            g2.Clear(BACKGROUND_COLOR);
            Brush brush = new SolidBrush(shape.color);
            foreach (IndexKeeper index in shape.positions)
            {
                g2.FillRectangle(brush, (float) (index.Y * 17 + 0.5), (float) ((index.X + 2) * 17 - 0.5), 16, 16);
            }
            brush.Dispose();

        }

        //исцртува нова форма
        public void AddPiece()
        {
            switch (next)
            {
                case 0:
                    currentShape = new OPiece(NUMBER_COLUMNS);
                    shade = new OPiece(NUMBER_COLUMNS);
                    break;
                case 1:
                    currentShape = new IPiece(NUMBER_COLUMNS);
                    shade = new OPiece(NUMBER_COLUMNS);
                    break;
                case 2:
                    currentShape = new TPiece(NUMBER_COLUMNS);
                    shade = new OPiece(NUMBER_COLUMNS);
                    break;
                case 3:
                    currentShape = new JPiece(NUMBER_COLUMNS);
                    shade = new OPiece(NUMBER_COLUMNS);
                    break;
                case 4:
                    currentShape = new LPiece(NUMBER_COLUMNS);
                    shade = new OPiece(NUMBER_COLUMNS);
                    break;
                case 5:
                    currentShape = new ZPiece(NUMBER_COLUMNS);
                    shade = new OPiece(NUMBER_COLUMNS);
                    break;
                case 6:
                    currentShape = new SPiece(NUMBER_COLUMNS);
                    shade = new OPiece(NUMBER_COLUMNS);
                    break;
            }

            foreach (IndexKeeper index in currentShape.positions)
            {
                matrix[index.X, index.Y].Color = currentShape.color;
                matrix[index.X, index.Y].taken = true;
            }

            Draw();

            Random random = new Random();
            next = random.Next(7);

            DrawNext(next);
        }
        
        //ја придвижува форматa надолу
        public void MoveDown(Shape shape)
        {
            if (shape == null) return;
            if (!CanGoDown(shape)) return;
            foreach (IndexKeeper index in shape.positions)
            {
                matrix[index.X, index.Y].taken = false;
                matrix[index.X, index.Y].Color = BACKGROUND_COLOR;
                index.X++;
                matrix[index.X, index.Y].taken = true;
                matrix[index.X, index.Y].Color = shape.color;
            }
            Draw();
        }

        //ја придвижува формата налево
        public void MoveLeft()
        {
            if (currentShape == null) return;
            if (!CanGoLeft()) return;

            for (int i = currentShape.positions.Count() - 1; i >= 0; i--)
            {
                IndexKeeper index = currentShape.positions[i];
                matrix[index.X, index.Y].taken = false;
                matrix[index.X, index.Y].Color = BACKGROUND_COLOR;
                currentShape.positions[i].Y--;
                matrix[index.X, index.Y].taken = true;
                matrix[index.X, index.Y].Color = currentShape.color;
            }
            Draw();
        }

        //ја придвижува формата надесно
        public void MoveRight()
        {
            if (currentShape == null) return;
            if (!CanGoRight()) return;
            foreach (IndexKeeper index in currentShape.positions)
            {
                matrix[index.X, index.Y].taken = false;
                matrix[index.X, index.Y].Color = BACKGROUND_COLOR;
                index.Y++;
                matrix[index.X, index.Y].taken = true;
                matrix[index.X, index.Y].Color = currentShape.color;
            }
            Draw();
        }

        //проверува дали формата може да се движи надолу
        public bool CanGoDown(Shape shape)
        {
            if (currentShape == null) return false;
            foreach (IndexKeeper index in shape.positions)
            {
                if (index.X == NUMBER_ROWS - 1) return false;
                if (matrix[index.X + 1, index.Y].taken && !IsInCurrentShape(new IndexKeeper(index.X + 1, index.Y))) return false;
            }

            return true;
        }

        //проверува дали формата може да се движи надесно
        public bool CanGoRight()
        {
            foreach (IndexKeeper index in currentShape.positions)
            {
                if (index.Y == NUMBER_COLUMNS - 1) return false;
                if (matrix[index.X, index.Y+1].taken && !IsInCurrentShape(new IndexKeeper(index.X, index.Y+1))) return false;
            }

            return true;
        }

        //проверува дали формата може да се движи налево
        public bool CanGoLeft()
        {
            foreach (IndexKeeper index in currentShape.positions)
            {
                if (index.Y == 0) return false;
                if (matrix[index.X, index.Y - 1].taken && !IsInCurrentShape(new IndexKeeper(index.X, index.Y - 1))) return false;
            }

            return true;
        }

        //проверува дали индексот [i, j] е во тековната форма
        public bool IsInCurrentShape(IndexKeeper index)
        {
            foreach (IndexKeeper i in currentShape.positions)
            {
                if (index.Equals(i)) return true;
            }

            return false;
        }

        //ја ротира тековната форма
        public void Rotate()
        {
            if (currentShape == null) return;
            IndexKeeper[] rotationPositions = currentShape.GetRotationPositions();
            if (CanRotate(rotationPositions))
            {
                foreach (IndexKeeper index in currentShape.positions){
                    matrix[index.X, index.Y].taken = false;
                    matrix[index.X, index.Y].Color = BACKGROUND_COLOR;
                }

                foreach (IndexKeeper index in rotationPositions)
                {
                    matrix[index.X, index.Y].taken = true;
                    matrix[index.X, index.Y].Color = currentShape.color;
                }

                currentShape.positions = rotationPositions;
                currentShape.SetState();

                Draw();
            }
        }

        //проверува дали тековната форма може да ротира
        public bool CanRotate(IndexKeeper[] indices)
        {
            if (indices == null) return false;
            foreach (IndexKeeper index in indices)
            {
                if (index.X < 0 || index.X >= NUMBER_ROWS) return false;
                if (index.Y < 0 || index.Y >= NUMBER_COLUMNS) return false;
                if (matrix[index.X, index.Y].taken && !IsInCurrentShape(index)) return false;
            }

            return true;
        }

        //проверува дали i-тиот ред е празен
        public bool IsRowFull(int i)
        {
            for (int k = 0; k < NUMBER_COLUMNS; k++)
            {
                if (!matrix[i, k].taken) return false;
            }

            return true;
        }

        //ги брише (со поместување) сите полни редови
        public void DeleteRows()
        {
            if (RowsToDelete.Count == 0) return;

            for (int i = RowsToDelete.Count - 1; i >= 0; --i)
            {
                MoveRowsDown(RowsToDelete[i]);
            }

            AddPoints();

            Draw();

            RowsToDelete = new List<int>();
        }

        //ги поместува сите редови над k-тиот за еден надолу
        public void MoveRowsDown(int k)
        {
            for (int i = k; i >= 1; i--)
            {
                for (int j = 0; j < NUMBER_COLUMNS; j++)
                {
                    matrix[i, j].Color = matrix[i - 1, j].Color;
                    matrix[i, j].taken = matrix[i - 1, j].taken;
                }
            }

            for (int j = 0; j < NUMBER_COLUMNS; j++)
            {
                matrix[0, j].Color = BACKGROUND_COLOR;
                matrix[0, j].taken = false;
            }
        }

        //КОПЧЕ ЗА ПРАЗНЕЊЕ
        private void button1_Click(object sender, EventArgs e)
        {
            Empty();
            AddPiece();
            timer1.Enabled = true;
            level = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentShape == null) AddPiece();
            if (CanGoDown(currentShape)) MoveDown(currentShape);
            else
            {
                FindFullRows();
                if (RowsToDelete.Count != 0)
                {
                    timer1.Enabled = false;
                    currentShape = null;
                    toggleTicks = 0;
                    timer2.Start(); //= true;
                    return;
                }
                AddPiece();
                if (!CanGoDown(currentShape))
                {
                    timer1.Stop();
                    //MessageBox.Show("kraj");
                    started = false;
                    //MessageBox.Show("Крај!");
                    //button1.Enabled = true;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show("Here");
            ++toggleTicks;
            if (toggleTicks == 7)
            {
                timer2.Stop(); //Enabled = false;
                DeleteRows();
                timer1.Enabled = true;
            }
            else
            {
                ToggleRowsColor();
            }
        }

        //ги наоѓа сите полни редови и ги сместува во листата на редови за бришење
        public void FindFullRows()
        {
            //timer1.Enabled = false;
            List<int> RowsToCheck = new List<int>();
            if (currentShape == null) return;
            foreach (IndexKeeper index in currentShape.positions)
            {
                if (!RowsToCheck.Contains(index.X)) RowsToCheck.Add(index.X);
            }
            foreach (int i in RowsToCheck)
            {
                if (IsRowFull(i)) RowsToDelete.Add(i);
            }
            //if (RowsToDelete.Count != 0) DeleteRows();
            //timer1.Enabled = true;
        }

        //наеднаш ја спушта формата
        public void FastDrop()
        {
            while (CanGoDown(currentShape)) MoveDown(currentShape);
            Draw();
        }

        //ги додава добиените поени после бришењето
        public void AddPoints()
        {
            int factor, rows;
            rows = RowsToDelete.Count();
            if (rows == 1) factor = 4;
            else if (rows == 2) factor = 10;
            else if (rows == 3) factor = 30;
            else factor = 120;

            //смени го бројот на редови
            deletedRows += rows;
            labels[1].Text = deletedRows.ToString();
            //смени го нивото
            if (labels[0] != null)
            {
                level = (int)deletedRows / 10;
                level++;
                labels[0].Text = level.ToString();
            }
            if (timer1.Interval > 100) timer1.Interval = INITIAL_SPEED - ((level-1) * 30);
            //смени ги поените
            points += (factor * level);
            labels[2].Text = string.Format("{0:0000}", points);
        }

        //ја завршува играта
        //public void EndGame()
        //{
            //MessageBox.Show("That's all folks");
            //started = false;
        //}
        public void ToggleRowsColor()
        {
            Brush brush = new SolidBrush(Color.White);
            foreach (int i in RowsToDelete)
            {
                for (int j = 0; j < NUMBER_COLUMNS; j++)
                {
                    if (matrix[i, j].Color != Color.White) matrix[i, j].Color = Color.White;
                    else if (matrix[i, j].Color != BACKGROUND_COLOR) matrix[i, j].Color = BACKGROUND_COLOR;
                }
            }

            Draw();
        }
    }
}

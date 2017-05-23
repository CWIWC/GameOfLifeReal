using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        universe univ = new universe(50, 50);
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        int generations = 0;

        public Form1()
        {
            InitializeComponent();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gpUniv_Paint(object sender, PaintEventArgs e)
        {
            int width = gpUniv.ClientSize.Width / univ.Max_X;
            int height = gpUniv.ClientSize.Height / univ.Max_X;

            Pen gridPen = new Pen(gridColor, 1);
            Brush cellBrush = new SolidBrush(cellColor);

            for (int y = 0; y < univ.Max_Y; y++)
            {
                for (int x = 0; x < univ.Max_X; x++)
                {
                    // RectangleF
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * width;
                    cellRect.Y = y * height;
                    cellRect.Width = width;
                    cellRect.Height = height;


                    if (univ.cells[x, y].State == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }
            }


            cellBrush.Dispose();
            gridPen.Dispose();
        }

        private void clickclickmerp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // sweet memes
                int width = gpUniv.ClientSize.Width / univ.Max_X;
                int height = gpUniv.ClientSize.Height / univ.Max_Y;

                int x = e.X / width;
                int y = e.Y / height;

                univ.cells[x, y].State = !univ.cells[x, y].State;

                gpUniv.Invalidate();
            }
        }

       

      
        public class universe
        {
            bool[,] nextCellState;
            public int Max_X { get; }
            public int Max_Y { get; }

            public cell[,] cells;
            public universe(int x, int y)
            {
                Max_X = x;
                Max_Y = y;
                nextCellState = new bool[Max_X, Max_Y];
                cells = new cell[Max_X, Max_Y];
                for (int mx = 0; mx < Max_X; mx++)
                {
                    for (int my = 0; my < Max_Y; my++)
                    {
                        cells[mx, my] = new cell();
                        cells[mx, my].State = false;
                    }
                }

            }

            public void nextGen()
            {
                //Living cells with less than 2 living neighbors die in the next generation.
                //Living cells with more than 3 living neighbors die in the next generation.
                //Living cells with 2 or 3 living neighbors live in the next generation.
                //Dead cells with exactly 3 living neighbors live in thle next generation.
                // Loop through every cell on the grid.
                for (int i = 0; i < Max_X; i++)
                {
                    for (int j = 0; j < Max_Y; j++)
                    {
                        // Check the cell's current state, and count its living neighbors.
                        bool living = cells[i, j].State;
                        int count = GetLiving(i, j);
                        bool result = false;

                        // Apply the rules and set the next state.
                        if (living && count < 2)
                            result = false;
                        if (living && (count == 2 || count == 3))
                            result = true;
                        if (living && count > 3)
                            result = false;
                        if (!living && count == 3)
                            result = true;

                        nextCellState[i, j] = result;
                    }
                }
                SetNextState();
            }

            public void SetNextState()
            {
                for (int i = 0; i < Max_X; i++)
                    for (int j = 0; j < Max_Y; j++)
                        cells[i, j].State = nextCellState[i, j];
            }



            public int GetLiving(int x, int y)
            {
                int count = 0;

                // Check right.
                if (x != Max_X - 1)
                    if (cells[x + 1, y].State)
                        count++;

                // Check bottom right.
                if (x != Max_X - 1 && y != Max_Y - 1)
                    if (cells[x + 1, y + 1].State)
                        count++;

                // Check bottom.
                if (y != Max_Y - 1)
                    if (cells[x, y + 1].State)
                        count++;

                // Check left.
                if (x != 0 && y != Max_Y - 1)
                    if (cells[x - 1, y + 1].State)
                        count++;

                // Check left.
                if (x != 0)
                    if (cells[x - 1, y].State)
                        count++;

                // Check left.
                if (x != 0 && y != 0)
                    if (cells[x - 1, y - 1].State)
                        count++;

                // Check top.
                if (y != 0)
                    if (cells[x, y - 1].State)
                        count++;

                // Check right.
                if (x != Max_X - 1 && y != 0)
                    if (cells[x + 1, y - 1].State)
                        count++;

                return count;
            }
        }
        public class cell
        {

            public bool State { get; set; }

            public cell()
            {
                State = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 20;
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            univ.nextGen();
            generations++;
            gpUniv.Invalidate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            univ = new universe(50,50);
            generations = 0;
            gpUniv.Invalidate();
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            univ.nextGen();
            generations++;
            gpUniv.Invalidate();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            univ = new universe(50, 50);
            generations = 0;
            gpUniv.Invalidate();
            timer1.Stop();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            univ.nextGen();
            generations++;
            gpUniv.Invalidate();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGit
{
    public partial class Form1 : Form
    {
        SolidBrush black_brush;
        SolidBrush green_brush;
        SolidBrush white_brush;
        Point[] snake;
        Point apple;
        Random r;
        int len = 1;
        int width, height;
        public Form1()
        {
            r = new Random();
            snake = new Point[10000];
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            width = pictureBox1.Width / 10;
            height = pictureBox1.Height / 10;
            snake[0].X = width / 2;
            snake[0].Y = height / 2;
            white_brush = new SolidBrush(Color.White);
            green_brush = new SolidBrush(Color.Green);
            black_brush = new SolidBrush(Color.Black);

            apple.X = r.Next(1, width);
            apple.Y = r.Next(1, height);
            // g.FillEllipse(green_brush, apple.X * 10, apple.Y * 10, 10, 10);
        }
        string direction = "up";

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);

            g.FillRectangle(white_brush, 0, 0, pictureBox1.Width, pictureBox1.Height);
            if (len > 4)
            for (int i = 1; i < len; i++)
                for (int j = i + 1; j < len; j++)
                {
                    if (snake[i].X == snake[j].X && snake[i].Y == snake[j].Y)
                        len = 3;
                }

            for (int i = 0; i < len; i++)
            {
                if (snake[i].X < 0) snake[i].X += width;
                if (snake[i].X > width) snake[i].X -= width;
                if (snake[i].Y < 0) snake[i].Y += height;
                if (snake[i].Y > height) snake[i].Y -= height;
                g.FillEllipse(black_brush, snake[i].X * 10, snake[i].Y * 10, 10, 10);
                if (apple.X == snake[i].X && apple.Y == snake[i].Y)
                {
                    apple.X = r.Next(1, width - 1);
                    apple.Y = r.Next(1, height - 1);
                    len++;
                }
            }
            g.FillEllipse(green_brush, apple.X * 10, apple.Y * 10, 10, 10);
            if (direction == "up") snake[0].Y -= 1;
            if (direction == "down") snake[0].Y += 1;
            if (direction == "left") snake[0].X -= 1;
            if (direction == "right") snake[0].X += 1;

            if (len > 10000 - 3)
            {
                len = 10000 - 3;
            }
            for (int i = len; i >= 0; i--)
            {
                snake[i + 1].X = snake[i].X;
                snake[i + 1].Y = snake[i].Y;
            }
            if (len < 4) len++;

            pictureBox1.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                direction = "up";
            }
            if (e.KeyCode == Keys.Down)
            {
                direction = "down";
            }
            if (e.KeyCode == Keys.Left)
            {
                direction = "left";
            }
            if (e.KeyCode == Keys.Right)
            {
                direction = "right";
            }
        }
    }
}

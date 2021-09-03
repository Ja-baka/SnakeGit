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
    // перечисление вместо строки
    public enum Direction : byte
    {
        Up,
        Down,
        Left,
        Right
    }

    public partial class Form : System.Windows.Forms.Form
    {
        private const int SIZE = 30;
        private readonly SolidBrush blackBrush;
        private readonly SolidBrush greenBrush;
        private readonly SolidBrush whiteBrush;
        private readonly Pen blackPen;
        private Point[] snake;
        private Point apple;
        private readonly Random random;
        private int length = 1;
        private readonly int width;
        private readonly int height;
        private Direction direction = Direction.Up;
        // private string direction = "up";
        public Form()
        {
            InitializeComponent();
            random = new Random();
            snake = new Point[10000];
            PictureBox.Image = new Bitmap(PictureBox.Width, PictureBox.Height);
            width = PictureBox.Width / SIZE;
            height = PictureBox.Height / SIZE;
            snake[0].X = width / 2;
            snake[0].Y = height / 2;
            whiteBrush = new SolidBrush(Color.White);
            greenBrush = new SolidBrush(Color.Green);
            blackBrush = new SolidBrush(Color.Black);
            blackPen = new Pen(Color.Black, 3);
            apple.X = random.Next(1, width);
            apple.Y = random.Next(1, height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage(PictureBox.Image);

            graphics.FillRectangle(whiteBrush, 0, 0, PictureBox.Width, PictureBox.Height);
            graphics.DrawRectangle(blackPen, 0, 0, width * SIZE, height * SIZE);
            if (length > 4)
            for (int i = 1; i < length; i++)
                for (int j = i + 1; j < length; j++)
                {
                    if (snake[i].X == snake[j].X && snake[i].Y == snake[j].Y)
                        {
                        length = 3;
                            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

                            wplayer.URL = "oof.mp3";
                            wplayer.controls.play();
                        }
                }

            for (int i = 0; i < length; i++)
            {
                if (snake[i].X < 0) snake[i].X += width;
                if (snake[i].X > width) snake[i].X -= width;
                if (snake[i].Y < 0) snake[i].Y += height;
                if (snake[i].Y > height) snake[i].Y -= height;
                graphics.FillEllipse(blackBrush, snake[i].X * SIZE, snake[i].Y * SIZE, SIZE, SIZE);
                if (apple.X == snake[i].X && apple.Y == snake[i].Y)
                {
                    apple.X = random.Next(1, width - 1);
                    apple.Y = random.Next(1, height - 1);
                    length++;
                    WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

                    wplayer.URL = "eating.mp3";
                    wplayer.controls.play();
                }
            }
            graphics.FillEllipse(greenBrush, apple.X * SIZE, apple.Y * SIZE, SIZE, SIZE);
            if (direction == Direction.Up) snake[0].Y -= 1;
            if (direction == Direction.Down) snake[0].Y += 1;
            if (direction == Direction.Left) snake[0].X -= 1;
            if (direction == Direction.Right) snake[0].X += 1;

            if (length > 10000 - 3)
            {
                length = 10000 - 3;
            }
            for (int i = length; i >= 0; i--)
            {
                snake[i + 1].X = snake[i].X;
                snake[i + 1].Y = snake[i].Y;
            }
            if (length < 4) length++;

            PictureBox.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                direction = Direction.Up;
            }
            if (e.KeyCode == Keys.Down)
            {
                direction = Direction.Down;
            }
            if (e.KeyCode == Keys.Left)
            {
                direction = Direction.Left;
            }
            if (e.KeyCode == Keys.Right)
            {
                direction = Direction.Right;
            }
        }
    }
}

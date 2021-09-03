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
        private readonly Random random;
        private readonly int width;
        private readonly int height;

        private readonly SolidBrush bluekBrush = new SolidBrush(Color.FromArgb(73, 119, 238));
        private readonly SolidBrush redBrush = new SolidBrush(Color.FromArgb(231, 71, 29));
        private readonly SolidBrush greenBrush = new SolidBrush(Color.FromArgb(170, 215, 81));
        private readonly Pen blackPen = new Pen(Color.Black, 3);

        private Point[] snake;
        private Point apple;

        private int length = 3;
        private Direction direction = Direction.Up;

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

            apple.X = random.Next(1, width);
            apple.Y = random.Next(1, height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage(PictureBox.Image);

            graphics.FillRectangle(greenBrush, 0, 0, PictureBox.Width, PictureBox.Height);
            graphics.DrawRectangle(blackPen, 0, 0, width * SIZE, height * SIZE);

            if (length > 4)
            {
                for (int i = 1; i < length; i++)
                {
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
                }
            }

            for (int i = 0; i < length; i++)
            {
                snake[0].X += snake[0].X < 0 ? width : 0;
                snake[0].Y += snake[0].Y < 0 ? height : 0;
                snake[0].X -= snake[0].X >= width ? width : 0;
                snake[0].Y -= snake[0].Y >= height ? height : 0;
                graphics.FillRectangle(bluekBrush, snake[i].X * SIZE, snake[i].Y * SIZE, SIZE, SIZE);
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

            graphics.FillEllipse(redBrush, apple.X * SIZE, apple.Y * SIZE, SIZE, SIZE);
            switch (direction)
            {
                case Direction.Up:
                    snake[0].Y -= 1;
                    break;
                case Direction.Down:
                    snake[0].Y += 1;
                    break;
                case Direction.Left:
                    snake[0].X -= 1;
                    break;
                case Direction.Right:
                    snake[0].X += 1;
                    break;
            }

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
            switch (e.KeyCode)
            {
                case Keys.Up:
                        direction = Direction.Up;
                    break;
                case Keys.Down:
                        direction = Direction.Down;
                    break;
                case Keys.Left:
                        direction = Direction.Left;
                    break;
                case Keys.Right:
                        direction = Direction.Right;
                    break;
            }
            
        }
    }
}

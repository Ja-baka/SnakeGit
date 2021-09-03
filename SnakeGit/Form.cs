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
        private const int SnakeScale = 30;
        private readonly Random random = new Random();
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

        private bool directionChanged = false;

        public Form()
        {
            InitializeComponent();

            width = pictureBox.Width / SnakeScale;
            height = pictureBox.Height / SnakeScale;

            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);

            snake = new Point[width * height];

            snake[0].X = width / 2;
            snake[0].Y = height / 2;

            apple.X = random.Next(1, width);
            apple.Y = random.Next(1, height);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // Рисуем поле
            Graphics graphics = Graphics.FromImage(pictureBox.Image);
            graphics.FillRectangle
            (
                greenBrush, 
                0, 
                0, 
                pictureBox.Width,
                pictureBox.Height
            );
            graphics.DrawRectangle
            (
                blackPen,
                0,
                0,
                width * SnakeScale,
                height * SnakeScale
            );

            // Перемещаем голову змея
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

            // Телепортация
            snake[0].X += snake[0].X < 0 ? width : 0;
            snake[0].Y += snake[0].Y < 0 ? height : 0;
            snake[0].X -= snake[0].X >= width ? width : 0;
            snake[0].Y -= snake[0].Y >= height ? height : 0;
            
            // Достижение максимальной длинны
            if (length >= snake.Length)
            {
                GameOver();
            }

            // Поедание яблока
            if (apple.X == snake[0].X && 
                apple.Y == snake[0].Y)
            {
                apple.X = random.Next(1, width - 1);
                apple.Y = random.Next(1, height - 1);
                length++;
                snake[length - 1] = snake[length - 2]; // Убирает моргающий квадратик в (0; 0)

                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer
                {
                    URL = "eating.mp3"
                };
                wplayer.controls.play();
            }

            // Если змей ест сам себя
            for (int i = 1; i < length; i++)
            {
                if (snake[0].X == snake[i].X &&
                    snake[0].Y == snake[i].Y)
                {
                    GameOver();
                }
            }

            // Двигаем змея
            // Графически
            for (int i = 0; i < length; i++)
            {
                graphics.FillRectangle
                (
                    bluekBrush, 
                    snake[i].X * SnakeScale, 
                    snake[i].Y * SnakeScale, 
                    SnakeScale, 
                    SnakeScale
                );

            }
            // Фактически
            for (int i = length - 2; i >= 0; i--)
            {
                snake[i + 1].X = snake[i].X;
                snake[i + 1].Y = snake[i].Y;
            }

            // Рисуем яблоко
            graphics.FillEllipse
            (
                redBrush, 
                apple.X * SnakeScale, 
                apple.Y * SnakeScale, 
                SnakeScale, 
                SnakeScale
            );

            pictureBox.Invalidate();
            directionChanged = false;
        }

        private void GameOver()
        {
            length = 3;
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer
            {
                URL = "oof.mp3"
            };
            wplayer.controls.play();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: 
                case Keys.Up:
                    if (direction != Direction.Down &&
                        !directionChanged)
                    {
                        direction = Direction.Up;
                        directionChanged = true;
                    }
                    break;
                case Keys.S:
                case Keys.Down:
                    if (direction != Direction.Up &&
                        !directionChanged)
                    {
                        direction = Direction.Down;
                        directionChanged = true;
                    }
                    break;
                case Keys.A:
                case Keys.Left:
                    if (direction != Direction.Right &&
                        !directionChanged)
                    {
                        direction = Direction.Left;
                        directionChanged = true;
                    }
                    break;
                case Keys.D:
                case Keys.Right:
                    if (direction != Direction.Left &&
                        !directionChanged)
                    {
                        direction = Direction.Right;
                        directionChanged = true;
                    }
                    break;
            }
            
        }
    }
}

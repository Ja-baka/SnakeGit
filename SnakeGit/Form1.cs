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
        private SolidBrush blackBrush;
        private SolidBrush greenBrush;
        private SolidBrush whiteBrush;
        private Pen blackPen;
        private Point[] snake;
        private Point apple;
        private Random random;
        private int length = 1;
        private int width;
        private int height;
        private string direction = "up";
        public Form1()
        {
            InitializeComponent();
            random = new Random();
            snake = new Point[10000];
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            width = pictureBox1.Width / 10;
            height = pictureBox1.Height / 10;
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
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);

            graphics.FillRectangle(whiteBrush, 0, 0, pictureBox1.Width, pictureBox1.Height);
            graphics.DrawRectangle(blackPen, 0, 0, width * 10, height * 10);
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
                graphics.FillEllipse(blackBrush, snake[i].X * 10, snake[i].Y * 10, 10, 10);
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
            graphics.FillEllipse(greenBrush, apple.X * 10, apple.Y * 10, 10, 10);
            if (direction == "up") snake[0].Y -= 1;
            if (direction == "down") snake[0].Y += 1;
            if (direction == "left") snake[0].X -= 1;
            if (direction == "right") snake[0].X += 1;

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

            pictureBox1.Invalidate();
        }

        // Второй таймер я не делал потому-что в жепу его
        // Сетку тоже

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

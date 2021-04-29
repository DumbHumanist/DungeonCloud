using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsGDI__Snake
{
    enum direction
    {
        Right,
        Left,
        Up,
        Down
    }
    public partial class Form1 : Form
    {
        List<Segment> snake = new List<Segment>();
        direction dir = direction.Right;
        int foodX;
        int foodY;
        private Theme CurrentTheme = new Theme();
        Timer timer;

        public Form1()
        {
            InitializeComponent();


            StartGame();
            timer = new Timer();
            timer.Tick += GameLoop;
            timer.Interval = 500;
            timer.Start();
            
        }

        private void StartGame()
        {
            string Dir = Environment.CurrentDirectory + @"\Icons";
            this.Icon = new Icon($"{Dir}\\icon.ico");

            string[] currentThemeLoad = File.ReadAllText($"{Dir}\\Themes\\currentTheme.txt").Split(';');
            CurrentTheme = new Theme(currentThemeLoad[0], currentThemeLoad[1], currentThemeLoad[2], currentThemeLoad[3], currentThemeLoad[4], currentThemeLoad[5]);


            snake.Add(new Segment(120, 100));
            snake.Add(new Segment(110, 100));
            snake.Add(new Segment(100, 100));
            
            this.BackColor = CurrentTheme.Background;
            PlaceFood();
        }
        private void PlaceFood()
        {
            Random r = new Random();
            foodX = r.Next(10, 240) / 10 * 10;
            foodY = r.Next(10, 150) / 10 * 10;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void GameLoop(object sender, System.EventArgs e)
        {
            Update();

            Invalidate();
        }

        private void Update()
        {
            if (snake[0].x == foodX && snake[0].y == foodY) 
            {
                Segment newSegment = new Segment(snake[snake.Count-1].x, snake[snake.Count-1].y);
                MoveSnake();
                snake.Add(newSegment);
                PlaceFood();
                if (timer.Interval >= 20)
                {
                    timer.Interval -= 10;
                }
                return;
            }
            MoveSnake();
        }

        public void MoveSnake()
        {
            for (int i = snake.Count-1; i >= 1; i--)
            {
                snake[i].x = snake[i - 1].x;
                snake[i].y = snake[i - 1].y; 
            }
            switch (dir)
            {
                case direction.Right:
                    {
                        snake[0].x += 10;
                        break;
                    }
                case direction.Left:
                    {
                        snake[0].x -= 10;
                        break;
                    }
                case direction.Up:
                    {
                        snake[0].y -= 10;
                        break;
                    }
                case direction.Down:
                    {
                        snake[0].y += 10;
                        break;
                    }
            }
            if (snake[0].x == 250)
                snake[0].x = 10;
            else if (snake[0].x == 0)
                snake[0].x = 240;
            else if (snake[0].y == 160)
                snake[0].y = 10;
            else if (snake[0].y == 0)
                snake[0].y = 150;
            for(int i = 1; i < snake.Count; i++)
            {
                if (snake[0].x == snake[i].x && snake[0].y == snake[i].y)
                {
                    snake.RemoveRange(3, snake.Count-3);
                    timer.Interval = 500;
                }

            }
        }

        private void Draw()
        {
            DrawFood();
            DrawSnake();
        }

        private void DrawSnake()
        {

            foreach (Segment s in snake)
            {
                DrawRect(s.x, s.y, CurrentTheme.Segment);
            }
            DrawRect(snake[0].x, snake[0].y, CurrentTheme.Head);
        }

        private void DrawFood()
        {
            DrawRect(foodX, foodY, CurrentTheme.Food);
        }


        private void DrawRect(int x, int y, Color color)
        {
            CreateGraphics().FillRectangle(new SolidBrush(color), new Rectangle(x, y, 10, 10));
        }

        private void Form1_KeyPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    {
                        if(snake[0].y - 10 != snake[1].y)
                            dir = direction.Up;
                        break;
                    }
                case Keys.Left:
                    {
                        if (snake[0].x - 10 != snake[1].x)
                            dir = direction.Left;
                        break;
                    }
                case Keys.Down:
                    {
                        if (snake[0].y + 10 != snake[1].y)
                            dir = direction.Down;
                        break;
                    }
                case Keys.Right:
                    {
                        if (snake[0].x + 10 != snake[1].x)
                            dir = direction.Right;
                        break;
                    }
            }
        }
    }
}

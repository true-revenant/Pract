using Pract.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pract.classes
{
    public delegate void LogDelegate(string log_str);

    static class Game
    {
        static BufferedGraphicsContext context;
        static int points;
        static BaseObject[] asteroids;
        static BaseObject[] stars;
        static BaseObject[] comets;
        static Bullet bullet;
        //static List<Bullet> bullets;
        static Ship ship;
        static HealthBox healthBox;
        static Timer timer;
        static bool healthBoxIsGenerated = false;

        public static BufferedGraphics buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game() { }

        public static void Init(Form form)
        {
            Graphics g;
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            if (Width >= 1000 || Width < 0 || Height >= 1000 || Height < 0) throw new ArgumentOutOfRangeException();

            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));

            LoadGameScene();
            points = 0;

            timer = new Timer() { Interval = 40 };
            timer.Tick += Timer_OnTick;
            timer.Start();

            form.KeyDown += Form_KeyDown;

            LogAction(DebugLog, "ИГРА НАЧАТА!!");
            LogAction(DebugLog, "*********************************************************************");
            LogAction(LogFile, "ИГРА НАЧАТА!!");
            LogAction(LogFile, "*********************************************************************");
        }

        public static void Draw()
        {
            // рисуем фон космос
            buffer.Graphics.DrawImage(Resources.background_2, new Rectangle(0, 0, Width, Height));
            // рисуем звезды
            foreach (var star in stars) star.Draw();
            // рисуем планету
            buffer.Graphics.DrawImage(Resources.planet, new Rectangle(100, 100, 200, 200));
            // рисуем кометы
            foreach (var comet in comets) comet.Draw();
            // рисуем астероиды
            foreach (var aster in asteroids) aster.Draw();

            ship.Draw();
            
            buffer.Graphics.DrawString($"Points: {points}", 
                new Font(FontFamily.GenericMonospace, 15), Brushes.YellowGreen, new Point(Width - 180, Height - 25));
            buffer.Graphics.DrawString($"Health: {ship.Enegry}",
                new Font(FontFamily.GenericMonospace, 15), Brushes.Red, new Point(Width - 180, Height - 50));

            if (bullet != null) bullet.Draw();
            if (healthBox != null) healthBox.Draw();

            buffer.Render();
        }

        private static void LoadGameScene()
        {
            var rnd = new Random();

            asteroids = new Asteroid[15];
            for (int i = 0; i < asteroids.Length; i++) asteroids[i] = GenerateAsteroid(rnd);

            stars = new Star[60];
            for (int i = 0; i < stars.Length; i++) stars[i] = GenerateStar(rnd);

            comets = new Comet[3];
            for (int i = 0; i < comets.Length; i++) comets[i] = GenerateComet(rnd);

            ship = new Ship(new Point(20, Height / 2), new Point(0, 0), new Size(50, 50));
            ship.MessageOnDeath += Ship_MessageOnDeath;
        }

        public static void Update()
        {
            for (int i = 0; i < asteroids.Length; i++)
            {
                // попадание пули в астероид
                asteroids[i].Update();
                Random rnd = new Random();
                if (bullet != null && asteroids[i].Collision(bullet))
                {
                    LogAction(DebugLog, $"Пуля попала в астероид! координаты попадания = [{bullet.Rect.X}, {bullet.Rect.Y}]");
                    LogAction(LogFile, $"Пуля попала в астероид! координаты попадания = [{bullet.Rect.X}, {bullet.Rect.Y}]");      
                    
                    bullet = null;
                    points += 10;
                    asteroids[i] = GenerateBorderAsteroid(rnd);
                    //Debug.WriteLine($"Пуля попала в астероид! координаты попадания = [{bullet.Rect.X}, {bullet.Rect.Y}]");

                }

                // попадание астероида в корабль
                if (asteroids[i].Collision(ship))
                {
                    ship.Enegry -= 2;
                    //Debug.WriteLine($"Астероид угодил в корабль! Здоровье = {ship.Enegry}");
                    LogAction(DebugLog, $"Астероид угодил в корабль! Здоровье = {ship.Enegry}");
                    LogAction(LogFile, $"Астероид угодил в корабль! Здоровье = {ship.Enegry}");

                    if (ship.Enegry < 0) ship.Die();
                    else if (ship.Enegry <= 50 && !healthBoxIsGenerated)
                    {
                        healthBox = (HealthBox)GenerateHealthBox();
                        healthBoxIsGenerated = true;
                    }
                }
            }
            
            foreach (var star in stars) star.Update();
            foreach (var comet in comets) comet.Update();
            
            if (bullet != null) bullet.Update();
            ship.Update();
        }

        private static BaseObject GenerateAsteroid(Random rnd)
        {
            var sizeX = rnd.Next(20, 40);
            var sizeY = rnd.Next(20, 40);
            var posX = rnd.Next(sizeX, Width - sizeX);
            var posY = rnd.Next(sizeY, Height - sizeY);
            var dirX = rnd.Next(1, 20);
            var dirY = rnd.Next(1, 20);

            return new Asteroid(new Point(posX, posY), new Point(dirX, dirY), new Size(sizeX, sizeY));
        }

        private static BaseObject GenerateBorderAsteroid(Random rnd)
        {
            var sizeX = rnd.Next(20, 40);
            var sizeY = rnd.Next(20, 40);
            var posY = rnd.Next(sizeY, Height - sizeY);
            var dirX = rnd.Next(1, 20);
            var dirY = rnd.Next(1, 20);

            return new Asteroid(new Point(sizeX / 2, posY), new Point(dirX, dirY), new Size(sizeX, sizeY));
        }

        private static BaseObject GenerateStar(Random rnd)
        {
            var size = rnd.Next(5, 10);
            var posX = rnd.Next(0, Width);
            var posY = rnd.Next(0, Height);

            return new Star(new Point(posX, posY), new Point(0, 0), new Size(size, size));
        }

        private static BaseObject GenerateComet(Random rnd)
        {
            var posX = rnd.Next(0, Width);
            var posY = rnd.Next(0, Height);
            var dirX = rnd.Next(50, 100);
            var dirY = rnd.Next(1, 5);

            return new Comet(new Point(posX, posY), new Point(dirX, dirY), new Size(20, 20));
        }

        private static BaseObject GenerateHealthBox()
        {
            Random rnd = new Random();
            var posX = rnd.Next(30, Width / 2);
            var posY = rnd.Next(30, Height - 50);

            return new HealthBox(new Point(posX, posY), new Point(0, 0), new Size(20, 20));
        }

        private static void Ship_MessageOnDeath(object sender, EventArgs e)
        {
            timer.Stop();
            //buffer.Graphics.DrawString("Game Over",
            //    new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold), Brushes.Orange, new Point(150, 200));
            buffer.Graphics.DrawImage(Resources.game_over, new Rectangle(300, 200, 200, 100));
            buffer.Render();

            LogAction(DebugLog, $"ИГРА ЗАВЕРШЕНА! Очков набрано {points}");
            LogAction(DebugLog, "*********************************************************************");
            LogAction(LogFile, $"ИГРА ЗАВЕРШЕНА! Очков набрано {points}");
            LogAction(LogFile, "*********************************************************************");
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    bullet = new Bullet(new Point(ship.Rect.X + 30, ship.Rect.Y + 15), new Point(1, 0), new Size(50, 20));
                    break;
                case Keys.Up:
                    ship.Up();
                    break;
                case Keys.Down:
                    ship.Down();
                    break;
                case Keys.Left:
                    ship.Left();
                    break;
                case Keys.Right:
                    ship.Right();
                    break;
            }
            if (healthBox != null && ship.Collision(healthBox))
            {
                ship.Enegry += 20;
                healthBox = null;
                healthBoxIsGenerated = false;
            }
        }

        private static void Timer_OnTick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        private static void LogAction(LogDelegate logF, string log_str) { logF(log_str); }
        private static void DebugLog(string log_str) { Debug.WriteLine(log_str); }
        private static void LogFile(string log_str)
        {
            using (var stream = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt", true))
            {
                stream.WriteLine(log_str);
            }
        }
    }
}

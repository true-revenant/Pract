using Pract.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pract.classes
{
    static class Game
    {
        private static BufferedGraphicsContext context;
        public static BufferedGraphics buffer;
        
        static BaseObject[] asteroids;
        static BaseObject[] stars;
        static BaseObject[] comets;
        
        static Bullet bullet;
        static Ship ship;
        static Timer timer;

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

            timer = new Timer() { Interval = 40 };
            timer.Tick += Timer_OnTick;
            timer.Start();

            form.KeyDown += Form_KeyDown;
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                bullet = new Bullet(new Point(50, 200), new Point(1, 0), new Size(20, 5));
            }
        }

        private static void Timer_OnTick(object sender, EventArgs e)
        {
            Draw();
            Update();
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

            if (bullet != null) bullet.Draw();

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
        }

        public static void Update()
        {
            for (int i = 0; i < asteroids.Length; i++)
            {
                asteroids[i].Update();
                Random rnd = new Random();
                if (bullet != null && asteroids[i].Collision(bullet))
                {
                    SystemSounds.Beep.Play();
                    bullet = null;
                    asteroids[i] = GenerateBorderAsteroid(rnd);
                    Debug.WriteLine("Пуля попала в астероид!");
                }
            }
            
            foreach (var star in stars) star.Update();
            foreach (var comet in comets) comet.Update();

            if (bullet != null) bullet.Update();
        }

        private static BaseObject GenerateAsteroid(Random rnd)
        {
            var sizeX = rnd.Next(20, 40);
            var sizeY = rnd.Next(20, 40);
            var posX = rnd.Next(0, Width);
            var posY = rnd.Next(0, Height);
            var dirX = rnd.Next(1, 20);
            var dirY = rnd.Next(1, 20);

            return new Asteroid(new Point(posX, posY), new Point(dirX, dirY), new Size(sizeX, sizeY));
        }

        private static BaseObject GenerateBorderAsteroid(Random rnd)
        {
            var sizeX = rnd.Next(20, 40);
            var sizeY = rnd.Next(20, 40);
            var posX = rnd.Next(2, sizeX);
            var posY = rnd.Next(2, sizeY);
            var dirX = rnd.Next(1, 20);
            var dirY = rnd.Next(1, 20);

            return new Asteroid(new Point(posX, posY), new Point(dirX, dirY), new Size(sizeX, sizeY));
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
    }
}

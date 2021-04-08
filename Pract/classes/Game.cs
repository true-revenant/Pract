using Pract.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pract.classes
{
    static class Game
    {
        private static BufferedGraphicsContext context;
        public static BufferedGraphics buffer;
        
        static Asteroid[] asteroids;
        static Star[] stars;
        static Comet[] comets;

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
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));

            LoadGameScene();

            Timer timer = new Timer() { Interval = 40 };
            timer.Tick += Timer_OnTick;
            timer.Start();
        }

        private static void Timer_OnTick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            //buffer.Graphics.Clear(Color.Black);
            buffer.Graphics.DrawImage(Resources.background_2, new Rectangle(0, 0, Width, Height));
            
            foreach (var aster in asteroids) aster.Draw();
            foreach (var star in stars) star.Draw();
            foreach (var comet in comets) comet.Draw();

            buffer.Graphics.DrawImage(Resources.planet, new Rectangle(100, 100, 200, 200));

            buffer.Render();
        }

        private static void LoadGameScene()
        {
            var rnd = new Random();

            asteroids = new Asteroid[15];
            for (int i = 0; i < asteroids.Length; i++)
            {
                var sizeX = rnd.Next(20, 40);
                var sizeY = rnd.Next(20, 40);
                var posX = rnd.Next(0, Width);
                var posY = rnd.Next(0, Height);
                var dirX = rnd.Next(1, 20);
                var dirY = rnd.Next(1, 20);

                asteroids[i] = new Asteroid(new Point(posX, posY), new Point(dirX, dirY), new Size(sizeX, sizeY));
            }

            stars = new Star[60];
            for (int i = 0; i < stars.Length; i++)
            {
                var size = rnd.Next(5, 10);
                var posX = rnd.Next(0, Width);
                var posY = rnd.Next(0, Height);

                stars[i] = new Star(new Point(posX, posY), new Point(0, 0), new Size(size, size));
            }

            comets = new Comet[3];
            for (int i = 0; i < comets.Length; i++)
            {
                var posX = rnd.Next(0, Width);
                var posY = rnd.Next(0, Height);
                var dirX = rnd.Next(50, 100);
                var dirY = rnd.Next(1, 5);

                comets[i] = new Comet(new Point(posX, posY), new Point(dirX, dirY), new Size(20, 20));
            }
        }

        public static void Update()
        {
            foreach (var aster in asteroids) aster.Update();
            foreach (var star in stars) star.Update();
            foreach (var comet in comets) comet.Update();
        }
    }
}

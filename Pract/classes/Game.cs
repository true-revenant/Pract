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
        #region private fields

        static BufferedGraphicsContext context;
        static int points;
        static Form gForm;

        static Star[] stars;
        static Comet[] comets;

        static List<Bullet> bullets = new List<Bullet>();
        static List<Asteroid> asteroids = new List<Asteroid>();

        static TextObject waveString;
        static TextObject pauseString;
        static TextObject restartString;
        static TextObject resultString;
        static Ship ship;
        static HealthBox healthBox;
        static Timer timer;
        static bool healthBoxIsGenerated = false;
        static bool gameIsPaused = false;
        static bool gameIsEnded = false;

        static Brush heathLineBrush;
        static Pen heathLinePen;
        static float heathLineLength;
        static int damage = 2;
        static int healthImpact;
        static int countOfAsteroids = 15;
        static int countOfWaves = 1;
        static string resultText;

        #endregion

        #region Public Fields

        public static BufferedGraphics buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }

        #endregion

        static Game() { }

        #region Public Methods

        public static void Init(Form form)
        {
            gForm = form;
            context = BufferedGraphicsManager.Current;
            Graphics g = gForm.CreateGraphics();
            
            Width = gForm.ClientSize.Width;
            Height = gForm.ClientSize.Height;

            if (Width >= 1500 || Width < 0 || Height >= 1500 || Height < 0) throw new ArgumentOutOfRangeException();

            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));

            LoadGameScene();
            points = 0;

            timer = new Timer() { Interval = 20 };
            timer.Tick += Timer_OnTick;
            timer.Start();

            gForm.KeyDown += Form_KeyDown;

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
            
            // отрисовка строки с отображением очков за сбитые астероиды
            buffer.Graphics.DrawString($"Points: {points}", 
                new Font(FontFamily.GenericMonospace, 10), Brushes.YellowGreen, new Point(Width - 180, Height - 25));

            if (ship.Enegry < 50)
            {
                heathLineBrush = Brushes.Red;
                heathLinePen = Pens.Red;
            }
            else
            {
                heathLineBrush = Brushes.Green;
                heathLinePen = Pens.Green;
            }
            // полоска здоровья корабля
            buffer.Graphics.DrawRectangle(heathLinePen, new Rectangle(new Point(Width - 180, Height - 50), new Size(103, 20)));
            buffer.Graphics.FillRectangle(heathLineBrush, new RectangleF(new Point(Width - 178, Height - 48), new SizeF(heathLineLength, 17)));

            foreach (var bullet in bullets)
                bullet.Draw();

            // отрисовка пуль и удаление их если они ушли за пределы экрана
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].Rect.X > Width)
                {
                    bullets.RemoveAt(i);
                    i++;
                }
                else bullets[i].Draw();
            }

            if (healthBox != null) healthBox.Draw();

            if (waveString != null && waveString.Rect.X <= Width) waveString.Draw();
            else waveString = null;

            if (pauseString != null) pauseString.Draw();
            if (restartString != null) restartString.Draw();
            if (resultString != null) resultString.Draw();

            buffer.Render();
        }

        #endregion

        #region Private Methods

        private static void LoadGameScene()
        {
            var rnd = new Random();

            for (int i = 0; i < countOfAsteroids; i++) asteroids.Add(GenerateAsteroid(rnd));

            stars = new Star[60];
            for (int i = 0; i < stars.Length; i++) stars[i] = GenerateStar(rnd);

            comets = new Comet[3];
            for (int i = 0; i < comets.Length; i++) comets[i] = GenerateComet(rnd);

            ship = new Ship(new Point(20, Height / 2), new Point(0, 0), new Size(50, 50));
            ship.MessageOnDeath += Ship_MessageOnDeath;
            heathLineLength = 100;

            GenerateWaveString();
        }

        private static void Update()
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].Update();

                //if (bullet != null && asteroids[i].Collision(bullet))
                //{
                //    LogAction(DebugLog, $"Пуля попала в астероид! координаты попадания = [{bullet.Rect.X}, {bullet.Rect.Y}]");
                //    LogAction(LogFile, $"Пуля попала в астероид! координаты попадания = [{bullet.Rect.X}, {bullet.Rect.Y}]");
                //    bullet = null;
                //    points += 10;
                //    asteroids[i] = GenerateBorderAsteroid(rnd);
                //    //Debug.WriteLine($"Пуля попала в астероид! координаты попадания = [{bullet.Rect.X}, {bullet.Rect.Y}]");
                //}

                // попадание астероида в корабль
                if (asteroids[i].Collision(ship))
                {
                    ship.Enegry -= damage;
                    heathLineLength -= damage;

                    LogAction(DebugLog, $"Астероид угодил в корабль! Здоровье = {ship.Enegry}");
                    LogAction(LogFile, $"Астероид угодил в корабль! Здоровье = {ship.Enegry}");

                    if (ship.Enegry < 0) ship.Die();
                    else if (ship.Enegry <= 50 && !healthBoxIsGenerated)
                    {
                        healthBox = (HealthBox)GenerateHealthBox();
                        healthBoxIsGenerated = true;
                    }
                }

                // попадание пули в астероид
                Random rnd = new Random();

                for (int j = 0; j < bullets.Count; j++)
                {
                    if (bullets[j].Collision(asteroids[i]))
                    {
                        if (asteroids.Count > 1)
                        {
                            LogAction(DebugLog, $"Пуля попала в астероид! координаты попадания = [{bullets[j].Rect.X}, {bullets[j].Rect.Y}]");
                            LogAction(LogFile, $"Пуля попала в астероид! координаты попадания = [{bullets[j].Rect.X}, {bullets[j].Rect.Y}]");
                            points += 10;
                            bullets.RemoveAt(j);
                            j--;
                            asteroids.RemoveAt(i);
                            if (i > 0) i--;
                        }
                        else
                        {
                            asteroids[i] = GenerateBorderAsteroid(rnd);
                            for (int k = 0; k < countOfAsteroids; k++)
                                asteroids.Add(GenerateAsteroid(rnd));
                            countOfAsteroids++;
                            countOfWaves++;
                            GenerateWaveString();
                            break;
                        }
                    }
                }
            }

            foreach (var star in stars) star.Update();
            foreach (var comet in comets) comet.Update();
            foreach (var bullet in bullets) bullet.Update();
            
            ship.Update();
            if (waveString != null) waveString.Update();
        }

        private static void GenerateWaveString()
        {
            waveString = new TextObject(new Point(-100, Height - 100), new Point(0, 0), new Size(100, 20), $"Волна {countOfWaves}", OutputTextType.WaveStringText);
        }

        private static void GeneratePauseString()
        {
            pauseString = new TextObject(new Point(450, 350), new Point(0, 0), new Size(100, 20), "Пауза", OutputTextType.PauseText);
        }

        private static void GenerateRestartString()
        {
            restartString = new TextObject(new Point(280, 480), new Point(0, 0), new Size(200, 20), "Чтобы начать заново нажмите Enter", OutputTextType.ForRestartText);
        }

        private static void GenerateResultString()
        {
            resultString = new TextObject(new Point(280, 450), new Point(0, 0), new Size(100, 20), resultText, OutputTextType.ResultText);
        }

        private static Asteroid GenerateAsteroid(Random rnd)
        {
            var sizeX = rnd.Next(30, 50);
            var sizeY = rnd.Next(30, 50);
            var posX = rnd.Next(sizeX, Width - sizeX);
            var posY = rnd.Next(sizeY, Height - sizeY);
            var dirX = rnd.Next(1, 20);
            var dirY = rnd.Next(1, 20);

            return new Asteroid(new Point(posX, posY), new Point(dirX, dirY), new Size(sizeX, sizeY));
        }

        private static Asteroid GenerateBorderAsteroid(Random rnd)
        {
            var sizeX = rnd.Next(20, 40);
            var sizeY = rnd.Next(20, 40);
            var posY = rnd.Next(sizeY, Height - sizeY);
            var dirX = rnd.Next(1, 20);
            var dirY = rnd.Next(1, 20);

            return new Asteroid(new Point(Width - sizeX, posY), new Point(dirX, dirY), new Size(sizeX, sizeY));
        }

        private static Star GenerateStar(Random rnd)
        {
            var size = rnd.Next(5, 10);
            var posX = rnd.Next(0, Width);
            var posY = rnd.Next(0, Height);

            return new Star(new Point(posX, posY), new Point(0, 0), new Size(size, size));
        }

        private static Comet GenerateComet(Random rnd)
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
            BestResultLogAndSet();
            GenerateRestartString();
            GenerateResultString();
            Draw();
            timer.Stop();
            
            buffer.Graphics.DrawImage(Resources.game_over, new Rectangle(400, 300, 200, 100));
            buffer.Graphics.DrawImage(Resources.explosion, ship.Rect);
            buffer.Render();

            LogAction(DebugLog, $"ИГРА ЗАВЕРШЕНА! Очков набрано {points}");
            LogAction(DebugLog, "*********************************************************************");
            LogAction(LogFile, $"ИГРА ЗАВЕРШЕНА! Очков набрано {points}");
            LogAction(LogFile, "*********************************************************************");
            gameIsEnded = true;
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    //bullet = new Bullet(new Point(ship.Rect.X + 30, ship.Rect.Y + 15), new Point(1, 0), new Size(50, 20));
                    if (bullets.Count < 6)
                        bullets.Add(new Bullet(new Point(ship.Rect.X + 30, ship.Rect.Y + 15), new Point(1, 0), new Size(50, 20)));
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
                case Keys.Space:
                    PauseResumeGame();
                    break;
                case Keys.Enter:
                    RestartGame();
                    break;
            }

            if (healthBox != null && ship.Collision(healthBox))
            {
                healthImpact = new Random().Next(20, 50);
                
                ship.Enegry += healthImpact;
                heathLineLength += healthImpact;
                //heathLineLength += 122 / 100 * healthImpact;
                //if (heathLineLength > 122) heathLineLength = 122;
                healthBox = null;
                healthBoxIsGenerated = false;
            }
        }

        private static void Timer_OnTick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        private static void PauseResumeGame()
        {
            if (!gameIsEnded)
            {
                if (gameIsPaused)
                {
                    Debug.WriteLine("Сняли с паузы!");
                    pauseString = null;
                    timer.Start();
                    gameIsPaused = false;
                }
                else
                {
                    Debug.WriteLine("Поставили на паузу!");
                    GeneratePauseString();
                    timer.Stop();
                    Draw();
                    gameIsPaused = true;
                }
            }
        }

        private static void RestartGame()
        {
            if (gameIsEnded)
            {
                gameIsEnded = false;
                healthBoxIsGenerated = false;
                countOfAsteroids = 15;
                countOfWaves = 1;
                asteroids.Clear();
                healthBox = null;
                restartString = null;
                resultString = null;
                heathLineLength = 100;
                
                buffer.Graphics.Clear(Color.Black);
                Init(gForm);
                Draw();
            }
        }

        // БЛОК ЛОГИКИ ЛОГИРОВАНИЯ
        private static void LogAction(LogDelegate logF, string log_str) { logF(log_str); }
        private static void DebugLog(string log_str) { Debug.WriteLine(log_str); }
        private static void LogFile(string log_str)
        {
            using (var stream = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt", true))
            {
                stream.WriteLine(log_str);
            }
        }
        private static void BestResultLogAndSet()
        {
            int prevScore = 0;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "bestScoreLog.txt"))
            {
                using (var stream = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "bestScoreLog.txt"))
                {
                    prevScore = Int32.Parse(stream.ReadLine());
                }
            }
            if (points > prevScore)
            {
                using (var stream = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "bestScoreLog.txt", false))
                {
                    stream.WriteLine(points);
                }
                resultText = $"Вы установили новый рекорд - {points}!";
            }
            else if (points == prevScore) resultText = $"Вы повторили лучши результат!";
            else resultText = $"Ваш результат - {points}!";
        }

        #endregion
    }
}

using Pract.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract.classes
{
    class Asteroid : BaseObject
    {
        Bitmap pic;

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            InitPic();
        }

        public override void Draw()
        {
            //Game.buffer.Graphics.FillEllipse(Brushes.LightCoral, position.X, position.Y, size.Width, size.Height);
            Game.buffer.Graphics.DrawImage(pic, new Rectangle(position.X, position.Y, size.Width, size.Height));
        }

        public override void Update()
        {
            position.X += direction.X;
            position.Y += direction.Y;

            if (position.X < size.Width / 2 || position.X > Game.Width - size.Width / 2) direction.X = -direction.X;
            if (position.Y < size.Height / 2 || position.Y > Game.Height - size.Height / 2) direction.Y = -direction.Y;
        }

        private void InitPic()
        {
            var res_mas = new Bitmap[4] {
                Resources.meteorBrown_big1,
                Resources.meteorBrown_big2,
                Resources.meteorBrown_big3,
                Resources.meteorBrown_big4 };

            var rnd = new Random();

            pic = res_mas[rnd.Next(0, res_mas.Length - 1)];
        }
    }
}

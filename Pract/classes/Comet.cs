using Pract.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract.classes
{
    class Comet : BaseObject
    {
        public Comet(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public override void Draw()
        {
            //Game.buffer.Graphics.FillEllipse(Brushes.White, position.X, position.Y, size.Width, size.Width);
            Game.buffer.Graphics.DrawImage(Resources.star3, new Rectangle(position.X, position.Y, size.Width, size.Width));
        }

        public override void Update()
        {
            position.X += direction.X;
            position.Y += direction.Y;

            if (position.X < -100 || position.X > Game.Width + 100) direction.X = -direction.X;
            if (position.Y < size.Height / 2 || position.Y > Game.Height - size.Height / 2) direction.Y = -direction.Y;
        }
    }
}

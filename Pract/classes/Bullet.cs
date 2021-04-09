using Pract.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract.classes
{
    class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size) {}

        public override void Draw()
        {
            //Game.buffer.Graphics.FillRectangle(Brushes.Red, new Rectangle(position.X, position.Y, size.Width, size.Height));
            Game.buffer.Graphics.DrawImage(Resources.bullet, new Rectangle(position.X, position.Y, size.Width, size.Height));
        }

        public override void Update()
        {
            position.X += 10;
        }
    }
}

using Pract.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract.classes
{
    class HealthBox : BaseObject
    {
        public HealthBox(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(Resources.health_box, new Rectangle(position, size));
        }

        public override void Update() { }
    }
}

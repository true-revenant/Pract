using Pract.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract.classes
{
    class Ship : BaseObject
    {
        public int Enegry { get; set; }

        public event EventHandler MessageOnDeath;

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Enegry = 100;
        }

        public void Up()
        {
            if (position.Y >= 40) position.Y -= 10;
        }

        public void Down()
        {
            if (position.Y <= Game.Height - 40) position.Y += 10;
        }

        public void Left()
        {
            if (position.X >= 40) position.X -= 10;
        }

        public void Right()
        {
            if (position.X <= Game.Width - 40) position.X += 10;
        }

        public void Die()
        {
            if (MessageOnDeath != null) MessageOnDeath.Invoke(this, new EventArgs());
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(Resources.ship, new Rectangle(position, size));
        }

        public override void Update() {}
    }
}

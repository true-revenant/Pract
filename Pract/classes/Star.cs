using Pract.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract.classes
{
    class Star : Asteroid
    {
        Bitmap pic;

        public Star(Point pos, Point dir, Size size) : base(pos, dir, size) 
        {
            InitPic();
        }

        public override void Draw()
        {
            //Game.buffer.Graphics.FillEllipse(Brushes.Yellow, position.X, position.Y, size.Width, size.Width);
            Game.buffer.Graphics.DrawImage(pic, new Rectangle(position.X, position.Y, size.Width, size.Width));
        }

        private void InitPic()
        {
            var res_mas = new Bitmap[2] {
                Resources.star1,
                Resources.star2};

            var rnd = new Random();

            pic = res_mas[rnd.Next(0, res_mas.Length - 1)];
        }
    }
}

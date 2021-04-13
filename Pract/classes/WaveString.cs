using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract.classes
{
    class WaveString : BaseObject
    {
        private string waveStr;

        public WaveString(Point pos, Point dir, Size size, string waveStr) : base(pos, dir, size)
        {
            this.waveStr = waveStr;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawString(waveStr, new Font(FontFamily.GenericMonospace, 30, FontStyle.Bold), Brushes.LightGray, position);
        }

        public override void Update()
        {
            position.X += 25;
        }
    }
}

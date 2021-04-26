using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract.classes
{
    class TextObject : BaseObject
    {
        private string text;
        OutputTextType tType;

        public TextObject(Point pos, Point dir, Size size, string str, OutputTextType tType) : base(pos, dir, size)
        {
            this.text = str;
            this.tType = tType;
        }

        public override void Draw()
        {
            switch (tType)
            {
                case OutputTextType.WaveStringText:
                    Game.buffer.Graphics.DrawString(text, new Font(FontFamily.GenericMonospace, 30, FontStyle.Bold), Brushes.LightGray, position);
                    break;
                case OutputTextType.PauseText:
                    Game.buffer.Graphics.DrawString(text, new Font(FontFamily.GenericMonospace, 30, FontStyle.Bold), Brushes.Yellow, position);
                    break;
                case OutputTextType.ForRestartText:
                    Game.buffer.Graphics.DrawString(text, new Font(FontFamily.GenericMonospace, 16, FontStyle.Regular), Brushes.Yellow, position);
                    break;
                case OutputTextType.ResultText:
                    Game.buffer.Graphics.DrawString(text, new Font(FontFamily.GenericMonospace, 16, FontStyle.Underline), Brushes.Yellow, position);
                    break;
            }
        }

        public override void Update()
        {
            if (tType == OutputTextType.WaveStringText) position.X += 25;
        }
    }

    enum OutputTextType
    {
        WaveStringText,
        PauseText,
        ForRestartText,
        ResultText
    }
}

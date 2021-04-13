using Pract.interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract.classes
{
    public abstract class BaseObject : ICollision
    {
        protected Point position;
        protected Size size;
        protected Point direction;

        protected BaseObject(Point pos, Point dir, Size size)
        {
            //if (pos.X < 0 || pos.Y < 0 || size.Width <= 0 || size.Height <= 0) throw new GameObjectException("Некорректно проинициализирван объект на сцене!");
            
            this.position = pos;
            this.direction = dir;
            this.size = size;
        }

        public abstract void Draw();
        public abstract void Update();

        #region Interface ICollision impementation

        public bool Collision(ICollision obj)
        {
            return Rect.IntersectsWith(obj.Rect);
        }

        public Rectangle Rect => new Rectangle(position, size);

        #endregion
    }

    public class GameObjectException : Exception
    {
        public GameObjectException(string message) : base(message)
        {
        }
    }
}

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
        int Enegry { get; set; }

        public static event EventHandler MessageOnDeath;

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Enegry = 100;
        }

        public void EnergyDecrease(int val)
        {
            Enegry -= val;
        }

        public void Up()
        {

        }

        public void Down()
        {

        }

        public void Die()
        {
            if (MessageOnDeath != null) MessageOnDeath.Invoke(this, new EventArgs());
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Update() {}
    }
}

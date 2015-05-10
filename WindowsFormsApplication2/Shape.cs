using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public abstract class Shape
    {
        public int state;
        public Color color;
        public IndexKeeper[] positions;

        public Shape(Color color, IndexKeeper[] positions)
        {
            this.color = color;
            this.positions = positions;
            state = 0;
        }

        public abstract IndexKeeper[] GetRotationPositions();
        public abstract void SetState();
    }
}

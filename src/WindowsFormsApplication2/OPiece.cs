using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class OPiece : Shape
    {
        private static int NUMBER_STATES = 0;
        public OPiece(int width)
            : base(Color.FromArgb(252, 187, 1), null)
        {
            int middle = (int)Math.Floor((width - 1) * 1.0 / 2);
            positions = new IndexKeeper[4];
            positions[3] = new IndexKeeper(0, middle);
            positions[2] = new IndexKeeper(0, middle+1);
            positions[1] = new IndexKeeper(1, middle);
            positions[0] = new IndexKeeper(1, middle+1);
            base.positions = positions;
        }

        public override IndexKeeper[] GetRotationPositions()
        {
            // нема потреба да се применува ф-јата кај O формата
            return null;
        }

        override public void SetState()
        {
            state = (state + 1) % NUMBER_STATES;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication2
{
    class ZPiece : Shape
    {
        private static int NUMBER_STATES = 2;

        public ZPiece(int width) : base(Color.Red, null)
        {
            int middle = (int)Math.Floor((width - 1) * 1.0 / 2);
            positions = new IndexKeeper[4];
            positions[3] = new IndexKeeper(0, middle-1);
            positions[2] = new IndexKeeper(0, middle);
            positions[1] = new IndexKeeper(1, middle);
            positions[0] = new IndexKeeper(1, middle+1);
            base.positions = positions;
        }

        public override IndexKeeper[] GetRotationPositions()
        {
            IndexKeeper[] rotationPositions = new IndexKeeper[4];

            if (state == 0)
            {
                int i = positions[2].X;
                int j = positions[2].Y;

                rotationPositions[0] = new IndexKeeper(i + 1, j - 1);
                rotationPositions[1] = new IndexKeeper(i, j);
                rotationPositions[2] = new IndexKeeper(i, j - 1);
                rotationPositions[3] = new IndexKeeper(i - 1, j);
            }
            else
            {
                int i = positions[1].X;
                int j = positions[1].Y;

                rotationPositions[3] = new IndexKeeper(i, j - 1);
                rotationPositions[2] = new IndexKeeper(i, j);
                rotationPositions[1] = new IndexKeeper(i + 1, j);
                rotationPositions[0] = new IndexKeeper(i + 1, j + 1);
            }

            return rotationPositions;
        }

        override public void SetState()
        {
            state = (state + 1) % NUMBER_STATES;
        }

    }
}

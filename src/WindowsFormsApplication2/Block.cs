using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class Block
    {
        public Color Color;
        public int PositionX;
        public int PositionY;
        public bool taken;

        public Block (Color color, int x, int y){
            PositionX = x;
            PositionY = y;
            Color = color;
            taken = false;
        }
    }
}

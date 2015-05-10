using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class IndexKeeper
    {
        public int X;
        public int Y;

        public IndexKeeper(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(IndexKeeper index)
        {
            return X == index.X && Y == index.Y;
        }
    }
}

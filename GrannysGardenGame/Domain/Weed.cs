using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrannysGardenGame.Domain
{
    public class Weed
    {
        private int X;
        private int Y;
        public WeedState State;
        public Weed(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrannysGardenGame.Domain
{
    public enum  FieldCellState
    {
        Empty,
        Weed
    }

    public class FieldCell
    {
        public int X;
        public int Y;
        public FieldCellState State;
    }
}

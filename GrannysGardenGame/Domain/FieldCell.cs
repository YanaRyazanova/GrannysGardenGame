using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrannysGardenGame.Domain
{
  
    public class FieldCell
    {
        public int X;
        public int Y;
        public FieldCellStates State;
        public FieldCell(int x, int y, FieldCellStates state)
        {
            X = x;
            Y = y;
            State = state;
        }
    }
}

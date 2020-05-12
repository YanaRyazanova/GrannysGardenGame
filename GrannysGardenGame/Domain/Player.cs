using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrannysGardenGame.Domain
{
    public enum PlayerActions
    {
        Dig,
        Defuse
    }
    public class Player
    {
        public int Health;
        public int Scores;
        

        public FieldCell CurrentPos;

        public Player(FieldCell initCell)
        {
            CurrentPos = initCell;
            Scores = 0;
            Health = 20;
        }

        public bool CanMove(int x, int y, Field field)
        {
            var cell = new FieldCell(x,y,FieldCellStates.Empty);
            if (!field.InBounds(cell) || field.field[x, y] is FieldCellStates.Weed)
                return false;
            return true;
        }
        
       
    }
}

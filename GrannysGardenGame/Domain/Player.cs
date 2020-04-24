using System;
using System.Collections.Generic;
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
        public int Health { get; }
        public int Scores { get; }

        public FieldCell CurrentPos;
        public CreatureCommand Act(int x, int y) 
        {
            var key = Game.KeyPressed;
            var position = new CreatureCommand();

            if (key == Keys.Right)
            {
                position.X += 1;
            }
            else if (key == Keys.Left)
            {
                position.X -= 1;
            }
            else if (key == Keys.Up)
            {
                position.Y -= 1;
            }
            else if (key == Keys.Down)
            {
                position.Y += 1;
            }

            var newX = x + position.X;
            var newY = y + position.Y;
            if (newX >= 0 && newX < Game.GetWigth
                && newY >= 0 && newY < Game.GetHeight
                && !Game.field.IsCellContainWeed(new Weed(newX, newY)))
            {
                return position;
            }
            return new CreatureCommand { X = 0, Y = 0 };
        }
        
        public void DigUpWeed(Weed weed)
        {
            if (Game.field.IsCellContainWeed(weed))
                weed.WeedState = WeedState.Dead;
        }
        public void FreezeWeed(Weed weed)
        {
            if (Game.field.IsCellContainWeed(weed))
                weed.WeedState = WeedState.Freezed;
        }
    }
}

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
        public int Health { get; }
        public int Scores { get; }

        public FieldCell CurrentPos;

        public Player(FieldCell initCell)
        {
            CurrentPos = initCell;
        }
        private static readonly Dictionary<MoveDirection, Size> directionToOffset = new Dictionary<MoveDirection, Size>
        {
            {MoveDirection.Up, new Size(0, -1)},
            {MoveDirection.Down, new Size(0, 1)},
            {MoveDirection.Left, new Size(-1, 0)},
            {MoveDirection.Right, new Size(1, 0)}
        };

        private static readonly Dictionary<Size, MoveDirection> offsetToDirection = new Dictionary<Size, MoveDirection>
        {
            {new Size(0, -1), MoveDirection.Up},
            {new Size(0, 1), MoveDirection.Down},
            {new Size(-1, 0), MoveDirection.Left},
            {new Size(1, 0), MoveDirection.Right}
        };
        public CreatureCommand Act(int x, int y) 
        {
            var key = Game.KeyPressed;
            var position = new CreatureCommand(0, 0);

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
            return new CreatureCommand(0, 0);
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

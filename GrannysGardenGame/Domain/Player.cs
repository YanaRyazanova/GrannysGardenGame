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
        
        public void Act(int x, int y) 
        {
            var key = Game.KeyPressed;
            var position = new FieldCell(0, 0, FieldCellStates.Empty);

            switch (key)
            {
                case Keys.Right:
                    position.X += 1;
                    break;
                case Keys.Left:
                    position.X -= 1;
                    break;
                case Keys.Up:
                    position.Y -= 1;
                    break;
                case Keys.Down:
                    position.Y += 1;
                    break;
            };
                
            var newX = x + position.X;
            var newY = y + position.Y;
            if (newX >= 0 && newX < Game.GetWigth
                && newY >= 0 && newY < Game.GetHeight
                && !Game.field.IsCellContainWeed(new Weed(newX, newY)))
            {
                position.State = FieldCellStates.Player;
                CurrentPos = new FieldCell(newX, newY, FieldCellStates.Player);
            }
            else
                CurrentPos =  new FieldCell(0, 0, FieldCellStates.Empty);
        }
        
        public void DigUpWeed(Weed curWeed)
        {
            if (Game.field.IsCellContainWeed(curWeed) 
                && (CurrentPos.X + 1 == curWeed.X || CurrentPos.X - 1 == curWeed.X
                || CurrentPos.Y + 1 == curWeed.Y || CurrentPos.X - 1 == curWeed.Y)) 
            {
                curWeed.WeedState = WeedStates.Dead;
                Scores += 4;
            }
               
        }
        public void FreezeWeed(Weed curWeed)
        {
            if (Game.field.IsCellContainWeed(curWeed)
                && (CurrentPos.X + 1 == curWeed.X || CurrentPos.X - 1 == curWeed.X
                || CurrentPos.Y + 1 == curWeed.Y || CurrentPos.X - 1 == curWeed.Y))
                curWeed.WeedState = WeedStates.Freezed;
        }
    }
}

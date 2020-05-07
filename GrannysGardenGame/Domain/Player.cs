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
        
        //public void DigUpWeed(Weed curWeed)
        //{
        //    if (Game.Field.weeds.Contains(curWeed) 
        //        && (CurrentPos.X + 1 == curWeed.X || CurrentPos.X - 1 == curWeed.X
        //        || CurrentPos.Y + 1 == curWeed.Y || CurrentPos.X - 1 == curWeed.Y)) 
        //    {
        //        curWeed.WeedState = WeedStates.Dead;
        //        Scores += 4;
        //    }
               
        //}
        //public void FreezeWeed(Weed curWeed)
        //{
        //    if (Game.field.IsCellContainWeed(curWeed))
        //        curWeed.WeedState = WeedStates.Freezed;
        //}
    }
}

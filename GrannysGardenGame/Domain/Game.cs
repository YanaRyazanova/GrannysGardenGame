using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GrannysGardenGame.Domain
{
    public enum GameStates
    {
        Active,
        Lose,
        Win
    }
    public class Game
    {
        //public static HashSet<Keys> KeyPressed;
        public Player player;
        public  Field field;
        
        public GameStates GameState;

        public Game(Player playerConst, Field fieldConst)
        {
            //KeyPressed = keys;
            player = playerConst;
            field = fieldConst;
            GameState = GameStates.Active;
        }
        public int Level { get; set; }
        public int GetWigth => field.Width;
        public int GetHeight => field.Height;


        public void GameEnd()
        {
            if(player.CurrentPos.X == field.winCell.X && player.CurrentPos.Y == field.winCell.Y) //Возможно потребуется переопределить метод
                GameState = GameStates.Win;
            if (player.Health <= 0)
                GameState = GameStates.Lose;
        }

        public void DigUpWeed(Weed curWeed)
        {
            if (field.weeds.Contains(curWeed)
                && (player.CurrentPos.X + 1 == curWeed.X || player.CurrentPos.X - 1 == curWeed.X
                                                         || player.CurrentPos.Y + 1 == curWeed.Y || player.CurrentPos.X - 1 == curWeed.Y))
            {
                curWeed.WeedState = WeedStates.Dead;
                player.Scores += 4;
            }

        }
        public void FreezeWeed(Weed curWeed)
        {
            if (field.IsCellContainWeed(curWeed))
                curWeed.WeedState = WeedStates.Freezed;
        }
    }
}

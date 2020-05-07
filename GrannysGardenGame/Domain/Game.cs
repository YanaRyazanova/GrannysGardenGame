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


        public void GameEnd(Player player, FieldCell specialCell)
        {
            if(player.CurrentPos.X == specialCell.X && player.CurrentPos.Y == specialCell.Y) //Возможно потребуется переопределить метод
                GameState = GameStates.Win;
            if (player.Health <= 0)
                GameState = GameStates.Lose;
        }
    }
}

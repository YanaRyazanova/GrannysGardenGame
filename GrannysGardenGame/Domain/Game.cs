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
        public static Keys KeyPressed;
        private Player player;
        public static Field field;
        
        private GameStates GameState;
        public int Level { get; set; }
        public static int GetWigth => field.Width;
        public static int GetHeight => field.Height;

        public void GameEnd(Player player, FieldCell specialCell)
        {
            if(player.CurrentPos.Equals(specialCell)) //Возможно потребуется переопределить метод
                GameState = GameStates.Win;
            if (player.Health < 0)
                GameState = GameStates.Lose;
        }
    }
}

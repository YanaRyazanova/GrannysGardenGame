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
        public int level;
        
        public GameStates GameState;

        public Game(Player playerConst, Field fieldConst)
        {
            //KeyPressed = keys;
            player = playerConst;
            field = fieldConst;
            GameState = GameStates.Active;
            level = 1;
        }
        public int Level { get; set; }
        public int GetWigth => field.Width;
        public int GetHeight => field.Height;


        public void GameEnd()
        {
            if(player.CurrentPos.X == field.winCell.X && player.CurrentPos.Y == field.winCell.Y 
                && field.weeds.Count == 0) //Возможно потребуется переопределить метод
            {
                GameState = GameStates.Win;
            }
            if (player.Health <= 0)
                GameState = GameStates.Lose;
        }

        public void DigUpWeed(Weed curWeed)
        {
            var i = FindWeed(curWeed);
            if (i != -1) 
            {
                if (player.CurrentPos.X == curWeed.X && player.CurrentPos.Y - 1 == curWeed.Y)
                {
                    field.weeds[i].WeedState = WeedStates.Dead;
                    field.field[field.weeds[i].X, field.weeds[i].Y] = FieldCellStates.Empty;
                    player.Scores += 4;
                }
            }   
        }

        public int FindWeed(Weed curWeed)
        {
            foreach(var item in field.weeds)
            {
                if (curWeed.X == item.X && curWeed.Y == item.Y)
                    return field.weeds.IndexOf(item);
            }
            return -1;
        }

        public void FreezeWeed(Weed curWeed)
        {
            var incidentWeeds = new List<Weed>();
            for (var x = -1; x <= 1; x++)
                for (var y = -1; y <= 1; y++)
                    if ((x == 0 || y == 0) && !(x == 0 && y == 0))
                        incidentWeeds.Add(new Weed(curWeed.X + x, curWeed.Y + y));
            foreach (var weed in incidentWeeds)
            {
                var i = FindWeed(weed);
                if (i != -1) 
                    field.weeds[i].WeedState = WeedStates.Freezed;
            }
            
        }
    }
}

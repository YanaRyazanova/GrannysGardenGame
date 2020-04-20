using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GrannysGardenGame.Domain
{
    public class Game
    {
        public static Keys KeyPressed;
        private Player player;
        private Field field;
        public int Level { get; set; }

    }
}

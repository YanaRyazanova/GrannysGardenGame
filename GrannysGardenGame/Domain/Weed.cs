using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrannysGardenGame.Domain
{
    public enum WeedState
    {
        Dead = 0,
        Freezed = 1,
        Alive = 2
    }
    public class Weed
    {
        private int X;
        private int Y;
        public WeedState State;
        public Weed(int x, int y)
        {
            X = x;
            Y = y;
            State = WeedState.Alive;
        }

        public Bullet Shoot()
        {
            var bullet = new Bullet(this.X, this.Y + 1);
            return bullet;
        }
        
        //public static IEnumerable<LinkedList<FieldCell>> BossSearch(Field field, FieldCell start, FieldCell playerPosition)
        //{
        //Для перемещений БигБосса поиск в ширину?
        //}
    }
}

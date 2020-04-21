using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrannysGardenGame.Domain
{
    public enum BulletState
    {
        NotExist = 0,
        Exist = 1
    }
    public class Bullet
    {
        private int X;
        private int Y;
        private BulletState state;
        public Bullet(int x, int y)
        {
            X = x;
            Y = y;
            state = BulletState.Exist;
        }

        public void MoveBullet()
        {
            this.Y++;
        }

        public bool DeadInConflict(Field field)
        {
            if (this.Y == field.Height)
                return true;

            return false;
        }

    }
}

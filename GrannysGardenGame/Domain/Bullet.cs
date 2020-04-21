using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrannysGardenGame.Domain
{
    public enum BulletState
    {
        NotExist,
        Exist
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

        public bool DeadInConflict(Field field, FieldCell position, Player player)
        {
            if (this.Y == field.Height)
            {
                this.state = BulletState.NotExist;
                return true;
            }
            if (this.X == player.CurrentPos.X && this.Y == player.CurrentPos.Y)
            {
                this.state = BulletState.NotExist;
                return true;
            }
            return false;
        }

    }
}

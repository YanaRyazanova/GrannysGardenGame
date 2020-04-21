using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrannysGardenGame.Domain
{
    class Bullet
    {
        public int X;
        public int Y;

        public Bullet(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void MoveBullet()
        {
            this.Y++;
        }

        public Bullet CreateBullet(int x, int y)
        {
            var bullet = new Bullet(x, y + 1);
            return bullet;
        }

        public bool BulletOutOfBorder(Field field)
        {
            if (this.Y == field.Height)
                return true;
            return false;
        }
    }
}

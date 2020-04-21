using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrannysGardenGame.Domain
{
    public class Field
    {
        private HashSet<Weed> weeds = new HashSet<Weed>();
        public int Width { get; }

        public int Height { get; }
        public Field(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public bool IsCellContainWeed(Weed cell)
        {
           return weeds.Contains(cell);
        }

    }
}

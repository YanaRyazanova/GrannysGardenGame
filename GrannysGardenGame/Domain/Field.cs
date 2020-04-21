using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrannysGardenGame.Domain
{
    public class Field
    {
        public FieldCell[,] field;
        private HashSet<Weed> weeds = new HashSet<Weed>();
        public FieldCell fieldCell; //Клетка, до которой нужно дойти, чтобы выйграть
        public int Width { get; }

        public int Height { get; }
        public Field(int width, int height, HashSet<Weed> weedsForField)
        {
            Width = width;
            Height = height;
            weeds = weedsForField;
        }

        public bool IsCellContainWeed(Weed cell)
        {
           return weeds.Contains(cell);
        }

        /*public Field FromLines(string[] lines)
        {
            var len1 = lines[0].Length;
            var len2 = lines.Length;
            var field = new FieldCell[len1, len2];
            var weeds = new List<FieldCell>();
            for (var y = 0; y < len2; y++)
                for (var x = 0; x < len1; x++)
                {
                    switch (lines[y][x])
                    {
                        case 'W':
                            field[x, y] = FieldCell.
                    }
                }
        }*/
    }
}

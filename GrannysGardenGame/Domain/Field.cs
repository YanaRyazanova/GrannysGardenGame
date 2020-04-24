using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrannysGardenGame.Domain
{
    public class Field
    {
        public FieldCellStates[,] field;
        private HashSet<Weed> weeds = new HashSet<Weed>();
        public FieldCell fieldCell; //Клетка, до которой нужно дойти, чтобы выйграть
        public int Width => field.GetLength(0);
        public int Height => field.GetLength(1);

        public Field(FieldCellStates[,] fieldInit, HashSet<Weed> weedsForField)
        {
            field = fieldInit;
            weeds = weedsForField;
        }

        public bool IsCellContainWeed(Weed cell)
        {
           return weeds.Contains(cell);
        }

        public Field FromLines(string[] lines)
        {
            var len1 = lines[0].Length;
            var len2 = lines.Length;
            var field = new FieldCellStates[len1, len2];
            var weeds = new HashSet<Weed>();
            FieldCell initialCell;
            for (var y = 0; y < len2; y++)
                for (var x = 0; x < len1; x++)
                {
                    switch (lines[y][x])
                    {
                        case 'W':
                            field[x, y] = FieldCellStates.Weed;
                            weeds.Add(new Weed(x, y));
                            break;
                        case '#':
                            field[x, y] = FieldCellStates.Empty;
                            break;
                        case '@':
                            field[x, y] = FieldCellStates.WinCell;
                            break;
                        case 'P':
                            field[x, y] = FieldCellStates.Player;
                            initialCell = new FieldCell(x, y, FieldCellStates.Empty);
                            break;
                        default:
                            field[x, y] = FieldCellStates.Player;
                            break;
                    }
                }
            return new Field(field, weeds);
        }
    }
}

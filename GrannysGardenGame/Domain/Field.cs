using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrannysGardenGame.Domain
{
    public class Field
    {
        public FieldCellStates[,] Field;
        private HashSet<Weed> weeds = new HashSet<Weed>();
        public FieldCell fieldCell; //Клетка, до которой нужно дойти, чтобы выйграть
        public int Width => Field.Length[0];

        public int Height { get; }
        public Field(FieldCellStates[,] field, HashSet<Weed> weedsForField)
        {
            Field = field;
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
            var weeds = new List<FieldCell>();
            FieldCell winCell;
            FieldCell initialCell;
            for (var y = 0; y < len2; y++)
                for (var x = 0; x < len1; x++)
                {
                    switch (lines[y][x])
                    {
                        case 'W':
                            field[x, y] = FieldCellStates.Weed;
                            weeds.Add(new FieldCell(x, y, FieldCellStates.Weed));
                            break;
                        case '#':
                            field[x, y] = FieldCellStates.Empty;
                            break;
                        case '@':
                            field[x, y] = FieldCellStates.WinCell;
                            winCell = new FieldCell(x, y, FieldCellStates.WinCell);
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
            return new Field()
        }
    }
}

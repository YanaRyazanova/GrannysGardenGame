﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrannysGardenGame.Domain
{
    public class Field
    {
        public FieldCellStates[,] field;

        public List<Weed> weeds = new List<Weed>();

        public FieldCell winCell; //Клетка, до которой нужно дойти, чтобы выйграть

        public FieldCell initialCell;
        public int Width => field.GetLength(0);
        public int Height => field.GetLength(1);

        public Field(FieldCellStates[,] fieldInit, List<Weed> weedsForField, FieldCell initCell, FieldCell winnerCell)
        {
            field = fieldInit;
            weeds = weedsForField;
            initialCell = initCell;
            winCell = winnerCell;
        }

        public bool IsCellContainWeed(Weed cell)
        {
           return weeds.Contains(cell);
        }

        public bool IsCellContainBullet(Bullet bullet)
        {
            return field[bullet.X, bullet.Y] == FieldCellStates.Bullet;
        }


        public static Field FromText(string text)
        {
            var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return FromLines(lines);
        }

        public static Field FromLines(string[] lines)
        {
            var len1 = lines[0].Length;
            var len2 = lines.Length;
            var field = new FieldCellStates[len1, len2];
            var weeds = new List<Weed>();
            var initialCell = new FieldCell(0, 0, FieldCellStates.Empty);
            var winCell = new FieldCell(0, 0, FieldCellStates.Empty);
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
                            winCell = new FieldCell(x, y, FieldCellStates.WinCell);
                            break;
                        case 'P':
                            field[x, y] = FieldCellStates.Player;
                            initialCell = new FieldCell(x, y, FieldCellStates.Player);
                            break;
                        default:
                            field[x, y] = FieldCellStates.Empty;
                            break;
                    }
                }
            return new Field(field, weeds, initialCell, winCell);
        }

        public bool InBounds(FieldCell cell)
        {
            return cell.X >= 0 && cell.X < this.Width && cell.Y >= 0 && cell.Y < this.Height;
        }
    }
}

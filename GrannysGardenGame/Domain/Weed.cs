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
        Dead,
        Freezed,
        Alive
    }
    public class Weed
    {
        private int X;
        private int Y;
        public WeedState WeedState;
        public Weed(int x, int y)
        {
            X = x;
            Y = y;
            WeedState = WeedState.Alive;
        }

        public Bullet Shoot()
        {
            var bullet = new Bullet(this.X, this.Y + 1);
            return bullet;
        }

        public static SinglyLinkedList<FieldCell> BossSearch(Field field, FieldCell start, FieldCell playerPosition)
        {
            var correctWay = new SinglyLinkedList<FieldCell>(start);
            var visited = new HashSet<FieldCell>();
            var queue = new Queue<SinglyLinkedList<FieldCell>>();
            visited.Add(start);
            queue.Enqueue(new SinglyLinkedList<FieldCell>(start));
            while (queue.Count != 0)
            {
                var way = queue.Dequeue();
                var incidentCells = GetIncidentCells(way.Value);
                foreach (var incidentCell in incidentCells)
                {
                    var nextWay =
                        new SinglyLinkedList<FieldCell>(
                            new FieldCell(incidentCell.X, incidentCell.Y, incidentCell.State), way);
                    if (!field.InBounds(nextWay.Value) ||
                        visited.Contains(nextWay.Value)) continue;
                    queue.Enqueue(nextWay);
                    visited.Add(nextWay.Value);
                    if (nextWay.Value == playerPosition)
                    {
                        correctWay = nextWay;
                        break;
                    }
                }
            }

            return correctWay;
        }

        private static List<FieldCell> GetIncidentCells(FieldCell cell)
        {
            var incidentPoints = new List<FieldCell>();
            for (var dy = -1; dy <= 1; dy++)
            {
                for (var dx = -1; dx <= 1; dx++)
                {
                    if (dx != 0 && dy != 0) continue;
                    incidentPoints.Add(new FieldCell(cell.X + dx, cell.Y + dy, FieldCellStates.Empty));
                }
            }

            return incidentPoints;
        }
    }
}

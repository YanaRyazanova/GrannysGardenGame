using GrannysGardenGame.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrannysGardenGame.View
{
    public class ScenePainter
    {
        public SizeF Size => new SizeF(currentField.Width, currentField.Height);
        public Size LevelSize => new Size(currentField.Width, currentField.Height);
        private Field currentField;
        private Bitmap fieldImage;

        public ScenePainter(Field level)  
        {
            currentField = level;
            CreateField();
        }

        private void CreateField()
        {
            var cellWidth = Properties.Resources.Grass.Width;
            var cellHeight = Properties.Resources.Grass.Height;
            fieldImage = new Bitmap(LevelSize.Width * cellWidth, LevelSize.Height * cellHeight);
            using (var graphics = Graphics.FromImage(fieldImage)) 
                for (var x = 0; x < currentField.Width; x++)
                    for (var y = 0; y < currentField.Height; y++)
                    {
                        var image = currentField.field[x, y] == FieldCellStates.Weed ? Properties.Resources.Zomb : Properties.Resources.Grass;
                        graphics.DrawImage(image, new Rectangle(x * cellWidth, y * cellHeight, cellWidth, cellHeight));
                    }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrannysGardenGame.View
{
    public class TestForm1 : Form
    {
        public TestForm1()
        {
           
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var playerImage = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\Player.png");
            var test = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\TextBox.png");
           
            e.Graphics.DrawImage(test, 0, 1);
             e.Graphics.DrawImage(playerImage, 0, 0);
        }
    }
}

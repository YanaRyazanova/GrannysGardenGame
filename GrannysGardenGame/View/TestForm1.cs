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
    public class TestForm1 : Form
    {
        private readonly HashSet<Keys> pressedKeys = new HashSet<Keys>();
        Game game;
        TextBox textBox;
        int x = 100;
        int y = 100;
        public TestForm1()
        {
            var textField = new[]
             {
                "W#@##",
                "##W#W",
                "#W###",
                "###W#",
                "W####",
                "##W##",
                "#####",
                "W#W##",
                "####P"
            };
            var field = Field.FromLines(textField);
            var player = new Player(field.initialCell);
            game = new Game(player, field);
            var timer = new Timer();
            timer.Interval = 100;
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // base.OnPaint(e);
            //var playerImage = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\Player.png");
            //var test = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\TextBox.png");
            //pressedKeys.Add(Keys.None);
            //if (pressedKeys.Last() == Keys.Right || pressedKeys.Last() == Keys.Left)
            //e.Graphics.DrawImage(playerImage, 100, 100);
            e.Graphics.FillRectangle((Brushes.Blue), x, y, 100, 100);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Right)
                x += 5;
            if (e.KeyData == Keys.Left)
                x -= 5;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            Invalidate();
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
            //Game.KeyPressed = pressedKeys.Any() ? pressedKeys.Min() : Keys.None;
        }
    }
}

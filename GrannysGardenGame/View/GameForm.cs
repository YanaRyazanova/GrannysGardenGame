using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrannysGardenGame.Domain;
using System.Drawing;

namespace GrannysGardenGame.View
{
    public class GameForm : Form
    {
        //private readonly ScenePainter scenePainter;
        //private readonly ScaledViewPanel scaledViewPanel;
        private Game game;
        private readonly HashSet<Keys> pressedKeys = new HashSet<Keys>();

        public GameForm()
        {
            //game = CreateLevel1();
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
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
            e.Graphics.DrawImage(player.playerImage, new PointF(player.CurrentPos.X, player.CurrentPos.Y));
        }
        /*public void PlayerMove(int x, int y) 
        {
            var key = Game.KeyPressed;
            var position = new FieldCell(0, 0, FieldCellStates.Empty);

            switch (key)
            {
                case Keys.Right:
                    position.X += 1;
                    break;
                case Keys.Left:
                    position.X -= 1;
                    break;
                case Keys.Up:
                    position.Y -= 1;
                    break;
                case Keys.Down:
                    position.Y += 1;
                    break;
            };
                
            var newX = x + position.X;
            var newY = y + position.Y;
            if (newX >= 0 && newX < Game.GetWigth
                && newY >= 0 && newY < Game.GetHeight
                && !Game.field.IsCellContainWeed(new Weed(newX, newY)))
            {
                position.State = FieldCellStates.Player;
                CurrentPos = new FieldCell(newX, newY, FieldCellStates.Player);
            }
            else
                CurrentPos =  new FieldCell(0, 0, FieldCellStates.Empty);
        }*/

        protected override void OnKeyDown(KeyEventArgs e)
        {
            pressedKeys.Add(e.KeyCode);
            Game.KeyPressed = e.KeyCode;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
            Game.KeyPressed = pressedKeys.Any() ? pressedKeys.Min() : Keys.None;
        }
        public void InitializeComponent() 
        {
            BackColor = Color.FromArgb(42, 212, 0);
            MinimumSize = new Size(400, 720);
            Width = 360;
            Height = 640;
            
            var houseImege = new PictureBox 
            {
                
                Image = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\House.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Dock = DockStyle.Fill,
            };

            var healthText = new PictureBox 
            {
                Image = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\HealthText.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Dock = DockStyle.None
            };

            var scoreText = new PictureBox 
            {
                Image = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\ScoreTextpng.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Dock = DockStyle.None
            };

            /*var level1 = new PictureBox 
            {
                Image = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\Level1.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Dock = DockStyle.Fill
            };*/

            var table = new TableLayoutPanel();
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, houseImege.Height));
            //table.RowStyles.Add(new RowStyle(SizeType.Absolute, level1.Height));
           
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            table.Controls.Add(houseImege, 1, 0);
            table.Controls.Add(healthText, 0, 0);
            //table.Controls.Add(level1, 0, 1);
            //table.SetColumnSpan(level1, 2);
            
            table.Dock = DockStyle.Fill;

            Controls.Add(table);
        }

        public Game CreateLevel1() 
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
            //game = new Game(pressedKeys, player, field);
            return game;
        }
    }
}

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
        int cellWidth = 74;
        int cellHeight = 64;

        public GameForm()
        {
            game = CreateLevel1();
            InitializeComponent();
            var timer = new Timer();
            timer.Interval = 100;
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var playerImage = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\Player.png");
            var zombImage = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\Zomb.png");
            var fieldImage = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\Field.png");
            e.Graphics.DrawImage(fieldImage, 0, 2, game.field.Width * cellWidth, game.field.Height * cellHeight);
            e.Graphics.DrawImage(playerImage, game.player.CurrentPos.X * cellWidth, game.player.CurrentPos.Y * cellHeight, 70, 97);
            
            foreach(var weed in game.field.weeds)
            {
                e.Graphics.DrawImage(zombImage, weed.X * cellWidth, weed.Y * cellHeight);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            var key = e.KeyData;
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

            var newX = game.player.CurrentPos.X + position.X;
            var newY = game.player.CurrentPos.Y + position.Y;
            if (newX >= 0 && newX < game.field.Width
                && newY >= 0 && newY < game.field.Height
               && !game.field.weeds.Contains(new Weed(newX, newY)))
            {
                position.State = FieldCellStates.Player;
                game.player.CurrentPos = new FieldCell(newX, newY, FieldCellStates.Player);
            }  
        }

        private void TimerTick(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        public void InitializeComponent() 
        {
            BackColor = Color.FromArgb(42, 212, 0);
            MinimumSize = new Size(390, 720);
            Width = 360;
            Height = 640;
            
            /*var houseImege = new PictureBox 
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
            };

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

            Controls.Add(table);*/
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
            game = new Game(player, field);
            return game;
        }
    }
}

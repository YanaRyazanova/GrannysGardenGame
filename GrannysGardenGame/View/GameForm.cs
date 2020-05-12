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
        int cellWidth = 54;
        int cellHeight = 54;

        public GameForm()
        {
            game = CreateLevel1();
            //InitializeComponent();
            var timer = new Timer();
            timer.Interval = 100;
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;
            DrawLevel(graphics);
            //InitializeComponent(graphics);
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

        

        public void DrawLevel(Graphics graphics) 
        {
            var playerImage = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\Player.png");
            var zombImage = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\Zomb.png");
            var fieldImage = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\Field.png");
            graphics.DrawImage(fieldImage, 0, 150, game.field.Width * cellWidth, game.field.Height * cellHeight);
            graphics.DrawImage(playerImage, game.player.CurrentPos.X * cellWidth, game.player.CurrentPos.Y * cellHeight + 120, 70, 97);
            
            foreach(var weed in game.field.weeds)
            {
                graphics.DrawImage(zombImage, weed.X * cellWidth, weed.Y * cellHeight + 130);
            }
        }

        public void InitializeComponent(Graphics graphics) 
        {
            BackColor = Color.FromArgb(42, 212, 0);
            MinimumSize = new Size(390, 720);
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

            var table = new TableLayoutPanel();
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, houseImege.Height));
           
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            table.Controls.Add(houseImege, 1, 0);
            table.Controls.Add(healthText, 0, 0);
            
            table.Dock = DockStyle.Fill;

            Controls.Add(table);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrannysGardenGame.Domain;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace GrannysGardenGame.View
{
    public class GameForm : Form
    {
        private Game game;
        private readonly HashSet<Keys> pressedKeys = new HashSet<Keys>();
        private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        List<Bullet> bullets = new List<Bullet>();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        int cellWidth = 74;
        int cellHeight = 64;

        public GameForm()
        {
            BackColor = Color.FromArgb(42, 212, 0);
            MinimumSize = new Size(390, 720);
            Width = 360;
            Height = 640;
            //var imagesDirectory = new DirectoryInfo("Images");
            //foreach (var e in imagesDirectory.GetFiles("*.png"))
            //    bitmaps[e.Name] = (Bitmap)Image.FromFile(e.FullName);
            game = CreateLevel1();

            GenerateBullets();

            //InitializeComponent();

            timer.Interval = 90;
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var playerImage = new Bitmap(@"C:\Users\mbara\OneDrive\Documents\ЯТП\Granny's Garden Game\GrannysGardenGame\Images\Player.png");
            var zombImage = new Bitmap(@"C:\Users\mbara\OneDrive\Documents\ЯТП\Granny's Garden Game\GrannysGardenGame\Images\Zomb.png");
            var fieldImage = new Bitmap(@"C:\Users\mbara\OneDrive\Documents\ЯТП\Granny's Garden Game\GrannysGardenGame\Images\Field.png");
            e.Graphics.DrawImage(fieldImage, 0, 2, game.field.Width * cellWidth, game.field.Height * cellHeight);
            e.Graphics.DrawImage(playerImage, game.player.CurrentPos.X * cellWidth, game.player.CurrentPos.Y * cellHeight, 70, 97);
            e.Graphics.FillRectangle(Brushes.Red,
                game.field.winCell.X * cellWidth, game.field.winCell.Y * cellHeight, cellWidth, cellHeight);


            foreach (var weed in game.field.weeds)
            {
                e.Graphics.DrawImage(zombImage, weed.X * cellWidth, weed.Y * cellHeight);
            }

            var bulletImage = new Bitmap(@"C:\Users\mbara\OneDrive\Documents\ЯТП\Granny's Garden Game\GrannysGardenGame\Images\Bullet.png");

            foreach (var bullet in bullets)
            {
                if (bullet.state == BulletState.Exist)
                    e.Graphics.DrawImage(bulletImage, bullet.X * cellWidth + 25, bullet.Y * cellHeight - 40, 30, 30);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            var key = e.KeyData;
            var position = new FieldCell(0, 0, FieldCellStates.Empty);

            switch (key)
            {
                case Keys.Right:
                    if (game.player.CanMove(game.player.CurrentPos.X + 1, game.player.CurrentPos.Y, game.field))
                        position.X += 1;
                    break;
                case Keys.Left:
                    if (game.player.CanMove(game.player.CurrentPos.X - 1, game.player.CurrentPos.Y, game.field))
                        position.X -= 1;
                    break;
                case Keys.Up:
                    if (game.player.CanMove(game.player.CurrentPos.X, game.player.CurrentPos.Y - 1, game.field))
                        position.Y -= 1;
                    break;
                case Keys.Down:
                    if (game.player.CanMove(game.player.CurrentPos.X, game.player.CurrentPos.Y + 1, game.field))
                        position.Y += 1;
                    break;
                    //case Keys.A:
                    //game.player.DigUpWeed();
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

            var houseImege = new PictureBox
            {

                Image = new Bitmap(@"C:\Users\mbara\OneDrive\Documents\ЯТП\Granny's Garden Game\GrannysGardenGame\Images\House.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Dock = DockStyle.Fill,
            };

            var healthText = new PictureBox
            {
                Image = new Bitmap(@"C:\Users\mbara\OneDrive\Documents\ЯТП\Granny's Garden Game\GrannysGardenGame\Images\HealthText.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Dock = DockStyle.None
            };

            var scoreText = new PictureBox
            {
                Image = new Bitmap(@"C:\Users\mbara\OneDrive\Documents\ЯТП\Granny's Garden Game\GrannysGardenGame\Images\ScoreTextpng.png"),
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.SuspendLayout();
            // 
            // GameForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(212)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(368, 664);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(390, 720);
            this.Name = "GameForm";
            this.Text = "Granny\'s Garden";
            this.ResumeLayout(false);

            Controls.Add(table);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            game.GameEnd();
            CheckGameState();
            MoveBullets();
            RewriteBulletsList();
            GenerateBullets();
            Invalidate();
        }

        public void CheckGameState()
        {
            if (game.GameState == GameStates.Win)
            {
                timer.Stop();
                this.Hide();
                var winWindow = new LevelPassed();
                winWindow.ShowDialog();
                this.Hide();
            }
        }

        private void MoveBullets()
        {
            foreach (var bullet in bullets)
            {
                bullet.MoveBullet();
                bullet.DeadInConflict(game.field, game.player);
            }
        }

        public void RewriteBulletsList()
        {
            for (var i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].state == BulletState.NotExist)
                    bullets.Remove(bullets[i]);
            }
        }

        public void GenerateBullets()
        {
            var weeds = game.field.weeds;
            if (bullets.Count == 0)
            {
                foreach (var weed in game.field.weeds)
                {
                    bullets.Add(weed.Shoot());
                }
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
                "#####",
                "#####",
                "#####",
                "W####",
                "####P"
            };
            var field = Field.FromLines(textField);
            var player = new Player(field.initialCell);
            game = new Game(player, field);
            return game;
        }
    }
}
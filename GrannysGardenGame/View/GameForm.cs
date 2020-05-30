﻿using System;
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
        int cellWidth = 78;
        int cellHeight = 54;

        public GameForm()
        {
            BackColor = Color.FromArgb(42, 212, 0);
            MinimumSize = new Size(390, 700);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Width = 360;
            Height = 640;
            game = ChangeLevel(CreateLevel());
            
            GenerateBullets();
            
            //InitializeComponent();
            
            var learnImage = new Bitmap(@".\Images\Learn.png");
            var toWinImage = new Bitmap(@".\Images\ToWin.png");
            var zaBabkuIPomidoryImage = new Bitmap(@".\Images\ZaBabku.png");
            var learnBox = new PictureBox
            {
                Width = 316,
                Height = 201,
                Location = new Point(25, 200),
                Image = learnImage
            };

            Controls.Add(learnBox);
            
            var toWinBox = new PictureBox
            {
                Width = 145,
                Height = 100,
                Location = new Point(120, 40),
                Image = toWinImage
            };
            
            Controls.Add(toWinBox);

            var zaBabkuBox = new PictureBox
            {
                Width = 144,
                Height = 75,
                Location = new Point(230, 490),
                Image = zaBabkuIPomidoryImage
            };

            Controls.Add(zaBabkuBox);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var houseImage = new Bitmap(@".\Images\House.png");
            var grannysImage = new Bitmap(@".\Images\Granny.png");
            var healthText = new Bitmap(@".\Images\HealthText.png");
            var scoreText = new Bitmap(@".\Images\ScoreTextpng.png");
            var h = game.player.Health;
            var s = game.player.Scores;
            var health = new Rectangle(new Point(42 + healthText.Width, 14), new Size((675 * h) / 100, 12));
            var health2 = new Rectangle(new Point(40 + healthText.Width, 12), new Size(139, 16));

            e.Graphics.FillRectangle(Brushes.LightGreen, 0, 0, 390, 70);
            e.Graphics.FillRectangle(Brushes.Black, health2);
            e.Graphics.FillRectangle(Brushes.Red, health);
            e.Graphics.DrawString(s.ToString(), new Font(new FontFamily("Segoe UI Symbol"), 18, FontStyle.Bold), Brushes.Black, 40 + scoreText.Width, 31);
            e.Graphics.DrawImage(healthText, 12,12);
            e.Graphics.DrawImage(scoreText, 12, 41);
            e.Graphics.DrawImage(grannysImage, 50, 60, 64, 79);
            e.Graphics.DrawImage(houseImage, 210, 0, 150, 140);
           

            var playerImage = new Bitmap(@".\Images\Player.png");
            var zombImage = new Bitmap(@".\Images\Zomb.png");
            var deadZomb = new Bitmap(@".\Images\DeadWeed.png");
            var freezedZomb = new Bitmap(@".\Images\FreezedZomb.png");
            var fieldImage = new Bitmap(@".\Images\FieldWithRedCell.png");

            e.Graphics.DrawImage(fieldImage, 0, 134, game.field.Width * cellWidth, game.field.Height * cellHeight);
            e.Graphics.DrawImage(playerImage, game.player.CurrentPos.X * cellWidth, game.player.CurrentPos.Y * cellHeight + 134, 70, 97);
            //e.Graphics.FillRectangle(Brushes.Red, 
                //game.field.winCell.X * cellWidth, game.field.winCell.Y * cellHeight + 134, cellWidth, cellHeight);

            foreach(var weed in game.field.weeds)
            {
                if (weed.WeedState == WeedStates.Alive)
                    e.Graphics.DrawImage(zombImage, weed.X * cellWidth, weed.Y * cellHeight + 134);
                if (weed.WeedState == WeedStates.Dead)
                    e.Graphics.DrawImage(deadZomb, weed.X * cellWidth, weed.Y * cellHeight + 134);
                if (weed.WeedState == WeedStates.Freezed)
                    e.Graphics.DrawImage(freezedZomb, weed.X * cellWidth, weed.Y * cellHeight + 134, zombImage.Width * 1.5f, zombImage.Height * 1.5f);
            }

            var bulletImage = new Bitmap(@".\Images\Bullet.png");

            foreach(var bullet in bullets) 
            {
                if (bullet.state == BulletState.Exist)
                    e.Graphics.DrawImage(bulletImage, bullet.X * cellWidth, bullet.Y * cellHeight - 40 + 134, 30, 30);
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
                case Keys.D:
                    game.DigUpWeed(new Weed(game.player.CurrentPos.X, game.player.CurrentPos.Y - 1));
                    break;
                case Keys.A:
                    game.FreezeWeed(new Weed(game.player.CurrentPos.X, game.player.CurrentPos.Y));
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            timer.Interval = 50;
            timer.Tick += TimerTick;
            timer.Start();
            Controls.Clear();
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
            var healthText = new PictureBox 
            {
                Image = new Bitmap(@".\Images\HealthText.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Dock = DockStyle.None
            };

            var scoreText = new PictureBox 
            {
                Image = new Bitmap(@".\Images\ScoreTextpng.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Dock = DockStyle.None
            };

            var table = new TableLayoutPanel();
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();
            
           
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            
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
            RewriteBulletsAndWeedsList();
            GenerateBullets();
            Invalidate();
        }

        public void CheckGameState()
        {
            if (game.GameState == GameStates.Win)
            {
                game.level++;
                ChangeLevel(game);
                timer.Stop();
                this.Hide();
                var winWindow = new LevelPassed();
                winWindow.ShowDialog();
                this.Close();
            }
            if (game.GameState == GameStates.Lose)
            {
                ChangeLevel(game);
                timer.Stop();
                this.Hide();
                var winWindow = new LoseForm();
                winWindow.ShowDialog();
                this.Close();
            }
        }

        private void MoveBullets()
        {
            foreach(var bullet in bullets) 
            {
                bullet.MoveBullet();
                bullet.DeadInConflict(game.field, game.player);
            }
        }

        public void RewriteBulletsAndWeedsList() 
        {
            for(var i = 0; i < bullets.Count; i++) 
            {
                if (bullets[i].state == BulletState.NotExist)
                    bullets.Remove(bullets[i]);
            }
            for(var i = 0; i < game.field.weeds.Count; i++)
            {
                if (game.field.weeds[i].WeedState == WeedStates.Dead)
                    game.field.weeds.RemoveAt(i);
            }
        }

        public void GenerateBullets() 
        {
            var weeds = game.field.weeds;
            if (bullets.Count == 0) 
            {
                foreach (var weed in game.field.weeds)
                { 
                    if (weed.WeedState == WeedStates.Alive)
                        bullets.Add(weed.Shoot());
                }
            }
        }

        public Game CreateLevel() 
        {
            var textField = level1;
            var field = Field.FromLines(textField);
            var player = new Player(field.initialCell);
            game = new Game(player, field);
            return game;
        }

        public Game ChangeLevel(Game game) 
        {
            if (game.level == 1) 
            {
                var field = Field.FromLines(level1);
                return new Game(new Player(field.initialCell), field);
            }
            if (game.level == 2)
            {
                var field = Field.FromLines(level2);
                return new Game(new Player(field.initialCell), field);
            }
            else 
                return game;
        }

        string[] level1 = new[] 
            {
                "W#@#W",
                "##W#W",
                "#W###",
                "W##W#",
                "#####",
                "##W##",
                "#####",
                "W#W##",
                "####P"
            };
        string[] level2 = new[] 
            {
                "WW@WW",
                "##W#W",
                "#W###",
                "W##W#",
                "WWWWW",
                "WWWWW",
                "WWWWW",
                "W#W##",
                "####P"
            };
    }
}

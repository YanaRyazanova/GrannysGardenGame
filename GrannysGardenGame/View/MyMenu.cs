using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrannysGardenGame.View
{
    public partial class MyMenu : Form
    {
        PictureBox logoImege;
        PictureBox textBox;
        Button newGameButton;
        Button continueGameButton;
        public int level;
   
        public MyMenu(int lev)
        {
            level = lev;
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            BackColor = Color.FromArgb(42, 212, 0);
            MinimumSize = new Size(420, 700);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Width = 360;
            Height = 400;
           
            var backLayer = new PictureBox 
            {
                BackColor = Color.FromArgb(37, 189, 0),
                Width = 282,
                Height = 700,
                Location = new Point(60, 0)
            };

            logoImege = new PictureBox 
            {
                Image = new Bitmap(@".\Images\NewLogo.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(27, 2)
            };

            textBox = new PictureBox 
            {
                Image = new Bitmap(@".\Images\TextBoxNew.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(60, 399)
            };
            
            newGameButton = new Button
            {
                Width = 199,
                Height = 65,
                Image = new Bitmap(@".\Images\NewGameButton.png"),
                Location = new Point(42, logoImege.Height + 18)
            };

            newGameButton.Click += (sender, args) => 
            {
                this.Hide();
                var gameForm = new GameForm();
                gameForm.ShowDialog();
                this.Show();
            };

            continueGameButton = new Button
            {
                Width = 199,
                Height = 65,
                Image = new Bitmap(@".\Images\ContinueGameButton.png"),
                Location = new Point(42, logoImege.Height + 13 * 2 + newGameButton.Height)
            };

            continueGameButton.Click += (sender, args) =>
            {
                this.Hide();
                if (level == 1)
                {
                    var gameForm = new GameForm();
                    gameForm.ShowDialog();
                }   
                else if(level == 2)
                {
                    var gameForm = new Level2();
                    gameForm.ShowDialog();
                }
                else 
                {
                    var gameForm = new BossLevel();
                    gameForm.ShowDialog();
                }
                this.Show();
            };

            backLayer.Controls.Add(logoImege);
            backLayer.Controls.Add(newGameButton);
            backLayer.Controls.Add(continueGameButton);
            Controls.Add(textBox);
            Controls.Add(backLayer);
        }
    }
}
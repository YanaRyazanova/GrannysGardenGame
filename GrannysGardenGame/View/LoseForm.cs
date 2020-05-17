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
    public partial class LoseForm : Form
    {
        PictureBox textBox;
        Button newGameButton;
        Button exitGameButton;
        public LoseForm()
        {
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            BackColor = Color.FromArgb(39, 196, 0);
            MinimumSize = new Size(420, 720);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Width = 360;
            Height = 400;


            textBox = new PictureBox
            {
                Image = new Bitmap(@".\Images\Ur Killed.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(28, 175)
            };

            newGameButton = new Button
            {
                Width = 200,
                Height = 65,
                Image = new Bitmap(@".\Images\PlayAgain.png"),
                Location = new Point(100, 285)
            };
            
            newGameButton.BringToFront();
            
            newGameButton.Click += (sender, args) =>
            {
                this.Hide();
                var gameForm = new GameForm();
                gameForm.ShowDialog();
                this.Show();
            };

            exitGameButton = new Button
            {
                Width = 200,
                Height = 65,
                Image = new Bitmap(@".\Images\ExitGame.png"),
                Location = new Point(100, newGameButton.Location.Y + newGameButton.Height + 10),
            };

            exitGameButton.BringToFront();
            
            exitGameButton.Click += (sender, args) =>
            {
                this.Hide();
                var myMenu = new MyMenu();
                myMenu.ShowDialog();
                this.Show();
            };

            Controls.Add(newGameButton);
            Controls.Add(exitGameButton);
            Controls.Add(textBox);

        }
    }
}
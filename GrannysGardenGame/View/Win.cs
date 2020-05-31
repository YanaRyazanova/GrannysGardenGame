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
    public partial class Win : Form
    {
        PictureBox textBox;
        Button continueGameButton;
        Button exitGameButton;

        public Win()
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
                Image = new Bitmap(@".\Images\U Won.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(28, 175)
            };

            exitGameButton = new Button
            {
                Width = 200,
                Height = 65,
                Image = new Bitmap(@".\Images\ExitGame.png"),
                Location = new Point(100, continueGameButton.Location.Y + continueGameButton.Height + 10),
            };

            exitGameButton.BringToFront();

            exitGameButton.Click += (sender, args) =>
            {
                this.Hide();
                var myMenu = new MyMenu(1);
                myMenu.ShowDialog();
                this.Close();
            };

            Controls.Add(continueGameButton);
            Controls.Add(exitGameButton);
            Controls.Add(textBox);
        }
    }
}
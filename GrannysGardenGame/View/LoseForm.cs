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
            BackColor = Color.FromArgb(42, 212, 0);
            MinimumSize = new Size(440, 720);
            Width = 360;
            Height = 640;

            var backLayer = new PictureBox
            {
                BackColor = Color.FromArgb(13, 0, 0, 0)
            };


            textBox = new PictureBox
            {
                Width = 340,
                Height = 120,
                Image = new Bitmap(@".\Images\Ur Killed Short.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Dock = DockStyle.None
            };

            newGameButton = new Button
            {
                Width = 340,
                Height = 65,
                Image = new Bitmap(@".\Images\PlayAgain.png"),
                Dock = DockStyle.None
            };
            newGameButton.Click += (sender, args) =>
            {
                this.Hide();
                var gameForm = new GameForm();
                gameForm.ShowDialog();
                this.Show();
            };

            exitGameButton = new Button
            {
                Width = 340,
                Height = 65,
                Image = new Bitmap(@".\Images\ExitGame.png"),
                Dock = DockStyle.None
            };

            exitGameButton.Click += (sender, args) =>
            {
                this.Hide();
                var myMenu = new MyMenu();
                myMenu.ShowDialog();
                this.Show();
            };

            var table = new TableLayoutPanel();
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, textBox.Height * 2));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, textBox.Height));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, newGameButton.Height));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, exitGameButton.Height));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));

            table.Controls.Add(textBox, 1, 1);
            table.Controls.Add(newGameButton, 1, 2);
            table.Controls.Add(exitGameButton, 1, 3);

            table.Dock = DockStyle.Fill;

            Controls.Add(table);

        }
    }
}
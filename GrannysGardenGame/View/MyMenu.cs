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
   
        public MyMenu()
        {
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            BackColor = Color.FromArgb(42, 212, 0);
            MinimumSize = new Size(400, 720);
            Width = 360;
            Height = 640;
           
            var backLayer = new PictureBox 
            {
                BackColor = Color.FromArgb(13, 0, 0, 0)
            };

            logoImege = new PictureBox 
            {
                Image = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\NewLogo.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Dock = DockStyle.Fill
            };

            textBox = new PictureBox 
            {
                Width = 312,
                Image = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\TextBox.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Dock = DockStyle.Fill
            };
            
            newGameButton = new Button
            {
                Width = 199,
                Height = 65,
                Image = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\NewGameButton.png"),
                Dock = DockStyle.None
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
                Image = new Bitmap(@"C:\Users\Пользователь\More\Desktop\Game\GrannysGardenGame\Images\ContinueGameButton.png"),
                Dock = DockStyle.None
            };

            var table = new TableLayoutPanel();
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, logoImege.Height));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, newGameButton.Height));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, continueGameButton.Height));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, textBox.Height));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));

            table.Controls.Add(logoImege, 1, 0);
            table.Controls.Add(newGameButton, 1, 1);
            table.Controls.Add(continueGameButton, 1, 2);
            table.Controls.Add(textBox, 1, 3);

            table.Dock = DockStyle.Fill;

            Controls.Add(table);
        }
    }
}

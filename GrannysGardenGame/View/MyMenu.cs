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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyMenu));
            this.SuspendLayout();
            // 
            // MyMenu
            // 
            this.ClientSize = new System.Drawing.Size(278, 244);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MyMenu";
            this.Text = "Granny\'s Garden";
            this.Load += new System.EventHandler(this.MyMenu_Load);
            this.ResumeLayout(false);

        }

        private void MyMenu_Load(object sender, EventArgs e)
        {

        }
    }
}

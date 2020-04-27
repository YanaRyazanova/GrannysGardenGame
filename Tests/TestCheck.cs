using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrannysGardenGame.Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TestCheck
    {
        //[Test]
        //public void FieldDecryption()
        //{
        //    var field = new[]
        //   {
        //        "W###",
        //        "####",
        //        "P###"
        //    };
        //    var actual = Field.FromLines(field);
        //    var exept = new FieldCell(0, 0, FieldCellStates.Weed);
        //    Assert.AreEqual(actual., exept.X);
        //    Assert.AreEqual(actual.Y, exept.Y);
        //}

        [Test]
        public void PlayerMovement()
        {
            var textField = new[]
            {
                "###",
                "###",
                "P##"
            };
            var field = Field.FromLines(textField);
            var init = field.initialCell;
            var player = new Player(init);
            var keys = new List<Keys>();
            keys.Add(Keys.Right);
            var game = new Game(keys[0], player, field);
            var actual = player.Act(init.X, init.Y);
            var exept = new FieldCell(1, 0, FieldCellStates.Player);
            Assert.AreEqual(actual.X, exept.X);
            Assert.AreEqual(actual.Y, exept.Y);
        }
    }
}

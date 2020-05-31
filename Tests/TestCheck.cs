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
        [Test]
        public void FieldDecryptionTest()
        {
            var textField = new[]
            {
                "W#@#",
                "#W##",
                "P###"
            };
            var field = Field.FromLines(textField);
            var initCell = new FieldCell(0, 2, FieldCellStates.Player);
            var winCell = new FieldCell(2, 0, FieldCellStates.WinCell);
            var weed1 = new Weed(0, 0);
            var weed2 = new Weed(1, 1);

            Assert.AreEqual(field.initialCell.X, initCell.X);
            Assert.AreEqual(field.initialCell.Y, initCell.Y);
            Assert.AreEqual(field.initialCell.State, initCell.State);

            Assert.AreEqual(field.winCell.X, winCell.X);
            Assert.AreEqual(field.winCell.Y, winCell.Y);
            Assert.AreEqual(field.winCell.State, winCell.State);

            Assert.AreEqual(field.weeds[0].X, weed1.X);
            Assert.AreEqual(field.weeds[0].Y, weed1.Y);
            Assert.AreEqual(field.weeds[0].WeedState, weed1.WeedState);

            Assert.AreEqual(field.weeds[1].X, weed2.X);
            Assert.AreEqual(field.weeds[1].Y, weed2.Y);
            Assert.AreEqual(field.weeds[1].WeedState, weed2.WeedState);
        }

        [Test]
        public void PlayerMovementTest()
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
            var game = new Game(player, field);
            player.CurrentPos.X++;
            var actual = player.CurrentPos;
            var exept = new FieldCell(1, 2, FieldCellStates.Player);
            Assert.AreEqual(exept.X, actual.X);
            Assert.AreEqual(exept.Y, actual.Y);
        }

        [Test]
        public void MoveBulletTest()
        {
            var bullet = new Bullet(1, 2);
            bullet.MoveBullet();
            Assert.AreEqual(3, bullet.Y);
            bullet.MoveBullet();
            bullet.MoveBullet();
            Assert.AreEqual(5, bullet.Y);
        }

        [Test]
        public void IsPossibleToConnectWithWeedTest()
        {
            var textField = new[]
          {
                "P#W",
                "###",
                "###"
            };
            var field = Field.FromLines(textField);
            var player = new Player(field.initialCell);
            var game = new Game(player, field);
            var weed = field.weeds[0];
            var str = "Bad";
            game.DigUpWeed(weed);
            if (weed.WeedState != WeedStates.Dead)
                str = "Test is correct";
            Assert.AreEqual(str, "Test is correct");
        }

        [Test]
        public void DigUpWeedTest()
        {
            var textField = new[]
           {
                "P#W",
                "###",
                "###"
            };
            var field = Field.FromLines(textField);
            var init = field.initialCell;
            var player = new Player(init);
            var keys = new List<Keys>();
            keys.Add(Keys.Right);
            var game = new Game(player, field);
            var weed = field.weeds[0];
            player.CurrentPos.X++;
            game.DigUpWeed(weed);
            Assert.AreEqual(weed.WeedState, WeedStates.Dead);
            Assert.AreEqual(4, player.Scores);
        }

        [Test]
        public void FreezeWeedTest()
        {
            var textField = new[]
           {
                "P#W",
                "###",
                "###"
            };
            var field = Field.FromLines(textField);
            var init = field.initialCell;
            var player = new Player(init);
            var keys = new List<Keys>();
            keys.Add(Keys.Right);
            var game = new Game(player, field);
            var weed = field.weeds[0];
            player.CurrentPos.X++;
            game.FreezeWeed(weed);
            Assert.AreEqual(weed.WeedState, WeedStates.Freezed);
        }

        [Test]
        public void BulletFliesOutOfTheFieldTest()
        {
            var textField = new[]
            {
            "W@##",
            "####",
            "###P",
            "####"
            };
            var field = Field.FromLines(textField);
            var player = new Player(field.initialCell);
            var weed = new Weed(GetCurrentPosition(FieldCellStates.Weed, field).X, GetCurrentPosition(FieldCellStates.Weed, field).Y);
            var bullet = weed.Shoot();
            bullet.MoveBullet();
            bullet.MoveBullet();
            Assert.AreEqual(false, bullet.DeadInConflict(field, player));
            bullet.MoveBullet();
            Assert.AreEqual(true, bullet.DeadInConflict(field, player));
        }

        [Test]
        public void BulletKillsPlayerTest()
        {
            var textField = new[]
            {
            "#@#W",
            "####",
            "###P",
            "####"
            };
            var field = Field.FromLines(textField);
            var player = new Player(field.initialCell);
            var weed = new Weed(GetCurrentPosition(FieldCellStates.Weed, field).X, GetCurrentPosition(FieldCellStates.Weed, field).Y);
            var bullet = weed.Shoot();
            Assert.AreEqual(false, bullet.DeadInConflict(field, player));
            bullet.MoveBullet();
            Assert.AreEqual(true, bullet.DeadInConflict(field, player)); 
            Assert.AreEqual(16, player.Health);
        }

        [Test]
        public void ShootTest()
        {
            var weed = new Weed(2, 2);
            var bullet = weed.Shoot();
            Assert.AreEqual(bullet.X, 2);
            Assert.AreEqual(bullet.Y, 3);
            bullet.MoveBullet();
            Assert.AreEqual(bullet.Y, 4);
        }
        [Test]
        public void GameChangeStateWinTest()
        {
            var textField = new[]
            {
                "P@#",
                "###",
                "###"
            };
            var field = Field.FromLines(textField);
            var init = field.initialCell;
            var player = new Player(init);
            var keys = new List<Keys>();
            keys.Add(Keys.Right);
            var game = new Game(player, field);
            player.CurrentPos.X++;
            game.GameEnd();
            Assert.AreEqual(game.GameState, GameStates.Win);
        }

        [Test]
        public void IsGameChangeStateLoseTest()
        {
            var textField = new[]
            {
                "#W#",
                "P##",
                "###"
            };
            var field = Field.FromLines(textField);
            var init = field.initialCell;
            var player = new Player(init);
            player.Health -= 16;
            var keys = new List<Keys>();
            keys.Add(Keys.Right);
            var game = new Game(player, field);
            player.CurrentPos.X++;
            var weed = field.weeds[0];
            var bullet = weed.Shoot();
            bullet.DeadInConflict(field, player);
            game.GameEnd();
            Assert.AreEqual(GameStates.Lose, game.GameState);
        }

        [Test]
        public void BossSearchTest()
        {
            var textField = new[]
            {
            "W@##",
            "####",
            "###P",
            "####"
            };
            var field = Field.FromLines(textField);
            var expectedPathLength = 6;
            var realPathLength = Weed.BossSearch(
            field,
            GetCurrentPosition(FieldCellStates.Weed, field),
            GetCurrentPosition(FieldCellStates.Player, field)).Length;
            Assert.AreEqual(expectedPathLength, realPathLength);
        }

        [Test]
        public void SmallBossSearchTest()
        {
            var textField = new[]
            {
            "W#",
            "#P"
            };
            var field = Field.FromLines(textField);
            var expectedPathLength = 3;
            var realPathLength = Weed.BossSearch(
            field,
            GetCurrentPosition(FieldCellStates.Weed, field),
            GetCurrentPosition(FieldCellStates.Player, field)).Length;
            Assert.AreEqual(expectedPathLength, realPathLength);
        }

        public FieldCell GetCurrentPosition(FieldCellStates state, Field field)
        {
            for (var i = 0; i < field.Width; i++)
                for (int j = 0; j < field.Height; j++)
                    if (field.field[i, j] == state)
                        return new FieldCell(i, j, state);
            throw new Exception("Something went wrong");
        }
    }
}

//using System;
//using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
//using PacManNamespace.Models;

//namespace PacmanTest
//{
//    [TestClass]
//    public class PacTest
//    {

//        //Tests Pacman Position changes and image source changes
//        [TestMethod]
//        public void Move_AllDirections_Success()
//        {
//            Pacman p = new Pacman();

//            //Right
//            p.Direction = Direction.Right;
//            Position oldPos = p.Position;
//            p.Move();
//            Assert.IsTrue(p.Position.row == Math.Round(oldPos.row));
//            Assert.IsTrue(p.Position.col == oldPos.col + p.Speed);
//            Assert.IsTrue(p.CurrentImageUrl == "ms-appx:///Assets/Images/png/pacman-R.png");

//            //Down
//            p.Direction = Direction.Down;
//            oldPos = p.Position;
//            p.Move();
//            Assert.IsTrue(p.Position.col == Math.Round(oldPos.col));
//            Assert.IsTrue(p.Position.row == oldPos.row - p.Speed);
//            Assert.IsTrue(p.CurrentImageUrl == "ms-appx:///Assets/Images/png/pacman-D.png");

//            //Left
//            p.Direction = Direction.Left;
//            oldPos = p.Position;
//            p.Move();
//            Assert.IsTrue(p.Position.row == Math.Round(oldPos.row));
//            Assert.IsTrue(p.Position.col == oldPos.col - p.Speed);
//            Assert.IsTrue(p.CurrentImageUrl == "ms-appx:///Assets/Images/png/pacman-L.png");

//            //Up
//            p.Direction = Direction.Up;
//            oldPos = p.Position;
//            p.Move();
//            Assert.IsTrue(p.Position.col == Math.Round(oldPos.col));
//            Assert.IsTrue(p.Position.row == oldPos.row + p.Speed);
//            Assert.IsTrue(p.CurrentImageUrl == "ms-appx:///Assets/Images/png/pacman-U.png");

//            //None
//            p.Direction = Direction.None;
//            Assert.IsTrue(p.CurrentImageUrl == "ms-appx:///Assets/Images/png/pacman-N.png");
//        }

//    }
//}

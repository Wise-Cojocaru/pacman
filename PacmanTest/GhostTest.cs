//using System;
//using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
//using PacManNamespace.Models;
//using PacManNamespace.Models.Ghosts;

//namespace PacmanTest
//{
//    [TestClass]
//    public class GhostTest
//    {

//        //Tests all movements by ghosts based on direction
//        public void Move_AllDirections_Success()
//        {
//            Map map = new Map();
//            Ghost g = new Ghost(map, TileType.Blinky);

//            //Right
//            g.Direction = Direction.Right;
//            Position oldPos = g.Position;
//            g.Move();
//            Assert.IsTrue(g.Position.row == Math.Round(oldPos.row));
//            Assert.IsTrue(g.Position.col == oldPos.col + g.Speed);

//            //Down
//            g.Direction = Direction.Down;
//            oldPos = g.Position;
//            g.Move();
//            Assert.IsTrue(g.Position.col == Math.Round(oldPos.col));
//            Assert.IsTrue(g.Position.row == oldPos.row - g.Speed);

//            //Left
//            g.Direction = Direction.Left;
//            oldPos = g.Position;
//            g.Move();
//            Assert.IsTrue(g.Position.row == Math.Round(oldPos.row));
//            Assert.IsTrue(g.Position.col == oldPos.col - g.Speed);

//            //Up
//            g.Direction = Direction.Up;
//            oldPos = g.Position;
//            g.Move();
//            Assert.IsTrue(g.Position.col == Math.Round(oldPos.col));
//            Assert.IsTrue(g.Position.row == oldPos.row + g.Speed);

//            //None
//            g.Direction = Direction.None;
//        }
        
     
//    }
//}

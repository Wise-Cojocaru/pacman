using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using PacManNamespace.Models;

namespace PacmanTest
{
    [TestClass]
    public class PacTest
    {

        [TestMethod]
        public void Move_AllDirections_Success()
        {
            Pacman p = new Pacman();

            //Right
            p.Direction = Direction.Right;
            Position oldPos = p.Position;
            p.Move();
            Assert.IsTrue(p.Position.row == Math.Round(oldPos.row));
            Assert.IsTrue(p.Position.col == oldPos.col + p.Speed);

            //Down
            p.Direction = Direction.Down;
            oldPos = p.Position;
            p.Move();
            Assert.IsTrue(p.Position.col == Math.Round(oldPos.col));
            Assert.IsTrue(p.Position.row == oldPos.row - p.Speed);

            //Left
            p.Direction = Direction.Left;
            oldPos = p.Position;
            p.Move();
            Assert.IsTrue(p.Position.row == Math.Round(oldPos.row));
            Assert.IsTrue(p.Position.col == oldPos.col - p.Speed);

            //Up
            p.Direction = Direction.Up;
            oldPos = p.Position;
            p.Move();
            Assert.IsTrue(p.Position.col == Math.Round(oldPos.col));
            Assert.IsTrue(p.Position.row == oldPos.row + p.Speed);
        }
    }
}

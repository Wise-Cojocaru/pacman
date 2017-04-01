using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using PacManNamespace.Models;
using System.IO;

namespace PacmanTest
{
    [TestClass]
    public class MapTest
    {

        //Tests that all characters in map file assign correct TileTypes to each Tile in map.Maze.
        [TestMethod]
        public void LoadMap_AllTileTypes_Success()
        {
            Map map = new Map();
            string file = "~/../Assets/maze.txt";
            map.LoadMap(file);
            string input = File.ReadAllText(file);
            string[] lines = input.Split('\n');

            int row = 0;
            foreach (string s in lines)
            {
                Assert.IsTrue(s.Length == 28);
                row++;
                int col = 0;
                foreach (char c in s)
                {
                    switch (c)
                    {
                        case 'W':
                            Assert.IsTrue(map.Maze[row, col].Type == TileType.Wall);
                            break;
                        case 'E':
                            Assert.IsTrue(map.Maze[row, col].Type == TileType.Empty);
                            break;
                        case 'D':
                            Assert.IsTrue(map.Maze[row, col].Type == TileType.Dot);
                            break;
                        case 'V':
                            Assert.IsTrue(map.Maze[row, col].Type == TileType.MakeVulnerable);
                            break;
                        case 'B':
                            Assert.IsTrue(map.Maze[row, col].Type == TileType.Blinky);
                            break;
                        case 'I':
                            Assert.IsTrue(map.Maze[row, col].Type == TileType.Inky);
                            break;
                        case 'P':
                            Assert.IsTrue(map.Maze[row, col].Type == TileType.Pinky);
                            break;
                        case 'M':
                            Assert.IsTrue(map.Maze[row, col].Type == TileType.Pacman);
                            break;
                        case 'C':
                            Assert.IsTrue(map.Maze[row, col].Type == TileType.Clyde);
                            break;
                    }
                    col++;
                }
            }
        }
    }
}
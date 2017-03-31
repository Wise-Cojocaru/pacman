using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManNamespace.Models
{
    
    class Map
    {
        public Tile[,] Maze { get; set; }

        public Map()
        {
            Maze = new Tile[31, 28];
        }

        public Tile Collision(Direction direction, Tile tile)
        {
            int x = (int)(tile.Position.col);
            int y = (int)(tile.Position.row);
            if (direction == Direction.Left)
            {
                x = (int)(tile.Position.col - 0.2);
                return Maze[y, x];
            }
            if (direction == Direction.Right)
            {
                x = (int)(tile.Position.col + 1 + 0.2);
                return Maze[y, x];
            }
            if (direction == Direction.Down)
            {
                y = (int)(tile.Position.row + 1 + 0.2);
                return Maze[y, x ];
            }
            if (direction == Direction.Up)
            {
                y = (int)(tile.Position.row - 0.2);
                return Maze[y, x];
            }

            return null;
        }

        
    }
}
    

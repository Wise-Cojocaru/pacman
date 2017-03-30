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
                if (direction == Direction.Left)
                {
                    double dx = Math.Ceiling(tile.Position.col - 1);
                    return Maze[(int)tile.Position.row, (int)dx];
                }
                if (direction == Direction.Right)
                {
                    double dx = (int)tile.Position.col + 1;
                    return Maze[(int)tile.Position.row, (int)dx];
                }

                if (direction == Direction.Up)
                {
                    double dy = Math.Ceiling(tile.Position.row - 1);
                    return Maze[(int)dy, (int)tile.Position.col];
                }
                if (direction == Direction.Down)
                {
                    double dy = (int)(tile.Position.row) + 1;
                    return Maze[(int)dy, (int)tile.Position.col];
                }
            
            return null;
        }

        
    }
}
    

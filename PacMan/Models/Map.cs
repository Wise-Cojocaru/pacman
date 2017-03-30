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
            Maze = new Tile[24, 21];
            
        }

        public Tile Collision(Direction direction, Tile tile)
        {
            if(direction == Direction.Left)
            {
                double dx = tile.Position.X - tile.Speed;
                return Maze[(int)dx, (int)tile.Position.Y];
            }
            if (direction == Direction.Right)
            {
                double dx = Math.Ceiling(tile.Position.X + tile.Speed);
                return Maze[(int)dx, (int)tile.Position.Y];
            }

            if (direction == Direction.Up)
            {
                double dy = tile.Position.Y - tile.Speed;
                return Maze[(int)tile.Position.X, (int)dy];
            }
            if (direction == Direction.Down)
            {
                double dy = Math.Ceiling(tile.Position.Y + tile.Speed);
                return Maze[(int)tile.Position.X, (int)dy];
            }
            return null;
        }

        
    }
}
    

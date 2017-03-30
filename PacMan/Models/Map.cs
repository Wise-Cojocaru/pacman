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

        public Tile CheckCollision(Direction direction, Tile tile)
        {

            return null;
        }
    }
}
    

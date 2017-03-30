using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManNamespace.Models
{
    class Dot: Tile
    {
        public Dot(int i, int j)
        {
            this.Type = TileType.Dot;
            this.Speed = 0;
            this.Direction = Direction.Left;
            this.Position = new Position() { col = j, row = i};
            this.Value = 5;
        }
    }
}

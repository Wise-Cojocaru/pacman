using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManNamespace.Models.Ghosts
{
    public class Ghost : Tile
    {
        public bool Vulnerable { get; set; }
        public Ghost(TileType type)
        {
            if (type == TileType.Blinky)
            {
                CurrentImageUrl = "Blinky.png";
            }
            if (type == TileType.Inky)
            {
                CurrentImageUrl = "Inky.png";
            }
            if (type == TileType.Pinky)
            {
                CurrentImageUrl = "Pinky.png";
            }
            if (type == TileType.Clyde)
            {
                CurrentImageUrl = "Clyde.png";
            }

            if(Vulnerable)
            {
                CurrentImageUrl = "vulnghost.png";
            }

            this.Type = type;
        }

        public void Follow()
        {

        }

        public void ShortestPathTo()
        {

        }
    }
}

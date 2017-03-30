using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManNamespace.Models
{
    class Pacman : Tile
    {
        public Dictionary<Direction, string> Images = new Dictionary<Direction, string>()
            {
                { Direction.Left, "ms-appx:///Assets/Images/png/pacman-L.png" },
                { Direction.Right,  "ms-appx:///Assets/Images/png/pacman-R.png" },
                { Direction.Up,  "ms-appx:///Assets/Images/png/pacman-U.png" },
                { Direction.Down,  "ms-appx:///Assets/Images/png/pacman-D.png" }
            };
        public Pacman()
        {
            this.Type = TileType.Pacman;
            this.Speed = 0.1;
            this.Direction = Direction.Left;
            this.Position = new Position();
            this.isMoving = true;
        }

        public void Animate()
        {

        }
        

    }
}

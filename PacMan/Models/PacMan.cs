using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManNamespace.Models
{
    public class Pacman : Tile
    {
        
        public Pacman()
        {
            this.Type = TileType.Pacman;
            this.Speed = 0.2;
            this.Direction = Direction.Left;
            this.Position = new Position();
            this.isMoving = true;

            Images = new Dictionary<Direction, string>()
            {
                { Direction.Left, "ms-appx:///Assets/Images/png/pacman-L.png" },
                { Direction.Right,  "ms-appx:///Assets/Images/png/pacman-R.png" },
                { Direction.Up,  "ms-appx:///Assets/Images/png/pacman-U.png" },
                { Direction.Down,  "ms-appx:///Assets/Images/png/pacman-D.png" },
                {Direction.None, "ms-appx:///Assets/Images/png/pacman-N.png" }
            };
            this.CurrentImageUrl = Images[Direction.Left];
        }

        public override void Animate()
        {

            if (CurrentImageUrl == Images[Direction.None])
            {
                CurrentImageUrl = Images[Direction];
            }
            else
            {
                CurrentImageUrl = Images[Direction.None];
            }

        }
        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace PacManNamespace.Models
{
    public class Pacman : Tile
    {
        public int Score { get; set; }
        public DispatcherTimer MoveOneTile;
        public Pacman()
        {
            this.Type = TileType.Pacman;
            this.Speed = 0.2;
            this.Direction = Direction.Left;
            this.Position = new Position();
            this.isMoving = true;
            this.Score = 0;
            Images = new Dictionary<Direction, string>()
            {
                { Direction.Left, "pacman-L.png" },
                { Direction.Right,  "pacman-R.png" },
                { Direction.Up,  "pacman-U.png" },
                { Direction.Down,  "pacman-D.png" },
                { Direction.None, "pacman-N.png" }
            };
            this.CurrentImageUrl = Images[Direction];


            MoveOneTile = new DispatcherTimer();
            MoveOneTile.Tick += dispatcherTimer_Tick;
            MoveOneTile.Interval = new TimeSpan(0, 0, 0, 0, 5);
        }
        private void dispatcherTimer_Tick(object sender, object e)
        {
            Move();
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

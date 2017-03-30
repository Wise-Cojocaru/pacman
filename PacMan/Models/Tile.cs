using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManNamespace.Models
{
    enum Direction { Left, Up, Right, Down, None}

    enum TileType { Empty, Wall, Dot, Blinky, Clyde, Inky, Pinky, Pacman, MakeVulnerable}

    class Tile
    {
        public TileType Type { get; set; }

        public bool isMoving { get; set; }
        public Direction PreviousDirection { get; set; }
        public string ImageUrl { get; set; }

        public Position Position = new Position();

        public Position AbsPosition {
            get
            {
                AbsPosition.col = Position.col + 10;
                AbsPosition.row = Position.row + 10;
                return AbsPosition;
            }


            set
            {
                AbsPosition = value;
            }
        }
        public double Speed { get; set; }

        public Direction Direction { get; set; }

        public int Value { get; set; }

        public void Move()
        {
            if (this.Direction == Direction.Left) Position.col -= Speed;
            if (this.Direction == Direction.Right) Position.col += Speed;
            if (this.Direction == Direction.Up) Position.row -= Speed;
            if (this.Direction == Direction.Down) Position.row += Speed;
        }
        
        public void UndoMove(Direction direction)
        {
            if (direction == Direction.Left) Position.col += Speed;
            if (direction == Direction.Right) Position.col -= Speed;
            if (direction == Direction.Up) Position.row += Speed;
            if (direction == Direction.Down) Position.row -= Speed;

        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManNamespace.Models
{
    enum Direction { Left, Up, Right, Down, None}

    enum TileType { Empty, Wall, Dot, Blinky, Clyde, Inky, Pinky, Pacman }

    class Tile
    {
        public TileType Type { get; set; }

        public string ImageUrl { get; set; }

        public Position Position {get;set;}

        public Position AbsPosition { get; set; }
        public int Speed { get; set; }

        public Direction Direction { get; set; }

        public int Value { get; set; }

        public void Move(Direction direction)
        {
            if (direction == Direction.Left) Position.X -= 0.1 * Speed;
            if (direction == Direction.Right) Position.X += 0.1 * Speed;
            if (direction == Direction.Up) Position.Y -= 0.1 * Speed;
            if (direction == Direction.Down) Position.Y += 0.1 * Speed;



        }

        public void UndoMove(Direction direction)
        {
            if (direction == Direction.Left) Position.X += 0.1 * Speed;
            if (direction == Direction.Right) Position.X -= 0.1 * Speed;
            if (direction == Direction.Up) Position.Y += 0.1 * Speed;
            if (direction == Direction.Down) Position.Y -= 0.1 * Speed;

        }
    }
}

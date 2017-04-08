using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace PacManNamespace.Models
{
    public enum Direction { Left, Up, Right, Down, None}

    public enum TileType { Empty, Wall, Dot, Blinky, Clyde, Inky, Pinky, Pacman, MakeVulnerable}

    
    public class Tile : Serialization
    {
        public Dictionary<Direction, string> Images;
        public TileType Type { get; set; }

        public bool isMoving { get; set; }

        public bool Vulnerable { get; set; }

        public Direction PreviousDirection = Direction.Left;
        public string CurrentImageUrl { get; set; }

        public event EventHandler<int> TileChanged;

        public Position Position = new Position();

        public Position StartPosition = new Position();

        public Position PrevPosition = new Position();

        public Position AbsPosition { get; set; }
        public double Speed { get; set; }

        public Direction Direction { get; set; }

        public int Value { get; set; }

        public int counter { get; set; }
        public Tile()
        {
            

            
        }

       

        public virtual void Move()
        {
            PrevPosition = Position;
            
            if (this.Direction == Direction.Left)
            {
                Position.row = Math.Round(Position.row);
                Position.col -= Speed;
            }

            if (this.Direction == Direction.Right)
            {
                Position.row = Math.Round(Position.row);
                Position.col += Speed;
            }

            if (this.Direction == Direction.Up)
            {
                Position.col = Math.Round(Position.col);
                Position.row -= Speed;
            }
            if (this.Direction == Direction.Down)
            {
                Position.col = Math.Round(Position.col);
                Position.row += Speed;
            }

           
        }
        
        public void UndoMove(Direction direction)
        {
            if (direction == Direction.Left) Position.col += Speed;
            if (direction == Direction.Right) Position.col -= Speed;
            if (direction == Direction.Up) Position.row += Speed;
            if (direction == Direction.Down) Position.row -= Speed;

        }
        public virtual void Animate()
        {
            

        }

        public virtual string Serialize()
        {
            throw new NotImplementedException();
        }

        public virtual void Deserialize()
        {
            throw new NotImplementedException();
        }
    }
}

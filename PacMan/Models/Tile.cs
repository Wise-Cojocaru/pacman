//------------------------------------------------------------------------------
// This class defines a structure of every object on the map. Each object that 
// can be added in the game derives from this class.
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace PacManNamespace.Models
{
    //enums
    public enum Direction { Left, Up, Right, Down, None}
    public enum TileType { Empty, Wall, Dot, Blinky, Clyde, Inky, Pinky, Pacman, MakeVulnerable, Bullet}

    public class Tile : Serialization
    {
        //a dictionary of image names
        public Dictionary<Direction, string> Images;
        //property which holds tile's type
        public TileType Type { get; set; }
        //is moving or not flag
        public bool isMoving { get; set; }
        // flag tat tells if the tile is vulnerable
        public bool Vulnerable { get; set; }
        //variable which holds previus direction
        public Direction PreviousDirection = Direction.Left;
        //property that holds current image string
        public string CurrentImageUrl { get; set; }
        // current tile's position
        public Position Position = new Position();
        // current tile's start position
        public Position StartPosition = new Position();
        // current tile's previous position
        public Position PrevPosition = new Position();
        //tile's speed
        public double Speed { get; set; }
        //tile's diretion
        public Direction Direction { get; set; }
        //tile's value
        public int Value { get; set; }
        //tile's constructor
        public Tile()
        {
            

            
        }
        //method that moves the tile by a small amount
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
        //method that animates the tile (overloaded)
        public virtual void Animate()
        {
            

        }
        //serialization method
        public virtual string Serialize()
        {
            string str = "";
            str += Math.Round(Position.row) + " ";
            str += Math.Round(Position.col) + ",";
            return str;
        }
        //deserialization method
        public virtual void Deserialize()
        {
            throw new NotImplementedException();
        }
    }
}

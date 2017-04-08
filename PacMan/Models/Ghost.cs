using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManNamespace.Models.Ghosts
{
    public class Ghost : Tile
    {
        Random r = new Random();
        public bool Vulnerable { get; set; }
        public List<Direction> DirectionsToChoose = new List<Direction>();
        public Map map { get; set; }
        public Ghost(Map map, TileType type)
        {
            this.Direction = Direction.Up;
            this.Speed = 0.2;
            this.map = map;

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
        public override void Move()
        {
            ChooseDirection();
            base.Move();

        }
        public void ChooseDirection()
        {
            int currRow = (int)Math.Round(this.Position.row);
            int currCol = (int)Math.Round(this.Position.col);

            double colDiff = Position.col - map.Characters[ObjectType.Pacman].Position.col;
            double rowDiff = Position.row - map.Characters[ObjectType.Pacman].Position.row;

            Tile tempTile = map.Collision(this.Direction, this);

            switch (tempTile.Type)
            {
                case TileType.Wall:

                    this.Position.row = Math.Round(this.Position.row);
                    this.Position.col = Math.Round(this.Position.col);
                    if (map.Maze[currRow + 1, currCol].Type != TileType.Wall)
                        DirectionsToChoose.Add(Direction.Down);
                    if (map.Maze[currRow - 1, currCol].Type != TileType.Wall)
                        DirectionsToChoose.Add(Direction.Up);
                    if (map.Maze[currRow, currCol + 1].Type != TileType.Wall)
                        DirectionsToChoose.Add(Direction.Right);
                    if (map.Maze[currRow, currCol - 1].Type != TileType.Wall)
                        DirectionsToChoose.Add(Direction.Left);

                    if(Math.Abs(colDiff) > Math.Abs(rowDiff))
                    {
                        if (colDiff < 0) Direction = Direction.Right;
                        else Direction = Direction.Left;

                    }
                    else
                    {
                        if (rowDiff < 0) Direction = Direction.Down;
                        else Direction = Direction.Up;
                    }

                    //this.Direction = DirectionsToChoose[Math.Abs(r.Next(-DirectionsToChoose.Count + 1, DirectionsToChoose.Count - 1))];
                    DirectionsToChoose = new List<Direction>();
                    break;
            }
        }

    }
}

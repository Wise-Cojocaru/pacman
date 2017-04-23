//------------------------------------------------------------------------------
// This class implements a Ghost model
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManNamespace.Models.Ghosts
{
    public class Ghost : Tile
    {
        //randomization class instance
        Random r = new Random();
        //a list of direction the ghost can go
        public List<Direction> DirectionsToChoose = new List<Direction>();
        //property which tells if the ghost has shot a bullet already
        public bool BulletShot { get; set; }
        // reference to current map
        public Map map { get; set; }
        //ghost's constructor
        public Ghost(Map map, TileType type)
        {
            this.Vulnerable = false;
            this.Direction = Direction.Up;
            this.Speed = 0.1;
            this.map = map;

            if (type == TileType.Blinky)
            {
                CurrentImageUrl = "Blinky.png";
                StartPosition.col = 15;
                StartPosition.row = 15;
            }
            if (type == TileType.Inky)
            {
                CurrentImageUrl = "Inky.png";
                StartPosition.col = 13;
                StartPosition.row = 15;
            }
            if (type == TileType.Pinky)
            {
                CurrentImageUrl = "Pinky.png";
                StartPosition.col = 12;
                StartPosition.row = 15;
            }
            if (type == TileType.Clyde)
            {
                CurrentImageUrl = "Clyde.png";
                StartPosition.col = 14;
                StartPosition.row = 15;
            }
            if (Vulnerable)
            {
                CurrentImageUrl = "vulnghost.png";
            }


            this.Type = type;
        }
        //a method that makes the ghost vulnerable
        public void MakeVulnerable()
        {
            CurrentImageUrl = "vulnghost.png";
            Vulnerable = true;

        }
        // a method that makes the ghost normal again
        public void MakeNormal()
        {
           CurrentImageUrl = Type.ToString() + ".png";
           Vulnerable = false;   
        }
        //a method that moves the ghost in a certain direction
        public override void Move()
        {
            ChooseDirection();
            base.Move();

        }
        // a method that chooses the direction
        public void ChooseDirection()
        {
            int currRow = (int)Math.Round(this.Position.row);
            int currCol = (int)Math.Round(this.Position.col);

            double colDiff = Position.col - map.Characters[ObjectType.Pacman].Position.col;
            double rowDiff = Position.row - map.Characters[ObjectType.Pacman].Position.row;

            Tile tempTile = map.Collision(this.Direction, this);
            Direction temp;

            if (Math.Abs(colDiff) > Math.Abs(rowDiff))
            {
                if (colDiff < 0) temp = Direction.Right;
                else temp = Direction.Left;

            }
            else
            {
                if (rowDiff < 0) temp = Direction.Down;
                else temp = Direction.Up;
            }
            switch (tempTile.Type)
            {
                case TileType.Wall:

                    this.Position.row = Math.Round(this.Position.row);
                    this.Position.col = Math.Round(this.Position.col);
                    if (map.Maze[currRow + 1, currCol].Type != TileType.Wall && Direction != Direction.Up)
                        DirectionsToChoose.Add(Direction.Down);
                    if (map.Maze[currRow - 1, currCol].Type != TileType.Wall && Direction != Direction.Down)
                        DirectionsToChoose.Add(Direction.Up);
                    if (map.Maze[currRow, currCol + 1].Type != TileType.Wall && Direction != Direction.Left)
                        DirectionsToChoose.Add(Direction.Right);
                    if (map.Maze[currRow, currCol - 1].Type != TileType.Wall && Direction != Direction.Right)
                        DirectionsToChoose.Add(Direction.Left);

                    if (DirectionsToChoose.Contains(temp))
                        this.Direction = temp;
                    else
                        this.Direction = DirectionsToChoose[Math.Abs(r.Next(-DirectionsToChoose.Count + 1, DirectionsToChoose.Count - 1))];
                   
                    break;
            }
            if(DirectionsToChoose.Count == 3)
            {
                this.Direction = temp;
            }
            
            if ((Math.Abs(colDiff) <= 0.1 || Math.Abs(rowDiff) <= 0.1) && !BulletShot)
            {
                Bullet b = new Bullet();
                map.Bullets.Add(b);
                b.Position.row = Math.Round( this.Position.row);
                b.Position.col = Math.Round(this.Position.col);
                b.Direction = this.Direction;

                Task t = Task.Run(() =>
                {
                    b.isMoving = true;
                    this.BulletShot = true;
                    Tile tile = map.Collision(b.Direction, b);
                    while (tile.Type != TileType.Wall && !map.CollisionWithPacman(b))
                    {
                        
                        b.Move();
                        Task.Delay(TimeSpan.FromMilliseconds(5)).Wait();
                        tile = map.Collision(b.Direction, b);

                    }
                    if(tile.Type != TileType.Wall)
                        map.CollidedWithPac = true;

                    b.isMoving = false;
                    this.BulletShot = false;

                });
            }
            DirectionsToChoose = new List<Direction>();
        }

    }
}

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
        

        public List<Direction> DirectionsToChoose = new List<Direction>();
        public bool BulletShotAndHitPacman { get; set; }
        public bool BulletShot { get; set; }
        public Map map { get; set; }
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
        public void MakeVulnerable()
        {
            Vulnerable = true;
            CurrentImageUrl = "vulnghost.png";
            
        }

        public void MakeNormal()
        {
           CurrentImageUrl = Type.ToString() + ".png";
           Vulnerable = false;   
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
            bool checkifToShoot = false;



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
                    while (tile.Type != TileType.Wall &&  !map.CollisionWithPacman(b))
                    {
                        b.Move();
                        Task.Delay(TimeSpan.FromMilliseconds(5)).Wait();
                        tile = map.Collision(b.Direction, b);
                    }
                    if (map.CollisionWithPacman(b))
                        map.CollidedWithPac = true;
                    else
                        map.CollidedWithPac = false;

                    b.isMoving = false;
                    this.BulletShot = false;

                });
            }
            DirectionsToChoose = new List<Direction>();
        }

    }
}

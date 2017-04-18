using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacManNamespace.Models;
using PacManNamespace.Models.Ghosts;

namespace PacManNamespace.Models
{
    
    public class Map
    {
        public Tile[,] Maze { get; set; }

        public Dictionary<ObjectType, Tile> Characters = new Dictionary<ObjectType, Tile>();

        public List<Tile> Bullets = new List<Tile>();
        public int CurrentLevel { get; set;}

        public bool CollidedWithPac { get; set; }

        public List<Tile> Dots = new List<Tile>();
        public Map()
        {
            Maze = new Tile[31, 28];
        }

        public void MakeGhostVulnerable()
        {
            foreach (ObjectType Type in Enum.GetValues(typeof(ObjectType)))
            {
                if (Type != ObjectType.Pacman)
                {
                    ((Ghost)Characters[Type]).MakeVulnerable();

                }
            }
        }
        public Tile CollisionWithGhost()
        {
            int pacRow = (int)(Characters[ObjectType.Pacman].Position.row);
            int pacCol = (int)(Characters[ObjectType.Pacman].Position.col);
            foreach (ObjectType key in Characters.Keys)
            {
                if (key != ObjectType.Pacman)
                {
                    if ((int)(Characters[key].Position.col) == pacCol && (int)(Characters[key].Position.row) == pacRow)
                    {
                        return Characters[key];
                    }
                }
            }
            return null;

        }

        public bool CollisionWithBullet()
        {
            return this.CollidedWithPac;
        }
        public bool CollisionWithPacman(Bullet b)
        {
            int pacRow = (int)(Characters[ObjectType.Pacman].Position.row);
            int pacCol = (int)(Characters[ObjectType.Pacman].Position.col);


            if ((int)(b.Position.col) == pacCol && (int)(b.Position.row) == pacRow)
            {
                return true;
            }

            return false;
        }
        public Tile Collision(Direction direction, Tile tile)
        {
            int x = (int)(tile.Position.col);
            int y = (int)(tile.Position.row);
            if (direction == Direction.Left)
            {
                x = (int)(tile.Position.col - Characters[ObjectType.Pacman].Speed);
                return Maze[y, x];
            }
            if (direction == Direction.Right)
            {
                x = (int)(tile.Position.col + 1 + Characters[ObjectType.Pacman].Speed);
                return Maze[y, x];
            }
            if (direction == Direction.Down)
            {
                y = (int)(tile.Position.row + 1 + Characters[ObjectType.Pacman].Speed);
                return Maze[y, x ];
            }
            if (direction == Direction.Up)
            {
                y = (int)(tile.Position.row - Characters[ObjectType.Pacman].Speed);
                return Maze[y, x];
            }
            return null;
        }
        public void RemoveFromMap(Tile t)
        {
            this.Dots.Remove(t);
            Tile temp = new Tile();
            temp.Type = TileType.Empty;
            Maze[(int)t.Position.row, (int)t.Position.col] = temp;
        }
        public void MoveTile(Tile t)
        {
            int i = (int)Math.Round(t.Position.row);
            int j = (int)Math.Round(t.Position.col);


            Tile temp = new Tile();
            temp.Type = TileType.Empty;

            Maze[(int)t.PrevPosition.row, (int)t.PrevPosition.col] = temp;
            Maze[i, j] = t;
            
        }
        public void LoadMap(string path)
        {
            String input = File.ReadAllText("~/../Assets/maze.txt");
            int i = 0, j = 0;
            Tile tempTile = null;
           
            // var dialog = new Windows.UI.Popups.MessageDialog(col.ToString());
            //await dialog.ShowAsync();
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                char[] str = row.Trim().ToCharArray();
                foreach (var col in str)
                {

                    if (col == 'W')
                    {
                        tempTile = new Tile();
                        tempTile.Type = TileType.Wall;
                    }
                    if (col == 'E')
                    {
                        tempTile = new Tile();
                        tempTile.Type = TileType.Empty;

                    }
                    if (col == 'D')
                    {
                        tempTile = new Tile();
                        tempTile.Type = TileType.Dot;
                        tempTile.CurrentImageUrl = "dot.png";
                        tempTile.Value = 1;
                        Dots.Add(tempTile);

                    }
                   
                    if (col == 'V')
                    {
                        tempTile = new Tile();
                        tempTile.Type = TileType.MakeVulnerable;
                        tempTile.CurrentImageUrl = "makevuln.png";
                        tempTile.Value = 10;
                        Dots.Add(tempTile);

                    }
                    if (col == 'B')
                    {
                        tempTile = new Ghost(this, TileType.Blinky);
                        Characters[ObjectType.Blinky] = tempTile;
                        tempTile.Value = 5;
                    }
                    if (col == 'I')
                    {
                        tempTile = new Ghost(this, TileType.Inky);
                        Characters[ObjectType.Inky] = tempTile;
                        tempTile.Value = 5;
                    }
                    if (col == 'P')
                    {
                        tempTile = new Ghost(this, TileType.Pinky);
                        Characters[ObjectType.Pinky] = tempTile;
                        tempTile.Value = 5;
                    }
                    if (col == 'C')
                    {
                        tempTile = new Ghost(this, TileType.Clyde);
                        Characters[ObjectType.Clyde] = tempTile;
                        tempTile.Value = 5;
                    }
                    if (col == 'M')
                    {
                        tempTile = new Pacman();
                        Characters[ObjectType.Pacman] = tempTile;
                        
                    }

                    if (tempTile != null)
                    {
                        tempTile.Position.col = j;
                        tempTile.Position.row = i;

                        tempTile.PrevPosition.col = j;
                        tempTile.PrevPosition.row = i;
                        this.Maze[i, j] = tempTile;
                    }

                    j++;
                }
                i++;
            }
        }
        public string Serialize()
        {
            string str = "";

            foreach (Tile dot in Dots)
            {
                str += string.Format("dot {0} {1}", dot.Position.row, dot.Position.col);
                str += ",";
            }

            foreach(var KeyValuePair in Characters)
            {
                str += KeyValuePair.Key + " ";
                str += KeyValuePair.Value.Serialize();
            }

            return str;
        }

        public void DeSerialize()
        {
            string path = "";
            Dots.Clear();
            FileStream stream = File.OpenRead(path);
            string data = File.ReadAllText(path);
            string[] dataSplit = data.Split(',');

            for (int i = 0; i < dataSplit.Length; i++)
            {
                string[] dataInfo = dataSplit[i].Split(' ');

                if (dataInfo[0] == "dot")
                {
                    var tempTile = new Tile();
                    tempTile.Type = TileType.Dot;
                    tempTile.Position.row = Convert.ToInt32(dataInfo[1]);
                    tempTile.Position.col = Convert.ToInt32(dataInfo[2]);
                    Dots.Add(tempTile);
                }

                else if (dataInfo[0] == "ObjectType.Pacman")
                {
                    Characters[ObjectType.Pacman].Position.row = Convert.ToInt32(dataInfo[1]);
                    Characters[ObjectType.Pacman].Position.col = Convert.ToInt32(dataInfo[2]);

                }
                else if (dataInfo[0] == "ObjectType.Inky")
                {
                    Characters[ObjectType.Inky].Position.row = Convert.ToInt32(dataInfo[1]);
                    Characters[ObjectType.Inky].Position.col = Convert.ToInt32(dataInfo[2]);
                }
                else if (dataInfo[0] == "ObjectType.Pinky")
                {
                    Characters[ObjectType.Pinky].Position.row = Convert.ToInt32(dataInfo[1]);
                    Characters[ObjectType.Pinky].Position.col = Convert.ToInt32(dataInfo[2]);
                }
                else if (dataInfo[0] == "ObjectType.Blinky")
                {
                    Characters[ObjectType.Blinky].Position.row = Convert.ToInt32(dataInfo[1]);
                    Characters[ObjectType.Blinky].Position.col = Convert.ToInt32(dataInfo[2]);
                }
                else if (dataInfo[0] == "ObjectType.Clyde")
                {
                    Characters[ObjectType.Clyde].Position.row = Convert.ToInt32(dataInfo[1]);
                    Characters[ObjectType.Clyde].Position.col = Convert.ToInt32(dataInfo[2]);
                }
            }
        }

    }
}
    

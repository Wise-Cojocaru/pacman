//------------------------------------------------------------------------------
// This class implements a Pacman Map model. Contains references to all the
// characters and objects on the map
//------------------------------------------------------------------------------
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
        //map's array reprezentation
        public Tile[,] Maze { get; set; }
        // dictionary that holds the game's characters
        public Dictionary<ObjectType, Tile> Characters = new Dictionary<ObjectType, Tile>();
        // list which contains all the bullets
        public List<Tile> Bullets = new List<Tile>();
        // property that contains the current level
        public int CurrentLevel { get; set;}
        // flag that tells if a bulle collided with the Pacman
        public bool CollidedWithPac = false;
        // a list that contains all the dots on the map
        public List<Tile> Dots = new List<Tile>();
        // a list of all the dots that make pacman vulnerable
        public List<Tile> MakeVulns = new List<Tile>();
        //tells if map is being loaded or not
        public bool isLoading = false;
        //map's constructor
        public Map()
        {
            Maze = new Tile[31, 28];
        }
        //method that makes the ghosts vulnerable
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
        //method that checks the collision with ghosts
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
        //method that checks if the bullet collides with pacman
        public bool CollisionWithPacman(Bullet b)
        {
            int pacRow = (int)Math.Round(Characters[ObjectType.Pacman].Position.row);
            int pacCol = (int)Math.Round(Characters[ObjectType.Pacman].Position.col);

            if ((int)Math.Round(b.Position.col) == pacCol && (int)Math.Round(b.Position.row) == pacRow)
            {
                CollidedWithPac = true;
                return true;
            }
            return false;
        }
        //method that checks if pacman collides with any object from the map (wall, dots)
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
        //method that removes a tile from the maze
        public void RemoveFromMap(Tile t)
        {
            if (t.Type == TileType.Dot)
                this.Dots.Remove(t);
            else
                this.MakeVulns.Remove(t);
            Tile temp = new Tile();
            temp.Type = TileType.Empty;
            Maze[(int)t.Position.row, (int)t.Position.col] = temp;
        }
        //method that moves a tile from the maze
        public void MoveTile(Tile t)
        {
            int i = (int)Math.Round(t.Position.row);
            int j = (int)Math.Round(t.Position.col);


            Tile temp = new Tile();
            temp.Type = TileType.Empty;

            Maze[(int)t.PrevPosition.row, (int)t.PrevPosition.col] = temp;
            Maze[i, j] = t;
            
        }
        //method that loads a map from file
        public void LoadMap(string path)
        {
            string input;
            if(!isLoading)
                input = File.ReadAllText("~/../Assets/maze.txt");
            else
                input = File.ReadAllText("~/../Assets/loadmaze.txt");
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
                            MakeVulns.Add(tempTile);

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
        //method that serializes the current map to a file
        public string Serialize()
        {
            string str = "";

            foreach (Tile dot in Dots)
            {
                str += string.Format("dot {0} {1}", dot.Position.row, dot.Position.col);
                str += ",";
            }

            foreach(Tile makeVuln in MakeVulns)
            {
                str += string.Format("mv {0} {1}", makeVuln.Position.row, makeVuln.Position.col);
                str += ",";
            }

            foreach(var KeyValuePair in Characters)
            {
                str += KeyValuePair.Key + " ";
                str += KeyValuePair.Value.Serialize();
            }

            return str;
        }
        //method that deserializes a map from file
        public void DeSerialize(string data)
        {
            string[] dataSplit = data.Split(',');

            for (int i = 0; i < dataSplit.Length; i++)
            {
                string[] dataInfo = dataSplit[i].Split(' ');

                if (dataInfo[0] == "dot")
                {
                    var tempTile = new Tile();
                    tempTile.Type = TileType.Dot;
                    tempTile.CurrentImageUrl = "dot.png";
                    tempTile.Value = 1;
                    Dots.Add(tempTile);
                    tempTile.Position.col = Convert.ToInt32(dataInfo[2]);
                    tempTile.Position.row = Convert.ToInt32(dataInfo[1]);

                    tempTile.PrevPosition.col = Convert.ToInt32(dataInfo[2]);
                    tempTile.PrevPosition.row = Convert.ToInt32(dataInfo[1]);
                    this.Maze[Convert.ToInt32(dataInfo[1]), Convert.ToInt32(dataInfo[2])] = tempTile;
                }

                else if (dataInfo[0] == "mv")
                {
                    var tempTile = new Tile();
                    tempTile.Type = TileType.MakeVulnerable;
                    tempTile.CurrentImageUrl = "makevuln.png";
                    tempTile.Value = 10;
                    MakeVulns.Add(tempTile);
                    tempTile.Position.col = Convert.ToInt32(dataInfo[2]);
                    tempTile.Position.row = Convert.ToInt32(dataInfo[1]);

                    tempTile.PrevPosition.col = Convert.ToInt32(dataInfo[2]);
                    tempTile.PrevPosition.row = Convert.ToInt32(dataInfo[1]);
                    this.Maze[Convert.ToInt32(dataInfo[1]), Convert.ToInt32(dataInfo[2])] = tempTile;
                }

                else if (dataInfo[0] == "Pacman")
                {
                    Characters[ObjectType.Pacman].Position.row = Convert.ToInt32(dataInfo[1]);
                    Characters[ObjectType.Pacman].Position.col = Convert.ToInt32(dataInfo[2]);

                }
                else if (dataInfo[0] == "Inky")
                {
                    Characters[ObjectType.Inky].Position.row = Convert.ToInt32(dataInfo[1]);
                    Characters[ObjectType.Inky].Position.col = Convert.ToInt32(dataInfo[2]);
                }
                else if (dataInfo[0] == "Pinky")
                {
                    Characters[ObjectType.Pinky].Position.row = Convert.ToInt32(dataInfo[1]);
                    Characters[ObjectType.Pinky].Position.col = Convert.ToInt32(dataInfo[2]);
                }
                else if (dataInfo[0] == "Blinky")
                {
                    Characters[ObjectType.Blinky].Position.row = Convert.ToInt32(dataInfo[1]);
                    Characters[ObjectType.Blinky].Position.col = Convert.ToInt32(dataInfo[2]);
                }
                else if (dataInfo[0] == "Clyde")
                {
                    Characters[ObjectType.Clyde].Position.row = Convert.ToInt32(dataInfo[1]);
                    Characters[ObjectType.Clyde].Position.col = Convert.ToInt32(dataInfo[2]);
                }
            }
        }

    }
}
    

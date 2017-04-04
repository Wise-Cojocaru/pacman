using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacManNamespace.Models.Ghosts;

namespace PacManNamespace.Models
{
    
    public class Map : Serialization
    {
        public Tile[,] Maze { get; set; }

        public Dictionary<ObjectType, Tile> Characters = new Dictionary<ObjectType, Tile>();

        public List<Tile> Dots = new List<Tile>();
        public Map()
        {
            Maze = new Tile[31, 28];
        }

        public Tile Collision(Direction direction, Tile tile)
        {
            int x = (int)(tile.Position.col);
            int y = (int)(tile.Position.row);
            if (direction == Direction.Left)
            {
                x = (int)(tile.Position.col - 0.2);
                return Maze[y, x];
            }
            if (direction == Direction.Right)
            {
                x = (int)(tile.Position.col + 1 + 0.2);
                return Maze[y, x];
            }
            if (direction == Direction.Down)
            {
                y = (int)(tile.Position.row + 1 + 0.2);
                return Maze[y, x ];
            }
            if (direction == Direction.Up)
            {
                y = (int)(tile.Position.row - 0.2);
                return Maze[y, x];
            }

            return null;
        }

        public void MoveTile(Tile t, Direction dir)
        {

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
                        Dots.Add(tempTile);

                    }
                    if (col == 'V')
                    {
                        tempTile = new Tile();
                        tempTile.Type = TileType.MakeVulnerable;

                    }
                    if (col == 'B')
                    {
                        tempTile = new Blinky();
                        tempTile.Type = TileType.Blinky;

                        Characters[ObjectType.Blinky] = tempTile;
                    }
                    if (col == 'I')
                    {
                        tempTile = new Inky();
                        tempTile.Type = TileType.Inky;

                        Characters[ObjectType.Inky] = tempTile;
                    }
                    if (col == 'P')
                    {
                        tempTile = new Pinky();
                        tempTile.Type = TileType.Pinky;

                        Characters[ObjectType.Pinky] = tempTile;
                    }
                    if (col == 'M')
                    {
                        tempTile = new Pacman();
                        tempTile.Type = TileType.Pacman;

                        Characters[ObjectType.Pacman] = tempTile;
                    }

                    if (col == 'C')
                    {
                        tempTile = new Clyde();
                        tempTile.Type = TileType.Clyde;

                        Characters[ObjectType.Clyde] = tempTile;
                    }

                    if (tempTile != null)
                    {
                        tempTile.Position.col = j;
                        tempTile.Position.row = i;
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
            for (int i = 0; i < 31; i++)
            {
                string Line = "";

                for(int j = 0; j < 28; j++)
                {
                    if (Maze[i,j].Type == TileType.Wall)
                    {
                        Line += "W";
                    }
                    if (Maze[i, j].Type == TileType.Empty)
                    {
                        Line += "E";
                    }
                    if (Maze[i, j].Type == TileType.Dot)
                    {
                        Line += "D";

                    }
                    if (Maze[i, j].Type == TileType.MakeVulnerable)
                    {
                        Line += "V";

                    }
                    if (Maze[i, j].Type == TileType.Blinky)
                    {
                        Line += "B";
                    }
                    if (Maze[i, j].Type == TileType.Inky)
                    {
                        Line += "I";
                    }
                    if (Maze[i, j].Type == TileType.Pinky)
                    {
                        Line += "P";
                    }
                    if (Maze[i, j].Type == TileType.Pacman)
                    {
                        Line += "M";
                    }

                    if (Maze[i, j].Type == TileType.Clyde)
                    {
                        Line += "C";
                    }

                }
                Line += "\n";
                str += Line;
                Line = "";

            }

            foreach(var KeyValuePair in Characters)
            {
                str += KeyValuePair.Value.Serialize();
                str += "\n";
            }

            return str;
        }

        public void Deserialize()
        {
            throw new NotImplementedException();
        }
    }
}
    

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacManNamespace.Models;
using PacManNamespace.Models.Ghosts;
using System.IO;

namespace PacManNamespace
{
    public enum ObjectType { Pacman, Blinky, Pinky, Inky, Clyde}

    public enum Level { Easy, Medium, Hard}
    public enum PacmanState { Moving, Stopped}
    class GameController
    {
        public List<Map> Maps = new List<Map>();

        public Dictionary<ObjectType, Tile> Objects = new Dictionary<ObjectType, Tile>();

        

        public GameController()
        {

        }

        public Position MovePacman(Direction direction)
        {
            



            
            
        }
        public void Init()
        {
            Maps.Add(new Map());
            LoadMap("", Maps[0]);
        }

        public void LoadMap(string path, Map map)
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
                        tempTile = new Dot(i, j);
                        tempTile.Type = TileType.Dot;

                    }
                    if (col == 'B')
                    {
                        tempTile = new Blinky();
                        tempTile.Type = TileType.Blinky;
                        Objects[ObjectType.Blinky] = tempTile;
                    }
                    if (col == 'I')
                    {
                        tempTile = new Inky();
                        tempTile.Type = TileType.Inky;
                        Objects[ObjectType.Inky] = tempTile;
                    }
                    if (col == 'P')
                    {
                        tempTile = new Pinky();
                        tempTile.Type = TileType.Pinky;
                        Objects[ObjectType.Pinky] = tempTile;
                    }
                    if (col == 'M')
                    {
                        
                        tempTile = new Pacman();
                        tempTile.Type = TileType.Pacman;
                        Objects[ObjectType.Pacman] = tempTile;
                    }

                    if (col == 'C')
                    {
                        tempTile = new Clyde();
                        tempTile.Type = TileType.Clyde;
                        tempTile.ImageUrl = "ms-appx:///Assets/big-yummy.bmp";
                        Objects[ObjectType.Clyde] = tempTile;
                    }
                    if (tempTile != null)
                        map.Maze[i, j] = tempTile;
                    j++;
                }
                i++;
            }
        }
    }
}

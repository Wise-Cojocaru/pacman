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
    public enum GameState { Playing, GameOver}
    class GameController
    {
        public List<Map> Maps = new List<Map>();

        public Dictionary<ObjectType, Tile> Characters = new Dictionary<ObjectType, Tile>();
        public List<Tile> Dots = new List<Tile>();
        public GameState GameState { get; set; }
        public Tile Pacman { get; set; }
        public Tile Blinky { get; set; }
        public Tile Pinky { get; set; }
        public Tile Inky { get; set; }
        public Tile Clyde { get; set; }
        
        public GameController()
        {

        }

        public void MovePacman()
        {
            Tile tempTile = Maps[0].Collision(Pacman.Direction, Pacman);
            switch(tempTile.Type)
            {
                case TileType.Blinky:
                    
                    break;
                case TileType.Pinky:
                    break;
                case TileType.Inky:
                    break;
                case TileType.Clyde:
                    break;
                case TileType.Wall:

                    Pacman.isMoving = false;
                    break;
                case TileType.Dot:

                    Pacman.Move();
                    break;
                default:
                    Pacman.Move();
                    break;
            }

        }
        public void Init()
        {
            
            LoadMap("");

            Pacman = Characters[ObjectType.Pacman];
            Blinky = Characters[ObjectType.Blinky];
            Pinky = Characters[ObjectType.Pinky];
            Inky = Characters[ObjectType.Inky];
            Clyde = Characters[ObjectType.Clyde];
        }

        public void LoadMap(string path)
        {
            Maps.Add(new Map());
            Map map = Maps[0];
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
                        map.Maze[i, j] = tempTile;
                    }

                    j++;
                }
                i++;
            }
        }
    }
}

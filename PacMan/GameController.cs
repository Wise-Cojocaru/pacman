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
    //
    public interface Serialization
    {
        string Serialize();
        void Deserialize();
    }
    public enum ObjectType { Pacman, Blinky, Pinky, Inky, Clyde}
    
    public enum Level { First, Second, Third}
    public enum GameState { Playing, GameOver}
    public class GameController
    {
        public List<Map> Maps = new List<Map>();

        
        public GameState GameState { get; set; }
        public Level CurrentLevel { get; set; }
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

                    Pacman.Position.row = Math.Round(Pacman.Position.row);
                    Pacman.Position.col = Math.Round(Pacman.Position.col);
                    break;
                case TileType.Dot:
                    ((Pacman)Pacman).Score += tempTile.Value;
                    tempTile = new Tile();
                    tempTile.Type = TileType.Empty;


                    Pacman.Animate();
                    Pacman.Move();
                    Maps[0].MoveTile(Pacman);
                    break;
                default:
                    Pacman.Animate();
                    Pacman.Move();
                    Maps[0].MoveTile(Pacman);
                    break;
            }

        }
        public void Init()
        {

            Maps.Add(new Map());
            Map map = Maps[0];
            map.LoadMap("");

            Pacman = map.Characters[ObjectType.Pacman];
            Blinky = map.Characters[ObjectType.Blinky];
            Pinky = map.Characters[ObjectType.Pinky];
            Inky = map.Characters[ObjectType.Inky];
            Clyde = map.Characters[ObjectType.Clyde];
        }

        public void Load(string path)
        {

        }
        public void Save(string path)
        {
            Map CurrentMap = Maps[0];

            using (StreamWriter stream = new FileInfo(path).AppendText())
            {
                CurrentMap.Serialize();
            }
        }

        
    }
}

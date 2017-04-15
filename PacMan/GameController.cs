using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacManNamespace.Models;
using System.IO;
using PacManNamespace.Models.Ghosts;
using System.Threading;
using Windows.Storage.Pickers;
using Windows.Storage;

namespace PacManNamespace
{
   
    public interface Serialization
    {
        string Serialize();
        void Deserialize();
    }
    public enum ObjectType { Pacman, Blinky, Pinky, Inky, Clyde}
    public enum SoundType { beginning, chomp, death, eatghost, extrapac,intermission }
    public enum Level { First, Second, Third}
    public enum GameState {None, Pause, Playing, Lost, Won}
    public class GameController
    {
        public List<Map> Maps = new List<Map>();
        public Tile LastCollidedWith { get; set; }
        public bool AteGhost { get; set; }
        public bool PacDead { get; set; }
        public bool isCheating { get; set; }
        public GameState GameState { get; set; }
        public int CurrentLevel { get; set; }
        public Tile Pacman { get; set; }
        public Tile Blinky { get; set; }
        public Tile Pinky { get; set; }
        public Tile Inky { get; set; }
        public Tile Clyde { get; set; }

        public Task<int> MakeGhostsNormalTask;
        public GameController()
        {

        }

        public void MovePacman()
        {
            
            Tile tempTile = Maps[0].Collision(Pacman.Direction, Pacman);
            LastCollidedWith = tempTile;

            switch (tempTile.Type)
            {
                
                case TileType.Wall:

                    Pacman.Animate();
                    Pacman.Position.row = Math.Round(Pacman.Position.row);
                    Pacman.Position.col = Math.Round(Pacman.Position.col);
                    break;

                case TileType.Dot:
                    ((Pacman)Pacman).Score += tempTile.Value;
                    Maps[0].RemoveFromMap(tempTile);

                    if(Maps[0].Dots.Count == 0)
                    {
                        GameState = GameState.Won;
                        ((Pacman)Pacman).Score += 100;
                    }
                    else
                    {
                        Pacman.Animate();
                        Pacman.Move();
                    }
                    break;

                case TileType.MakeVulnerable:
                    ((Pacman)Pacman).Score += tempTile.Value;
                    Maps[0].RemoveFromMap(tempTile);
                    Maps[0].MakeGhostVulnerable();

                    
                    MakeGhostsNormalTask = Task.Run(() => MakeGhostNormal());
                   
                    if (Maps[0].Dots.Count == 0)
                    {
                        GameState = GameState.Won;
                    }
                    else
                    {
                        Pacman.Animate();
                        Pacman.Move();
                    }
                    break;
                default:
                    Pacman.Animate();
                    Pacman.Move();
                    break;
            }

            if ((Pacman.Position.row) % 1 < 0.2 && (Pacman.Position.col % 1) < 0.2)
                Pacman.Direction = Pacman.PreviousDirection;

            Tile GhostTile = Maps[0].CollisionWithGhost();
            if (GhostTile != null)
            {
                AteGhost = true;

                if (GhostTile.Vulnerable)
                {
                    AteGhost = true;
                    ((Pacman)Pacman).Score += GhostTile.Value;
                    GhostTile.Position.col = GhostTile.StartPosition.col;
                    GhostTile.Position.row = GhostTile.StartPosition.row;
                    GhostTile.Direction = Direction.Up;
                }
                else
                {
                    if (!isCheating)
                    {
                        PacDead = true;
                        ((Pacman)Pacman).Lives -= 1;
                        if (((Pacman)Pacman).Lives == 0)
                        {
                            PacDead = true;
                            GameState = GameState.Lost;
                        }
                        else
                        {
                            GameState = GameState.Pause;
                            Pacman.Position.col = Pacman.StartPosition.col;
                            Pacman.Position.row = Pacman.StartPosition.row;
                            Pacman.Direction = Direction.Left;
                        }
                    }
                    
                    
                }
            }
            
            
        }

        private Task<int> MakeGhostNormal()
        {
            Task.Delay(TimeSpan.FromSeconds(10)).Wait();
            foreach (ObjectType Type in Enum.GetValues(typeof(ObjectType)))
            {
                if (Type != ObjectType.Pacman)
                    ((Ghost)Maps[0].Characters[Type]).MakeNormal();
            }
            return Task.FromResult(0);
        }

        public void MoveGhosts()
        {
            foreach (ObjectType Type in Enum.GetValues(typeof(ObjectType)))
            {
                if(Type != ObjectType.Pacman)
                    ((Ghost)Maps[0].Characters[Type]).Move();
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
        public async void Save(string path)
        {
            Map CurrentMap = Maps[0];
            try
            {
                using (StreamWriter stream = new FileInfo(path).AppendText())
                {

                    stream.Write(CurrentMap.Serialize());

                }
            }catch(UnauthorizedAccessException e)
            {
                
            }
            
        }

       

       
    }
}

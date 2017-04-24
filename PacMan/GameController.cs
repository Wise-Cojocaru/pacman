//------------------------------------------------------------------------------
// This class implements the Pacman game logic. Holds references to Maps and
// game objects. Implements the main game loop and the core methods for the pacman
// game to be playable.
//------------------------------------------------------------------------------
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
    //Serialization Interface
    public interface Serialization
    {
        string Serialize();
        void Deserialize();
    }
    //enums
    public enum ObjectType { Pacman, Blinky, Pinky, Inky, Clyde}
    public enum SoundType { beginning, chomp, death, eatghost, extrapac,intermission }
    public enum Level { First, Second, Third}
    public enum GameState {None, Pause, Playing, Lost, Won}
    public class GameController
    {
        //Contains a list of maps
        public List<Map> Maps = new List<Map>();

        //Holds a reference to the last object pacman collided with (from the map)
        public Tile LastCollidedWith { get; set; }
        //A flag which is set when pacman eats a ghost
        public bool AteGhost { get; set; }
        //flag to hold pacman state
        public bool PacDead { get; set; }
        //flat to hold the game mode (cheating or normal)
        public bool isCheating { get; set; }
        //current gamestate property
        public GameState GameState { get; set; }
        //current level property
        public int CurrentLevel { get; set; }
        //direct property reference to pacman
        public Tile Pacman { get; set; }
        //direct property reference to blinky
        public Tile Blinky { get; set; }
        //direct property reference to Pinky
        public Tile Pinky { get; set; }
        //direct property reference to Inky
        public Tile Inky { get; set; }
        //direct property reference to Clyde
        public Tile Clyde { get; set; }
        //flag which tells if the map is still being loaded
        public bool isLoading = false;
        //variable which holds reference to a task
        public Task<int> MakeGhostsNormalTask;
        //gamecontroller constructor
        public GameController()
        {

        }
        // This method moves the Pacman and checks for collision with Ghosts, Bullets, Dots, Walls and updates the coresponding properties to inform
        // the view of the latest collision.
        public void MovePacman()
        {

            if (Maps[0].CollidedWithPac)
            {
                Maps[0].CollidedWithPac = false;
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
                        ((Pacman)Pacman).Score += 100;
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
        // This method waits 10 seconds after which it changes each ghosts' Vulnerable flag to false
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
        // This method moves each ghost
        public void MoveGhosts()
        {
            foreach (ObjectType Type in Enum.GetValues(typeof(ObjectType)))
            {
                if(Type != ObjectType.Pacman)
                    ((Ghost)Maps[0].Characters[Type]).Move();
            }
        }
        // This method initiates the model
        public void Init()
        {

            Maps.Add(new Map());
            Map map = Maps[0];
            if (isLoading)
            {
                map.isLoading = true;
                map.LoadMap("");
            }

            else
            {
                map.isLoading = false;
                map.LoadMap("");
            }
                

            Pacman = map.Characters[ObjectType.Pacman];
            Blinky = map.Characters[ObjectType.Blinky];
            Pinky = map.Characters[ObjectType.Pinky];
            Inky = map.Characters[ObjectType.Inky];
            Clyde = map.Characters[ObjectType.Clyde];
        }
        // This method serializes the gamestate.
        public string Serialize()
        {
            string controllerInfo = "";
            controllerInfo += ((Pacman)Pacman).Score.ToString() + ",";
            return controllerInfo;
        }
        // This method deserializes the gamestate
        public void Deserialize(string data, Map m)
        {
            m = Maps[0];

            m.DeSerialize(data);
            string[] dataSplit = data.Split(',');
            ((Pacman)Pacman).Score = Convert.ToInt32(dataSplit[dataSplit.Length - 5]);
        }

    }
}

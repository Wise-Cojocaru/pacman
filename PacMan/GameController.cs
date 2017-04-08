﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacManNamespace.Models;
using System.IO;
using PacManNamespace.Models.Ghosts;

namespace PacManNamespace
{
   
    public interface Serialization
    {
        string Serialize();
        void Deserialize();
    }
    public enum ObjectType { Pacman, Blinky, Pinky, Inky, Clyde}
    
    public enum Level { First, Second, Third}
    public enum GameState {None, Pause, Playing, Lost, Won}
    public class GameController
    {
        public List<Map> Maps = new List<Map>();

        public Tile LastCollidedWith { get; set; }
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
            LastCollidedWith = tempTile;

            switch (tempTile.Type)
            {
                
                case TileType.Wall:

                    Pacman.Position.row = Math.Round(Pacman.Position.row);
                    Pacman.Position.col = Math.Round(Pacman.Position.col);
                    break;

                case TileType.Dot:
                    ((Pacman)Pacman).Score += tempTile.Value;
                    Maps[0].RemoveFromMap(tempTile);

                    if(Maps[0].Dots.Count == 0)
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

                if (GhostTile.Vulnerable)
                {
                    ((Pacman)Pacman).Score += GhostTile.Value;
                    GhostTile.Position = GhostTile.StartPosition;
                }
                else
                {
                    ((Pacman)Pacman).Lives -= 1;
                    if (((Pacman)Pacman).Lives == 0)
                    {
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
        public void MoveGhosts()
        {
            ((Ghost)Blinky).Move();
            ((Ghost)Inky).Move();
            ((Ghost)Pinky).Move();
            ((Ghost)Clyde).Move();
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

﻿//------------------------------------------------------------------------------
// This page is where the user plays the actual game.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using System.Threading.Tasks;
using PacManNamespace.Models;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using Windows.Storage.Streams;
using PacMan;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PacManNamespace
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        //Instance of GameController that controls all game aspects
        GameController controller = new GameController();
        
        //BitMapIcon used for custom Pacman color
        BitmapIcon bmi = new BitmapIcon();

        //DispatcherTimer used for main game loop
        DispatcherTimer dispatcherTimer;

        //Dictionary containing object type and image for Pacman and each ghost
        public Dictionary<ObjectType, Image> UICharacters = new Dictionary<ObjectType, Image>();

        //Dictionary containing each dot and image for each dot
        public Dictionary<Tile, UIElement> UIDots = new Dictionary<Tile, UIElement>();

        //Dictionary containing each bullet and image for each bullet
        public Dictionary<Tile, UIElement> Bullets = new Dictionary<Tile, UIElement>();

        //30 second delay used for dispatcherTimer
        public TimeSpan delay = TimeSpan.FromMinutes(0.5);

        //Default color of Pacman
        private Color color = Color.FromArgb(255, 255, 241, 0);

        //Image used for each dot in UIDots
        private Image Dot;

        //Image used for each MakeVuln in UIDots
        private Image MV;

        //Received color info in type string
        string colorString;

        //Flag that tells if a sound is being played
        public bool PlayingSound { get; set; }

        //Path to project Assets/Images/png/ folder
        public const string pathToPng = "ms-appx:///Assets/Images/png/";

        //Initializes page and sets method name for when user presses a key.
        public GamePage()
        {
           
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;

        }

        //Sets each item on the map after initial page initialization. Also sets
        //images to each object type and places them at a certain position on the canvas.
        public void Init()
        {
            foreach (Tile dot in controller.Maps[0].Dots)
            {
                
                Dot = new Image();
                Dot.Name = dot.ToString();
                Dot.Height = 20;
                Dot.Width = 20;
                Dot.Source = new BitmapImage(new Uri(pathToPng + dot.CurrentImageUrl));
                UIDots[dot] = Dot;
                this.Canvas.Children.Add(UIDots[dot]);
                PlaceOnCanvas(dot.Position, UIDots[dot]);
                controller.Maps[0].Maze[(int)dot.Position.row, (int)dot.Position.col].Type = dot.Type;
            }

            foreach (Tile makevuln in controller.Maps[0].MakeVulns)
            {

                MV = new Image();
                MV.Name = makevuln.ToString();
                MV.Height = 20;
                MV.Width = 20;
                MV.Source = new BitmapImage(new Uri(pathToPng + makevuln.CurrentImageUrl));
                UIDots[makevuln] = MV;
                this.Canvas.Children.Add(UIDots[makevuln]);
                PlaceOnCanvas(makevuln.Position, UIDots[makevuln]);
                controller.Maps[0].Maze[(int)makevuln.Position.row, (int)makevuln.Position.col].Type = makevuln.Type;
            }

            foreach (ObjectType Type in Enum.GetValues(typeof(ObjectType)))
            {
                Image Object = new Image();
                Object.Height = 20;
                Object.Width = 20;
                
                Object.Source = new BitmapImage(new Uri(pathToPng + controller.Maps[0].Characters[Type].CurrentImageUrl));
                this.Canvas.Children.Add(Object);
                PlaceOnCanvas(controller.Maps[0].Characters[Type].Position, Object);
                UICharacters[Type] = Object;

                if (Type == ObjectType.Pacman)
                {

                    bmi.UriSource = new Uri(pathToPng + "pacman-L.png");
                    bmi.Height = Object.Height;
                    bmi.Width = Object.Width;
                    bmi.Foreground = new SolidColorBrush(color);
                    PlaceOnCanvas(controller.Pacman.Position, bmi);
                    this.Canvas.Children.Add(bmi);
                }
                
            }

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 5);

        }

        //Moves Pacman and ghosts while checking for collisions and gamestate changes for
        //every dispatchertimer tick (in this case, every 5 milliseconds).
        private async void dispatcherTimer_Tick(object sender, object e)
        {

            controller.MovePacman();
            UpdateIcon();
            controller.MoveGhosts();

            if (controller.LastCollidedWith != null)
            {
                if (controller.LastCollidedWith.Type == TileType.Dot || controller.LastCollidedWith.Type == TileType.MakeVulnerable)
                {
                    
                    Task.Run(() => PlaySound(SoundType.chomp));
                    try
                    {
                        Canvas.Children.Remove(UIDots[controller.LastCollidedWith]);
                    }
                    catch (Exception ex)
                    {

                    }
                   
                   UIDots[controller.LastCollidedWith] = null;
                   controller.LastCollidedWith = null;
                }

            }

            if (controller.AteGhost)
            {
                Task.Run(() => PlaySound(SoundType.eatghost));
                controller.AteGhost = false;
            }
            if (controller.PacDead && !controller.isCheating)
            {
                Task.Run(() => PlaySound(SoundType.death));
                controller.PacDead = false;
            }
            if (controller.GameState == GameState.Lost)
            {
                Task.Run(() => PlaySound(SoundType.death));
                controller.PacDead = false;
            }
            Score.Text = ((Pacman)controller.Pacman).Score.ToString();
            LivesNr.Text = ((Pacman)controller.Pacman).Lives.ToString();


            foreach (ObjectType Type in Enum.GetValues(typeof(ObjectType)))
            {
                UICharacters[Type].Source = new BitmapImage(new Uri(pathToPng + controller.Maps[0].Characters[Type].CurrentImageUrl));
                PlaceOnCanvas(controller.Maps[0].Characters[Type].Position, UICharacters[Type]);
            }

            foreach (Tile b in controller.Maps[controller.CurrentLevel].Bullets)
            {
                

                if (!Bullets.ContainsKey(b))
                {
                    Image Bullet = new Image();
                    Bullet.Height = 20;
                    Bullet.Width = 20;
                    Bullet.Source = new BitmapImage(new Uri(pathToPng + "bullet-R.png"));
                    Bullets[b] = Bullet;
                    this.Canvas.Children.Add(Bullets[b]);
                }
                if (!b.isMoving)
                {
                    Canvas.Children.Remove(Bullets[b]);
                    Bullets.Remove(b);
                }
                else
                {

                PlaceOnCanvas(b.Position, Bullets[b]);
                }

            }

            controller.Maps[0].Bullets.RemoveAll((b) => 
            {
                return !b.isMoving;
            });
   

            if (controller.GameState == GameState.Lost)
            {
                dispatcherTimer.Stop();
                this.GameOver.Visibility = Visibility.Visible;
            }
            if (controller.GameState == GameState.Pause)
            {
                dispatcherTimer.Stop();
                this.pressEnter.Visibility = Visibility.Visible;
            }
            if (controller.GameState == GameState.Won)
            {
                dispatcherTimer.Stop();
                this.Won.Visibility = Visibility.Visible;
            }

        }


        //Plays a sound with sound source being controller by what Soundtype parameter is being passed.
        public async void PlaySound(SoundType type)
        {
          Task.Run(async () =>
                {

                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
                    async () =>
                        {
                            MediaElement mysong = new MediaElement();
                            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
                            Windows.Storage.StorageFile file = await folder.GetFileAsync(type.ToString() + ".wav");
                            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                            mysong.SetSource(stream, file.ContentType);
                            mysong.Volume = 0.1;
                            mysong.Play();
                            
                        });
                    
                });

         }



        //Places on object on the page canvas based on object's Position "P" and the object's
        //UI element "element".
        public void PlaceOnCanvas(Position P, UIElement element)
        {
            Canvas.SetLeft(element, P.col * 20);
            Canvas.SetTop(element, P.row * 20);
        }

        //Controls Pacman direction and gamestate based on user input.
        private async void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {

            Direction dir = Direction.None;
            if (args.VirtualKey == Windows.System.VirtualKey.Enter)
            {
                if (controller.GameState == GameState.None)
                {
                    controller.Init();
                    this.Init();
                    this.CountDownText.Visibility = Visibility.Visible;
                    this.pressEnter.Visibility = Visibility.Collapsed;

                    Task t = Task.Run(async () =>
                   {
                       Task.Run(() => PlaySound(SoundType.beginning));
                        
                       for (int i = 3; i > 0; i--)
                       {

                           await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
                           () =>
                           {
                               this.CountDownText.Text = i.ToString();

                            });
                           Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                       }
                   });
                   await t;
                   this.CountDownText.Visibility = Visibility.Collapsed;

                }

                if (controller.GameState == GameState.Pause || controller.GameState == GameState.None)
                {
                    dispatcherTimer.Start();
                    this.pressEnter.Visibility = Visibility.Collapsed;
                    this.saveControlsText.Visibility = Visibility.Visible;
                    controller.GameState = GameState.Playing;
                }

            }

            if (args.VirtualKey == Windows.System.VirtualKey.Up) dir = Direction.Up;
            if (args.VirtualKey == Windows.System.VirtualKey.Down) dir = Direction.Down;
            if (args.VirtualKey == Windows.System.VirtualKey.Left) dir = Direction.Left;
            if (args.VirtualKey == Windows.System.VirtualKey.Right) dir = Direction.Right;
            if (args.VirtualKey == Windows.System.VirtualKey.W) dir = Direction.Up;
            if (args.VirtualKey == Windows.System.VirtualKey.A) dir = Direction.Left;
            if (args.VirtualKey == Windows.System.VirtualKey.S) dir = Direction.Down;
            if (args.VirtualKey == Windows.System.VirtualKey.D) dir = Direction.Right;
            if (args.VirtualKey == Windows.System.VirtualKey.C)
            {
                controller.isCheating = !controller.isCheating;
                if(controller.isCheating)
                    CheatText.Visibility = Visibility.Visible;
                else
                    CheatText.Visibility = Visibility.Collapsed;
            }

            if (args.VirtualKey == Windows.System.VirtualKey.K && controller.GameState == GameState.Playing)
            {
                dispatcherTimer.Stop();
                controller.GameState = GameState.Pause;
                Map currentMap = controller.Maps[0];
                this.Frame.Navigate(typeof(LoadSavePage), currentMap.Serialize() + controller.Serialize() + string.Format("{0},{1},{2},{3}", Convert.ToInt32(color.A).ToString(), Convert.ToInt32(color.R).ToString(),
                Convert.ToInt32(color.G).ToString(), Convert.ToInt32(color.B).ToString()));

            }

            if (dir != Direction.None)
            {

                controller.Pacman.PreviousDirection = controller.Pacman.Direction;
                controller.Pacman.PreviousDirection = dir;

            }
        }

        //Updates the colored Pacman icon to be in sync with Pacman image's image source. Ensure
        //colored Pacman has same animation pattern as default Pacman.
        private void UpdateIcon()
        {

            bmi.UriSource = new Uri(pathToPng + controller.Pacman.CurrentImageUrl);
            PlaceOnCanvas(controller.Pacman.Position, bmi);
        }


        //Takes Parameter and either loads the page or sets Pacman color based on how many arguments
        //are based when string Paramater is split on commas.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                string receivedString = e.Parameter.ToString();
                if(receivedString.Split(',').Length == 4)
                {
                    string[] colorArray = receivedString.Split(',');
                    try
                    {
                        color = Color.FromArgb(Convert.ToByte(colorArray[0]), Convert.ToByte(colorArray[1]), Convert.ToByte(colorArray[2]), Convert.ToByte(colorArray[3]));
                    }
                    catch (FormatException ex)
                    {
                        color = Color.FromArgb(255, 255, 241, 0);
                    }
                }
                else if(receivedString.Split(',').Length > 4)
                {
                    string[] splitData = receivedString.Split(',');
                    int alpha = Convert.ToInt32(splitData[splitData.Length - 4]);
                    int red = Convert.ToInt32(splitData[splitData.Length - 3]);
                    int green = Convert.ToInt32(splitData[splitData.Length - 2]);
                    int blue = Convert.ToInt32(splitData[splitData.Length - 1]);

                    color = Color.FromArgb(Convert.ToByte(alpha), Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));
                    controller.isLoading = true;
                    controller.Init();
                    controller.Deserialize(receivedString, controller.Maps[0]);
                    this.Init();
                    controller.LastCollidedWith = null;

                    controller.GameState = GameState.Playing;
                    pressEnter.Visibility = Visibility.Collapsed;
                    dispatcherTimer.Start();

                }
                    
            }
                
                    
        }

    }
}
    



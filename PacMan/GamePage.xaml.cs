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
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PacManNamespace
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        GameController controller = new GameController();

        DispatcherTimer dispatcherTimer;

        public Dictionary<ObjectType, Image> UICharacters = new Dictionary<ObjectType, Image>();

        public Dictionary<Tile, UIElement> UIDots = new Dictionary<Tile, UIElement>();

        public TimeSpan delay = TimeSpan.FromMinutes(0.5);

        public const String pathToPng= "ms-appx:///Assets/Images/png/";
        public GamePage()
        {
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            this.InitializeComponent();
            controller.Init();
 
        }

        
        public void Init()
        {
            

            foreach (Tile dot in controller.Maps[0].Dots)
            {
                
                Image Dot = new Image();
                Dot.Name = dot.ToString();
                Dot.Height = 20;
                Dot.Width = 20;
                Dot.Source = new BitmapImage(new Uri(pathToPng + dot.CurrentImageUrl));
                UIDots[dot] = Dot;
                this.Canvas.Children.Add(UIDots[dot]);
                PlaceOnCanvas(dot.Position, UIDots[dot]);


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
            }
            

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {

            controller.MovePacman();
            controller.MoveGhosts();
            
            if (controller.LastCollidedWith != null)
            {
                if (controller.LastCollidedWith.Type == TileType.Dot || controller.LastCollidedWith.Type == TileType.MakeVulnerable)
                {
                    Canvas.Children.Remove(UIDots[controller.LastCollidedWith]);
                    UIDots[controller.LastCollidedWith] = null;
                }

            }

            Score.Text = ((Pacman)controller.Pacman).Score.ToString();
            LivesNr.Text = ((Pacman)controller.Pacman).Lives.ToString();


            foreach (ObjectType Type in Enum.GetValues(typeof(ObjectType)))
            {
                UICharacters[Type].Source = new BitmapImage(new Uri(pathToPng + controller.Maps[0].Characters[Type].CurrentImageUrl));
                PlaceOnCanvas(controller.Maps[0].Characters[Type].Position, UICharacters[Type]);
            }


            if (controller.GameState == GameState.Lost )
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

        public void PlaceOnCanvas(Position P, UIElement element)
        {
            Canvas.SetLeft(element, P.col * 20);
            Canvas.SetTop(element, P.row * 20);
        }
        
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {

            Direction dir = Direction.None;
            if (args.VirtualKey == Windows.System.VirtualKey.Enter)
            {
                if (controller.GameState == GameState.None)
                {
                    this.Init();

                }
                if (controller.GameState == GameState.Pause || controller.GameState == GameState.None)
                {
                    dispatcherTimer.Start();
                    this.pressEnter.Visibility = Visibility.Collapsed;
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

            if (dir != Direction.None)
            {
                
                controller.Pacman.PreviousDirection = controller.Pacman.Direction;
                controller.Pacman.PreviousDirection = dir;

            }
        }



    }
    
}


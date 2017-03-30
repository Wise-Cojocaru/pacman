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

        public Dictionary<ObjectType, UIElement> UICharacters = new Dictionary<ObjectType, UIElement>();

        public List<UIElement> UIDots = new List<UIElement>();

        public TimeSpan delay = TimeSpan.FromMinutes(0.5);

        
        public GamePage()
        {
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            this.InitializeComponent();
            controller.Init();
            this.Init();

            
        }

        

        public void Init()
        {
            Image Pacman = new Image();
            Pacman.Height = 20;
            Pacman.Width = 20;
            Pacman.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/png/pacman_third.png"));

            this.Canvas.Children.Add(Pacman);
            PlaceOnCanvas(controller.Characters[ObjectType.Pacman].Position, Pacman);
            UICharacters[ObjectType.Pacman] = Pacman;


            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            if(controller.GameState == GameState.GameOver)
            {
                dispatcherTimer.Stop();
            }

            controller.MovePacman();
            PlaceOnCanvas(controller.Pacman.Position, UICharacters[ObjectType.Pacman]);
        }

        public void PlaceOnCanvas(Position P, UIElement element)
        {
            Canvas.SetLeft(element, P.col * 20);
            Canvas.SetTop(element, P.row * 20);
        }
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {

            Direction dir = Direction.None;
            if(args.VirtualKey == Windows.System.VirtualKey.Enter) dispatcherTimer.Start();
            if (args.VirtualKey == Windows.System.VirtualKey.Up) dir = Direction.Up;
            if (args.VirtualKey == Windows.System.VirtualKey.Down) dir = Direction.Down;
            if (args.VirtualKey == Windows.System.VirtualKey.Left) dir = Direction.Left;
            if (args.VirtualKey == Windows.System.VirtualKey.Right) dir = Direction.Right;

            if (dir != Direction.None)
            {
                controller.Pacman.Direction = dir;
            }
        }



    }
    
}


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
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PacManNamespace
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        GameController controller = new GameController();

        public Dictionary<ObjectType, UIElement> UIObjects = new Dictionary<ObjectType, UIElement>();
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
            Pacman.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/png/pacman-L.png"));

            this.Canvas.Children.Add(Pacman);
            PlaceOnCanvas(controller.Objects[ObjectType.Pacman].Position, Pacman);
            UIObjects[ObjectType.Pacman] = Pacman;
        }

        public void PlaceOnCanvas(Position P, UIElement element)
        {
            Canvas.SetLeft(element, P.X * 20);
            Canvas.SetTop(element, P.Y * 20);
        }
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            Direction dir = Direction.None;
            if (args.VirtualKey == Windows.System.VirtualKey.Up) dir = Direction.Up;
            if (args.VirtualKey == Windows.System.VirtualKey.Down) dir = Direction.Down;
            if (args.VirtualKey == Windows.System.VirtualKey.Left) dir = Direction.Left;
            if (args.VirtualKey == Windows.System.VirtualKey.Right) dir = Direction.Right;

            if (dir != Direction.None)
            {
                Position pacPos = controller.MovePacman(dir);
                PlaceOnCanvas(pacPos, UIObjects[ObjectType.Pacman]);
                if(pacPos.X != -1 && pacPos.Y != -1)
                    CoreWindow_KeyDown(sender, args);
            }
        }
        

       
    }
    
}


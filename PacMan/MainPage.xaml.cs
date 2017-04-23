//------------------------------------------------------------------------------
// This page is the title screen where the user can access the about, help,
// customize, and game pages.
//------------------------------------------------------------------------------

using PacMan;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PacManNamespace
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //Sets the color of the Pacman to the default value in case the user does
        //not set a custom one.
        private string colorString = String.Format("{0},{1},{2},{3}", 255, 255, 241, 0);

        public MainPage()
        {
            this.InitializeComponent();
        }

        //Navigates to the "About" page and sends Pacman color info
        //if the user clicks the "About" button.
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutPage), colorString);
        }

        //Navigates to the "Help" page and sends Pacman color info
        //if the user clicks the "Help" button.
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HelpPage), colorString);
        }

        //Navigates to the game page and sends Pacman color info
        //if the user clicks the "Start" button.
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage), colorString);
        }

        //Navigates to the "Customize" page and sends Pacman color info
        //if the user clicks the "Customize" button.
        private void btnCustomize_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomizePage), colorString);
        }

        //Sets string for Pacman color to string received from whatever
        //page is being navigated from.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                colorString = e.Parameter.ToString();
            }
                

        }

        //Plays title screen song when the page is loaded.
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Task T2 = Task.Run(async () =>
           {


               await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
               async () =>
               {
                   MediaElement mysong = new MediaElement();

                   mysong.Position = TimeSpan.FromMilliseconds(0);
                   Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
                   Windows.Storage.StorageFile file = await folder.GetFileAsync("intermission.wav");
                   var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                   mysong.SetSource(stream, file.ContentType);
                   mysong.Volume = 0.1;
                   mysong.Play();


               });



           });

        }
    }
}

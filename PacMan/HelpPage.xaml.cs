//------------------------------------------------------------------------------
// This page gives the user instructions on how to play the game.
//------------------------------------------------------------------------------

using PacManNamespace;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PacMan
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HelpPage : Page
    {
        private string colorString;

        //Initializes page and sets method for user key input.
        public HelpPage()
        {
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            this.InitializeComponent();
        }

        //Navigates back to title screen when ESC is pressed. Also passes colorString for the Pacman color
        //back to title screen.
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {

            if (args.VirtualKey == Windows.System.VirtualKey.Escape) this.Frame.Navigate(typeof(MainPage), colorString);
        }

        //Sets colorString based on string sent to it from title screen.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                colorString = e.Parameter.ToString();
            }


        }
    }
}

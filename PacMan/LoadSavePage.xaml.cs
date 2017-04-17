using PacManNamespace;
using PacManNamespace.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class LoadSavePage : Page
    {

        string mapSerializedStr = "";
        const string pathToSaves = "ms-appx///Assets/Saves/";

        public LoadSavePage()
        {
            this.InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage), "");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void txtsaveFile_GotFocus(object sender, RoutedEventArgs e)
        {
            txtsaveFile.Background = new SolidColorBrush(Colors.Yellow);
            txtsaveFile.BorderBrush = new SolidColorBrush(Colors.Yellow);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            mapSerializedStr = e.Parameter.ToString();
        }

        private void Save()
        {
            string filePath = "Assets/Saves/" + txtsaveFile.Text + ".csv";
            
            File.WriteAllText(filePath, mapSerializedStr);

        }

        private void Load()
        {
            
        }
    }
}

//------------------------------------------------------------------------------
// This page contains three RPG sliders with which the user can change the color
// of their pacman.
//------------------------------------------------------------------------------

using PacManNamespace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Effects;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PacMan
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomizePage : Page
    {

        //Color with the RGB values of the sliders
        private Color color;
        //Set equal to parameter when page is navigated to
        private string colorString;

        public CustomizePage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            SetStartValues();
        }

        //Sets the "Preview" Pacman's color based on RGB slider values.
        private void SetPacmanColor()
        {
            color = Color.FromArgb(255, Convert.ToByte(redSlider.Value), Convert.ToByte(greenSlider.Value), Convert.ToByte(blueSlider.Value));
            iconPacman.Foreground = new SolidColorBrush(color);

        }

        //Changes Textblock values for whatever color is changed and updates preview
        //Pacman color whenever a slider value is changed.
        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider s = sender as Slider;

            switch (s.Name)
            {
                case ("redSlider"):
                    txtRedVal.Text = redSlider.Value.ToString();
                    break;
                case ("greenSlider"):
                    txtGreenVal.Text = greenSlider.Value.ToString();
                    break;
                case ("blueSlider"):
                    txtBlueVal.Text = blueSlider.Value.ToString();
                    break;
            }
            SetPacmanColor();
        }

        //Sets the start values for the preview Pacman to the color of the default Pacman.
        private void SetStartValues()
        {
            redSlider.Value = 255;
            greenSlider.Value = 241;
            blueSlider.Value = 0;
        }

        //Sends user back to title screen when ESC is pressed.
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {

            if (args.VirtualKey == Windows.System.VirtualKey.Escape) this.Frame.Navigate(typeof(MainPage), String.Format("{0},{1},{2},{3}", 255, (int)redSlider.Value, (int)greenSlider.Value, (int)blueSlider.Value));
        }

        //Receives color values sent by other pages when navigated to and sets slider
        //values and "Preview" Pacman color based on the string values.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                colorString = e.Parameter.ToString();
                string[] colorArray = colorString.Split(',');
                if(colorString != "")
                {
                    color = Color.FromArgb(Convert.ToByte(colorArray[0]), Convert.ToByte(colorArray[1]), Convert.ToByte(colorArray[2]), Convert.ToByte(colorArray[3]));
                    redSlider.Value = Convert.ToDouble(colorArray[1]);
                    greenSlider.Value = Convert.ToDouble(colorArray[2]);
                    blueSlider.Value = Convert.ToDouble(colorArray[3]);
                }
                
            }


        }
    }
}

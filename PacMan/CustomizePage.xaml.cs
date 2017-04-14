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

        private Color color;
        private string colorString;

        public CustomizePage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            SetStartValues();
        }

        private void SetPacmanColor()
        {
            color = Color.FromArgb(255, Convert.ToByte(redSlider.Value), Convert.ToByte(greenSlider.Value), Convert.ToByte(blueSlider.Value));
            iconPacman.Foreground = new SolidColorBrush(color);

        }

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

        private void SetStartValues()
        {
            redSlider.Value = 255;
            greenSlider.Value = 241;
            blueSlider.Value = 0;
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {

            if (args.VirtualKey == Windows.System.VirtualKey.Escape) this.Frame.Navigate(typeof(MainPage), String.Format("{0},{1},{2},{3}", 255, (int)redSlider.Value, (int)greenSlider.Value, (int)blueSlider.Value));
        }

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

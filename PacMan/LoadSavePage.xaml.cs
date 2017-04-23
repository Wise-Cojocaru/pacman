//------------------------------------------------------------------------------
// This page allows the user to save current game or load any game they have
// saved in the past.
//------------------------------------------------------------------------------

using PacManNamespace;
using PacManNamespace.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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

        //String later set for when OnNavigatedTo() is called.
        string mapSerializedStr = "";

        public LoadSavePage()
        {

            this.InitializeComponent();
        }

        //Opens the file with the name the user selected and sets its contents to the string fileContents.
        //Navigates to GamePage with the Paramter fileContents to be loaded by GamePage.
        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if(loadFileView.SelectedItem != null)
            {
                string fileContents = "";
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile selectedFile = await storageFolder.GetFileAsync(loadFileView.SelectedItem.ToString() + ".csv");
                Stream stream = await selectedFile.OpenStreamForReadAsync();
                using (StreamReader reader = new StreamReader(stream))
                {
                    fileContents = reader.ReadToEnd();
                }
                stream.Dispose();
                this.Frame.Navigate(typeof(GamePage), fileContents);
            }
            
        }

        //Saves the game to specified file name if the file name given by the user is not empty
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(txtsaveFile.Text == "")
                txtChooseFileName.Text = "Name must be valid";
            else
                Save();
        }

        //Attempt at getting background of save textbox to be yellow after the user
        //clicked on it.
        private void txtsaveFile_GotFocus(object sender, RoutedEventArgs e)
        {
            txtsaveFile.Background = new SolidColorBrush(Colors.Yellow);
            txtsaveFile.BorderBrush = new SolidColorBrush(Colors.Yellow);
        }


        //Receives the string of character data from GamePage, updates the list of saved games,
        //and removes the previous page from the stack.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            mapSerializedStr = e.Parameter.ToString();
            UpdateListView();
            this.Frame.BackStack.RemoveAt(this.Frame.BackStack.Count - 1);
        }

        //Updates the ListView containing the filenames of saved games.
        private async void UpdateListView()
        {
            loadFileView.Items.Clear();
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            foreach(StorageFile file in await storageFolder.GetFilesAsync())
            {
                loadFileView.Items.Add(file.DisplayName);
            }
        }

        //Writes string received from GamePage to a csv file whose name is given by the user.
        private async void Save()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile userCreatedFile = await storageFolder.CreateFileAsync(txtsaveFile.Text + ".csv", CreationCollisionOption.ReplaceExisting);
            Stream stream = await userCreatedFile.OpenStreamForWriteAsync();
            string path = storageFolder.Path;
            string fPath = userCreatedFile.Path;
            using (StreamWriter sr = new StreamWriter(stream))
            {
                sr.Write(mapSerializedStr);
            }
            stream.Dispose();
            txtChooseFileName.Text = "Game saved!";
            UpdateListView();

        }

    }
}

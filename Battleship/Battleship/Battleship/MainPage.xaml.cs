using Battleship.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Battleship
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MediaPlayer musicPlayer = new MediaPlayer();
        private Board board = new Board();
        public MainPage()
        {
            this.InitializeComponent();
            SetMusic();
        }

        private void NewGameButton(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateGame));
        }
        private async void LoadGameButton(object sender, RoutedEventArgs e)
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.FileTypeFilter.Add(".bshp");
            StorageFile file = await fileOpenPicker.PickSingleFileAsync();
            board.PlayerGrid.Ships = null;
            board.ComputerGrid.Ships = null;
            if (file != null)
            {
                using (Stream fs = await file.OpenStreamForReadAsync())
                {
                    
                    board = Serializer.Deserialize<Board>(fs);
                    board.PlayerGrid.LoadSaves();
                    board.ComputerGrid.LoadSaves();
                }
            }
            this.Frame.Navigate(typeof(PlayGame), board);
        }

        public async void SetMusic()
        {
            MainPage.musicPlayer.AutoPlay = true;
            MainPage.musicPlayer.IsLoopingEnabled  = true;
            StorageFolder musicFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile musicFile = await musicFolder.GetFileAsync("Waves.wav");
            MainPage.musicPlayer.Source = MediaSource.CreateFromStorageFile(musicFile);
        }
    }
}

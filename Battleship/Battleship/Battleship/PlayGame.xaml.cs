using Battleship.Enums;
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
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Battleship
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayGame : Page
    {
        private Board board = new Board();
        private Random rand = new Random();
        private static Brush borderBrush = new SolidColorBrush(Colors.Black);
        private bool playerTurn = true;
        private int shipIndex = -1;
        private MediaPlayer miss = new MediaPlayer();
        private MediaPlayer hit = new MediaPlayer();
        private MediaPlayer win = new MediaPlayer();
        private MediaPlayer lose = new MediaPlayer();
        public PlayGame()
        {
            this.InitializeComponent();
            miss.AutoPlay = false;
            hit.AutoPlay = false;
            win.AutoPlay = false;
            lose.AutoPlay = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Board && e.Parameter != null)
            {
                board = e.Parameter as Board;
                BindGrids();
            }
            if (e.Parameter is PlayGrid && e.Parameter != null)
            {
                board.PlayerGrid = e.Parameter as PlayGrid;
                IntilizeComputerGrid();
                BindGrids();
            }
            base.OnNavigatedTo(e);
            SetMusic();
        }

        public void IntilizeComputerGrid()
        {
            List<int> shipSizes = new List<int>() { 5, 4, 3, 3, 2 };
            List<string> directions = new List<string>();
            board.ComputerGrid.Ships = new Ship[]
            {
                new Ship() { Name = ShipNames.Carrier },
                new Ship() { Name = ShipNames.Battleship },
                new Ship() { Name = ShipNames.Destroyer },
                new Ship() { Name = ShipNames.Submarine },
                new Ship() { Name = ShipNames.Patrol_Boat }
            };
            for (int c = 0; c < 10; c++)
            {
                for (int r = 0; r < 10; r++)
                {
                    board.ComputerGrid.PlayerGrid[c, r].Location = $"{c}, {r}";
                    board.ComputerGrid.HiddenGrid[c, r].Location = $"{c}, {r}";
                }
            }
            foreach (int size in shipSizes)
            {
                shipIndex++;
                do
                {
                    int column = rand.Next(0, 10);
                    int row = rand.Next(0, 10);
                    if (board.ComputerGrid.PlayerGrid[column, row].GridState == CellState.Water)
                    {
                        directions = CheckDirections(size, row, column);
                        if (directions.Count > 0)
                        {
                            int direct = rand.Next(0, directions.Count);
                            board.ComputerGrid.PlayerGrid[column, row].GridState = CellState.Ship;
                            board.ComputerGrid.Ships[shipIndex].ShipCells.Add(board.ComputerGrid.PlayerGrid[column, row]);
                            if (directions[direct] == "Up")
                            {
                                Up(size, row, column);
                            }
                            else if (directions[direct] == "Down")
                            {
                                Down(size, row, column);
                            }
                            else if (directions[direct] == "Left")
                            {
                                Left(size, row, column);
                            }
                            else if (directions[direct] == "Right")
                            {
                                Right(size, row, column);
                            }
                        }
                    }
                } while (directions.Count == 0);
                directions.Clear();
            }
        }

        private List<string> CheckDirections(int size, int row, int column)
        {

            bool otherShips = false;
            List<string> directions = new List<string>();
            if (row - (size - 1) >= 0)
            {
                for (int i = 1; i < size; i++)
                {
                    if (board.ComputerGrid.PlayerGrid[column, row - i].GridState == CellState.Ship)
                    {
                        otherShips = true;
                        break;
                    }
                }
                if (!otherShips)
                {
                    directions.Add("Up");
                }
            }
            otherShips = false;
            if (row + (size - 1) <= 9)
            {
                for (int i = 1; i < size; i++)
                {
                    if (board.ComputerGrid.PlayerGrid[column, row + i].GridState == CellState.Ship)
                    {
                        otherShips = true;
                        break;
                    }
                }
                if (!otherShips)
                {
                    directions.Add("Down");
                }
            }
            otherShips = false;
            if (column - (size - 1) >= 0)
            {
                for (int i = 1; i < size; i++)
                {
                    if (board.ComputerGrid.PlayerGrid[column - i, row].GridState == CellState.Ship)
                    {
                        otherShips = true;
                        break;
                    }
                }
                if (!otherShips)
                {
                    directions.Add("Left");
                }
            }
            otherShips = false;
            if (column + (size - 1) <= 9)
            {
                for (int i = 1; i < size; i++)
                {
                    if (board.ComputerGrid.PlayerGrid[column + i, row].GridState == CellState.Ship)
                    {
                        otherShips = true;
                        break;
                    }
                }
                if (!otherShips)
                {
                    directions.Add("Right");
                }
            }
            return directions;
        }

        private void Up(int size, int row, int column)
        {
            for (int i = 1; i < size; i++)
            {
                board.ComputerGrid.PlayerGrid[column, row - i].GridState = CellState.Ship;
                board.ComputerGrid.Ships[shipIndex].ShipCells.Add(board.ComputerGrid.PlayerGrid[column, row - i]);

            }
        }

        private void Down(int size, int row, int column)
        {
            for (int i = 1; i < size; i++)
            {
                board.ComputerGrid.PlayerGrid[column, row + i].GridState = CellState.Ship;
                board.ComputerGrid.Ships[shipIndex].ShipCells.Add(board.ComputerGrid.PlayerGrid[column, row + i]);

            }
        }

        private void Left(int size, int row, int column)
        {
            for (int i = 1; i < size; i++)
            {
                board.ComputerGrid.PlayerGrid[column - i, row].GridState = CellState.Ship;
                board.ComputerGrid.Ships[shipIndex].ShipCells.Add(board.ComputerGrid.PlayerGrid[column - i, row]);

            }
        }

        private void Right(int size, int row, int column)
        {
            for (int i = 1; i < size; i++)
            {
                board.ComputerGrid.PlayerGrid[column + i, row].GridState = CellState.Ship;
                board.ComputerGrid.Ships[shipIndex].ShipCells.Add(board.ComputerGrid.PlayerGrid[column + i, row]);

            }
        }

        public void BindGrids()
        {
            StateValueToFillColorConverter stateValueToFillColorConverter = new StateValueToFillColorConverter();
            BindComputerGrid();

            for (int c = 1; c < 11; c++)
            {
                for (int r = 1; r < 11; r++)
                {

                    //Creates a black border that will take the size of the grid cell
                    Border border = new Border
                    {
                        BorderThickness = new Thickness(1),
                        BorderBrush = borderBrush,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch
                    };

                    //Create a new Rectangle with a name of Column, Row and Fill of barkBlue
                    Rectangle rect = new Rectangle
                    {
                        Name = $"{c - 1}, {r - 1}"
                    };

                    //Creates a binding for the cell IsAlive property, sets converter to bool converter, and 2-way mode
                    Binding newBinding = new Binding
                    {
                        Source = board.PlayerGrid.PlayerGrid[c - 1, r - 1],
                        Path = new PropertyPath("GridState"),
                        Converter = stateValueToFillColorConverter,
                        Mode = BindingMode.TwoWay
                    };

                    //Sets the binding to the retangle's fill property
                    rect.SetBinding(Rectangle.FillProperty, newBinding);

                    //Sets the border to the corresponding row and column
                    Grid.SetColumn(border, c);
                    Grid.SetRow(border, r);

                    //Adds rectangle to the border
                    border.Child = rect;

                    //Adds the border to Play
                    PlayerGrid.Children.Add(border);
                }
            }
        }

        public void BindComputerGrid()
        {
            StateValueToFillColorConverter stateValueToFillColorConverter = new StateValueToFillColorConverter();
            foreach (var c in ComputerGrid.Children)
            {
                if (c is Border)
                {
                    Border b = c as Border;
                    if (b.Child is Rectangle)
                    {
                        ComputerGrid.Children.Remove(c);
                    }
                }
            }
            if (board.ComputerGrid.IsHidden)
            {
                for (int c = 1; c < 11; c++)
                {
                    for (int r = 1; r < 11; r++)
                    {
                        //Creates a black border that will take the size of the grid cell
                        Border border = new Border
                        {
                            BorderThickness = new Thickness(1),
                            BorderBrush = borderBrush,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Stretch
                        };

                        //Create a new Rectangle with a name of Column, Row and Fill of barkBlue
                        Rectangle rect = new Rectangle
                        {
                            Name = $"{c - 1}, {r - 1}"
                        };

                        //Creates a binding for the cell IsAlive property, sets converter to bool converter, and 2-way mode
                        Binding newBinding = new Binding
                        {
                            Source = board.ComputerGrid.HiddenGrid[c - 1, r - 1],
                            Path = new PropertyPath("GridState"),
                            Converter = stateValueToFillColorConverter,
                            Mode = BindingMode.TwoWay
                        };

                        rect.Tapped += Attack_Ship;

                        //Sets the binding to the retangle's fill property
                        rect.SetBinding(Rectangle.FillProperty, newBinding);

                        //Sets the border to the corresponding row and column
                        Grid.SetColumn(border, c);
                        Grid.SetRow(border, r);

                        //Adds rectangle to the border
                        border.Child = rect;

                        //Adds the border to Play
                        ComputerGrid.Children.Add(border);
                    }
                }
            }
            else
            {
                for (int c = 1; c < 11; c++)
                {
                    for (int r = 1; r < 11; r++)
                    {

                        //Creates a black border that will take the size of the grid cell
                        Border border = new Border
                        {
                            BorderThickness = new Thickness(1),
                            BorderBrush = borderBrush,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Stretch
                        };

                        //Create a new Rectangle with a name of Column, Row and Fill of barkBlue
                        Rectangle rect = new Rectangle
                        {
                            Name = $"{c - 1}, {r - 1}"
                        };

                        //Creates a binding for the cell IsAlive property, sets converter to bool converter, and 2-way mode
                        Binding newBinding = new Binding
                        {
                            Source = board.ComputerGrid.PlayerGrid[c - 1, r - 1],
                            Path = new PropertyPath("GridState"),
                            Converter = stateValueToFillColorConverter,
                            Mode = BindingMode.TwoWay
                        };

                        rect.Tapped += Attack_Ship;

                        //Sets the binding to the retangle's fill property
                        rect.SetBinding(Rectangle.FillProperty, newBinding);

                        //Sets the border to the corresponding row and column
                        Grid.SetColumn(border, c);
                        Grid.SetRow(border, r);

                        //Adds rectangle to the border
                        border.Child = rect;

                        //Adds the border to Play
                        ComputerGrid.Children.Add(border);
                    }
                }
            }
        }

        private async void Attack_Ship(object sender, TappedRoutedEventArgs e)
        {
            StorageFolder musicFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile missMusicFile = await musicFolder.GetFileAsync("Miss.wav");
            StorageFile hitMusicFile = await musicFolder.GetFileAsync("Hit.wav");
            StorageFile winMusicFile = await musicFolder.GetFileAsync("Win.wav");
            hit.Source = MediaSource.CreateFromStorageFile(hitMusicFile);
            miss.Source = MediaSource.CreateFromStorageFile(missMusicFile);
            win.Source = MediaSource.CreateFromStorageFile(winMusicFile);
            if (playerTurn)
            {
                //Checks if sender is a rectangle
                if (sender is Rectangle)
                {
                    Rectangle rect = sender as Rectangle;
                    string[] coordinates = rect.Name.Split(", ");
                    int column = int.Parse(coordinates[0]);
                    int row = int.Parse(coordinates[1]);

                    if (board.ComputerGrid.HiddenGrid[column, row].GridState == CellState.Water)
                    {
                        if (board.ComputerGrid.PlayerGrid[column, row].GridState == CellState.Water)
                        {
                            board.ComputerGrid.PlayerGrid[column, row].GridState = CellState.Miss;
                            board.ComputerGrid.HiddenGrid[column, row].GridState = CellState.Miss;
                            miss.Play();
                            await (new MessageDialog("You Missed!")).ShowAsync();
                            playerTurn = false;
                            ComputerTurn();
                        }
                        else if (board.ComputerGrid.PlayerGrid[column, row].GridState == CellState.Ship)
                        {

                            foreach (Ship s in board.ComputerGrid.Ships)
                            {
                                foreach(Cell c in s.ShipCells)
                                {
                                    if(c.Location == board.ComputerGrid.PlayerGrid[column, row].Location)
                                    {
                                        c.GridState = CellState.Hit;
                                        s.CheckSunk();
                                        board.ComputerGrid.CheckEndGame();
                                        break;
                                    }
                                }
                            }
                            board.ComputerGrid.PlayerGrid[column, row].GridState = CellState.Hit;
                            board.ComputerGrid.HiddenGrid[column, row].GridState = CellState.Hit;
                            if (board.ComputerGrid.GameEnd)
                            {
                                win.Play();
                                await (new MessageDialog("You Win!")).ShowAsync();
                                this.Frame.Navigate(typeof(MainPage));
                            }
                            else
                            {
                                hit.Play();
                                await (new MessageDialog("You Hit!")).ShowAsync();
                                playerTurn = false;
                                ComputerTurn();
                            }
                        }
                    }
                    else
                    {
                        await (new MessageDialog("Please select a viable target.")).ShowAsync();
                    }
                }
            }
        }

        public async void ComputerTurn()
        {
            StorageFolder musicFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile missMusicFile = await musicFolder.GetFileAsync("Miss.wav");
            StorageFile hitMusicFile = await musicFolder.GetFileAsync("Hit.wav");
            StorageFile loseMusicFile = await musicFolder.GetFileAsync("Lose.wav");
            hit.Source = MediaSource.CreateFromStorageFile(hitMusicFile);
            miss.Source = MediaSource.CreateFromStorageFile(missMusicFile);
            lose.Source = MediaSource.CreateFromStorageFile(loseMusicFile);

            do
            {
                int column = rand.Next(0, 10);
                int row = rand.Next(0, 10);
                if (board.PlayerGrid.PlayerGrid[column, row].GridState == CellState.Water)
                {
                    board.PlayerGrid.PlayerGrid[column, row].GridState = CellState.Miss;
                    playerTurn = true;
                    miss.Play();
                    await (new MessageDialog("Computer Missed!")).ShowAsync();
                }
                else if (board.PlayerGrid.PlayerGrid[column, row].GridState == CellState.Ship)
                {
                    foreach (Ship s in board.PlayerGrid.Ships)
                    {
                        foreach (Cell c in s.ShipCells)
                        {
                            if (c.Location == board.PlayerGrid.PlayerGrid[column, row].Location)
                            {
                                c.GridState = CellState.Hit;
                                s.CheckSunk();
                                board.PlayerGrid.CheckEndGame();
                                break;
                            }
                        }
                    }
                    board.PlayerGrid.PlayerGrid[column, row].GridState = CellState.Hit;
                    if (board.PlayerGrid.GameEnd)
                    {
                        lose.Play();
                        await (new MessageDialog("Computer Wins!")).ShowAsync();
                        this.Frame.Navigate(typeof(MainPage));
                    }
                    else
                    {
                        hit.Play();
                        playerTurn = true;
                        await (new MessageDialog("Computer Hit!")).ShowAsync();
                    }
                }
            } while (!playerTurn);
        }

        private async void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker fileSavePicker = new FileSavePicker();
            fileSavePicker.FileTypeChoices.Add("Battleship", new List<string>() { ".bshp" });
            fileSavePicker.SuggestedFileName = "New Battleship";
            StorageFile file = await fileSavePicker.PickSaveFileAsync();
            if (file != null)
            {
                using (Stream fs = await file.OpenStreamForWriteAsync())
                {
                    fs.SetLength(0);
                    board.ComputerGrid.SetSaves();
                    board.PlayerGrid.SetSaves();
                    CachedFileManager.DeferUpdates(file);
                    Serializer.Serialize(fs, board);
                }
            }
            this.Frame.Navigate(typeof(MainPage));
        }

        private void CheatMode_Invoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            board.ComputerGrid.IsHidden = !board.ComputerGrid.IsHidden;
            BindComputerGrid();
        }

        private async void ComputerWins_Invoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            int column = 0;
            int row = 0;
            string[] coordinates;
            StorageFolder musicFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile hitMusicFile = await musicFolder.GetFileAsync("Hit.wav");
            hit.Source = MediaSource.CreateFromStorageFile(hitMusicFile);
            StorageFile loseMusicFile = await musicFolder.GetFileAsync("Lose.wav");
            lose.Source = MediaSource.CreateFromStorageFile(loseMusicFile);

            foreach (Ship s in board.PlayerGrid.Ships)
            {
                foreach(Cell c in s.ShipCells)
                {
                    coordinates = c.Location.Split(", ");
                    column = int.Parse(coordinates[0]);
                    row = int.Parse(coordinates[1]);
                    c.GridState = CellState.Hit;
                    board.PlayerGrid.PlayerGrid[column, row].GridState = CellState.Hit;
                    s.CheckSunk();
                    board.PlayerGrid.CheckEndGame();
                }
            }
            lose.Play();
            await (new MessageDialog("Computer Wins!")).ShowAsync();
            this.Frame.Navigate(typeof(MainPage));
        }

        public async void SetMusic()
        {
            MainPage.musicPlayer.AutoPlay = true;
            MainPage.musicPlayer.IsLoopingEnabled = true;
            StorageFolder musicFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile musicFile = await musicFolder.GetFileAsync("Battleship2.wav");
            MainPage.musicPlayer.Source = MediaSource.CreateFromStorageFile(musicFile);
        }
    }
}

using Battleship.Enums;
using Battleship.Models;
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
using Windows.UI;
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
    public sealed partial class CreateGame : Page
    {
        private PlayGrid createGrid = new PlayGrid("Fresh");
        private static Brush borderBrush = new SolidColorBrush(Colors.Black);
        private static readonly Brush water = new SolidColorBrush(Colors.Blue);
        private int shipSize = 0;
        private Cell currentCell;
        private bool canSetShip = true;
        private string shipName;
        private int shipIndex;
        public CreateGame()
        {
            this.InitializeComponent();
            createGrid.IsHidden = false;

            StateValueToFillColorConverter stateValueToFillColorConverter = new StateValueToFillColorConverter();
            for (int c = 1; c < 11; c++)
            {
                for (int r = 1; r < 11; r++)
                {
                    //Intilizes and sets the playCell object to a new cell as dead with it's corresponding row and column
                    createGrid.PlayerGrid[c-1, r-1] = new Cell
                    {
                        GridState = CellState.Water,
                        Location = $"{c-1}, {r-1}"
                    };

                    createGrid.HiddenGrid[c - 1, r - 1] = new Cell
                    {
                        GridState = CellState.Water,
                        Location = $"{c - 1}, {r - 1}"
                    };

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
                        Name = $"{c - 1}, {r - 1}",
                        Fill = water
                    };
                    //Subsctibes Tapped Toggle_Cell event to the rectangle
                    rect.Tapped += Set_Ship;

                    //Creates a binding for the cell IsAlive property, sets converter to bool converter, and 2-way mode
                    Binding newBinding = new Binding
                    {
                        Source = createGrid.PlayerGrid[c-1, r-1],
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
                    CreationView.Children.Add(border);
                }
            }
            SetMusic();
        }

        private async void Set_Ship(object sender, TappedRoutedEventArgs e)
        {
            //Checks if sender is a rectangle
            if (sender is Rectangle && !string.IsNullOrEmpty(ShipNameBox.SelectedValue as string))
            {
                if (canSetShip)
                {
                    //Grabs the rectangle name to form coordinates
                    Rectangle rect = sender as Rectangle;
                    string[] coordinates = rect.Name.Split(", ");
                    int column = int.Parse(coordinates[0]);
                    int row = int.Parse(coordinates[1]);
                    currentCell = createGrid.PlayerGrid[column, row];
                    bool canPlace = false;
                    if (ShipNameBox.SelectedValue.ToString() == "Carrier")
                    {
                        shipSize = 5;
                        shipName = "Carrier";
                        shipIndex = 0;
                        createGrid.Ships[shipIndex].ShipCells.Add(createGrid.PlayerGrid[column, row]);
                    }
                    else if (ShipNameBox.SelectedValue.ToString() == "Battleship")
                    {
                        shipSize = 4;
                        shipName = "Battleship";

                        shipIndex = 1;
                        createGrid.Ships[shipIndex].ShipCells.Add(createGrid.PlayerGrid[column, row]);

                    }
                    else if (ShipNameBox.SelectedValue.ToString() == "Destroyer" || ShipNameBox.SelectedValue.ToString() == "Submarine")
                    {
                        shipSize = 3;
                        if (ShipNameBox.SelectedValue.ToString() == "Submarine")
                        {
                            shipName = "Submarine";

                            shipIndex = 3;
                            createGrid.Ships[shipIndex].ShipCells.Add(createGrid.PlayerGrid[column, row]);

                        }
                        else
                        {
                            shipName = "Destroyer";

                            shipIndex = 2;

                            createGrid.Ships[shipIndex].ShipCells.Add(createGrid.PlayerGrid[column, row]);

                        }
                    }
                    else if (ShipNameBox.SelectedValue.ToString() == "Patrol Boat")
                    {
                        shipSize = 2;
                        shipName = "Patrol Boat";
                        shipIndex = 4;
                        createGrid.Ships[shipIndex].ShipCells.Add(createGrid.PlayerGrid[column, row]);
                    }
                    canPlace = CheckDirections(shipSize);
                    //Goes to playCells specific cell and toggle the IsAlive state
                    if (canPlace)
                    {
                        createGrid.PlayerGrid[column, row].GridState = CellState.Ship;
                        canSetShip = false;
                        
                        EnableDirections(shipSize);
                        ShipNameBox.Items.Remove(shipName);
                    }
                    else
                    {
                        await (new MessageDialog("Please place start location in viable location.")).ShowAsync();
                    }
                    
                }
                else
                {
                    await (new MessageDialog("Please finish placing ship.")).ShowAsync();
                }
            }
            else
            {
                await (new MessageDialog("Please select a ship type.")).ShowAsync();
            }
        }

        private bool CheckDirections(int size)
        {
            string[] coordinates = currentCell.Location.Split(", ");
            int column = int.Parse(coordinates[0]);
            int row = int.Parse(coordinates[1]);
            bool otherShips = false;
            bool canPlace = true;
            int canPlaceDirections = 0;
            if (row - (size - 1) >= 0)
            {
                for (int i = 1; i < size; i++)
                {
                    if (createGrid.PlayerGrid[column, row - i].GridState == CellState.Ship)
                    {
                        otherShips = true;
                        break;
                    }
                }
                if (!otherShips)
                {
                    canPlaceDirections++;
                }
            }
            otherShips = false;
            if (row + (size - 1) <= 9 && canPlaceDirections == 0)
            {
                for (int i = 1; i < size; i++)
                {
                    if (createGrid.PlayerGrid[column, row + i].GridState == CellState.Ship)
                    {
                        otherShips = true;
                        break;
                    }
                }
                if (!otherShips)
                {
                    canPlaceDirections++;
                }
            }
            otherShips = false;
            if (column - (size - 1) >= 0 && canPlaceDirections == 0)
            {
                for (int i = 1; i < size; i++)
                {
                    if (createGrid.PlayerGrid[column - i, row].GridState == CellState.Ship)
                    {
                        otherShips = true;
                        break;
                    }
                }
                if (!otherShips)
                {
                    canPlaceDirections++;
                }
            }
            otherShips = false;
            if (column + (size - 1) <= 9 && canPlaceDirections == 0)
            {
                for (int i = 1; i < size; i++)
                {
                    if (createGrid.PlayerGrid[column + i, row].GridState == CellState.Ship)
                    {
                        otherShips = true;
                        break;
                    }
                }
                if (!otherShips)
                {
                    canPlaceDirections++;
                }
            }
            if(canPlaceDirections == 0)
            {
                canPlace = false;
            }
            return canPlace;
        }

        private void EnableDirections(int size)
        {
            string[] coordinates = currentCell.Location.Split(", ");
            int column = int.Parse(coordinates[0]);
            int row = int.Parse(coordinates[1]);
            bool otherShips = false;
            if(row - (size - 1) >= 0)
            {
                for (int i = 1; i < size; i++)
                {
                    if(createGrid.PlayerGrid[column, row - i].GridState == CellState.Ship)
                    {
                        otherShips = true;
                        break;
                    }
                }
                if (!otherShips)
                {
                    Up.IsEnabled = true;
                }
            }
            otherShips = false;
            if (row + (size - 1) <= 9)
            {
                for (int i = 1; i < size; i++)
                {
                    if (createGrid.PlayerGrid[column , row + i].GridState == CellState.Ship)
                    {
                        otherShips = true;
                        break;
                    }
                }
                if (!otherShips)
                {
                    Down.IsEnabled = true;
                }
            }
            otherShips = false;
            if (column - (size - 1) >= 0)
            {
                for (int i = 1; i < size; i++)
                {
                    if (createGrid.PlayerGrid[column - i, row].GridState == CellState.Ship)
                    {
                        otherShips = true;
                        break;
                    }
                }
                if (!otherShips)
                {
                    Left.IsEnabled = true;
                }
            }
            otherShips = false;
            if (column + (size - 1) <= 9)
            {
                for (int i = 1; i < size; i++)
                {
                    if (createGrid.PlayerGrid[column + i, row].GridState == CellState.Ship)
                    {
                        otherShips = true;
                        break;
                    }
                }
                if (!otherShips)
                {
                    Right.IsEnabled = true;
                }
            }
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            string[] coordinates = currentCell.Location.Split(", ");
            int column = int.Parse(coordinates[0]);
            int row = int.Parse(coordinates[1]);
            for (int i = 1; i < shipSize; i++)
            {
                createGrid.PlayerGrid[column, row - i].GridState = CellState.Ship;
                createGrid.Ships[shipIndex].ShipCells.Add(createGrid.PlayerGrid[column, row - i]);
            }
            Up.IsEnabled = false;
            Down.IsEnabled = false;
            Left.IsEnabled = false;
            Right.IsEnabled = false;
            canSetShip = true;

            if (ShipNameBox.Items.Count == 0)
            {
                StartGame.IsEnabled = true;
                canSetShip = false;
            }
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            string[] coordinates = currentCell.Location.Split(", ");
            int column = int.Parse(coordinates[0]);
            int row = int.Parse(coordinates[1]);
            for (int i = 1; i < shipSize; i++)
            {
                createGrid.PlayerGrid[column - i, row].GridState = CellState.Ship;
                createGrid.Ships[shipIndex].ShipCells.Add(createGrid.PlayerGrid[column - 1, row]);
            }
            Up.IsEnabled = false;
            Down.IsEnabled = false;
            Left.IsEnabled = false;
            Right.IsEnabled = false;
            canSetShip = true;

            if (ShipNameBox.Items.Count == 0)
            {
                StartGame.IsEnabled = true;
                canSetShip = false;
            }
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            string[] coordinates = currentCell.Location.Split(", ");
            int column = int.Parse(coordinates[0]);
            int row = int.Parse(coordinates[1]);
            for (int i = 1; i < shipSize; i++)
            {
                createGrid.PlayerGrid[column + i, row].GridState = CellState.Ship;
                createGrid.Ships[shipIndex].ShipCells.Add(createGrid.PlayerGrid[column + 1, row]);

            }
            Up.IsEnabled = false;
            Down.IsEnabled = false;
            Left.IsEnabled = false;
            Right.IsEnabled = false;
            canSetShip = true;

            if (ShipNameBox.Items.Count == 0)
            {
                StartGame.IsEnabled = true;
                canSetShip = false;
            }
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            string[] coordinates = currentCell.Location.Split(", ");
            int column = int.Parse(coordinates[0]);
            int row = int.Parse(coordinates[1]);
            for (int i = 1; i < shipSize; i++)
            {
                createGrid.PlayerGrid[column, row + i].GridState = CellState.Ship;
                createGrid.Ships[shipIndex].ShipCells.Add(createGrid.PlayerGrid[column, row + i]);
            }
            Up.IsEnabled = false;
            Down.IsEnabled = false;
            Left.IsEnabled = false;
            Right.IsEnabled = false;
            canSetShip = true;
            if(ShipNameBox.Items.Count == 0)
            {
                StartGame.IsEnabled = true;
                canSetShip = false;
            }
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PlayGame), createGrid);
        }

        public async void SetMusic()
        {
            MainPage.musicPlayer.AutoPlay = true;
            MainPage.musicPlayer.IsLoopingEnabled = true;
            StorageFolder musicFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile musicFile = await musicFolder.GetFileAsync("Waves.wav");
            MainPage.musicPlayer.Source = MediaSource.CreateFromStorageFile(musicFile);
        }
    }
    
}

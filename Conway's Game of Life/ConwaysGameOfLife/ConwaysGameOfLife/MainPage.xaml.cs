using ConwaysGameOfLife.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ConwaysGameOfLife
{

    public class BoolToFillColorConverter : IValueConverter
    {
        private static readonly Brush dead = new SolidColorBrush(Colors.DarkBlue);
        private static readonly Brush alive = new SolidColorBrush(Colors.ForestGreen);
        /// <summary>
        /// Based on the IsAlive property of the cell returns a color to represent that state
        /// </summary>
        /// <param name="value">Value is the binded property, which is a Cell's IsAlive property</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns>Returns the Color representing Alive or Dead</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            bool check;
            if (value is bool)
            {
                check = bool.Parse(value.ToString());
                if (check)
                {
                    return alive;
                }
                else
                {
                    return dead;
                }
            }
            else
            {
                return new SolidColorBrush(Colors.Black);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
    /// <summary>
    /// Conways Game of Life
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Initilizes objects for brush, random and playCells
        private static Brush dead = new SolidColorBrush(Colors.DarkBlue);
        private static Brush alive = new SolidColorBrush(Colors.ForestGreen);
        private static Brush borderBrush = new SolidColorBrush(Colors.Black);
        private static Random rand = new Random();
        private static Cell[,] playCells;
        private DispatcherTimer dispatchTimer = new DispatcherTimer();
        /// <summary>
        /// Starts the Page using the XAML format
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

        }

        /// <summary>
        /// Using the create button create a Grid Based on the HeightNum and WidthNum TextBox
        /// </summary>
        /// <param name="sender">Create Button</param>
        /// <param name="e"></param>
        private async void Create_Grid(object sender, RoutedEventArgs e)
        {
            //Intilizes the rows and columns based on textboxs and bool to fill converter
            int rows = int.Parse(HeightNum.Text);
            int columns = int.Parse(WidthNum.Text);
            BoolToFillColorConverter boolToFillColorConverter = new BoolToFillColorConverter();
            //Checks if the rows and columns are not within the range of 10-100
            if (rows < 10 || rows > 100 || columns < 10 || columns > 100)
            {
                //Popup window stating that the input was invalid
                await (new MessageDialog("Please enter a number from 10-100 in both height and width.")).ShowAsync();
            }
            else
            {
                //Clears the grid named Play of columns and rows ir there are columns
                if (Play.ColumnDefinitions.Count != 0)
                {
                    Play.Children.Clear();
                    Play.ColumnDefinitions.Clear();
                    Play.RowDefinitions.Clear();
                }
                //Fills out Play with columns based on the input
                for (int i = 0; i < columns; i++)
                {
                    var col = new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    };
                    Play.ColumnDefinitions.Add(col);
                }
                //Fills out Play with rows based on the input
                for (int i = 0; i < rows; i++)
                {
                    var row = new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    };
                    Play.RowDefinitions.Add(row);
                }
                
                //makes playCells equal to a new cell 2d array based on columns and rows
                playCells = new Cell[columns, rows];

                //Nested for loops to cycle throught playCells and Play, c = column # and r = row #
                for (int c = 0; c < columns; c++)
                {
                    for (int r = 0; r < rows; r++)
                    {
                        //Intilizes and sets the playCell object to a new cell as dead with it's corresponding row and column
                        playCells[c, r] = new Cell
                        {
                            IsAlive = false,
                            NextState = false
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
                            Name = $"{c}, {r}",
                            Fill = dead
                        };
                        //Subsctibes Tapped Toggle_Cell event to the rectangle
                        rect.Tapped += Toggle_Cell;

                        //Creates a binding for the cell IsAlive property, sets converter to bool converter, and 2-way mode
                        Binding newBinding = new Binding
                        {
                            Source = playCells[c, r],
                            Path = new PropertyPath("IsAlive"),
                            Converter = boolToFillColorConverter,
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
                        Play.Children.Add(border);
                    }
                }
            }
            //Enables play Button and play one button
            PlayButton.IsEnabled = true;
            PlayOneButton.IsEnabled = true;
        }

        /// <summary>
        /// When a rectangle is tapped change its corrsponing cells IsAlive
        /// </summary>
        /// <param name="sender">The rectangle</param>
        /// <param name="e"></param>
        private void Toggle_Cell(object sender, TappedRoutedEventArgs e)
        {
            //Checks if sender is a rectangle
            if (sender is Rectangle)
            {
                //Grabs the rectangle name to form coordinates
                Rectangle rect = sender as Rectangle;
                string[] coordinates = rect.Name.Split(", ");
                int column = int.Parse(coordinates[0]);
                int row = int.Parse(coordinates[1]);
                //Goes to playCells specific cell and toggle the IsAlive state
                playCells[column, row].Toggle();
            }
        }

        /// <summary>
        /// Plays one generation on Skip 1 button
        /// </summary>
        /// <param name="sender">Skip 1 Button</param>
        /// <param name="e"></param>
        private void Play_One(object sender, RoutedEventArgs e)
        {
            //Initilizes count of living neighbours, rows, columns, and a 2-d bool array
            int countLiveNeighbours = 0;
            int rows = Play.RowDefinitions.Count;
            int columns = Play.ColumnDefinitions.Count;

            //Iterates through each playCell (c = column, r = row)
            for (int c = 0; c < columns; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    //Checks if the cell is in the center
                    if (c != 0 && c != (columns - 1) && r != 0 && r != (rows - 1))
                    {
                        //Checks all 8 neighbors, if alive increment countAliveNeighbours
                        if (playCells[c - 1, r].IsAlive) { countLiveNeighbours++; };
                        if (playCells[c + 1, r].IsAlive) { countLiveNeighbours++; };
                        if (playCells[c, r - 1].IsAlive) { countLiveNeighbours++; };
                        if (playCells[c, r + 1].IsAlive) { countLiveNeighbours++; };
                        if (playCells[c + 1, r + 1].IsAlive && countLiveNeighbours < 4) { countLiveNeighbours++; };
                        if (playCells[c + 1, r - 1].IsAlive && countLiveNeighbours < 4) { countLiveNeighbours++; };
                        if (playCells[c - 1, r + 1].IsAlive && countLiveNeighbours < 4) { countLiveNeighbours++; };
                        if (playCells[c - 1, r - 1].IsAlive && countLiveNeighbours < 4) { countLiveNeighbours++; };
                    }
                    else
                    {
                        //Check for Top Left Corner
                        if (c == 0 && r == 0)
                        {
                            //Checks all 3 neighbors, if alive increment countAliveNeighbours
                            if (playCells[c + 1, r].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c, r + 1].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c + 1, r + 1].IsAlive) { countLiveNeighbours++; };
                        }
                        //Checks Top Right Corner
                        else if (c == 0 && r == rows - 1)
                        {
                            //Checks all 3 neighbors, if alive increment countAliveNeighbours
                            if (playCells[c + 1, r].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c, r - 1].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c + 1, r - 1].IsAlive) { countLiveNeighbours++; };
                        }
                        //Check for Bottom Left Corner
                        else if (c == columns - 1 && r == 0)
                        {
                            //Checks all 3 neighbors, if alive increment countAliveNeighbours
                            if (playCells[c - 1, r].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c, r + 1].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c - 1, r + 1].IsAlive) { countLiveNeighbours++; };
                        }
                        //Check Bottom Right Corner
                        else if (c == columns - 1 && r == rows - 1)
                        {
                            //Checks all 3 neighbors, if alive increment countAliveNeighbours
                            if (playCells[c - 1, r].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c, r - 1].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c - 1, r - 1].IsAlive) { countLiveNeighbours++; };
                        }
                        //Check for cells on Left Edge
                        else if (c == 0)
                        {
                            //Checks all 5 neighbors, if alive increment countAliveNeighbours
                            if (playCells[c + 1, r].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c, r + 1].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c + 1, r + 1].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c, r - 1].IsAlive && countLiveNeighbours < 4) { countLiveNeighbours++; };
                            if (playCells[c + 1, r - 1].IsAlive && countLiveNeighbours < 4) { countLiveNeighbours++; };
                        }
                        //Check for cells on Right Edge
                        else if (c == columns - 1)
                        {
                            //Checks all 5 neighbors, if alive increment countAliveNeighbours
                            if (playCells[c - 1, r].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c, r - 1].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c - 1, r - 1].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c, r + 1].IsAlive && countLiveNeighbours < 4) { countLiveNeighbours++; };
                            if (playCells[c - 1, r + 1].IsAlive && countLiveNeighbours < 4) { countLiveNeighbours++; };
                        }
                        //Check for cells on Top Edge
                        else if (r == 0)
                        {
                            //Checks all 5 neighbors, if alive increment countAliveNeighbours
                            if (playCells[c - 1, r].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c, r + 1].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c - 1, r + 1].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c + 1, r + 1].IsAlive && countLiveNeighbours < 4) { countLiveNeighbours++; };
                            if (playCells[c + 1, r].IsAlive && countLiveNeighbours < 4) { countLiveNeighbours++; };
                        }
                        //Check for Cells on Bottom Edge
                        else if (r == rows - 1)
                        {
                            //Checks all 5 neighbors, if alive increment countAliveNeighbours
                            if (playCells[c - 1, r].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c, r - 1].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c - 1, r - 1].IsAlive) { countLiveNeighbours++; };
                            if (playCells[c + 1, r - 1].IsAlive && countLiveNeighbours < 4) { countLiveNeighbours++; };
                            if (playCells[c - 1, r - 1].IsAlive && countLiveNeighbours < 4) { countLiveNeighbours++; };
                        }
                    }
                    //Checks if the current cell is Alive
                    if (playCells[c, r].IsAlive)
                    {
                        //If live neighbors is less than 2 or over 3, kill cell
                        if (countLiveNeighbours < 2 || countLiveNeighbours > 3)
                        {
                            playCells[c, r].NextState = false;
                        }
                        //If live neighbors is 2-3, keep alive
                        //else
                        //{
                        //    playCells[c, r].NextState = true;
                        //}
                    }
                    else
                    {
                        //If live neighbors is 3, revive cell
                        if (countLiveNeighbours == 3)
                        {
                            playCells[c, r].NextState = true;
                        }
                        //If live neighbors not 3, keep cell dead
                        //else
                        //{
                        //    playCells[c, r].NextState = false;
                        //}
                    }
                    //Reset counf for living neighbours
                    countLiveNeighbours = 0;
                }
            }
            //Cycle through the playCells
            for (int c = 0; c < columns; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    //Change the playCells IsAlive based on newPlayCells
                    if(playCells[c, r].IsAlive != playCells[c, r].NextState)
                    {
                        playCells[c, r].IsAlive = playCells[c, r].NextState;
                    }
                }
            }
        }

        /// <summary>
        /// Creates a fresh Grid and randomly Sets each IsAlive Property
        /// </summary>
        /// <param name="sender">Randomize Button</param>
        /// <param name="e"></param>
        private async void Randomize(object sender, RoutedEventArgs e)
        {
            //Intilizes the rows and columns based on textboxs and bool to fill converter and randomize
            int rows = int.Parse(HeightNum.Text);
            int columns = int.Parse(WidthNum.Text);
            int randomize = 0;
            BoolToFillColorConverter boolToFillColorConverter = new BoolToFillColorConverter();
            //Checks if the rows and columns are not within the range of 10-100
            if (rows < 10 || rows > 100 || columns < 10 || columns > 100)
            {
                //Popup window stating that the input was invalid
                await (new MessageDialog("Please enter a number from 10-100 in both height and width.")).ShowAsync();
            }
            else
            {
                //Clears the grid named Play of columns and rows ir there are columns
                if (Play.ColumnDefinitions.Count != 0)
                {
                    Play.Children.Clear();
                    Play.ColumnDefinitions.Clear();
                    Play.RowDefinitions.Clear();
                }
                //Fills out Play with columns based on the input
                for (int i = 0; i < columns; i++)
                {
                    var col = new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    };
                    Play.ColumnDefinitions.Add(col);
                }
                //Fills out Play with rows based on the input
                for (int i = 0; i < rows; i++)
                {
                    var row = new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    };
                    Play.RowDefinitions.Add(row);
                }
                //makes playCells equal to a new cell 2d array based on columns and rows
                
                playCells = new Cell[columns, rows];

                //Nested for loops to cycle throught playCells and Play, c = column # and r = row #
                for (int c = 0; c < columns; c++)
                {
                    for (int r = 0; r < rows; r++)
                    {
                        //Intilizes and sets the playCell object to a new cell as dead with it's corresponding row and column
                        playCells[c, r] = new Cell
                        {
                            IsAlive = false
                        };
                        //Randomize picks a number from 1-2
                        randomize = rand.Next(1, 3);
                        //Makes the cell live if randomize is equal to 2
                        if (randomize == 2)
                        {
                            playCells[c, r].Toggle();
                            playCells[c, r].NextState = true;
                        }

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
                            Name = $"{c}, {r}",
                            Fill = dead
                        };
                        //Subsctibes Tapped Toggle_Cell event to the rectangle
                        rect.Tapped += Toggle_Cell;

                        //Creates a binding for the cell IsAlive property, sets converter to bool converter, and 2-way mode
                        Binding newBinding = new Binding
                        {
                            Source = playCells[c, r],
                            Path = new PropertyPath("IsAlive"),
                            Converter = boolToFillColorConverter,
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
                        Play.Children.Add(border);
                    }
                }
            }
            //Enables play Button and play one button
            PlayButton.IsEnabled = true;
            PlayOneButton.IsEnabled = true;
        }

        /// <summary>
        /// Sets and automatic play play based on the Gen per Seconds Textbox
        /// </summary>
        /// <param name="sender">Play Button</param>
        /// <param name="e"></param>
        private async void Play_Cycle(object sender, TappedRoutedEventArgs e)
        {
            //Sets the time from PlayNum Textbox
            int times = int.Parse(PlayNum.Text);
            //Checks if times ranges from 1-15
            if (times > 0 && times < 16)
            {
                dispatchTimer.Interval = TimeSpan.FromMilliseconds(1000 / times);
                dispatchTimer.Tick += Timer_Tick;
                //Checks if the sender is a button
                if (sender is Button)
                {
                    //Sets cycle equal to sender as a button
                    Button cycle = sender as Button;
                    //Checks the button contect as Play or Stop
                    if (cycle.Content.Equals("Play"))
                    {
                        //Changes the button content to Stop
                        cycle.Content = "Stop";
                        //Disables PlayNum textbox, CreateGrid Button, PlayOne, and RandomizeGrid Button
                        PlayNum.IsEnabled = false;
                        CreateGrid.IsEnabled = false;
                        RandomizeGrid.IsEnabled = false;
                        PlayOneButton.IsEnabled = false;
                        //Runs Play_One every times per second till Play Button content is not equal to Stop
                        //while (cycle.Content.Equals("Stop"))
                        //{
                        //    Play_One(sender, e);
                        //    await Task.Delay(1000 / times);
                        //}
                        dispatchTimer.Start();
                    }
                    else if (cycle.Content.Equals("Stop"))
                    {
                        //Changes Play button Content to Play and enables PlayNum textbox, CreateGrid, PlayOne and RandomizeGrid Buttons
                        cycle.Content = "Play";
                        dispatchTimer.Stop();
                        PlayNum.IsEnabled = true;
                        CreateGrid.IsEnabled = true;
                        RandomizeGrid.IsEnabled = true;
                        PlayOneButton.IsEnabled = true;
                    }
                }
            }
            //Times is not within range, popsup a message saying the the number is invalid
            else
            {
                await (new MessageDialog("Please enter a number from 1-15 in Gen per Seconds.")).ShowAsync();
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            Play_One(PlayOneButton, new RoutedEventArgs());
        }

    }
}

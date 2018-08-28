using Battleship.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Battleship
{
    public class StateValueToFillColorConverter : IValueConverter
    {
        private static readonly Brush water = new SolidColorBrush(Colors.Blue);
        private static readonly Brush ship = new SolidColorBrush(Colors.Gray);
        private static readonly Brush miss = new SolidColorBrush(Colors.White);
        private static readonly Brush hit = new SolidColorBrush(Colors.Red);
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is CellState)
            {
                CellState cellState = (CellState)value;

                if (cellState == CellState.Water)
                {
                    return water;

                }
                else if (cellState == CellState.Ship)
                {
                    return ship;

                }
                else if (cellState == CellState.Hit)
                {
                    return hit;
                }
                else if (cellState == CellState.Miss)
                {
                    return miss;
                }
                else
                {
                    return new SolidColorBrush(Colors.Black);
                }
            }
            else
            {
                return new SolidColorBrush(Colors.Black);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new SolidColorBrush(Colors.Black);
        }
    }
}

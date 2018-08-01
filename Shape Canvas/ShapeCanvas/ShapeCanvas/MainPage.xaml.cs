using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace ShapeCanvas
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Brush color = new SolidColorBrush();
        private Random rand = new Random();

        public MainPage()
        {
            this.InitializeComponent();
            Play.Clip = new RectangleGeometry
            {
                Rect = new Rect { Height = 200, Width = 195 }
            };
        }

        private void Play_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var position = e.GetPosition(sender as Canvas);
            Canvas can = sender as Canvas;
            int shape = rand.Next(1, 3);
            int height = rand.Next(25, 101);
            int width = rand.Next(25, 101);
            byte alpha = 255;
            byte r = (byte)rand.Next(0, 256);
            byte g = (byte)rand.Next(0, 256);
            byte b = (byte)rand.Next(0, 256);
            color = new SolidColorBrush(Color.FromArgb(alpha, r, g, b));
            if (shape == 1)
            {
                Rectangle rect = new Rectangle
                {
                    Height = height,
                    Width = width,
                    Fill = color
                };
                Canvas.SetLeft(rect, position.X - (width / 2));
                Canvas.SetTop(rect, position.Y - (height / 2));
                rect.RightTapped += RemoveShapeOnRightTap;
                can.Children.Add(rect);
            }
            else if (shape == 2)
            {
                Ellipse ellip = new Ellipse
                {
                    Height = height,
                    Width = width,
                    Fill = color
                };
                Canvas.SetTop(ellip, position.Y - (height / 2));
                Canvas.SetLeft(ellip, position.X - (width / 2));
                ellip.RightTapped += RemoveShapeOnRightTap;
                can.Children.Add(ellip);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Play.Children.Clear();
        }

        private void RemoveShapeOnRightTap(object sender, RightTappedRoutedEventArgs e)
        {
            if (sender is Shape)
            {
                Shape shap = sender as Shape;
                foreach (Shape shape in Play.Children)
                {
                    if (shap.Equals(shape))
                    {
                        Play.Children.Remove(shap);
                    }
                }
            }
        }
    }
}

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

namespace UWPEventDemo_A
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Brush on = new SolidColorBrush(Colors.Yellow);
        private Brush off = new SolidColorBrush(Colors.Black);
        private bool isOn = false;

        public MainPage()
        {
            this.InitializeComponent();

            Canvas c = new Canvas();
            Shape s;
        }

        private async void Canvas_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await (new MessageDialog("You Tapped")).ShowAsync();
        }

        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    isOn = !isOn;
        //    LightbulbRect.Fill = isOn ? on : off;
        //}
    }
}

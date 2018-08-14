using ItemsControlDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ItemsControlDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Zombie> zombies = new List<Zombie>()
        {
            new Zombie() { NameInLife= "Robert Paulson", DoesThrillerDance = true, NumOfTeeth = 12, Variant = "Tank"},
            new Zombie() { NameInLife= "Michael Jackson", DoesThrillerDance = true, NumOfTeeth = 74, Variant = "Thriller Zombie"},
            new Zombie() { NameInLife= "Ron Swanson", DoesThrillerDance = false, NumOfTeeth = 2, Variant = "Mustache"},
            new Zombie() { NameInLife= "Robert Wesker", DoesThrillerDance = false, NumOfTeeth = 32, Variant = "Betrayer/Corporate Schill"}

        };
        public MainPage()
        {
            this.InitializeComponent();

            ZombiesListView.ItemsSource = zombies;
        }
    }
}

using DataBindingDemo_A.Models;
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

namespace DataBindingDemo_A
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /*
         *Data Binding requires 3 things
         * 1) A Source
         *      Set the Source property of the DataBinding object directly
         *      Set the DataContext property of the dependent
         *      Set the ElementName property of the Databinding dependent
         * 2) A Path
         *      This refers to the property of the source object (or the entire source itself)
         * 3) A Mode
         *      Determines the modality of the Binding
         */

        private Dragon sourceDragon = new Dragon()
        {
            Name = "Toothless",
            Age = 17,
            CanFly = true,
            ScaleColor = "NightFury Black"
        };
        public MainPage()
        {
            this.InitializeComponent();
            Binding newBinding = new Binding();
            newBinding.Source = sourceDragon;
            newBinding.Path = new PropertyPath("Name");
            newBinding.Mode = BindingMode.TwoWay;

            NameTextBox.SetBinding(TextBox.TextProperty, newBinding);

            AgeTextBox.DataContext = sourceDragon;
            ScaleColorTextBox.DataContext = sourceDragon;
            CanFlyTextBox.DataContext = sourceDragon;
            Name2TextBox.DataContext = sourceDragon;
            Age2TextBox.DataContext = sourceDragon;
            ScaleColor2TextBox.DataContext = sourceDragon;
            CanFly2TextBox.DataContext = sourceDragon;
        }
    }
}

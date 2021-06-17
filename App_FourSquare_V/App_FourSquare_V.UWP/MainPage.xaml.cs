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

namespace App_FourSquare_V.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            Xamarin.FormsMaps.Init("AnvniotNMK44EH4B0O8lLLyG1iVJkAA0P26rAlKeGcKsZvRZoMGOVjHFBQDH9EIk");
            //AnvniotNMK44EH4B0O8lLLyG1iVJkAA0P26rAlKeGcKsZvRZoMGOVjHFBQDH9EIk
            LoadApplication(new App_FourSquare_V.App());
        }
    }
}

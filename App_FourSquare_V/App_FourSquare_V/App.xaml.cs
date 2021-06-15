using App_FourSquare_V.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_FourSquare_V
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new FQMainView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

using App_FourSquare_V.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_FourSquare_V.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FQMainView : ContentPage
    {
        public FQMainView()
        {
            InitializeComponent();

            BindingContext = new FQViewModel();
        }
    }
}
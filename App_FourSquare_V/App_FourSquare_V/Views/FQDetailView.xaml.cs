using App_FourSquare_V.Models;
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
    public partial class FQDetailView : ContentPage
    {
        public FQDetailView(FQViewModel FQ_VMain)
        {
            InitializeComponent();

            BindingContext = new FQDetailViewModel(FQ_VMain);
        }

        public FQDetailView(FQViewModel FQ_VMain, FQModel placeSelected)
        {
            InitializeComponent();

            BindingContext = new FQDetailViewModel(FQ_VMain, placeSelected);
        }

      

    }
}
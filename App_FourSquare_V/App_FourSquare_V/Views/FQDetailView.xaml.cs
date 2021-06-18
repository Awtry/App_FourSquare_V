using App_FourSquare_V.Models;
using App_FourSquare_V.Service;
using App_FourSquare_V.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
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

            placeSelected.Picture = new ImageService().SaveImageFromBase64(placeSelected.Picture, placeSelected.id_Place);
            FQMap.FQ = placeSelected;

            if (placeSelected != null)
            {
                FQMap.MoveToRegion(
                     MapSpan.FromCenterAndRadius(
                       new Position(
                           placeSelected.Latitude,
                           placeSelected.Longitude
                          ),
                       Distance.FromMiles(.5)
                    )
                 );
            }

            FQMap.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = placeSelected.Name,
                    Position = new Position(
                        placeSelected.Latitude,
                        placeSelected.Longitude
                    )
                }
             );


        }


    }

}
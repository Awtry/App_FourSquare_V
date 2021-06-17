using App_FourSquare_V.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_FourSquare_V.ViewModels
{
    public class FQDetailViewModel : BaseViewModel
    {

        // ------------------- LLAMADAS GLOBALES ---------------------

        FQViewModel FQViewModel;

        // ------------------- ENCAPSULAMIENTO ---------------------

        int FQ_ID;

        string _FQName;
        public string FQName { get => _FQName; set => SetProperty(ref _FQName, value); }

        string _FQLocation;
        public string FQLocation { get => _FQLocation; set => SetProperty(ref _FQLocation, value); }

        double _FQLatitude;
        public double FQLatitude { get => _FQLatitude; set => SetProperty(ref _FQLatitude, value); }

        double _FQLongitude;
        public double FQLongitude { get => _FQLongitude; set => SetProperty(ref _FQLongitude, value); }


        // ------------------- IMAGEN ---------------------

        string _FQPicture;
        public string FQPicture { get => _FQPicture; set => SetProperty(ref _FQPicture, value); }

        // ------------------- CONSTRUCTORES ---------------------

        private FQModel placeSelected;
        public FQModel PlaceSelected
        {
            get => placeSelected;
            set => SetProperty(ref placeSelected, value);
        }

        public FQDetailViewModel(FQViewModel FQ_VModel)
        {
            FQViewModel = FQ_VModel;
        }

        public FQDetailViewModel(FQViewModel FQ_VModel, FQModel placeSelected)
        {
            FQViewModel = FQ_VModel;

            FQ_ID = placeSelected.Id_Place;
            FQName = placeSelected.Name;
            FQLocation = placeSelected.Location;
            FQPicture = placeSelected.Picture;
            FQLatitude = placeSelected.Latitude;
            FQLongitude = placeSelected.Longitude;
        }
    }
}

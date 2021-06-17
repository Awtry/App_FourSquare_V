using App_FourSquare_V.Models;
using App_FourSquare_V.Service;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_FourSquare_V.ViewModels
{
    public class FQDetailViewModel : BaseViewModel
    {

        // ------------------- LLAMADAS GLOBALES ---------------------

        FQViewModel FQViewModel;

        // ------------------- COMANDOS ---------------------

        Command _SaveCommand;
        public Command SaveCommand => _SaveCommand ?? (_SaveCommand = new Command(SaveAction));

        Command _DeleteCommand;
        public Command DeleteCommand => _DeleteCommand ?? (_DeleteCommand = new Command(DeleteAction));

        Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(LocationAction));

        Command _takePictureCommand;
        public Command takePictureCommand => _takePictureCommand ?? (_takePictureCommand = new Command(PhotoAction));

        Command _selectPictureCommand;
        public Command selectPictureCommand => _selectPictureCommand ?? (_selectPictureCommand = new Command(SelectPhotoAction));

        // ------------------- ENCAPSULAMIENTO ---------------------

        #region ENCAPSULAMIENTO

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

        #endregion

        // ------------------- CONSTRUCTORES ---------------------

        #region CONSTRUCTORES 

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

            
        #endregion

        // ------------------- ACCIONES ---------------------

        private async void SaveAction()
        {
            ResponseModel response;

            try
            {
                FQModel placeSelected = new FQModel
                {
                    Id_Place = FQ_ID,
                    Name = FQName,
                    Location = FQLocation,
                    Latitude = FQLatitude,
                    Longitude = FQLongitude,
                    Picture = FQPicture
                };

                if (placeSelected.Id_Place > 0)
                {
                    //Update
                    response = await new ApiService().PutDataAsync("FQ", placeSelected);
                }
                else
                {
                    //Post
                    response = await new ApiService().PostDataAsync("FQ", placeSelected);
                }

                if (response == null || !response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("FourSquare_V", $"Error adding the place mate:  {response.Message}", "OK");
                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }

            //TODO: Agregar la recarga automatica después del agregrar o actualizar.
           
        }

        private async void DeleteAction()
        {
            ResponseModel response;
            try
            {
                if (FQ_ID > 0)
                {
                    response = await new ApiService().DeleteDataAsync("FQ", FQ_ID);
                }

                //TODO: Agregar la recarga automática
                Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void LocationAction(object obj)
        {
            try
            {

                FQLatitude = FQLongitude = 0;
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    FQLatitude = location.Latitude;
                    FQLongitude = location.Longitude;

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                await Application.Current.MainPage.DisplayAlert("FourSquare_V", $"El GPS no esta soportado en este dispositivo:: ({fnsEx.Message})", "OK");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                await Application.Current.MainPage.DisplayAlert("FourSquare_V", $"No esta activado el GPS:: ({fneEx.Message})", "OK");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                await Application.Current.MainPage.DisplayAlert("FourSquare_V", $"No hay permiso para coordenadas:: ({pEx.Message})", "OK");
            }
            catch (Exception ex)
            {
                // Unable to get location
                await Application.Current.MainPage.DisplayAlert("FourSquare_V", $"We couldn't find your place, sorry mate :: ({ex.Message})", "OK");
            }
        }

        private async void PhotoAction()
        {

            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("No hay cámara", "No existe una cámara", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "Place_Picture.jpg"
                });

                if (file == null)
                    return;

                FQPicture = await new ImageService().ConvertImageFilePathToBase64(file.Path);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("FourSquare_V", $"Se generó un error en: {ex.Message}", "OK");
            }


        }

        private async void SelectPhotoAction()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("No hay cámara", "No es posible seleccionar fotografías del dispositivo", "OK");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });

                if (file == null)
                    return;

                FQPicture = await new ImageService().ConvertImageFilePathToBase64(file.Path);
              
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("FourSquare_V", $"Se generó un error en: {ex.Message}", "OK");
            }
        }

       
    }
}

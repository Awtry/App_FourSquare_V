using App_FourSquare_V.Service;
using App_FourSquare_V.ViewModels;
using App_FourSquare_V.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Diagnostics;
using App_FourSquare_V.Views;

namespace App_FourSquare_V.ViewModels
{
    public class FQViewModel : BaseViewModel
    {

        #region Alan

        /* Command _LoadCommand;UpUED¿
         public Command LoadCommand => _LoadCommand ?? (_LoadCommand = new Command(LoadAction));*/

        /* List<FQModel> _FQList;
         public List<FQModel> FQList
         {
             get => _FQList;
             set => SetProperty(ref _FQList, value);
         }*/

        /*private async void LoadPlace()
        {
            IsBusy = true;
            ApiResponse response = await new ApiService().GetDataAsync("FQ");
            if(response == null || !response.IsSuccess)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Ops!", $"Error al cargar... {response.Message}", "Ok");
                return;
            }

            FQList = JsonConvert.DeserializeObject<List<FQModel>>(response.Result.ToString());
            IsBusy = false;
        }

        private void LoadAction()
        {
            LoadPlace(); 
        }*/
        #endregion
        
        //Revisar modo de uso final

        public Command LoadPlacesCommand { get; set; }

        Command _NewPlaceCommand;
        public Command NewPlaceCommand => _NewPlaceCommand ?? (_NewPlaceCommand = new Command(NewPlaceAction));

        Command _SelectCommand;
        public Command SelectCommand => _SelectCommand ?? (_SelectCommand = new Command(SelectPlaceAction));

        private List<FQModel> places;
        public List<FQModel> Places { get => places; set => SetProperty(ref places, value); }

        public FQViewModel()
        {
            Places = new List<FQModel>();

            LoadPlacesCommand = new Command(LoadPlacesAction);

            LoadPlacesAction();
        }

        FQModel placeSelected;
        public FQModel PlaceSelected
        {
            get => placeSelected;
            set => SetProperty(ref placeSelected, value);
        }

        private void NewPlaceAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FQDetailView(this));
        }

        private void SelectPlaceAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FQDetailView(this, PlaceSelected));
        }

        public void Reload_Places()
        {
            LoadPlaces();
        }

        private void LoadPlacesAction()
        {
            LoadPlaces();
        }

        private async void LoadPlaces()
        {
            try
            {
                Debug.WriteLine($"Getting places from WebApi");

                IsBusy = true;
                Places.Clear();
                ResponseModel response = await new ApiService().GetDataAsync("FQ");
                if (response != null && response.IsSuccess)
                {
                    //Drivers = (List<DriverModel>)response.Result;
                    Places = JsonConvert.DeserializeObject<List<FQModel>>(response.Result.ToString());
                }
                else
                {
                    Debug.WriteLine($"There's been a null while querying the API  { response.Message}");
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Error, Constructor FQViewModel: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
    }
}

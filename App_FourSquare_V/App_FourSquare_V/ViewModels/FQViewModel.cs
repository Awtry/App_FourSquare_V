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
        public Command NewPlaceCommand { get; set; }
        public Command SelectCommand { get; set; }

        Command _PlaceSelectedCommand;
        public Command PlaceSelectedCommand => _PlaceSelectedCommand ?? (_PlaceSelectedCommand = new Command(SelectPlaceAction));

       

        //TODO: 
        //NewPlaceCommand
        //SelectCommand
        //PlaceSelected

        private List<FQModel> places;
        public List<FQModel> Places { get => places; set => SetProperty(ref places, value); }

        public FQViewModel()
        {
            Places = new List<FQModel>();

            LoadPlacesCommand = new Command(LoadPlacesAction);

            LoadPlacesAction();
        }

        private FQModel placeSelected;
        public FQModel PlaceSelected
        {
            get => placeSelected;
            set => SetProperty(ref placeSelected, value);
        }

        private void SelectPlaceAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FQDetailView(this, PlaceSelected));
        }

        private async void LoadPlacesAction()
        {
            try
            {
                Debug.WriteLine($"Ejecutando la carga de lugares desde la web api");

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
                    Debug.WriteLine($"Se regreso un null al consultar la web api:  { response.Message}");
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Se genero un error, Constructor FQViewModel: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
    }
}

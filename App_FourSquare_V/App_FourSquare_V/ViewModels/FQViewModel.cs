using App_FourSquare_V.Service;
using App_FourSquare_V.ViewModels;
using App_FourSquare_V.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace App_FourSquare_V.ViewModels
{
    public class FQViewModel : BaseViewModel
    {

        Command _LoadCommand;
        public Command LoadCommand => _LoadCommand ?? (_LoadCommand = new Command(LoadAction));

        List<FQModel> _FQList;
        public List<FQModel> FQList
        {
            get => _FQList;
            set => SetProperty(ref _FQList, value);
        }


        private async void LoadPlace()
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
        }
    }
}

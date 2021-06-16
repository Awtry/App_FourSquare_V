using App_FourSquare_V.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App_FourSquare_V.Service
{
    public class ApiService
    {
        private string ApiUrl = "https://webfoursquare.azurewebsites.net/";

        public async Task<ResponseModel> GetDataAsync(string controller)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new System.Uri(ApiUrl)
                };
                var response = await client.GetAsync(controller);
                var result = await response.Content.ReadAsStringAsync();

                if(!response.IsSuccessStatusCode)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                return JsonConvert.DeserializeObject<ResponseModel>(result);
            }catch(Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}

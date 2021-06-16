using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Web_FourSqueare.Models
{
    //Agregar bien Carpeta Models y Clase FQModel.cs

    //Agregar en carpeta Controllers (Eliminar el existende de Swagger) -> Agregar
    //-> Nuevo Controlador -> API -> Controlador de API con acciones de lectura y escritura -> FQController

    //MINUTO 50:30

    public class FQModel
    {
        string ConnectionString = "Server=tcp:fqdatabase7.database.windows.net,1433;Initial Catalog=fqdatabase;Persist Security Info=False;User ID=fqdatabaseuser;Password=Fqdatabase70306;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public int Id_Place { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public ResponseModel GetAll() //Descargar Nugget System.Data.SqlClient
        {
            List<FQModel> list = new List<FQModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string comsql = "SELECT * FROM Place";
                    using (SqlCommand cmd = new SqlCommand(comsql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new FQModel
                                {
                                    Id_Place = (int)reader["Id_Place"],
                                    Name = reader["Name"].ToString(),
                                    Location = reader["Location"].ToString(),
                                    Latitude = (double)reader["Latitude"],
                                    Longitude = (double)reader["Longitude"]
                                });
                            }
                        }
                    }

                    return new ResponseModel
                    {
                        IsSuccess = true,
                        Message  =  "No han habido problemas al cargar los datos #Winning",
                        Result = list
                    };

                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"Dommage ! Ha habido un problema: {ex.Message}",
                    Result = null
                };
            }
        }
    }
}
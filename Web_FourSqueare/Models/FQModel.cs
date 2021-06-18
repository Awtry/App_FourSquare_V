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

        public int id_Place { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Picture { get; set; }


        public ResponseModel GetAll(string connectionString) //Descargar Nugget System.Data.SqlClient
        {
            List<FQModel> list = new List<FQModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                                    id_Place = (int)reader["id_Place"],
                                    Name = reader["Name"].ToString(),
                                    Picture = reader["Picture"].ToString(),
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
                        Message = "No problems detected, #Winning",
                        Result = list
                    };

                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"Hey ! Something went wrong... {ex.Message}",
                    Result = null
                };
            }
        }

        public ResponseModel GetById(string connectionString, int id)
        {
            FQModel FQ = new FQModel();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string comsql = "SELECT * FROM Place WHERE id_Place = @id_Place";
                    using (SqlCommand cmd = new SqlCommand(comsql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id_Place", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                FQ = new FQModel
                                {
                                    id_Place = (int)reader["id_Place"],
                                    Name = reader["Name"].ToString(),
                                    Picture = reader["Picture"].ToString(),
                                    Location = reader["Location"].ToString(),
                                    Latitude = (double)reader["Latitude"],
                                    Longitude = (double)reader["Longitude"]
                                };
                            }
                        }
                    }

                    return new ResponseModel
                    {
                        IsSuccess = true,
                        Message = "No problems detected, #Winning",
                        Result = FQ
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"Dommage ! There has been an error: {ex.Message}",
                    Result = null
                };
            }
        }

        public ResponseModel Add(string connectionString)
        {
            try
            {
                object newID;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string comsql = "INSERT INTO Place (Name, Location, Latitude, Longitude, Picture) VALUES(@Name, @Location, @Latitude, @Longitude, @Picture); SELECT @@IDENTITY;";

                    using (SqlCommand cmd = new SqlCommand(comsql, connection))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Location", Location);
                        cmd.Parameters.AddWithValue("@Latitude", Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", Longitude);
                        cmd.Parameters.AddWithValue("@Picture", Picture);
                        newID = cmd.ExecuteScalar();

                        if (newID != null && newID.ToString().Length > 0)
                        {
                            return new ResponseModel
                            {
                                IsSuccess = true,
                                Message = "No problem detected, #Winning",
                                Result = newID
                            };
                        }
                        else
                        {
                            return new ResponseModel
                            {
                                IsSuccess = false,
                                Message = "Dommage ! There has been an error",
                                Result = newID
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"Dommage ! There has been an error: {ex.Message}",
                    Result = null
                };
            }
        }
    
        
        public ResponseModel Update(string connectionString, int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string comsql = "UPDATE Place SET Name = @Name, Location = @Location, Latitude = @Latitude, Longitude = @Longitude, Picture = @Picture WHERE id_Place = @id_Place;  SELECT @@IDENTITY;";

                    using (SqlCommand cmd = new SqlCommand(comsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Location", Location);
                        cmd.Parameters.AddWithValue("@Latitude", Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", Longitude);
                        cmd.Parameters.AddWithValue("@Picture", Picture);
                        cmd.Parameters.AddWithValue("@id_Place", id);
                        cmd.ExecuteNonQuery();

                        return new ResponseModel
                        {
                            IsSuccess = true,
                            Message = "´The Place was updated successfully #Winning",
                            Result = id
                        };

                    }

                }
            }
            catch (Exception ex)
            {

                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"The Place was not updated, here is the problem mate: ({ex.Message})",
                    Result = null
                };
            }
        }

        public ResponseModel Delete(string connectionString, int id)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string comsql = "DELETE FROM Place WHERE id_Place = @id_Place;";
                    using (SqlCommand cmd = new SqlCommand(comsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@id_Place", id);
                        cmd.ExecuteNonQuery();

                        return new ResponseModel
                        {
                            IsSuccess = true,
                            Message = "Place's deleted succesfully ! ",
                            Result = id
                        };

                    }
                }
            }catch(Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"We couldn't erase the place: {ex.Message}",
                    Result = null
                };
            }
        }
    
    }
}

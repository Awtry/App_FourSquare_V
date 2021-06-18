using System;
using System.Collections.Generic;
using System.Text;

namespace App_FourSquare_V.Models
{
    public class FQModel
    {
        public int id_Place { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Picture { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class CoordinatesModel
    {
        public int Id {get; set;}
        public int GeometryId {get; set;}
        public double Longitude {get; set;}
        public double Latitude {get; set;}
    }
}
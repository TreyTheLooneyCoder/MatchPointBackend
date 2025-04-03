using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class AmenityModel
    {
        public int AmenityID {get; set;}
        public string? Restroom {get; set;}
        public string? Fountains {get; set;}
        public string? OutdoorLights {get; set;}
    }
}
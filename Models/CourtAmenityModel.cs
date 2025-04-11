using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class CourtAmenityModel
    {
        public int Id {get; set;}
        public AmenityModel[]? Amenities {get; set;}
    }
}
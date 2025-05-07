using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class AddLocationDTO
    {
        public int Id {get; set;}
        public string? CourtName {get; set;}
        public string? Lat {get; set;}
        public string? Lng {get; set;}
        public List<string>? Conditions {get; set;}
        public List<string>? Amenities {get; set;}
    }
}
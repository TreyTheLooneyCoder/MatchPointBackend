using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models.DTOs
{
    public class CourtInfoDTO
    {
        public int Id {get; set;}
        public string? Courtname {get; set;}
        public float CourtRating {get; set;} 
        public float SafetyRating {get; set;}
        public List<string>? Conditions {get; set;}
        public List<string>? Amenities {get; set;}
    }
}
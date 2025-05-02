using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class CourtModel
    {
        public int Id {get; set;}
        public string? CourtName {get; set;} 
        public float CourtRating {get; set;} 
        public float SafetyRating {get; set;} 
        public List<string>? Conditions {get; set;}
        public List<string>? Amenities {get; set;}
        public float Latitude {get; set;}
        public float Longitude {get; set;}
        public List<CommentModel>? Comments {get;set;}
    }
}
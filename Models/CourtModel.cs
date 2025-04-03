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
        public int CourtRating {get; set;} 
        public int SafetyRating {get; set;} 
        public CourtConditionModel Condition {get; set;}
        public AmenityModel Amenities {get; set;}
        public int latitude {get; set;}
        public int longitude {get; set;}
        public CommentModel Comments {get;set;}


    }
}
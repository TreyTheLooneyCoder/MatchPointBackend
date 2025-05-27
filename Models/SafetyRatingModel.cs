using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class SafetyRatingModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float SafetyRating { get; set; }
    }
}
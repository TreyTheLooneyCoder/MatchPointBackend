using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models.DTOs
{
    public class SafetyRatingDTO
    {
        public int LocationId { get; set; }
        public float SafetyRating { get; set; }
    }
}
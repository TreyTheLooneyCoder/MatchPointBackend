using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models.DTOs
{
    public class CourtRatingDTO
    {
        public int LocationId { get; set; }
        public float CourtRating { get; set; }
    }
}
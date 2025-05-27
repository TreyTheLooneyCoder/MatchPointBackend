using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class CourtRatingModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float CourtRating { get; set; }
    }
}
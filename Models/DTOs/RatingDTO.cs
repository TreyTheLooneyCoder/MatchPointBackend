using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models.DTOs
{
    public class RatingDTO
    {
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public float Rating { get; set; }
    }
}
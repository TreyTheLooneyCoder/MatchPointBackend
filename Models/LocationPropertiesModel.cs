using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class LocationPropertiesModel
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string? CourtName { get; set; }
        public ICollection<CourtRatingModel>? CourtRating { get; set; }
        public double? AverageCourtRating { get; set; }
        public ICollection<SafetyRatingModel>? SafetyRating { get; set; }
        public double? AverageSafetyRating { get; set; }
        public List<string>? Conditions { get; set; }
        public List<string>? Amenities { get; set; }
        public ICollection<CommentModel>? Comments { get; set; }
        public string? Surface { get; set; }
        public List<string>? Images { get; set; }
        public bool isDeleted { get; set; }
    }
}
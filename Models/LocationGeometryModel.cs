using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class LocationGeometryModel
    {
        public int Id {get; set;}
        public int LocationId {get; set;}
        public List<double>? Coodinates {get; set;}
        public string? Type {get; set;} = "Point";
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class LocationGeometryModel
    {
        public int Id {get; set;}
        public required List<CoodinatesModel> Coodinates {get; set;}
        public required string Type {get; set;}
    }
}
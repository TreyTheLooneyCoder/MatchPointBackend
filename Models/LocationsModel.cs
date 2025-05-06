using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class LocationsModel
    {
        public int Id {get; set;}
        public required string Type {get; set;}
        public LocationPropertiesModel? Properties {get; set;}
        
        public required LocationGeometryModel Geometry {get; set;}
    }
}
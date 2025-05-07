using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class LocationsModel
    {
        public int Id {get; set;}
        public int CollectionId {get; set;}
        public string? Type {get; set;}
        public LocationPropertiesModel? Properties {get; set;}
        
        public LocationGeometryModel? Geometry {get; set;}
    }
}
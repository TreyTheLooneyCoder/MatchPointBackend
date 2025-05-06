using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class LocationCollectionModel
    {
        public int Id {get; set;}
        public required string Type {get; set;}
        public ICollection<LocationsModel>? Features {get; set;}
    }
}
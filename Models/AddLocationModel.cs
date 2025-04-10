using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class AddLocationModel
    {
        public int LocationID {get; set;}
        public string? LocationName {get; set;}
        public int Latitude {get; set;}
        public int Longitude {get; set;}
    }
}
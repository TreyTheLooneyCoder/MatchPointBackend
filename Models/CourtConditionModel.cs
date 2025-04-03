using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class CourtConditionModel
    {
        public int ConditionID {get; set;}
        public string? PoorSurface {get; set;}
        public string? Cracks {get; set;}
    }
}
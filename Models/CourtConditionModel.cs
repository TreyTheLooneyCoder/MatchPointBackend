using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class CourtConditionModel
    {
        public int Id {get; set;}
        public ConditionModel[]? Conditions {get; set;}
    }
}
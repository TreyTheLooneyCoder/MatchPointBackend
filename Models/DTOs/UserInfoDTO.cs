using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class UserInfoDTO
    {
        public int Id {get; set;}
        public string? Username {get; set;}
        public string? Email {get; set;}
    }
}
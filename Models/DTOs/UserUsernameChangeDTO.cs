using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models.DTOs
{
    public class UserUsernameChangeDTO
    {
        public string? Username {get; set;}
        public string? NewUsername {get; set;}
    }
}
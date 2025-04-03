using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class CommentModel
    {
        public int Id {get; set;}
        public int UserId {get; set;}
        public string? Username {get; set;}
        public string? Comment {get; set;}
    }
}
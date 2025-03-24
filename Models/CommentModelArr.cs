using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models
{
    public class CommentModelArr
    {
        public int Id {get; set;}
        public int UserId {get; set;}
        public string? Commentor {get; set;}
        public string? Comment {get; set;}
    }
}
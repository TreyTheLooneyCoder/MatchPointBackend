using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models.DTOs
{
    public class CommentInfoDTO
    {
        public int Id {get; set;}
        public string? Username {get; set;}
        public string? Comment {get; set;}
    }
}
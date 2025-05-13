using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchPointBackend.Models.DTOs
{
    public class EditCommentDTO
    {
        public int CommentId {get; set;}
        public string? Commentor {get; set;}
        public string? NewComment {get; set;}
    }
}
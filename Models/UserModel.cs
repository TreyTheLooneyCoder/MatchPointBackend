namespace MatchPointBackend.Models
{
    public class UserModel
    {
        public int Id {get; set;}
        public string? Email {get; set;}
        public string? Salt {get; set;}
        public string? Hash {get; set;}

        // public List<CommentModelArr> CommentArr {get; set;}
    }
}
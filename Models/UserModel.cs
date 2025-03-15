
namespace MatchPointBackend.Models
{
    public class UserModel
    {
        public string? Email {get; set;}
        public string? Salt {get; set;}
        public string? Hash {get; set;}
    }
}
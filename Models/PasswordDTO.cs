
namespace MatchPointBackend.Models
{
    public class PasswordDTO
    {
        public string? Salt {get; set;}
        public string? Hash {get; set;}
    }
}
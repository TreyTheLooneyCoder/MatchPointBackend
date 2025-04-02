using MatchPointBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchPointBackend.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options){

        }

        public DbSet<UserModel> User {get; set;} 
    }
}
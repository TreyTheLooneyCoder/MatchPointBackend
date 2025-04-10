using MatchPointBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchPointBackend.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options){

        }

        public DbSet<UserModel> Users {get; set;}

        public DbSet<CourtModel> Locations {get; set;} 
    }
}
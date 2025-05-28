using MatchPointBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchPointBackend.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options){

        }

        public DbSet<UserModel> Users {get; set;}
        public DbSet<LocationCollectionModel> LocationCollection {get; set;}
        public DbSet<LocationsModel> Locations {get; set;}
        public DbSet<LocationPropertiesModel> LocationProperties {get; set;}
        public DbSet<LocationGeometryModel> LocationGeometry {get; set;}
        public DbSet<CommentModel> Comments {get; set;}

    }
}
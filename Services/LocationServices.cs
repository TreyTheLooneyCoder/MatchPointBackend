using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchPointBackend.Context;
using MatchPointBackend.Models;
using MatchPointBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchPointBackend.Services
{
    public class LocationServices
    {
        private readonly DataContext _dataContext;

        public LocationServices(DataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
        }   

        public async Task<List<LocationsModel>> GetLocationsAsync() => await _dataContext.Locations.ToListAsync();

        public async Task<CourtInfoDTO> EditLocations(string courtName)
        {
            var currentCourt = await _dataContext.Locations.SingleOrDefaultAsync(location => location.Properties.CourtName == courtName);

            CourtInfoDTO location = new();

            // location.Id = currentCourt.Id;
            // location.Courtname = currentCourt.CourtName;
            // location.CourtRating = currentCourt.CourtRating;
            // location.SafetyRating = currentCourt.SafetyRating;
            // location.Conditions = currentCourt.Conditions;
            // location.Amenities = currentCourt.Amenities;

            return location;
        }    

        private async Task<bool> DoesLocationExist(List<double> Coords) => await _dataContext.LocationGeometry.SingleOrDefaultAsync(location => location.Coordinates == Coords) != null;
        private async Task<bool> DoesCollectionExist(int id) => await _dataContext.LocationCollection.SingleOrDefaultAsync(location => location.Id == id) != null;
        public async Task<bool> AddLocation(AddLocationDTO newLocation)
        {
            // bool latTryparse = double.TryParse(newLocation.Lat, out double convertedLat);

            // bool lngTryparse = double.TryParse(newLocation.Lng, out double convertedLng);

            if (await DoesLocationExist(newLocation.Coordinates)) return false;


            LocationsModel locationToAdd = new();

            // CoordinatesModel newCoords = new();
            // newCoords.Latitude = convertedLat;
            // newCoords.Longitude = convertedLng;

            LocationGeometryModel newGeometry = new();
            newGeometry.LocationId = locationToAdd.Id;
            newGeometry.Type = "Point";
            newGeometry.Coordinates = newLocation.Coordinates;

            LocationPropertiesModel newPropeties = new();
            newPropeties.LocationId = locationToAdd.Id;
            newPropeties.CourtName = newLocation.CourtName;
            newPropeties.Conditions = newLocation.Conditions;
            newPropeties.Amenities = newLocation.Amenities;
            newPropeties.isDeleted = false;
            newPropeties.Surface = newLocation.Surface;
            newPropeties.Images = newLocation.Image;

            locationToAdd.Type = "Feature";
            locationToAdd.Properties = newPropeties;
            locationToAdd.Geometry = newGeometry;

            
            await _dataContext.Locations.AddAsync(locationToAdd);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        private async Task<bool> CreateCollection()
        {
            if(await DoesCollectionExist(0) == false){
                LocationCollectionModel newCollection = new();
                return await _dataContext.SaveChangesAsync() == 0;
            }
            return true;
        }

        public async Task<LocationsModel> GetLocationById(int Id)
        {
            var currentLocation = await _dataContext.Locations
            .Include(location => location.Geometry)
            .Include(location => location.Properties)
            .Include(location => location.Properties.CourtRating)
            .Include(location => location.Properties.SafetyRating)
            .Include(location => location.Properties.Comments)
            .SingleOrDefaultAsync(location => location.Id == Id);

            
            return currentLocation;
        }

        public async Task<LocationPropertiesModel> GetLocationPropertiesByLocationId(int Id)
        {
            var currentLocation = await _dataContext.LocationProperties
            .Include(ratings => ratings.SafetyRating)
            .Include(ratings => ratings.CourtRating)
            .Include(comments => comments.Comments)
            .SingleOrDefaultAsync(properties => properties.Id == Id);

            return currentLocation;
        }

        public async Task<List<LocationsModel>> GetLocationByCoords(string latitude, string longitude)
        {
            bool latTryparse = double.TryParse(latitude, out double convertedLat);

            bool lngTryParse = double.TryParse(longitude, out double convertedLng);

            if(!latTryparse || !lngTryParse) return null;

            var locationCollection = await _dataContext.Locations
                .Where(location =>
                    location.Geometry.Coordinates[1] >= convertedLat - 0.01 &&
                    location.Geometry.Coordinates[1] <= convertedLat + 0.01 &&
                    location.Geometry.Coordinates[0] >= convertedLng - 0.01 &&
                    location.Geometry.Coordinates[0] <= convertedLng + 0.01)
                .Include(location => location.Geometry)
                .Include(location => location.Properties)
                .Include(location => location.Properties.CourtRating)
                .Include(location => location.Properties.SafetyRating)
                .Include(location => location.Properties.Comments)
                .ToListAsync();
            return locationCollection;
        }

        public async Task<List<LocationsModel>> Get5miLocationByCoords(string latitude, string longitude)
        {
            bool latTryparse = double.TryParse(latitude, out double convertedLat);

            bool lngTryParse = double.TryParse(longitude, out double convertedLng);

            if(!latTryparse || !lngTryParse) return null;

            var locationCollection = await _dataContext.Locations
                .Where(location =>
                    location.Geometry.Coordinates[1] >= convertedLat - 0.0723 &&
                    location.Geometry.Coordinates[1] <= convertedLat + 0.0723 &&
                    location.Geometry.Coordinates[0] >= convertedLng - 0.0723 &&
                    location.Geometry.Coordinates[0] <= convertedLng + 0.0723)
                .Include(location => location.Geometry)
                .Include(location => location.Properties)
                .Include(location => location.Properties.CourtRating)
                .Include(location => location.Properties.SafetyRating)
                .Include(location => location.Properties.Comments)
                .ToListAsync();
            return locationCollection;
        }

        public async Task<LocationsModel> GetLocationByCourtname(string courtname)
        {
            var currentLocation = await _dataContext.Locations.SingleOrDefaultAsync(location => location.Properties.CourtName == courtname);
            
            return currentLocation;
        }

        public async Task<bool> AddSafetyRating(RatingDTO ratings)
        {
            LocationPropertiesModel locationToEdit = await GetLocationPropertiesByLocationId(ratings.LocationId);
            SafetyRatingModel safetyRatingToEdit = new();

            if (await DoesUserSafetyRatingExist(ratings.UserId, ratings.LocationId)) return false;
            if(locationToEdit == null) return false;

            safetyRatingToEdit.UserId = ratings.UserId;
            safetyRatingToEdit.SafetyRating = ratings.Rating;
            safetyRatingToEdit.LocationPropertiesId = locationToEdit.Id;
            safetyRatingToEdit.LocationId = ratings.LocationId;

            locationToEdit.SafetyRating.Add(safetyRatingToEdit);

            double calcAvg = locationToEdit.SafetyRating.Average(rating => rating.SafetyRating);
            locationToEdit.AverageSafetyRating = calcAvg;
            
            return await _dataContext.SaveChangesAsync() != 0;
        }
        
        public async Task<bool> AddCourtRating(RatingDTO ratings)
        {
            LocationPropertiesModel locationToEdit = await GetLocationPropertiesByLocationId(ratings.LocationId);
            CourtRatingModel courtRatingToEdit = new();
        
            if(await DoesUserCourtRatingExist(ratings.UserId, ratings.LocationId)) return false;
            if(locationToEdit == null) return false;

            courtRatingToEdit.UserId = ratings.UserId;
            courtRatingToEdit.CourtRating = ratings.Rating;
            courtRatingToEdit.LocationPropertiesId = locationToEdit.Id;
            courtRatingToEdit.LocationId = ratings.LocationId;

            locationToEdit.CourtRating.Add(courtRatingToEdit);

            double calcAvg = locationToEdit.CourtRating.Average(rating => rating.CourtRating);
            locationToEdit.AverageCourtRating = calcAvg;

            _dataContext.LocationProperties.Update(locationToEdit);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        private async Task<bool> DoesUserSafetyRatingExist(int userId, int locationId) => await _dataContext.SafetyRatings.AnyAsync(rating => rating.UserId == userId && rating.LocationId == locationId);
        private async Task<bool> DoesUserCourtRatingExist(int userId, int locationId) => await _dataContext.CourtRatings.AnyAsync(rating => rating.UserId == userId && rating.LocationId == locationId);

        private async Task<CommentModel> GetCommentsByCommentId(int Id)
        {
            var currentComment = await _dataContext.Comments.SingleOrDefaultAsync(comment => comment.Id == Id);

            return currentComment;
        }

        private async Task<UserModel> GetUserByUserId(int Id)
        {
            var currentComment = await _dataContext.Users.SingleOrDefaultAsync(user => user.Id == Id);

            return currentComment;
        }

        public async Task<CommentModel> GetCommentsByUser(string username) 
        {
            var currentComment = await _dataContext.Comments.SingleOrDefaultAsync(comment => comment.Username == username);
            
            return currentComment;
        }

        public async Task<List<CommentModel>> GetCommentsByLocationId(int location) 
        {
            var currentComment = await _dataContext.Comments.Where(comment => comment.LocationId == location).ToListAsync();
            
            return currentComment;
        }

        public async Task<bool> AddComment(CommentInfoDTO commentToAdd)
        {
            UserModel user = await GetUserByUserId(commentToAdd.UserId);
            CommentModel newComment = new();

            newComment.LocationId = commentToAdd.LocationId;
            newComment.Comment = commentToAdd.Comment;
            newComment.UserId = commentToAdd.UserId;
            newComment.Username = user.Username;

            await _dataContext.Comments.AddAsync(newComment);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> EditComment(EditCommentDTO comment)
        {
            CommentModel foundComment = await GetCommentsByCommentId(comment.CommentId);

            if (foundComment == null) return false;

            foundComment.Comment = comment.NewComment;

            _dataContext.Comments.Update(foundComment);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> DeleteComment(EditCommentDTO Id)
        {
            CommentModel commentToDelete = await GetCommentsByCommentId(Id.CommentId);
            if (commentToDelete == null) return false;
            
            commentToDelete.IsDeleted = true;
            _dataContext.Comments.Update(commentToDelete);

            return await _dataContext.SaveChangesAsync() != 0;
        }
    }
}
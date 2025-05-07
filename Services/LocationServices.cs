using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchPointBackend.Context;
using MatchPointBackend.Models;
using MatchPointBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;

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

        private async Task<bool> DoesLocationExist(double latitude, double longitude) => await _dataContext.Coordinates.SingleOrDefaultAsync(location => location.Latitude == latitude && location.Longitude == longitude) != null;
        public async Task<bool> AddLocation(AddLocationDTO newLocation)
        {
            bool latTryparse = double.TryParse(newLocation.Lat, out double convertedLat);

            bool lngTryparse = double.TryParse(newLocation.Lng, out double convertedLng);

            if (await DoesLocationExist(convertedLat, convertedLng)) return false;

            LocationsModel locationToAdd = new();

            CoordinatesModel newCoords = new();
            newCoords.Latitude = convertedLat;
            newCoords.Latitude = convertedLng;

            LocationGeometryModel newGeometry = new();
            newGeometry.LocationId = locationToAdd.Id;
            newGeometry.Type = "Point";

            LocationPropertiesModel newPropeties = new();
            newPropeties.LocationId = locationToAdd.Id;
            newPropeties.CourtName = newLocation.CourtName;
            newPropeties.Conditions = newLocation.Conditions;
            newPropeties.Amenities = newLocation.Amenities;

            locationToAdd.Type = "Feature";
            locationToAdd.Properties = newPropeties;
            locationToAdd.Geometry = newGeometry;

            
            await _dataContext.Locations.AddAsync(locationToAdd);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<LocationsModel> GetLocationById(int Id)
        {
            var currentLocation = await _dataContext.Locations.SingleOrDefaultAsync(location => location.Id == Id);
            
            return currentLocation;
        }

        public async Task<string> GetLocationByCoords(string latitude, string longitude)
        {
            // bool latTryparse = float.TryParse(latitude, out float convertedLat);

            // bool lngTryparse = float.TryParse(longitude, out float convertedLng);

            // var currentLocation = await _dataContext.Locations.Where(location => (location.Latitude <= convertedLat + 0.1) && (location.Latitude >= convertedLat - 0.1) && (location.Longitude >= convertedLng + 0.1) && (location.Longitude >= convertedLng - 0.1)).ToListAsync();

            string currentLocation = latitude + longitude;
            
            return currentLocation;
        }

        public async Task<LocationsModel> GetLocationByCourtname(string courtname)
        {
            var currentLocation = await _dataContext.Locations.SingleOrDefaultAsync(location => location.Properties.CourtName == courtname);
            
            return currentLocation;
        }
    }
}
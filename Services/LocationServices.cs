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

        public async Task<List<CourtModel>> GetLocationsAsync() => await _dataContext.Locations.ToListAsync();

        public async Task<CourtInfoDTO> EditLocations(string courtName)
        {
            var currentCourt = await _dataContext.Locations.SingleOrDefaultAsync(location => location.CourtName == courtName);

            CourtInfoDTO location = new();

            location.Id = currentCourt.Id;
            location.Courtname = currentCourt.CourtName;
            location.CourtRating = currentCourt.CourtRating;
            location.SafetyRating = currentCourt.SafetyRating;
            location.Conditions = currentCourt.Conditions;
            location.Amenities = currentCourt.Amenities;

            return location;
        }    

        private async Task<bool> DoesLocationExist(int latitude, int longitude) => await _dataContext.Locations.SingleOrDefaultAsync(location => location.Latitude == latitude && location.Longitude == longitude) != null;
        public async Task<bool> AddLocation(AddLocationDTO newLocation)
        {
            if (await DoesLocationExist(newLocation.Latitude, newLocation.Longitude)) return false;
            
            CourtModel locationToAdd = new();
            locationToAdd.CourtName = newLocation.CourtName;
            locationToAdd.Latitude = newLocation.Latitude;
            locationToAdd.Longitude = newLocation.Longitude;
            locationToAdd.Conditions = newLocation.Conditions;
            locationToAdd.Amenities = newLocation.Amenities;


            await _dataContext.Locations.AddAsync(locationToAdd);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<CourtModel> GetLocationById(int Id)
        {
            var currentLocation = await _dataContext.Locations.SingleOrDefaultAsync(location => location.Id == Id);

            CourtModel location = new();

            location.Id = currentLocation.Id;
            

            return location;
        }

        public async Task<CourtModel> GetLocationByCoords(int latitude, int longitude)
        {
            var currentLocation = await _dataContext.Locations.SingleOrDefaultAsync(location => location.Latitude == latitude && location.Longitude == longitude);

            CourtModel location = new();

            location.Latitude = currentLocation.Latitude;
            location.Longitude = currentLocation.Longitude;
            

            return location;
        }

        public async Task<CourtModel> GetLocationByCourtname(string courtname)
        {
            var currentLocation = await _dataContext.Locations.SingleOrDefaultAsync(location => location.CourtName == courtname);

            CourtModel location = new();

            location.CourtName = currentLocation.CourtName;
            location.Id = currentLocation.Id;
            

            return location;
        }
    }
}
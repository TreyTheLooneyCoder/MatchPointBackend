using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchPointBackend.Models;
using MatchPointBackend.Models.DTOs;
using MatchPointBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchPointBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly LocationServices _locationServices;

        public LocationController(LocationServices locationServices){
            _locationServices = locationServices;
        }

        [HttpGet("GetAllLocations")]
        public async Task<IActionResult> GetAllLocations()
        {
            var locations = await _locationServices.GetLocationsAsync();
            if(locations != null) return Ok(locations);
            return BadRequest(new { Message = "No locations found." });
        }

        [HttpPost("AddNewLocation")]
        [Authorize]
        public async Task<IActionResult> AddLocation(AddLocationDTO location)
        {
            bool success = await _locationServices.AddLocation(location);
            if (success) return Ok(new { success = true });
            return BadRequest(new { Message = "Location was not added" });
        }

        [HttpGet]
        [Route("GetLocationInfoById/{Id}")]
        public async Task<IActionResult> GetLocationById(int Id)
        {
            CourtModel locations = await _locationServices.GetLocationById(Id);
            if (locations != null)
            {
                return Ok(locations);
            }
            else
            {
                return Unauthorized(new { message = "Failed to get location." });
            }
        }

        [HttpGet]
        [Route("GetLocationInfoByCoords/{latitude}/{longitude}")]
        public async Task<IActionResult> GetLocationByCoords(int latitude, int longitude)
        {
            CourtModel locations = await _locationServices.GetLocationByCoords(latitude, longitude);
            if (locations != null)
            {
                return Ok(locations);
            }
            else
            {
                return Unauthorized(new { message = "Failed to get location." });
            }
        }

        [HttpGet]
        [Route("GetLocationInfoByCourtname/{courtname}")]
        public async Task<IActionResult> GetLocationByCourtname(string courtname)
        {
            CourtModel locations = await _locationServices.GetLocationByCourtname(courtname);
            if (locations != null)
            {
                return Ok(locations);
            }
            else
            {
                return Unauthorized(new { message = "Failed to get location." });
            }
        }
    }
}
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
    public class LocationsController : ControllerBase
    {
        private readonly LocationServices _locationServices;

        public LocationsController(LocationServices locationServices){
            _locationServices = locationServices;
        }

        [HttpGet("GetLocations")]
        public async Task<IActionResult> GetLocations(string courtName)
        {
            CourtInfoDTO locations = await _locationServices.GetLocations(courtName);
            if (locations != null)
            {
                return Ok(locations);
            }
            else
            {
                return Unauthorized(new { message = "Failed to get locations." });
            }
        }

        [HttpPost("AddNewLocation")]
        [Authorize]
        public async Task<IActionResult> AddLocation(AddLocationDTO location)
        {
            bool success = await _locationServices.AddLocation(location);
            if (success) return Ok(new { success = true });
            return BadRequest(new { Message = "Location was not added" });
        }
    }
}
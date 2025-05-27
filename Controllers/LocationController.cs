using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            return Unauthorized(new { Message = "Location was not added" });
        }

        [HttpGet]
        [Route("GetLocationInfoById/{Id}")]
        public async Task<IActionResult> GetLocationById(int Id)
        {
            LocationsModel locations = await _locationServices.GetLocationById(Id);
            if (locations != null)
            {
                return Ok(locations);
            }
            else
            {
                return BadRequest(new { message = "Failed to get location." });
            }
        }

        [HttpGet]
        [Route("GetLocationInfoByCoords/{lat}/{lng}")]
        public async Task<IActionResult> GetLocationByCoords(string lat, string lng)
        {
            List<LocationsModel> locations = await _locationServices.GetLocationByCoords(lat, lng);
            if (locations != null)
            {
                return Ok(locations);
            }
            else
            {
                return BadRequest(new { message = "Failed to get location." });
            }
        }

        [HttpGet]
        [Route("Get5miLocationInfoByCoords/{lat}/{lng}")]
        public async Task<IActionResult> Get5miLocationByCoords(string lat, string lng)
        {
            List<LocationsModel> locations = await _locationServices.Get5miLocationByCoords(lat, lng);
            if (locations != null)
            {
                return Ok(locations);
            }
            else
            {
                return BadRequest(new { message = "Failed to get location." });
            }
        }

        [HttpGet]
        [Route("GetLocationInfoByCourtname/{courtname}")]
        public async Task<IActionResult> GetLocationByCourtname(string courtname)
        {
            LocationsModel locations = await _locationServices.GetLocationByCourtname(courtname);
            if (locations != null)
            {
                return Ok(locations);
            }
            else
            {
                return BadRequest(new { message = "Failed to get location." });
            }
        }

        [HttpGet]
        [Route("GetCommentsByUser/{username}")]
        public async Task<IActionResult> GetCommentsByUser(string username)
        {
            CommentModel comments = await _locationServices.GetCommentsByUser(username);
            if (comments != null)
            {
                return Ok(comments);
            }
            else
            {
                return BadRequest(new { message = "Failed to get comment." });
            }
        }

        [HttpGet]
        [Route("GetCommentsByLocationId/{locationId}")]
        public async Task<IActionResult> GetCommentsByLocationId(int locationId)
        {
            CommentModel comments = await _locationServices.GetCommentsByLocationId(locationId);
            if (comments != null)
            {
                return Ok(comments);
            }
            else
            {
                return BadRequest(new { message = "Failed to get comment." });
            }
        }

        // No RatingDTO yet was making build fail when trying to drop

        // [HttpPut]
        // [Route("EditRating")]
        // [Authorize]
        // public async Task<IActionResult> EditRating(RatingDTO ratings)
        // {
        //     bool success = await _locationServices.EditComment(ratings);
        //     if (success) return Ok(new { success = true });
        //     return Unauthorized(new { Message = "Comment was not edited" });
        // }

        [HttpPost("AddComment")]
        [Authorize]
        public async Task<IActionResult> AddComment(CommentInfoDTO comment)
        {
            bool success = await _locationServices.AddComment(comment);
            if (success) return Ok(new { success = true });
            return Unauthorized(new { Message = "Comment was not added" });
        }

        [HttpPut("EditComment")]
        [Authorize]
        public async Task<IActionResult> EditComment(EditCommentDTO comment)
        {
            bool success = await _locationServices.EditComment(comment);
            if (success) return Ok(new { success = true });
            return Unauthorized(new { Message = "Comment was not edited" });
        }

        [HttpDelete("DeleteComment")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(EditCommentDTO Id)
        {
            bool success = await _locationServices.DeleteComment(Id);
            if (success) return Ok(new { success = true });
            return Unauthorized(new { Message = "Comment was not deleted" });
        }
    }
}
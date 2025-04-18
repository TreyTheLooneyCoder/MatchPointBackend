using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchPointBackend.Models;
using MatchPointBackend.Models.DTOs;
using MatchPointBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MatchPointBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class LoggedInController : ControllerBase
    {
        private readonly LoggedInServices _loggedInServices;

        public LoggedInController(LoggedInServices loggedInServices){
            _loggedInServices = loggedInServices;
        }


        [HttpPut("EditUsername")]
        public async Task<IActionResult> EditUsername([FromBody]UserUsernameChangeDTO user)
        {
            bool success = await _loggedInServices.EditUsernameAsync(user);
            if (success) return Ok(new { Success = true });
            return BadRequest(new { Message = "Username already exist." });
        }

        [HttpPut("EditPassword")]
        public async Task<IActionResult> EditPassword([FromBody]UserLoginDTO user)
        {
            bool success = await _loggedInServices.EditPasswordAsync(user);
            if (success) return Ok(new { Success = true });
            return BadRequest(new { Message = "Password change was unsuccessful." });
        }

        [HttpDelete("DeleteProfile")]
        public async Task<IActionResult> DeleteProfile(string user)
        {
            bool success = await _loggedInServices.DeleteProfile(user);
            if (success) return Ok(new { success = true });
            return BadRequest(new { Message = "Profile was not deleted" });
        }

        [HttpPost]
        public async Task<IActionResult> AddLocation(CourtModel location)
        {
            bool success = await _loggedInServices.AddLocation(location);
            if (success) return Ok(new { success = true });
            return BadRequest(new { Message = "Location was not added" });
        }
    }
}
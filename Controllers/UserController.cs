using System.Threading.Tasks;
using MatchPointBackend.Models;
using MatchPointBackend.Models.DTOs;
using MatchPointBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace MatchPointBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userServices;
        public UserController(UserService userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
        {
            bool success = await _userServices.CreateUser(user);
            //we want to return objects since this would be a api call
            if (success) return Ok(new { Success = true });
            return BadRequest(new { Success = false, Message = "Username already Exists" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
        {
            string stringToken = await _userServices.Login(user);
            if (stringToken != null)
            {
                return Ok(new { Token = stringToken });
            }
            else
            {
                return Unauthorized(new { Message = "Login was unsuccessful. Invalid Email or Password" });
            }
        }

        [HttpGet]
        [Route("GetUserInfoByUsername/{username}")]
        public async Task<IActionResult> GetUserInfoByUsername(string username)
        {
            UserInfoDTO users = await _userServices.GetUserInfoByUsername(username);
            if (users != null)
            {
                return Ok(users);
            }
            else
            {
                return Unauthorized(new { message = "Failed to get user." });
            }

        }

        [HttpGet]
        [Route("GetUserInfoByEmail/{email}")]
        public async Task<IActionResult> GetUserInfoByEmail(string email)
        {
            UserInfoDTO users = await _userServices.GetUserInfoByEmail(email);
            if (users != null)
            {
                return Ok(users);
            }
            else
            {
                return Unauthorized(new { message = "Failed to get user." });
            }

        }

        [HttpPut("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] UserLoginDTO user){
            bool success = await _userServices.ForgotPasswordAsync(user);
            if(success) return Ok(new{Success = true});
            return BadRequest(new{Message = "Password was not edited."});
        }

        [HttpPut("EditUsername")]
        [Authorize]
        public async Task<IActionResult> EditUsername([FromBody]UserUsernameChangeDTO user)
        {
            bool success = await _userServices.EditUsernameAsync(user);
            if (success) return Ok(new { Success = true });
            return BadRequest(new { Message = "Username already exist." });
        }

        [HttpPut("EditPassword")]
        [Authorize]
        public async Task<IActionResult> EditPassword([FromBody]UserLoginDTO user)
        {
            bool success = await _userServices.EditPasswordAsync(user);
            if (success) return Ok(new { Success = true });
            return BadRequest(new { Message = "Password change was unsuccessful." });
        }

        [HttpDelete("DeleteProfile")]
        [Authorize]
        public async Task<IActionResult> DeleteProfile(string user)
        {
            bool success = await _userServices.DeleteProfile(user);
            if (success) return Ok(new { success = true });
            return BadRequest(new { Message = "Profile was not deleted" });
        }

    }
}
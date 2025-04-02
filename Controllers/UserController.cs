using System.Threading.Tasks;
using MatchPointBackend.Models;
using MatchPointBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchPointBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userServices;
        public UserController(UserService userServices){
            _userServices = userServices;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody]UserDTO user) {
            bool success = await _userServices.CreateUser(user);
            //we want to return objects since this would be a api call
            if(success) return Ok(new {Success = true});
            return BadRequest(new {Success = false, Message = "Username already Exists"});
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]UserDTO user){
            string stringToken = await _userServices.Login(user);
            if(stringToken != null){
                return Ok(new { Token = stringToken });
            }else{
                return Unauthorized(new { Message = "Login was unsuccessful. Invalid Email or Password" });
            }
        }
        
        [HttpGet]
        [Route("GetUserInfoByUsername/${username}")]
        public async Task<IActionResult> GetUserInfoByUsername(string username){
            UserInfoDTO users = await _userServices.GetUserInfoByUsername(username);
            if(users != null){
                return Ok(users);
            }else{
                return Unauthorized(new { message = "Failed to get user."});
            }
            
        }

        [HttpGet]
        [Route("GetUserInfoByEmail/${email}")]
        public async Task<IActionResult> GetUserInfoByEmail(string email){
            UserInfoDTO users = await _userServices.GetUserInfoByEmail(email);
            if(users != null){
                return Ok(users);
            }else{
                return Unauthorized(new { message = "Failed to get user."});
            }
            
        }

    }
}
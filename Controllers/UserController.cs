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
        public bool CreateUser([FromBody]UserDTO newUser){
            return _userServices.CreateUser(newUser);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]UserDTO user){
            string stringToken = _userServices.Login(user);

            if(stringToken != null){
                return Ok(new { Token = stringToken });
            }else{
                return Unauthorized(new { Message = "Login was unsuccessful. Invalid Email or Password" });
            }
        }
        
        [Authorize]
        [HttpGet]
        [Route("AuthenticUser")]
        public string AuthenticUserCheck(){
            return "You are logged in and allowed to be here. Yay!!";
        }

    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MatchPointBackend.Context;
using MatchPointBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MatchPointBackend.Services
{
    public class UserService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _config;

        public UserService(DataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
            _config = config;
        }

        public async Task<bool> CreateUser(UserDTO newUser)
        {
            if (await DoesUserEmailExist(newUser.Email)) return false;
            if (await DoesUsernameExist(newUser.Username)) return false;

            UserModel userToAdd = new();
            userToAdd.Username = newUser.Username;
            userToAdd.Email = newUser.Email;

            PasswordDTO hashPassword = HashPassword(newUser.Password);
            userToAdd.Hash = hashPassword.Hash;
            userToAdd.Salt = hashPassword.Salt;

            await _dataContext.Users.AddAsync(userToAdd);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        private async Task<bool> DoesUsernameExist(string username) => await _dataContext.Users.SingleOrDefaultAsync(users => users.Username == username) != null;
        private async Task<bool> DoesUserEmailExist(string email) => await _dataContext.Users.SingleOrDefaultAsync(users => users.Email == email) != null;


        private static PasswordDTO HashPassword(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(64);

            string salt = Convert.ToBase64String(saltBytes);

            string hash;

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 310000, HashAlgorithmName.SHA256))
            {
                hash = Convert.ToBase64String(deriveBytes.GetBytes(32));
            }

            PasswordDTO hashedPassword = new();
            hashedPassword.Salt = salt;
            hashedPassword.Hash = hash;

            return hashedPassword;
        }

        public async Task<string> Login(UserLoginDTO user)
        {
            string result = null;

            UserModel foundUser = await GetUserByUsername(user.Username);

            if (foundUser == null)
            {
                return result;
            }

            if (!VerifyPassword(user.Password, foundUser.Salt, foundUser.Hash)) return null;

            return GenerateJWToken(new List<Claim>());
        }

        private async Task<UserModel> GetUserByUsername(string userName) => await _dataContext.Users.SingleOrDefaultAsync(user => user.Username == userName);

        private string GenerateJWToken(List<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://matchpointbackend-c4btg3ekhea4gqcz.westus-01.azurewebsites.net/", //http://localhost:5000",
                audience: "https://matchpointbackend-c4btg3ekhea4gqcz.westus-01.azurewebsites.net/", //http://localhost:5000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private static bool VerifyPassword(string password, string salt, string hash)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            string newHash;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 310000, HashAlgorithmName.SHA256))
            {
                newHash = Convert.ToBase64String(deriveBytes.GetBytes(32));
            }

            return hash == newHash;
        }

        public async Task<UserInfoDTO> GetUserInfoByUsername(string username)
        {
            var currentUser = await _dataContext.Users.SingleOrDefaultAsync(user => user.Username == username);

            UserInfoDTO user = new();

            user.Id = currentUser.Id;
            user.Username = currentUser.Username;
            user.Email = currentUser.Email;

            return user;
        }

        public async Task<UserInfoDTO> GetUserInfoByEmail(string email)
        {
            UserModel currentUser = await _dataContext.Users.SingleOrDefaultAsync(user => user.Email == email);

            UserInfoDTO user = new();

            user.Id = currentUser.Id;
            user.Username = currentUser.Username;
            user.Email = currentUser.Email;

            return user;
        }

        public async Task<bool> EditUsernameAsync(string username, string newUsername){
            UserModel foundUser = await GetUserByUsername(username);

            if(foundUser == null) return false;

            foundUser.Username = newUsername;
            
            _dataContext.Users.Update(foundUser);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> EditPasswordAsync(string username, string newPassword){
            UserModel foundUser = await GetUserByUsername(username);

            if(foundUser == null) return false;

            PasswordDTO hashPassword = HashPassword(newPassword);
            foundUser.Hash = hashPassword.Hash;
            foundUser.Salt = hashPassword.Salt;

            _dataContext.Users.Update(foundUser);
            return await _dataContext.SaveChangesAsync() != 0;
        }

    }
}